using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.Domain.Entities;
using Menu.Domain.Interfaces;
using Menu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Menu.Infrastructure.Repositories
{
    public class TipoComidaRepository : Repository<TipoComida>, ITipoComidaRepository
    {
        // ═══════════════════════════════════════════════════════════
        // CONSTRUCTOR
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Constructor: Recibe DbContext y lo pasa al Repository base
        /// </summary>
        public TipoComidaRepository(MenuDbContext context) : base(context)
        {
            // El base(context) pasa el context al constructor de Repository<T>
            // Ya no necesitas inicializar _context ni _dbSet (están en la clase base)
        }

        
        // ═══════════════════════════════════════════════════════════
        // MÉTODOS CON RELACIONES
        // ═══════════════════════════════════════════════════════════
        
        public async Task<TipoComida?> GetByIdWithComidasAsync(int id)
        {
            return await _dbSet
                .Include(t => t.Comidas)  // ← Incluye la colección de comidas
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TipoComida>> GetAllWithComidasAsync()
        {
            return await _dbSet
                .Include(t => t.Comidas)     // ← Incluye todas las comidas
                .AsNoTracking()              // ← No rastrear cambios (performance)
                .ToListAsync();
        }

        
        // ═══════════════════════════════════════════════════════════
        // VALIDACIONES
        // ═══════════════════════════════════════════════════════════
        
        public async Task<bool> TieneComidasAsync(int tipoComidaId)
        {
            // Verifica si existe AL MENOS UNA comida con ese TipoComidaId
            return await _context.Comidas
                .AnyAsync(c => c.TipoComidaId == tipoComidaId);
        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            // ToUpper() para comparación case-insensitive
            // "Desayuno" == "desayuno" == "DESAYUNO"
            return await _dbSet
                .AnyAsync(t => t.Nombre.ToUpper() == nombre.ToUpper());
        }

        public async Task<bool> ExisteNombreAsync(string nombre, int excludeId)
        {
            // Busca el nombre PERO excluye el ID especificado
            // Útil para UPDATE: "¿ Existe 'Almuerzo' en otro registro ?"
            return await _dbSet
                .AnyAsync(t => t.Nombre.ToUpper() == nombre.ToUpper() 
                            && t.Id != excludeId);
        }

        
        // ═══════════════════════════════════════════════════════════
        // CONSULTAS ESPECÍFICAS
        // ═══════════════════════════════════════════════════════════
        
        public async Task<TipoComida?> GetByNombreAsync(string nombre)
        {
            return await _dbSet
                .FirstOrDefaultAsync(t => t.Nombre.ToUpper() == nombre.ToUpper());
        }

        public async Task<int> ContarComidasAsync(int tipoComidaId)
        {
            return await _context.Comidas
                .CountAsync(c => c.TipoComidaId == tipoComidaId);
        }
    }
}
