namespace ShoppingApp.Web.ViewModels;

public class ProductsViewModel
{
    public List<Product> Products { get; init; }
    public List<Category> Categories { get; init; }

    public int? SelectedCategoryId { get; set; }

    public int GetProductCount(int categoryId)
    {
        return Products.Count(p => p.CategoryId == categoryId);
    }

    public string GetProductImage(byte[] imageBytes)
    {
        if (imageBytes is null)
            return string.Empty;

        return $"data:image/png;base64, {@Convert.ToBase64String(imageBytes)}";
    }
}
