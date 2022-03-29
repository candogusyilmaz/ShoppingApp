using Microsoft.Extensions.Caching.Memory;

using ShoppingApp.Infrastructure.Abstract;

namespace ShoppingApp.Business.Concrete;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMemoryCache _cache;
    private const string _cacheKey = "AllCategories";

    private List<Category> _cachedCategories => _cache.Get<List<Category>>(_cacheKey);

    public CategoryService(ICategoryRepository categoryRepository, IMemoryCache cache)
    {
        _categoryRepository = categoryRepository;
        _cache = cache;
    }

    public async Task Create(Category entity)
    {
        await _categoryRepository.Add(entity);
        _cache.Remove(_cacheKey);
    }

    public async Task Update(Category entity)
    {
        await _categoryRepository.Update(entity);
        _cache.Remove(_cacheKey);
    }

    public async Task Delete(int categoryId)
    {
        Category categoryToDelete = await _categoryRepository.Get(s => s.Id == categoryId);

        if (categoryToDelete is null)
            throw new Exception("Category not found.");

        await _categoryRepository.Delete(categoryToDelete);
        _cache.Remove(_cacheKey);
    }

    public async Task<Category> GetCategoriesById(int categoryId)
    {
        List<Category> categories = _cachedCategories;

        if (categories is not null)
            return categories.FirstOrDefault(s => s.Id == categoryId);

        return await _categoryRepository.Get(s => s.Id == categoryId);
    }

    public async Task<List<Category>> GetCategories()
    {
        if (_cachedCategories is null)
            _cache.Set(_cacheKey, await _categoryRepository.GetAll(), TimeSpan.FromMinutes(5));

        return _cachedCategories;
    }
}
