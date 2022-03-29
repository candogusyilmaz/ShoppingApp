using ShoppingApp.Core.DataAccess;
using ShoppingApp.Core.Entities;

namespace ShoppingApp.Infrastructure.Abstract;

public interface IUserRepository : IRepository<User>
{
    Task<User> Get(string email, string password);
}
