using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.Domain.Entities;

namespace Menu.Domain.Interfaces
{
    public interface ITipoComidaRepository : IRepository<TipoComida>
    {
        // ═══════════════════════════════════════════════════════════
        // MÉTODOS CON RELACIONES (Incluir entidades relacionadas)
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Obtiene un tipo de comida con todas sus comidas relacionadas
        /// Ejemplo: "Desayuno" con [Café, Tostadas, Medialunas]
        /// </summary>
        Task<TipoComida?> GetByIdWithComidasAsync(int id);
        
        /// <summary>
        /// Obtiene todos los tipos de comida con sus comidas
        /// Útil para mostrar menú completo organizado por tipo
        /// </summary>
        Task<IEnumerable<TipoComida>> GetAllWithComidasAsync();
        
        
        // ═══════════════════════════════════════════════════════════
        // VALIDACIONES (Proteger integridad de datos)
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Verifica si un tipo de comida tiene comidas asignadas
        /// Se usa ANTES de eliminar para evitar eliminar "Desayuno" si hay platos
        /// </summary>
        Task<bool> TieneComidasAsync(int tipoComidaId);
        
        /// <summary>
        /// Verifica si ya existe un tipo de comida con ese nombre
        /// Evita duplicados: "Desayuno", "desayuno", "DESAYUNO"
        /// </summary>
        Task<bool> ExisteNombreAsync(string nombre);
        
        /// <summary>
        /// Verifica si existe un nombre, excluyendo un ID específico
        /// Se usa en UPDATE para validar sin considerar el registro mismo
        /// </summary>
        Task<bool> ExisteNombreAsync(string nombre, int excludeId);
        
        
        // ═══════════════════════════════════════════════════════════
        // CONSULTAS ESPECÍFICAS (Búsquedas y filtros)
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Busca un tipo de comida por nombre exacto (case-insensitive)
        /// </summary>
        Task<TipoComida?> GetByNombreAsync(string nombre);
        
        /// <summary>
        /// Cuenta cuántas comidas tiene un tipo específico
        /// Útil para mostrar "Desayuno (5 platos)"
        /// </summary>
        Task<int> ContarComidasAsync(int tipoComidaId);
    }
}
