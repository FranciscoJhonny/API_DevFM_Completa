using DevFM.Business.Models;
using System;
using System.Threading.Tasks;

namespace DevFM.Business.Intefaces
{
    public interface IPessoaRepository:IRepository<Pessoa>
    {
        Task<Pessoa> ObterPessoaEndereco(Guid id);
    }
}
