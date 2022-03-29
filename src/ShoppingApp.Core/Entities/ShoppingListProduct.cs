namespace ShoppingApp.Core.Entities;

public class ShoppingListProduct : IEntity
{
    public int Id { get; set; }

    public string Description { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int ShoppingListId { get; set; }
    public ShoppingList ShoppingList { get; set; }

    public ShoppingListProduct()
    {

    }

    public ShoppingListProduct(int id)
    {
        Id = id;
    }

    public ShoppingListProduct(int productId, int shoppingListId, string description)
    {
        ProductId = productId;
        ShoppingListId = shoppingListId;
        Description = description;
    }

    public void UpdateDescription(string description)
    {
        Description = description;
    }
}
