using inventory_api.Models;

using Microsoft.AspNetCore.Mvc;

namespace inventory_api.Services.CategoryService
{
    public interface ICategoryService
    {

        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategory(int id, Category category);
        Task DeleteCategory(int id);

    }
}
