using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ShoppingApp.Core.Entities;
using ShoppingApp.Infrastructure;

namespace ShoppingApp.Tests.Infrastructure;

public abstract class EfRepositoryTestsBase
{
    protected readonly AppDbContext context;

    protected readonly List<Category> _categories = new()
    {
        new Category { Id = 1, Name = "Fruits"},
        new Category { Id = 2, Name = "Electronic"},
    };

    protected readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Apple", CategoryId = 1 },
        new Product { Id = 2, Name = "Orange", CategoryId = 1 },
        new Product { Id = 3, Name = "Melon", CategoryId = 1 },
        new Product { Id = 4, Name = "Monitor", CategoryId = 2 },
        new Product { Id = 5, Name = "Keyboard", CategoryId = 2 },
        new Product { Id = 6, Name = "Mouse", CategoryId = 2 },
    };

    protected readonly List<User> _users = new()
    {
        new User { Id = 1, FirstName = "John", LastName= "Doe", Email = "johndoe@gmail.com", Password = "johnDoe1", Role = "Admin"},
        new User { Id = 2, FirstName = "Keanu", LastName= "Reeves", Email = "reeves@gmail.com", Password = "reev3sKeanu", Role = "Basic"},
        new User { Id = 3, FirstName = "Man with", LastName= "No Lists", Email = "manwithNo@gmail.com", Password = "reev3sKeanu", Role = "Basic"}
    };

    protected readonly List<ShoppingList> _shoppingLists = new()
    {
        new ShoppingList { Id = 1, Name = "John's Not Empty List" },
        new ShoppingList { Id = 2, Name = "John's Empty List" },
        new ShoppingList { Id = 3, Name = "John and Keanu's Not Empty List" },
        new ShoppingList { Id = 4, Name = "John and Keanu's Empty List" },
        new ShoppingList { Id = 5, Name = "Keanu's Empty List" }
    };

    protected readonly List<ShoppingListUser> _shoppingListUsers = new()
    {
        new ShoppingListUser { Id = 1, UserId = 1, IsOwner = true, ShoppingListId = 1 },
        new ShoppingListUser { Id = 2, UserId = 1, IsOwner = true, ShoppingListId = 2 },
        new ShoppingListUser { Id = 3, UserId = 1, IsOwner = true, ShoppingListId = 3 },
        new ShoppingListUser { Id = 4, UserId = 2, IsOwner = true, ShoppingListId = 3 },
        new ShoppingListUser { Id = 5, UserId = 1, IsOwner = true, ShoppingListId = 4 },
        new ShoppingListUser { Id = 6, UserId = 2, IsOwner = true, ShoppingListId = 4 },
        new ShoppingListUser { Id = 7, UserId = 2, IsOwner = true, ShoppingListId = 5 }
    };

    protected readonly List<ShoppingListProduct> _shoppingListProducts = new()
    {
        new ShoppingListProduct { Id = 1, ProductId = 1, Description = "rnd..1", ShoppingListId = 1 },
        new ShoppingListProduct { Id = 2, ProductId = 3, Description = "rnd..2", ShoppingListId = 1 },
        new ShoppingListProduct { Id = 3, ProductId = 5, Description = "rnd..3", ShoppingListId = 1 },
        new ShoppingListProduct { Id = 4, ProductId = 1, Description = "rnd..1..", ShoppingListId = 3 },
        new ShoppingListProduct { Id = 5, ProductId = 2, Description = "rnd..2..", ShoppingListId = 3 },
        new ShoppingListProduct { Id = 6, ProductId = 4, Description = "rnd..3..", ShoppingListId = 3 },
        new ShoppingListProduct { Id = 7, ProductId = 5, Description = "rnd..4..", ShoppingListId = 3 },
    };

    public EfRepositoryTestsBase()
    {
        DbContextOptionsBuilder<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>();
        options.UseInMemoryDatabase(Guid.NewGuid().ToString());

        context = new AppDbContext(options.Options);
    }

    protected void Seed()
    {
        context.Categories.AddRange(_categories);
        context.Products.AddRange(_products);
        context.Users.AddRange(_users);
        context.ShoppingLists.AddRange(_shoppingLists);
        context.ShoppingListUsers.AddRange(_shoppingListUsers);
        context.ShoppingListProducts.AddRange(_shoppingListProducts);
        context.SaveChanges();
        context.ChangeTracker.Clear();
    }
}
