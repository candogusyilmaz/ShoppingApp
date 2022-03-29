using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using ShoppingApp.Core.DataAccess;
using ShoppingApp.Core.Entities;

namespace ShoppingApp.Infrastructure.Abstract;
public interface IProductRepository : IRepository<Product>
{
}
