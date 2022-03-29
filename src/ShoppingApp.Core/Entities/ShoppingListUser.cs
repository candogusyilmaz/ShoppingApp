using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShoppingApp.Core.Common;

namespace ShoppingApp.Core.Entities;

public class ShoppingListUser : IEntity
{
    public int Id { get; set; }

    public int ShoppingListId { get; set; }
    public ShoppingList ShoppingList { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public bool IsOwner { get; set; }

    public ShoppingListUser()
    {
    }

    public ShoppingListUser(int id)
    {
        Id = id;
    }

    public ShoppingListUser(int shoppingListId, int userId, bool isOwner)
    {
        ShoppingListId = shoppingListId;
        UserId = userId;
        IsOwner = isOwner;
    }
}
