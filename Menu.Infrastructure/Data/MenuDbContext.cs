using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Menu.Infrastructure.Data

{
    public class MenuDbContext : DbContext //esta es la herencia a DbContext

            /*
        ¿Qué heredás concretamente de DbContext?

        Gracias a la herencia, MenuDbContext obtiene:

        SaveChanges()

        SaveChangesAsync()

        Set<TEntity>()

        Tracking de entidades

        Change Tracker

        Conexión a la base de datos

        Pipeline de consultas LINQ → SQL

        OnModelCreating(ModelBuilder)
        */
    {
        public MenuDbContext(DbContextOptions<MenuDbContext> options) : base(options) //se delega la responsabilidad a la clase base
        {
        }

        public DbSet<TipoComida> TiposComida { get; set; } = null!;
        public DbSet<Comida> Comidas { get; set; } = null!;
        public DbSet<Ingrediente> Ingredientes { get; set; } = null!;
        public DbSet<ComidaIngrediente> ComidaIngredientes { get; set; } = null!;

        //UTILIZACION DE FLUENT API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MenuDbContext).Assembly);
            /*
             DbContext tiene una implementación base

                Vos la extendés, no la reemplazás

                ApplyConfigurationsFromAssembly busca todas las clases que implementen:

                IEntityTypeConfiguration<T>
                             */
        }
    }
}
