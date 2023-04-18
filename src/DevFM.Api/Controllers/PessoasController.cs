using AutoMapper;
using DevFM.Api.Extensions;
using DevFM.Api.ViewModels;
using DevFM.Business.Intefaces;
using DevFM.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DevFM.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/pessoas")]
    public class PessoasController : MainController
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IPessoaService _pessoaService;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;

        public PessoasController(IPessoaRepository pessoaRepository,
            IMapper mapper,
            IPessoaService pessoaService,
            IEnderecoRepository enderecoRepository,
            INotificador notificador,
            IUser user) : base(notificador, user)
        {
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
            _pessoaService = pessoaService;
            _enderecoRepository = enderecoRepository;
        }

        [AllowAnonymous]//acessa qual quer uma pessoas
        [HttpGet]
        public async Task<IEnumerable<PessoaViewModel>> ObterTodos()
        {
            var pessoa = _mapper.Map<IEnumerable<PessoaViewModel>>(await _pessoaRepository.ObterTodos());

            return pessoa;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PessoaViewModel>> ObterPorId(Guid id)
        {
            var pessoa = await ObterPessoaEndereco(id);

            if (pessoa == null) return NotFound();

            return pessoa;
        }

        [HttpGet("obter-endereco/{id:guid}")]
        public async Task<EnderecoViewModel> ObterEnderecoPorId(Guid id)
        {
            return _mapper.Map<EnderecoViewModel>(await _enderecoRepository.ObterPorId(id));
        }

        [HttpPut("atualizar-endereco/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id, EnderecoViewModel enderecoViewModel)
        {
            if (id != enderecoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(enderecoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _pessoaService.AtualizarEndereco(_mapper.Map<Endereco>(enderecoViewModel));

            return CustomResponse(enderecoViewModel);
        }
        [ClaimsAuthorize("Pessoa", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<PessoaViewModel>> Adicionar(PessoaViewModel pessoaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imagemNome = Guid.NewGuid() + "_" + pessoaViewModel.Imagem;

            if (!UploadArquivo(pessoaViewModel.ImagemUpload, imagemNome))
            {
                return CustomResponse(pessoaViewModel);
            }

            pessoaViewModel.Imagem = imagemNome;

            await _pessoaService.Adicionar(_mapper.Map<Pessoa>(pessoaViewModel));

            return CustomResponse(pessoaViewModel);

        }
        [ClaimsAuthorize("Pessoa", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PessoaViewModel>> Atualizar(Guid id, PessoaViewModel pessoaViewModel)
        {
            if (id != pessoaViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query ");
                return CustomResponse(pessoaViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);


            await _pessoaService.Atualizar(_mapper.Map<Pessoa>(pessoaViewModel));

            return CustomResponse(pessoaViewModel);

        }
        [ClaimsAuthorize("Pessoa", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PessoaViewModel>> Excluir(Guid id)
        {
            var pessoaViewModel = await ObterPessoaEndereco(id);

            if (pessoaViewModel == null) return NotFound();

            await _pessoaService.Remover(id);

            return CustomResponse(pessoaViewModel);
        }

        private async Task<PessoaViewModel> ObterPessoaEndereco(Guid id)
        {
            return _mapper.Map<PessoaViewModel>(await _pessoaRepository.ObterPessoaEndereco(id));
        }

        private bool UploadArquivo(string arquivo, string imgNome)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                NotificarErro("Forneça uma imagem para pessoa!");
                return false;
            }

            var imageDataByteArray = Convert.FromBase64String(arquivo);

            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/app/demo-webapi/src/imgs", imgNome);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgNome);
            //var filePath = Path.Combine(@"C:\Projetos\Cursos\DesenvolvedorIO\Instrutores\eduardo-pires\ASP.NET Core\03 - WebAPI\Projetos\01 MinhaAPICompleta\src\DevIO.Api\wwwroot", imgNome);

            if (System.IO.File.Exists(filePath))
            {
                NotificarErro("Já existe um arquivo com este nome!");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

            return true;
        }
    }
}
