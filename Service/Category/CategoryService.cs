using Data.Domains;
using Data.UnitOfWork;
using Service.Base;

namespace Service;

public class CategoryService : BaseService<Category>, ICategoryService
{
    public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}