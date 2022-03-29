using ShoppingApp.Infrastructure.Abstract;

namespace ShoppingApp.Business.Concrete;

internal class ShoppingListService : IShoppingListService
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IProductRepository _productRepository;
    private readonly IShoppingListProductRepository _shoppingListProductRepository;
    private readonly IClaimService _claimService;
    private readonly IShoppingListUserRepository _shoppingListUserRepository;
    private readonly IUserRepository _userRepository;

    public ShoppingListService(IShoppingListRepository shoppingListRepository,
        IProductRepository productRepository,
        IShoppingListProductRepository shoppingListProductRepository,
        IClaimService claimService,
        IShoppingListUserRepository shoppingListUserRepository,
        IUserRepository userRepository)
    {
        _shoppingListRepository = shoppingListRepository;
        _productRepository = productRepository;
        _shoppingListProductRepository = shoppingListProductRepository;
        _claimService = claimService;
        _shoppingListUserRepository = shoppingListUserRepository;
        _userRepository = userRepository;
    }

    public async Task Create(ShoppingList entity)
    {
        await _shoppingListRepository.Add(entity);
    }

    public async Task<ShoppingList> GetShoppingList(int shoppingListId)
    {
        bool hasAccess = await _shoppingListUserRepository.HasAccess(_claimService.UserId, shoppingListId);

        if (hasAccess is false)
            throw new Exception("User doesn't have access to the list.");

        return await _shoppingListRepository.Get(s => s.Id == shoppingListId);
    }

    public async Task<List<ShoppingList>> GetShoppingLists(int userId)
    {
        if (userId <= 0)
            return null;

        return await _shoppingListRepository.GetShoppingLists(userId);
    }

    public async Task AddProductToList(ShoppingListProduct shoppingListProduct)
    {
        Product product = await _productRepository.Get(s => s.Id == shoppingListProduct.ProductId);

        if (product is null)
            throw new Exception("Product not found.");

        ShoppingList shoppingList = await _shoppingListRepository.ShoppingListWithProducts(_claimService.UserId, shoppingListProduct.ShoppingListId);

        if (shoppingList is null)
            throw new Exception("Shopping list not found.");

        if (shoppingList.IsActiveShopping)
            throw new Exception("Shopping list can not be edited while someone is shopping.");

        if (shoppingList.ShoppingListProducts.Any(s => s.ProductId == product.Id))
            throw new Exception($"Shopping list already contains {product.Name}");

        await _shoppingListProductRepository.Add(shoppingListProduct);
    }

    public async Task ShareTo(int shoppingListId, int userToShareId)
    {
        ShoppingList list = await _shoppingListRepository.Get(s => s.Id == shoppingListId);

        if (list is null)
            throw new Exception("Shopping list not found.");

        bool isCurrentUserOwner = await _shoppingListUserRepository.IsOwner(_claimService.UserId, list.Id);

        if (isCurrentUserOwner is false)
            throw new Exception("Only the owners can share the list.");

        bool isAlreadySharedToUser = await _shoppingListUserRepository.HasAccess(userToShareId, list.Id);

        if (isAlreadySharedToUser is true)
            throw new Exception("User already has access to the list.");

        await _shoppingListUserRepository.Add(new ShoppingListUser(list.Id, userToShareId, false));
    }

    public async Task UnshareTo(int shoppingListId, int userToUnshareId)
    {
        ShoppingList list = await _shoppingListRepository.Get(s => s.Id == shoppingListId);

        if (list is null)
            throw new Exception("Shopping list not found.");

        bool isCurrentUserOwner = await _shoppingListUserRepository.IsOwner(_claimService.UserId, list.Id);

        if (isCurrentUserOwner is false)
            throw new Exception("Only the owner can unshare.");

        ShoppingListUser sharedUser = await _shoppingListUserRepository.Get(s => s.UserId == userToUnshareId && s.ShoppingListId == shoppingListId);

        if (sharedUser is null)
            throw new Exception("User doesn't have access to the list.");

        if (sharedUser.IsOwner)
            throw new Exception("Owners can not be unshared from the list.");

        await _shoppingListUserRepository.Delete(sharedUser);
    }

    public async Task<List<ShoppingListProduct>> GetItemsWithProduct(int shoppingListId)
    {
        bool userHasAccess = await _shoppingListUserRepository.HasAccess(_claimService.UserId, shoppingListId);

        if (userHasAccess is false)
            throw new Exception("User doesn't have acesss to the list.");

        return await _shoppingListProductRepository.GetItemsWithProduct(shoppingListId);
    }

    public async Task SetActiveShopping(int shoppingListId, bool activeShopping)
    {
        bool hasAccess = await _shoppingListUserRepository.HasAccess(_claimService.UserId, shoppingListId);

        if (hasAccess is false)
            throw new Exception("User doesn't have acesss to the list.");

        ShoppingList list = await _shoppingListRepository.Get(s => s.Id == shoppingListId);

        if (list.IsActiveShopping == activeShopping)
            throw new Exception("Status must be different than the one currently set.");

        if (list.IsActiveShopping)
        {
            if (list.ActiveShoppingUserId != _claimService.UserId)
                throw new Exception("Only the user who's shopping can change the status of the list.");

            list.IsActiveShopping = activeShopping;
            list.ActiveShoppingUserId = null;
        }
        else
        {
            list.IsActiveShopping = true;
            list.ActiveShoppingUserId = _claimService.UserId;
        }

        await _shoppingListRepository.Update(list);
    }

    public async Task<IEnumerable<User>> GetShareableUsers(int shoppingListId)
    {
        IEnumerable<User> sharedUsers = (await _shoppingListUserRepository.GetAllWithUser(shoppingListId)).Select(s => s.User);
        List<User> allUsers = await _userRepository.GetAll();

        return allUsers.Except(sharedUsers);
    }

    public async Task Delete(int shoppingListId)
    {
        bool isOwner = await _shoppingListUserRepository.IsOwner(_claimService.UserId, shoppingListId);

        if (isOwner is false)
            throw new Exception("Only the owner can delete the list.");

        bool isActiveShopping = await _shoppingListRepository.IsActiveShopping(shoppingListId);

        if (isActiveShopping)
            throw new Exception("Shopping list can't be deleted while on shopping.");

        await _shoppingListRepository.DeleteShoppingList(shoppingListId);
    }
}
