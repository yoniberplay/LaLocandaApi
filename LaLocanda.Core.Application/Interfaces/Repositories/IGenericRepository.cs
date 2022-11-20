using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaLocanda.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T t);
        Task DeleteAsync(T t);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllWithIncludesAsync(List<string> props);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdWithIncludeAsync(int id, List<string> props, List<string> colls);
        Task UpdateAsync(T t, int id);
    }
}