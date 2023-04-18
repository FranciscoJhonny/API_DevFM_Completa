using DevFM.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFM.Data.Mappings
{
    public class PessoaMapping : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Documento)
              .IsRequired()
              .HasColumnType("varchar(14)");

            builder.Property(p => p.Imagem)
               .IsRequired()
               .HasColumnType("varchar(100)");

            // 1 : 1 => Pessoa : Endereco
            builder.HasOne(f => f.Endereco)
                .WithOne(e => e.Pessoa);

            // 1 : N => Pessoa : Endereco
            // 1 : N => Fornecedor : Produtos
            //builder.HasMany(f => f.Produtos)
            //    .WithOne(p => p.Fornecedor)
            //    .HasForeignKey(p => p.FornecedorId);

            builder.ToTable("Pessoas");
        }
    }
}
