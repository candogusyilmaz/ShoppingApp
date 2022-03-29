using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShoppingApp.Core.DataAccess;
using ShoppingApp.Core.Entities;

namespace ShoppingApp.Infrastructure.Abstract;
public interface IShoppingListRepository : IRepository<ShoppingList>
{
    Task DeleteShoppingList(int shoppingListId);
    Task<List<ShoppingList>> GetShoppingLists(int userId);
    Task<bool> IsActiveShopping(int shoppingListId);
    Task<ShoppingList> ShoppingListWithProducts(int userId, int shoppingListId);
}
