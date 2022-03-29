using Microsoft.EntityFrameworkCore;

using ShoppingApp.Core.DataAccess.Ef;
using ShoppingApp.Core.Entities;
using ShoppingApp.Infrastructure.Abstract;

namespace ShoppingApp.Infrastructure.Concrete.Ef;

public class EfUserRepository : EfRepository<User, AppDbContext>, IUserRepository
{
    public EfUserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public new async Task Add(User user)
    {
        //if (_dbSet.Any(s => s.Email == user.Email))
        //    throw new ArgumentException("A user with the same email already exists.");

        await base.Add(user);
    }

    public async Task<User> Get(string email, string password)
    {
        return await _dbSet.SingleOrDefaultAsync(s => s.Email == email && s.Password == password);
    }
}
