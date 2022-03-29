using ShoppingApp.Core.DataAccess.Ef;
using ShoppingApp.Core.Entities;
using ShoppingApp.Infrastructure.Abstract;

namespace ShoppingApp.Infrastructure.Concrete.Ef;

public class EfCategoryRepository : EfRepository<Category, AppDbContext>, ICategoryRepository
{
    public EfCategoryRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
