using ShoppingApp.Infrastructure.Abstract;

namespace ShoppingApp.Business.Concrete;

internal class ShoppingListProductService : IShoppingListProductService
{
    private readonly IShoppingListProductRepository _slpRepository;

    public ShoppingListProductService(IShoppingListProductRepository slpRepository)
    {
        _slpRepository = slpRepository;
    }
}
