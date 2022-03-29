using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace ShoppingApp.Infrastructure.Concrete.Ef;

public class EfProductRepository : EfRepository<Product, AppDbContext>, IProductRepository
{
    public EfProductRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public new async Task Add(Product entity)
    {
        if (_dbSet.Any(s => s.Name == entity.Name))
            throw new ArgumentException("A product with the same name already exists.");

        await base.Add(entity);
    }

    public new async Task Update(Product entity)
    {
        if (_dbSet.Any(s => s.Name == entity.Name))
            throw new ArgumentException("A product with the same name already exists.");

        await base.Update(entity);
    }

    public new async Task<List<Product>> GetAll()
    {
        return await _dbSet.Include(s => s.Category).ToListAsync();
    }

    public new async Task<List<Product>> GetAll(Expression<Func<Product, bool>> expression)
    {
        return await _dbSet.Where(expression).Include(s => s.Category).ToListAsync();
    }
}
