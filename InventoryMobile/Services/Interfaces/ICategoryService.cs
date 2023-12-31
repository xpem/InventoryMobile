using Models;

namespace InventoryMobile.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();

        Task<List<Category>> GetCategoriesWithSubCategories();

        Task<Category> GetCategoryById(int id);

        Task<(bool, string)> AddCategory(Category category);

        Task<(bool, string)> AltCategory(Category category);

        Task<(bool, string)> DelCategory(int id);
    }
}
