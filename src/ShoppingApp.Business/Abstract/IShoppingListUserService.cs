namespace ShoppingApp.Business.Abstract;

public interface IShoppingListUserService
{
    Task<bool> HasAccess(int userId, int shoppingListId);
}
