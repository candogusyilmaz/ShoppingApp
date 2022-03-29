using ShoppingApp.Core.Entities;

namespace ShoppingApp.Business.Abstract;

public interface IUserService
{
    Task Create(User entity);
    Task<User> ValidateLogin(string email, string password);
    Task<List<User>> GetAll();
}
