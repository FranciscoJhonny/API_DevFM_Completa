using DevFM.Business.Models;
using System;
using System.Threading.Tasks;

namespace DevFM.Business.Intefaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorPessoa(Guid fornecedorId);
    }
}
