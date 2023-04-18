using DevFM.Business.Intefaces;
using DevFM.Business.Models;
using DevFM.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Business.Services
{
   public class PessoaService : BaseService, IPessoaService
    {
        private readonly IPessoaRepository _PessoaRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public PessoaService(IPessoaRepository PessoaRepository,
                                 IEnderecoRepository enderecoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _PessoaRepository = PessoaRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<bool> Adicionar(Pessoa pessoa)
        {
            if (!ExecutarValidacao(new PessoaValidation(), pessoa)
                || !ExecutarValidacao(new EnderecoValidation(), pessoa.Endereco)) return false;

            if (_PessoaRepository.Buscar(f => f.Documento == pessoa.Documento).Result.Any())
            {
                Notificar("Já existe um pessoa com este documento informado.");
                return false;
            }

            await _PessoaRepository.Adicionar(pessoa);
            return true;
        }

        public async Task<bool> Atualizar(Pessoa pessoa)
        {
            if (!ExecutarValidacao(new PessoaValidation(), pessoa)) return false;

            if (_PessoaRepository.Buscar(f => f.Documento == pessoa.Documento && f.Id != pessoa.Id).Result.Any())
            {
                Notificar("Já existe um Pessoa com este documento infomado.");
                return false;
            }

            await _PessoaRepository.Atualizar(pessoa);
            return true;
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task<bool> Remover(Guid id)
        {

            var endereco = await _enderecoRepository.ObterEnderecoPorPessoa(id);

            if (endereco != null)
            {
                await _enderecoRepository.Remover(endereco.Id);
            }

            await _PessoaRepository.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _PessoaRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}
