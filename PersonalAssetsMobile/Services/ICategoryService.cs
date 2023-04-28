using Models;

namespace PersonalAssetsMobile.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();

        Task<Category> GetCategoryById(int id);

        Task<(bool, string)> AddCategory(Category category);

        Task<(bool, string)> AltCategory(Category category);
    }
}
