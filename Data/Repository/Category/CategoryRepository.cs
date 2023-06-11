using Data.Contexts;
using Data.Domains;
using Data.Repository.Generic;

namespace Data.Repository;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}