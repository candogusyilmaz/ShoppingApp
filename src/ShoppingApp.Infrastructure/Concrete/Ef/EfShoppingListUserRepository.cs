using Microsoft.EntityFrameworkCore;

using ShoppingApp.Core.DataAccess.Ef;
using ShoppingApp.Core.Entities;
using ShoppingApp.Infrastructure.Abstract;

namespace ShoppingApp.Infrastructure.Concrete.Ef;

public class EfShoppingListUserRepository : EfRepository<ShoppingListUser, AppDbContext>, IShoppingListUserRepository
{
    public EfShoppingListUserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> HasAccess(int userId, int shoppingListId)
    {
        return await _dbSet.AnyAsync(s => s.UserId == userId && s.ShoppingListId == shoppingListId);
    }

    public async Task<bool> IsOwner(int userId, int shoppingListId)
    {
        return await _dbSet.AnyAsync(s => s.UserId == userId && s.ShoppingListId == shoppingListId && s.IsOwner == true);
    }

    public async Task<List<ShoppingListUser>> GetAllWithUser(int shoppingListId)
    {
        return await _dbSet.Include(s => s.User).Where(s => s.ShoppingListId == shoppingListId).ToListAsync();
    }

    public async Task DeleteShoppingListUsers(int shoppingListId)
    {
        var items = await _dbSet.Where(s => s.ShoppingListId == shoppingListId).Select(s => new ShoppingListUser(s.Id)).ToArrayAsync();

        _dbSet.RemoveRange(items);
        await _dbContext.SaveChangesAsync();
    }
}
