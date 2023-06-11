using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Base
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}