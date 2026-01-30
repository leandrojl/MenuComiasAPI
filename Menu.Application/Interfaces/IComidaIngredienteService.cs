using Menu.Application.DTO.ComidaIngrediente;

namespace Menu.Application.Interfaces
{
    public interface IComidaIngredienteService
    {
        // ═══════════════════════════════════════════════════════════
        // CONSULTAS
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Obtiene todas las relaciones comida-ingrediente
        /// </summary>
        Task<IEnumerable<ComidaIngredienteDto>> GetAllAsync();
        
        /// <summary>
        /// Obtiene una relación por ID
        /// </summary>
        Task<ComidaIngredienteDto?> GetByIdAsync(int id);
        
        /// <summary>
        /// Obtiene todos los ingredientes de una comida
        /// </summary>
        Task<IEnumerable<ComidaIngredienteDto>> GetByComidaIdAsync(int comidaId);
        
        /// <summary>
        /// Obtiene todas las comidas que usan un ingrediente
        /// </summary>
        Task<IEnumerable<ComidaIngredienteDto>> GetByIngredienteIdAsync(int ingredienteId);
        
        // ═══════════════════════════════════════════════════════════
        // ASIGNACIONES
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Asigna un ingrediente a una comida
        /// </summary>
        Task<ComidaIngredienteDto> AsignarIngredienteAsync(AsignarIngredienteDto dto);
        
        /// <summary>
        /// Asigna múltiples ingredientes a una comida
        /// </summary>
        Task AsignarMultiplesIngredientesAsync(int comidaId, List<int> ingredienteIds);
        
        /// <summary>
        /// Actualiza la descripción de una relación
        /// </summary>
        Task UpdateAsync(int id, UpdateComidaIngredienteDto dto);
        
        // ═══════════════════════════════════════════════════════════
        // DESASIGNACIONES
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Elimina una relación por ID
        /// </summary>
        Task DeleteAsync(int id);
        
        /// <summary>
        /// Desasigna un ingrediente de una comida
        /// </summary>
        Task DesasignarIngredienteAsync(int comidaId, int ingredienteId);
        
        /// <summary>
        /// Desasigna todos los ingredientes de una comida
        /// </summary>
        Task DesasignarTodosIngredientesAsync(int comidaId);
        
        // ═══════════════════════════════════════════════════════════
        // VALIDACIONES
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Verifica si existe una relación por ID
        /// </summary>
        Task<bool> ExistsAsync(int id);
        
        /// <summary>
        /// Verifica si existe la relación entre comida e ingrediente
        /// </summary>
        Task<bool> ExisteRelacionAsync(int comidaId, int ingredienteId);
    }
}
