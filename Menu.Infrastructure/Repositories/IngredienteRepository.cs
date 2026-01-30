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
    public class IngredienteRepository : Repository<Ingrediente>, IIngredienteRepository
    {
        // ═══════════════════════════════════════════════════════════
        // CONSTRUCTOR
        // ═══════════════════════════════════════════════════════════
        
        public IngredienteRepository(MenuDbContext context) : base(context)
        {
        }

        // ═══════════════════════════════════════════════════════════
        // MÉTODOS CON RELACIONES
        // ═══════════════════════════════════════════════════════════

        public async Task<Ingrediente?> GetByIdWithComidasAsync(int id)
        {
            return await _dbSet
                .Include(i => i.ComidaIngredientes)
                    .ThenInclude(ci => ci.Comida)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Ingrediente>> GetAllWithComidasAsync()
        {
            return await _dbSet
                .Include(i => i.ComidaIngredientes)
                    .ThenInclude(ci => ci.Comida)
                .AsNoTracking()
                .ToListAsync();
        }

        // ═══════════════════════════════════════════════════════════
        // CONSULTAS DE FILTRADO
        // ═══════════════════════════════════════════════════════════

        public async Task<IEnumerable<Ingrediente>> SearchByNombreAsync(string nombre)
        {
            return await _dbSet
                .Where(i => i.Nombre.ToUpper().Contains(nombre.ToUpper()))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Ingrediente?> GetByNombreAsync(string nombre)
        {
            return await _dbSet
                .FirstOrDefaultAsync(i => i.Nombre.ToUpper() == nombre.ToUpper());
        }

        // ═══════════════════════════════════════════════════════════
        // VALIDACIONES
        // ═══════════════════════════════════════════════════════════

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            return await _dbSet
                .AnyAsync(i => i.Nombre.ToUpper() == nombre.ToUpper());
        }

        public async Task<bool> ExisteNombreAsync(string nombre, int excludeId)
        {
            return await _dbSet
                .AnyAsync(i => i.Nombre.ToUpper() == nombre.ToUpper() 
                            && i.Id != excludeId);
        }

        public async Task<bool> TieneComidasAsync(int ingredienteId)
        {
            return await _context.ComidaIngredientes
                .AnyAsync(ci => ci.IngredienteId == ingredienteId);
        }

        public async Task<int> ContarComidasAsync(int ingredienteId)
        {
            return await _context.ComidaIngredientes
                .CountAsync(ci => ci.IngredienteId == ingredienteId);
        }
    }
}
