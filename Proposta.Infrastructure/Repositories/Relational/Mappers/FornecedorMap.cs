using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proposta.Domain.Entity;
using System;

namespace Proposta.Infrastructure.Repositories.Relational.Mappers
{
    public class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Property(entity => entity.Codigo).HasColumnName("FornecedorCodigo");

            builder.HasKey(entity => entity.Codigo);

            builder.ToTable(nameof(Fornecedor), "dbo");
        }
    }
}
