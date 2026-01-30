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
    public class ComidaIngredienteRepository : Repository<ComidaIngrediente>, IComidaIngredienteRepository
    {
        // ═══════════════════════════════════════════════════════════
        // CONSTRUCTOR
        // ═══════════════════════════════════════════════════════════
        
        public ComidaIngredienteRepository(MenuDbContext context) : base(context)
        {
        }

        // ═══════════════════════════════════════════════════════════
        // MÉTODOS CON RELACIONES
        // ═══════════════════════════════════════════════════════════

        public async Task<ComidaIngrediente?> GetByIdWithRelationsAsync(int id)
        {
            return await _dbSet
                .Include(ci => ci.Comida)
                .Include(ci => ci.Ingrediente)
                .FirstOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task<IEnumerable<ComidaIngrediente>> GetAllWithRelationsAsync()
        {
            return await _dbSet
                .Include(ci => ci.Comida)
                .Include(ci => ci.Ingrediente)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ComidaIngrediente>> GetByComidaIdAsync(int comidaId)
        {
            return await _dbSet
                .Include(ci => ci.Comida)
                .Include(ci => ci.Ingrediente)
                .Where(ci => ci.ComidaId == comidaId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ComidaIngrediente>> GetByIngredienteIdAsync(int ingredienteId)
        {
            return await _dbSet
                .Include(ci => ci.Comida)
                .Include(ci => ci.Ingrediente)
                .Where(ci => ci.IngredienteId == ingredienteId)
                .AsNoTracking()
                .ToListAsync();
        }

        // ═══════════════════════════════════════════════════════════
        // VALIDACIONES
        // ═══════════════════════════════════════════════════════════

        public async Task<bool> ExisteRelacionAsync(int comidaId, int ingredienteId)
        {
            return await _dbSet
                .AnyAsync(ci => ci.ComidaId == comidaId && ci.IngredienteId == ingredienteId);
        }

        public async Task<ComidaIngrediente?> GetByComidaAndIngredienteAsync(int comidaId, int ingredienteId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(ci => ci.ComidaId == comidaId && ci.IngredienteId == ingredienteId);
        }
    }
}
