using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShoppingApp.Core.Common;

namespace ShoppingApp.Core.Entities;

public class ShoppingList : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActiveShopping { get; set; }

    public int? ActiveShoppingUserId { get; set; }
    public User ActiveShoppingUser { get; set; }

    public ICollection<ShoppingListUser> ShoppingListUsers { get; set; } = new List<ShoppingListUser>();
    public ICollection<ShoppingListProduct> ShoppingListProducts { get; set; } = new List<ShoppingListProduct>();

    public ShoppingList()
    {

    }

    public ShoppingList(int id)
    {
        Id = id;
    }

    public ShoppingList(int ownerId, string name)
    {
        Name = name;
        ShoppingListUsers.Add(new ShoppingListUser(Id, ownerId, true));
    }
}
