using Menu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Menu.Infrastructure.Data.Configurations
{
    public class ComidaIngredienteConfiguration : IEntityTypeConfiguration<ComidaIngrediente>
    {
        public void Configure(EntityTypeBuilder<ComidaIngrediente> entity)
        { // A. Tabla
            entity.ToTable("ComidaIngredientes");

            // B. Clave primaria simple (tu entidad ya tiene un Id)
            entity.HasKey(ci => ci.Id);

            // C. Configurar propiedades
            entity.Property(ci => ci.Descripcion)
                  .HasMaxLength(500);  // Puede ser null, no ponemos IsRequired

            // D. Relación con Comida
            entity.HasOne(ci => ci.Comida)
                  .WithMany(c => c.ComidaIngredientes)
                  .HasForeignKey(ci => ci.ComidaId)
                  .OnDelete(DeleteBehavior.Cascade); // Si borro comida, borro sus ingredientes

            // E. Relación con Ingrediente
            entity.HasOne(ci => ci.Ingrediente)
                  .WithMany(i => i.ComidaIngredientes)
                  .HasForeignKey(ci => ci.IngredienteId)
                  .OnDelete(DeleteBehavior.Cascade); // Si borro ingrediente, borro sus relaciones

            // F. Índice compuesto para evitar duplicados
            entity.HasIndex(ci => new { ci.ComidaId, ci.IngredienteId })
                  .IsUnique();
        }
    }

}
