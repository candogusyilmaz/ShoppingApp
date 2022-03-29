using Microsoft.EntityFrameworkCore;

using ShoppingApp.Core.DataAccess.Ef;
using ShoppingApp.Core.Entities;
using ShoppingApp.Infrastructure;
using ShoppingApp.Infrastructure.Abstract;

namespace ShoppingApp.Infrastructure.Concrete.Ef;

public class EfShoppingListRepository : EfRepository<ShoppingList, AppDbContext>, IShoppingListRepository
{

    public EfShoppingListRepository(AppDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<List<ShoppingList>> GetShoppingLists(int userId)
    {
        return await _dbSet.Where(s => s.ShoppingListUsers.Any(s => s.UserId == userId)).ToListAsync();
    }

    public async Task<ShoppingList> ShoppingListWithProducts(int userId, int shoppingListId)
    {
        return await _dbSet.Include(s => s.ShoppingListProducts)
            .Where(s => s.Id == shoppingListId && s.ShoppingListUsers.Any(s => s.UserId == userId))
            .FirstOrDefaultAsync();
    }

    public async Task<bool> IsActiveShopping(int shoppingListId)
    {
        return await _dbSet.AnyAsync(s => s.Id == shoppingListId && s.IsActiveShopping);
    }

    public async Task DeleteShoppingList(int shoppingListId)
    {
        ShoppingList list = await _dbSet.Where(s => s.Id == shoppingListId)
            .Select(s => new ShoppingList()
            {
                Id = s.Id,
                ShoppingListUsers = s.ShoppingListUsers.Select(x => new ShoppingListUser(x.Id)).ToList(),
                ShoppingListProducts = s.ShoppingListProducts.Select(y => new ShoppingListProduct(y.Id)).ToList()
            })
            .FirstOrDefaultAsync();

        foreach (ShoppingListUser item in list.ShoppingListUsers)
            _dbContext.Entry(item).State = EntityState.Deleted;

        foreach (ShoppingListProduct item in list.ShoppingListProducts)
            _dbContext.Entry(item).State = EntityState.Deleted;

        _dbContext.Entry(list).State = EntityState.Deleted;
        await _dbContext.SaveChangesAsync();
    }
}
