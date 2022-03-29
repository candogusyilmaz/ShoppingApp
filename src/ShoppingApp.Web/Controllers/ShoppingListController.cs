
using Microsoft.AspNetCore.Authorization;

namespace ShoppingApp.Web.Controllers;

[Authorize]
public class ShoppingListController : Controller
{
    private readonly IShoppingListService _shoppingListService;
    private readonly IShoppingListUserService _accessibleUserService;
    private readonly IUserService _userService;
    private readonly IShoppingListProductService _shoppingListProductService;
    private readonly IClaimService _claimService;

    public ShoppingListController(IShoppingListService shoppingListService, IShoppingListUserService accessibleUserService, IUserService userService, IShoppingListProductService shoppingListProductService, IClaimService claimService)
    {
        _shoppingListService = shoppingListService;
        _accessibleUserService = accessibleUserService;
        _userService = userService;
        _shoppingListProductService = shoppingListProductService;
        _claimService = claimService;
    }

    [Route("/")]
    [Route("/lists")]
    public async Task<IActionResult> MyLists()
    {
        List<ShoppingList> lists = await _shoppingListService.GetShoppingLists(_claimService.UserId);

        return View(lists);
    }

    public async Task<IActionResult> Share(int id)
    {
        ShoppingList list = await _shoppingListService.GetShoppingList(id);

        IEnumerable<User> users = await _shoppingListService.GetShareableUsers(list.Id);

        ViewBag.ShoppingListId = id;

        return View(users.Select(s => new UserShoppingListShareDto { Id = s.Id, FullName = $"{s.FirstName} {s.LastName}", Email = s.Email }).ToList());
    }

    [Route("/lists/{id:int}")]
    public async Task<IActionResult> View(int id)
    {
        List<ShoppingListProduct> listProducts = await _shoppingListService.GetItemsWithProduct(id);

        return View(listProducts);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int shoppingListId)
    {
        await _shoppingListService.Delete(shoppingListId);

        return RedirectToAction(nameof(MyLists));
    }

    [HttpPost]
    public async Task<IActionResult> Share(int shoppingListId, int userId)
    {
        await _shoppingListService.ShareTo(shoppingListId, userId);

        return RedirectToAction(nameof(MyLists));
    }

    [HttpPost]
    public async Task<IActionResult> Add(ShoppingListAddDto shoppingList)
    {
        if (ModelState.IsValid)
            await _shoppingListService.Create(new ShoppingList(_claimService.UserId, shoppingList.Name));

        return RedirectToAction(nameof(MyLists));
    }
}
