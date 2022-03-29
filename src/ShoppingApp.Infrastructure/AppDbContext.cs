using System.Reflection;

using Microsoft.EntityFrameworkCore;

using ShoppingApp.Core.Entities;

namespace ShoppingApp.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ShoppingList> ShoppingLists { get; set; }
    public DbSet<ShoppingListProduct> ShoppingListProducts { get; set; }
    public DbSet<ShoppingListUser> ShoppingListUsers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
