using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.Domain.Entities;
using Menu.Domain.Interfaces;

namespace Menu.Domain.Interfaces
{
    public interface IComidaRepository : IRepository<Comida>
    {
        // ═══════════════════════════════════════════════════════════
        // MÉTODOS CON RELACIONES (INCLUDES)
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Obtiene una comida por ID incluyendo su TipoComida
        /// </summary>
        Task<Comida?> GetByIdWithTipoComidaAsync(int id);
        
        /// <summary>
        /// Obtiene una comida por ID incluyendo sus ingredientes
        /// </summary>
        Task<Comida?> GetByIdWithIngredientesAsync(int id);
        
        /// <summary>
        /// Obtiene una comida por ID con todas sus relaciones (TipoComida e Ingredientes)
        /// </summary>
        Task<Comida?> GetByIdWithAllRelationsAsync(int id);
        
        /// <summary>
        /// Obtiene todas las comidas con su TipoComida
        /// </summary>
        Task<IEnumerable<Comida>> GetAllWithTipoComidaAsync();
        
        /// <summary>
        /// Obtiene todas las comidas con sus ingredientes
        /// </summary>
        Task<IEnumerable<Comida>> GetAllWithIngredientesAsync();
        
        /// <summary>
        /// Obtiene todas las comidas con todas sus relaciones
        /// </summary>
        Task<IEnumerable<Comida>> GetAllWithAllRelationsAsync();

        // ═══════════════════════════════════════════════════════════
        // CONSULTAS DE FILTRADO
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Obtiene todas las comidas de un tipo específico
        /// </summary>
        Task<IEnumerable<Comida>> GetByTipoComidaIdAsync(int tipoComidaId);
        
        /// <summary>
        /// Obtiene comidas por rango de precio
        /// </summary>
        Task<IEnumerable<Comida>> GetByPrecioRangeAsync(decimal minPrecio, decimal maxPrecio);
        
        /// <summary>
        /// Busca comidas por nombre (contiene)
        /// </summary>
        Task<IEnumerable<Comida>> SearchByNombreAsync(string nombre);

        // ═══════════════════════════════════════════════════════════
        // VALIDACIONES
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Verifica si existe una comida con el nombre dado
        /// </summary>
        Task<bool> ExisteNombreAsync(string nombre);
        
        /// <summary>
        /// Verifica si existe una comida con el nombre dado, excluyendo un ID específico
        /// </summary>
        Task<bool> ExisteNombreAsync(string nombre, int excludeId);
        
        /// <summary>
        /// Verifica si la comida tiene ingredientes asignados
        /// </summary>
        Task<bool> TieneIngredientesAsync(int comidaId);
    }
}
