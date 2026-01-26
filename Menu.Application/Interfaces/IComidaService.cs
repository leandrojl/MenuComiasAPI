using Menu.Application.DTO.Comida;

namespace Menu.Application.Interfaces
{
       public interface IComidaService
{
        Task<IEnumerable<ComidaDto>> GetAllAsync();
        Task<ComidaDto?> GetByIdAsync(int id);
        Task<ComidaDto> CreateAsync(CreateComidaDto dto);
        Task UpdateAsync(int id, UpdateComidaDto dto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
} 

}
