using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mapping
{
    public class FuncaoMap : IEntityTypeConfiguration<Function>
    {
        public void Configure(EntityTypeBuilder<Function> builder)
        {
            builder.Property(f => f.Id).ValueGeneratedOnAdd();
            builder.Property(f => f.Descricao).IsRequired().HasMaxLength(30);


            builder.HasData(
                new Function
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Morador",
                    NormalizedName = "MORADOR",
                    Descricao = "Morador do prédio"

                },

                new Function
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Sindico",
                    NormalizedName = "SINDICO",
                    Descricao = "Síndico do prédio"

                },
                new Function
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Administrador",
                    NormalizedName = "ADMINISTRADOR",
                    Descricao = "Administrador do prédio"

                });
            builder.ToTable("Functions");
        }
    }
}
