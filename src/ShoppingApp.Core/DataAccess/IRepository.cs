using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using ShoppingApp.Core.Common;

namespace ShoppingApp.Core.DataAccess;

public interface IRepository<T> where T : IEntity
{
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task<T> Get(Expression<Func<T, bool>> expression);
    Task<List<T>> GetAll();
    Task<List<T>> GetAll(Expression<Func<T, bool>> expression);
}
