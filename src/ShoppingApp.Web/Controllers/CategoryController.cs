using Microsoft.AspNetCore.Authorization;

using ShoppingApp.Business.Abstract;
using ShoppingApp.Core.Enums;

namespace ShoppingApp.Web.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.CategoryList = (await _categoryService.GetCategories()).Select(s => new CategoryDto { Id = s.Id, Name = s.Name }).ToList();
        ViewBag.SelectedCategory = new CategoryDto();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Save(CategoryDto category)
    {
        if (category.Id == -1)
            await _categoryService.Create(new Category { Name = category.Name });
        else
            await _categoryService.Update(new Category { Id = category.Id, Name = category.Name });

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _categoryService.GetCategoriesById(id);

        ViewBag.SelectedCategory = new CategoryDto { Id = item.Id, Name = item.Name };
        ViewBag.CategoryList = (await _categoryService.GetCategories()).Select(s => new CategoryDto { Id = s.Id, Name = s.Name }).ToList();

        return View(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.Delete(id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<JsonResult> GetAll()
    {
        return new JsonResult(await _categoryService.GetCategories());
    }
}
