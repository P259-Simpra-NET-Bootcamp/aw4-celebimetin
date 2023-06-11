using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository.Dapper
{
    public interface IDapperRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> AddAsync(T entity);
        int Update(T entity);
        int Delete(int id);
    }
}