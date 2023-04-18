using AutoMapper;
using DevFM.Api.ViewModels;
using DevFM.Business.Models;

namespace DevFM.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Pessoa, PessoaViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
        }
    }
}
