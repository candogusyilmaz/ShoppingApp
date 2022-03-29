using ShoppingApp.Infrastructure.Abstract;

namespace ShoppingApp.Business.Concrete;

internal class ShoppingListUserService : IShoppingListUserService
{
    private readonly IShoppingListUserRepository _shoppingListUserRepository;

    public ShoppingListUserService(IShoppingListUserRepository shoppingListUserRepository)
    {
        _shoppingListUserRepository = shoppingListUserRepository;
    }

    public async Task<bool> HasAccess(int userId, int shoppingListId)
    {
        return await _shoppingListUserRepository.HasAccess(userId, shoppingListId);
    }
}
