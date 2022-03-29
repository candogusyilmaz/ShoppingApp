using ShoppingApp.Core.Entities;

namespace ShoppingApp.Business.Abstract;

public interface IProductService
{
    Task Create(Product entity);
    Task<Product> GetProduct(int productId);
    Task<List<Product>> GetProductsByCategoryId(int categoryId);
    Task<List<Product>> GetProducts();
}
