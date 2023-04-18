using DevFM.Business.Intefaces;
using DevFM.Business.Models;
using DevFM.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Data.Repository
{
    public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(APIDbContext context) : base(context)
        {
        }

        public async Task<Pessoa> ObterPessoaEndereco(Guid id)
        {
            return await Db.Pessoas.AsNoTracking()
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
