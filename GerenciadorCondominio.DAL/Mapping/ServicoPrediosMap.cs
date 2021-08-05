using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mapping
{
    public class ServicoPrediosMap : IEntityTypeConfiguration<ServicoPredio>
    {
        public void Configure(EntityTypeBuilder<ServicoPredio> builder)
        {
            builder.HasKey(s => s.ServicoPredioId);
            builder.Property(s => s.ServicoId).IsRequired();
            builder.Property(s => s.DataExecucao).IsRequired();

            builder.HasOne(s => s.Servico).WithMany(s => s.servicoPredios).HasForeignKey(s => s.ServicoId);

            builder.ToTable("ServicoPredios");
        }
    }
}
