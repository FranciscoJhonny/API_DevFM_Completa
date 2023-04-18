using DevFM.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Business.Intefaces
{
    public interface  IPessoaService: IDisposable
    {
        Task<bool> Adicionar(Pessoa pessoa);
        Task<bool> Atualizar(Pessoa pessoa);
        Task<bool> Remover(Guid id);

        Task AtualizarEndereco(Endereco endereco);
    }
}
