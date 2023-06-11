using Core.Models;
using Data.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Base
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IUnitOfWork unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await unitOfWork.DapperRepository<T>().GetAllAsync();
        }

        public async Task<T> Get(int id)
        {
            return await unitOfWork.DapperRepository<T>().GetByIdAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            var list = await unitOfWork.DapperRepository<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            var list = unitOfWork.DapperRepository<T>().Update(entity);
        }

        public void Delete(int id)
        {
            unitOfWork.DapperRepository<T>().Delete(id);
        }
    }
}