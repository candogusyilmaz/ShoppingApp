using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ShoppingApp.Infrastructure.Abstract;
using ShoppingApp.Infrastructure.Concrete.Ef;

namespace ShoppingApp.Infrastructure;
public static class DIExtensions
{
    public static IServiceCollection AddEfDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer("Server=.;Database=denemedb;Trusted_Connection=true;"));
        services.AddScoped<IUserRepository, EfUserRepository>();
        services.AddScoped<ICategoryRepository, EfCategoryRepository>();
        services.AddScoped<IProductRepository, EfProductRepository>();
        services.AddScoped<IShoppingListRepository, EfShoppingListRepository>();
        services.AddScoped<IShoppingListUserRepository, EfShoppingListUserRepository>();
        services.AddScoped<IShoppingListProductRepository, EfShoppingListProductRepository>();

        return services;
    }
}
