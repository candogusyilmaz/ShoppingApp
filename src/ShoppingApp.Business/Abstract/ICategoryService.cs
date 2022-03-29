using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShoppingApp.Core.Entities;

namespace ShoppingApp.Business.Abstract;

public interface ICategoryService
{
    Task Create(Category entity);
    Task Delete(int categoryId);
    Task<Category> GetCategoriesById(int categoryId);
    Task<List<Category>> GetCategories();
    Task Update(Category entity);
}
