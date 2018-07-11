using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Usuario.Infrastructure.Repositories.Relational.Mappers
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario.Domain.Entity.Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario.Domain.Entity.Usuario> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Property(entity => entity.Codigo).HasColumnName("UsuarioCodigo");

            builder.HasKey(entity => entity.Codigo);

            builder.ToTable(nameof(Usuario.Domain.Entity.Usuario), "dbo");
        }
    }
}
