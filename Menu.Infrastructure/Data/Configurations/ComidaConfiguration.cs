using Menu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Menu.Infrastructure.Data.Configurations
{
    public class ComidaConfiguration : IEntityTypeConfiguration<Comida>
    {
        public void Configure(EntityTypeBuilder<Comida> entity)
        {
            entity.ToTable("Comidas");

            entity.HasKey(c => c.Id);

            entity.Property(c => c.Nombre)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(c => c.Precio)
                  .IsRequired()
                  .HasColumnType("decimal(18,2)");

            entity.Property(c => c.Porcion)
                  .IsRequired()
                  .HasMaxLength(50);

            // En ComidaConfiguration.cs, agregar:
            entity.Property(c => c.CuantasPersonasComen)
                  .IsRequired();

            entity.HasOne(c => c.TipoComida)
                  .WithMany(t => t.Comidas)
                  .HasForeignKey(c => c.TipoComidaId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
