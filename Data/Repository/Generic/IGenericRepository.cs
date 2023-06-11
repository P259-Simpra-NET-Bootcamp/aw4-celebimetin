using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository.Generic
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}