using Microsoft.Extensions.Caching.Memory;

namespace ShoppingApp.Business.Concrete;

internal class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMemoryCache _cache;

    private List<Product> CachedProducts => _cache.Get<List<Product>>("products");

    public ProductService(IProductRepository productRepository, IMemoryCache cache)
    {
        _productRepository = productRepository;
        _cache = cache;
    }

    public async Task Create(Product entity)
    {
        await _productRepository.Add(entity);
    }

    public async Task<Product> GetProduct(int productId)
    {
        List<Product> products = CachedProducts;

        if (products is not null)
            return products.FirstOrDefault(s => s.Id == productId);

        return await _productRepository.Get(s => s.Id == productId);
    }

    public async Task<List<Product>> GetProducts()
    {
        if (CachedProducts is null)
            _cache.Set("products", await _productRepository.GetAll(), TimeSpan.FromMinutes(5));

        return CachedProducts;
    }

    public async Task<List<Product>> GetProductsByCategoryId(int categoryId)
    {
        return await _productRepository.GetAll(s => s.CategoryId == categoryId);
    }
}
