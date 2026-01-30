using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.Domain.Entities;

namespace Menu.Domain.Interfaces
{
    public interface IComidaIngredienteRepository : IRepository<ComidaIngrediente>
    {
        // ═══════════════════════════════════════════════════════════
        // MÉTODOS CON RELACIONES
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Obtiene una relación por ID con Comida e Ingrediente incluidos
        /// </summary>
        Task<ComidaIngrediente?> GetByIdWithRelationsAsync(int id);
        
        /// <summary>
        /// Obtiene todas las relaciones con Comida e Ingrediente incluidos
        /// </summary>
        Task<IEnumerable<ComidaIngrediente>> GetAllWithRelationsAsync();
        
        /// <summary>
        /// Obtiene todos los ingredientes de una comida específica
        /// </summary>
        Task<IEnumerable<ComidaIngrediente>> GetByComidaIdAsync(int comidaId);
        
        /// <summary>
        /// Obtiene todas las comidas que usan un ingrediente específico
        /// </summary>
        Task<IEnumerable<ComidaIngrediente>> GetByIngredienteIdAsync(int ingredienteId);

        // ═══════════════════════════════════════════════════════════
        // VALIDACIONES
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Verifica si ya existe la relación entre comida e ingrediente
        /// </summary>
        Task<bool> ExisteRelacionAsync(int comidaId, int ingredienteId);
        
        /// <summary>
        /// Obtiene la relación existente entre comida e ingrediente
        /// </summary>
        Task<ComidaIngrediente?> GetByComidaAndIngredienteAsync(int comidaId, int ingredienteId);
    }
}
