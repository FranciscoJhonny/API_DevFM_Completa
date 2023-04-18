using DevFM.Business.Intefaces;
using DevFM.Business.Models;
using DevFM.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DevFM.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(APIDbContext context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorPessoa(Guid pessoaId)
        {
            return await Db.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(f => f.PessoaId == pessoaId);
        }
    }
}
