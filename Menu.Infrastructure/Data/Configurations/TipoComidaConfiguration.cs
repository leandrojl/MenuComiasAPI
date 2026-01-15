using Menu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Menu.Infrastructure.Data.Configurations
{
    public class TipoComidaConfiguration : IEntityTypeConfiguration<TipoComida>
    {
        public void Configure(EntityTypeBuilder<TipoComida> entity)
        {
            entity.ToTable("TiposComida");

            entity.HasKey(t => t.Id);

            entity.Property(t => t.Nombre)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.HasMany(t => t.Comidas)
                  .WithOne(c => c.TipoComida)
                  .HasForeignKey(c => c.TipoComidaId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
