using Menu.Application.DTO.TipoComida;

namespace Menu.Application.Interfaces
{
    public interface ITipoComidaService
    {
        // ═══════════════════════════════════════════════════════════
        // OPERACIONES CRUD BÁSICAS
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Obtiene todos los tipos de comida
        /// </summary>
        Task<IEnumerable<TipoComidaDto>> GetAllAsync();
        
        /// <summary>
        /// Obtiene un tipo de comida por ID
        /// </summary>
        Task<TipoComidaDto?> GetByIdAsync(int id);
        
        /// <summary>
        /// Crea un nuevo tipo de comida
        /// </summary>
        Task<TipoComidaDto> CreateAsync(CreateTipoComidaDto dto);
        
        /// <summary>
        /// Actualiza un tipo de comida existente
        /// </summary>
        Task UpdateAsync(int id, UpdateTipoComidaDto dto);
        
        /// <summary>
        /// Elimina un tipo de comida
        /// </summary>
        Task DeleteAsync(int id);
        
        // ═══════════════════════════════════════════════════════════
        // CONSULTAS CON RELACIONES
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Obtiene un tipo de comida con sus comidas asociadas
        /// </summary>
        Task<TipoComidaWithComidasDto?> GetByIdWithComidasAsync(int id);
        
        /// <summary>
        /// Obtiene todos los tipos de comida con sus comidas
        /// </summary>
        Task<IEnumerable<TipoComidaWithComidasDto>> GetAllWithComidasAsync();
        
        // ═══════════════════════════════════════════════════════════
        // VALIDACIONES
        // ═══════════════════════════════════════════════════════════
        
        /// <summary>
        /// Verifica si existe un tipo de comida
        /// </summary>
        Task<bool> ExistsAsync(int id);
        
        /// <summary>
        /// Verifica si un tipo de comida tiene comidas asociadas
        /// </summary>
        Task<bool> TieneComidasAsync(int id);
        
        /// <summary>
        /// Verifica si existe un tipo de comida con el nombre dado
        /// </summary>
        Task<bool> ExisteNombreAsync(string nombre);
    }
}
