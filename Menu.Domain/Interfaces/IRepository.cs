using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Domain.Interfaces
{
    public interface IRepository<T>
    {
        // MÉTODOS BÁSICOS CRUD
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        // MÉTODOS DE EXISTENCIA
        Task<bool> ExistsAsync(int id);

        // MÉTODOS DE GUARDADO
        Task<int> SaveChangesAsync();
    }
}
