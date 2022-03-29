using ShoppingApp.Core.Entities;

namespace ShoppingApp.Business.Abstract;

public interface IShoppingListService
{
    Task AddProductToList(ShoppingListProduct shoppingListProduct);
    Task Create(ShoppingList entity);
    Task<ShoppingList> GetShoppingList(int shoppingListId);
    Task<List<ShoppingListProduct>> GetItemsWithProduct(int shoppingListId);
    Task<List<ShoppingList>> GetShoppingLists(int userId);
    Task ShareTo(int shoppingListId, int userToShareId);
    Task SetActiveShopping(int shoppingListId, bool activeShopping);
    Task UnshareTo(int shoppingListId, int userToUnshareId);
    Task<IEnumerable<User>> GetShareableUsers(int shoppingListId);
    Task Delete(int shoppingListId);
}
