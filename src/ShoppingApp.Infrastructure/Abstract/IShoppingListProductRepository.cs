using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShoppingApp.Core.DataAccess;
using ShoppingApp.Core.Entities;

namespace ShoppingApp.Infrastructure.Abstract;
public interface IShoppingListProductRepository : IRepository<ShoppingListProduct>
{
    Task DeleteProductsFromList(int shoppingListId);
    Task<List<ShoppingListProduct>> GetItemsWithProduct(int shoppingListId);
}
