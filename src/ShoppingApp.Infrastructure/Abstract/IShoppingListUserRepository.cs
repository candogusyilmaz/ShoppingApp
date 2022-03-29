using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShoppingApp.Core.DataAccess;
using ShoppingApp.Core.Entities;

namespace ShoppingApp.Infrastructure.Abstract;
public interface IShoppingListUserRepository : IRepository<ShoppingListUser>
{
    Task DeleteShoppingListUsers(int shoppingListId);
    Task<List<ShoppingListUser>> GetAllWithUser(int shoppingListId);
    Task<bool> HasAccess(int userId, int shoppingListId);
    Task<bool> IsOwner(int userId, int shoppingListId);
}
