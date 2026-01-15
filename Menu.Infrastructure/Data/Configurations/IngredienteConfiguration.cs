using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Menu.Infrastructure.Data.Configurations
{
    public class IngredienteConfiguration : IEntityTypeConfiguration<Ingrediente>
    {
        public void Configure(EntityTypeBuilder<Ingrediente> entity)
        {
            // A. Tabla
            entity.ToTable("Ingredientes");

            // B. PK
            entity.HasKey(i => i.Id);

            // C. Propiedades
            entity.Property(i => i.Nombre)
                  .IsRequired()
                  .HasMaxLength(100);

            // D. Índice único para evitar ingredientes duplicados
            entity.HasIndex(i => i.Nombre)
                  .IsUnique();
        }
    }
}
