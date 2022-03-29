using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using ShoppingApp.Core.Common;
using ShoppingApp.Core.DataAccess;

namespace ShoppingApp.Core.DataAccess.Ef;

public class EfRepository<TEntity, TContext> : IRepository<TEntity> where TEntity : class, IEntity where TContext : DbContext
{
    protected readonly TContext _dbContext;
    protected DbSet<TEntity> _dbSet;

    public EfRepository(TContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task Add(TEntity entity)
    {
        _dbSet.Add(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(TEntity entity)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TEntity> Get(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.SingleOrDefaultAsync(expression);
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.Where(expression).ToListAsync();
    }
}
