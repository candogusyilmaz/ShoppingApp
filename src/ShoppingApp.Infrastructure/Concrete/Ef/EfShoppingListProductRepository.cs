using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using ShoppingApp.Core.DataAccess.Ef;
using ShoppingApp.Core.Entities;
using ShoppingApp.Infrastructure.Abstract;

namespace ShoppingApp.Infrastructure.Concrete.Ef;

public class EfShoppingListProductRepository : EfRepository<ShoppingListProduct, AppDbContext>, IShoppingListProductRepository
{
    public EfShoppingListProductRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public new async Task<ICollection<ShoppingListProduct>> GetAll()
    {
        return await _dbSet.Include(s => s.Product).ToListAsync();
    }

    public new async Task<ICollection<ShoppingListProduct>> GetAll(Expression<Func<ShoppingListProduct, bool>> expression)
    {
        return await _dbSet.Where(expression).Include(s => s.Product).ToListAsync();
    }

    public async Task<List<ShoppingListProduct>> GetItemsWithProduct(int shoppingListId)
    {
        return await _dbSet.Where(s => s.ShoppingListId == shoppingListId).Include(s => s.Product).ToListAsync();
    }

    public async Task DeleteProductsFromList(int shoppingListId)
    {
        ShoppingListProduct[] items = await _dbSet.Where(s => s.ShoppingListId == shoppingListId).Select(s => new ShoppingListProduct(s.Id)).ToArrayAsync();

        _dbSet.RemoveRange(items);
        await _dbContext.SaveChangesAsync();
    }
}
