using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.Domain.Interfaces;
using Menu.Infrastructure.Data;
using Menu.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Menu.Infrastructure.Repositories
{
    public class ComidaRepository : Repository<Comida>, IComidaRepository
    {
        // ═══════════════════════════════════════════════════════════
        // CONSTRUCTOR
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Constructor: Recibe DbContext y lo pasa al Repository base
        /// </summary>
        public ComidaRepository(MenuDbContext context) : base(context)
        {
            // El base(context) pasa el context al constructor de Repository<T>
            // Ya no necesitas inicializar _context ni _dbSet (están en la clase base)
        }

        // ═══════════════════════════════════════════════════════════
        // MÉTODOS CON RELACIONES (INCLUDES)
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Obtiene una comida por ID incluyendo su TipoComida
        /// </summary>
        public async Task<Comida?> GetByIdWithTipoComidaAsync(int id)
        {
            return await _dbSet
                .Include(c => c.TipoComida)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Obtiene una comida por ID incluyendo sus ingredientes
        /// </summary>
        public async Task<Comida?> GetByIdWithIngredientesAsync(int id)
        {
            return await _dbSet
                .Include(c => c.ComidaIngredientes)
                    .ThenInclude(ci => ci.Ingrediente)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Obtiene una comida por ID con todas sus relaciones (TipoComida e Ingredientes)
        /// </summary>
        public async Task<Comida?> GetByIdWithAllRelationsAsync(int id)
        {
            return await _dbSet
                .Include(c => c.TipoComida)
                .Include(c => c.ComidaIngredientes)
                    .ThenInclude(ci => ci.Ingrediente)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Obtiene todas las comidas con su TipoComida
        /// </summary>
        public async Task<IEnumerable<Comida>> GetAllWithTipoComidaAsync()
        {
            return await _dbSet
                .Include(c => c.TipoComida)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene todas las comidas con sus ingredientes
        /// </summary>
        public async Task<IEnumerable<Comida>> GetAllWithIngredientesAsync()
        {
            return await _dbSet
                .Include(c => c.ComidaIngredientes)
                    .ThenInclude(ci => ci.Ingrediente)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene todas las comidas con todas sus relaciones
        /// </summary>
        public async Task<IEnumerable<Comida>> GetAllWithAllRelationsAsync()
        {
            return await _dbSet
                .Include(c => c.TipoComida)
                .Include(c => c.ComidaIngredientes)
                    .ThenInclude(ci => ci.Ingrediente)
                .AsNoTracking()
                .ToListAsync();
        }

        // ═══════════════════════════════════════════════════════════
        // CONSULTAS DE FILTRADO
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Obtiene todas las comidas de un tipo específico
        /// </summary>
        public async Task<IEnumerable<Comida>> GetByTipoComidaIdAsync(int tipoComidaId)
        {
            return await _dbSet
                .Include(c => c.TipoComida)
                .Where(c => c.TipoComidaId == tipoComidaId)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene comidas por rango de precio
        /// </summary>
        public async Task<IEnumerable<Comida>> GetByPrecioRangeAsync(decimal minPrecio, decimal maxPrecio)
        {
            return await _dbSet
                .Include(c => c.TipoComida)
                .Where(c => c.Precio >= minPrecio && c.Precio <= maxPrecio)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Busca comidas por nombre (contiene, case-insensitive)
        /// </summary>
        public async Task<IEnumerable<Comida>> SearchByNombreAsync(string nombre)
        {
            return await _dbSet
                .Include(c => c.TipoComida)
                .Where(c => c.Nombre.ToUpper().Contains(nombre.ToUpper()))
                .AsNoTracking()
                .ToListAsync();
        }

        // ═══════════════════════════════════════════════════════════
        // VALIDACIONES
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Verifica si existe una comida con el nombre dado
        /// </summary>
        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            return await _dbSet
                .AnyAsync(c => c.Nombre.ToUpper() == nombre.ToUpper());
        }

        /// <summary>
        /// Verifica si existe una comida con el nombre dado, excluyendo un ID específico
        /// Útil para UPDATE: verificar que no se duplique el nombre con otro registro
        /// </summary>
        public async Task<bool> ExisteNombreAsync(string nombre, int excludeId)
        {
            return await _dbSet
                .AnyAsync(c => c.Nombre.ToUpper() == nombre.ToUpper() 
                            && c.Id != excludeId);
        }

        /// <summary>
        /// Verifica si la comida tiene ingredientes asignados
        /// </summary>
        public async Task<bool> TieneIngredientesAsync(int comidaId)
        {
            return await _context.ComidaIngredientes
                .AnyAsync(ci => ci.ComidaId == comidaId);
        }
    }
}
