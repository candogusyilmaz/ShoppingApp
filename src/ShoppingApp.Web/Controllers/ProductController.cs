using Microsoft.AspNetCore.Authorization;

using ShoppingApp.Web.ViewModels;

namespace ShoppingApp.Web.Controllers;

[Authorize]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IShoppingListService _shoppingListService;
    private readonly IShoppingListProductService _shoppingListProductService;
    private readonly IClaimService _claimService;

    public ProductController(IProductService productService, ICategoryService categoryService, IShoppingListService shoppingListService, IShoppingListProductService shoppingListProductService, IClaimService claimService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _shoppingListService = shoppingListService;
        _shoppingListProductService = shoppingListProductService;
        _claimService = claimService;
    }

    //[Route("/products")]
    //public async Task<IActionResult> Index(int? categoryId)
    //{
    //    product

    //    if (categoryId.HasValue)
    //        products = await _productService.GetProductsByCategoryId(categoryId.Value);
    //    else
    //        products = await _productService.GetProducts();

    //    return View("Products");
    //}

    [Route("/products")]
    public async Task<IActionResult> Products(int? categoryId)
    {
        List<Product> products = await _productService.GetProducts();
        List<Category> categories = await _categoryService.GetCategories();

        ProductsViewModel vm = new()
        {
            SelectedCategoryId = categoryId,
            Products = products,
            Categories = categories
        };

        return View("Products", vm);
    }

    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> Edit(int id)
    {
        Product product = await _productService.GetProduct(id);
        ViewBag.Categories = (await _categoryService.GetCategories()).ToList();

        return View(new ProductUpdateDto { Id = product.Id, Image = null, Name = product.Name, CategoryId = product.CategoryId });
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Edit(ProductUpdateDto product)
    {
        await Task.CompletedTask;
        // TODO: image thingy
        //var origin = await _productService.Get(s => s.Id == product.Id);

        //if(product.Image is null)
        //{
        //    origin.Name = product.Name;
        //    origin.CategoryId = product.CategoryId;

        //    await _productService.Update(new Product { Id = product.Id, Image = origin.Image, Name = origin.Name, CategoryId = origin.CategoryId });
        //}
        //else
        //{
        //    using var fileStream = product.Image.OpenReadStream();
        //    byte[] imageBuffer = new byte[fileStream.Length];
        //    fileStream.Read(imageBuffer, 0, imageBuffer.Length);

        //    await _productService.Update(new Product
        //    {
        //        Id = product.Id,
        //        Name = product.Name,
        //        Image = imageBuffer,
        //        CategoryId = product.CategoryId,
        //    });
        //}

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> AddToList(int id)
    {
        ViewBag.ShoppingLists = await _shoppingListService.GetShoppingLists(_claimService.UserId);

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddToList(ShoppingListProductAddDto shoppingListProduct)
    {
        await _shoppingListService.AddProductToList(new ShoppingListProduct(shoppingListProduct.ProductId, shoppingListProduct.ShoppingListId, shoppingListProduct.Description));

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _categoryService.GetCategories();

        return View();
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDto product)
    {
        if (ModelState.IsValid is false)
            return RedirectToAction(nameof(Create));

        using Stream fileStream = product.Image.OpenReadStream();
        byte[] imageBuffer = new byte[fileStream.Length];
        fileStream.Read(imageBuffer, 0, imageBuffer.Length);

        await _productService.Create(new Product
        {
            Name = product.Name,
            Image = imageBuffer,
            CategoryId = product.CategoryId,
        });

        return RedirectToAction(nameof(Index));
    }
}
