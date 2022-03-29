
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ShoppingApp.Business.Abstract;
using ShoppingApp.Business.Concrete;
using ShoppingApp.Infrastructure;

namespace ShoppingApp.Business;

public static class DIExtensions
{
    public static IServiceCollection AddBusinessDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEfDataAccess(configuration);
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IShoppingListService, ShoppingListService>();
        services.AddScoped<IShoppingListUserService, ShoppingListUserService>();
        services.AddScoped<IShoppingListProductService, ShoppingListProductService>();

        return services;
    }
}
