using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.Domain.Entities;

namespace Menu.Domain.Interfaces
{
    public interface IIngredienteRepository : IRepository<Ingrediente>
    {
        // ═══════════════════════════════════════════════════════════
        // MÉTODOS CON RELACIONES
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Obtiene un ingrediente con las comidas que lo usan
        /// </summary>
        Task<Ingrediente?> GetByIdWithComidasAsync(int id);
        
        /// <summary>
        /// Obtiene todos los ingredientes con sus comidas
        /// </summary>
        Task<IEnumerable<Ingrediente>> GetAllWithComidasAsync();

        // ═══════════════════════════════════════════════════════════
        // CONSULTAS DE FILTRADO
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Busca ingredientes por nombre (contiene, case-insensitive)
        /// </summary>
        Task<IEnumerable<Ingrediente>> SearchByNombreAsync(string nombre);
        
        /// <summary>
        /// Obtiene un ingrediente por nombre exacto
        /// </summary>
        Task<Ingrediente?> GetByNombreAsync(string nombre);

        // ═══════════════════════════════════════════════════════════
        // VALIDACIONES
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Verifica si existe un ingrediente con el nombre dado
        /// </summary>
        Task<bool> ExisteNombreAsync(string nombre);
        
        /// <summary>
        /// Verifica si existe un ingrediente con el nombre dado, excluyendo un ID
        /// </summary>
        Task<bool> ExisteNombreAsync(string nombre, int excludeId);
        
        /// <summary>
        /// Verifica si el ingrediente está siendo usado en alguna comida
        /// </summary>
        Task<bool> TieneComidasAsync(int ingredienteId);
        
        /// <summary>
        /// Cuenta cuántas comidas usan este ingrediente
        /// </summary>
        Task<int> ContarComidasAsync(int ingredienteId);
    }
}
