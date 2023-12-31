using Models;
using Models.Responses;

namespace BLL
{
    public interface ICategoryBLL
    {
        Task<BLLResponse> AddCategory(Category category);
        Task<BLLResponse> AltCategory(Category category);
        Task<BLLResponse> DelCategory(int id);
        Task<BLLResponse> GetCategories();
        Task<BLLResponse> GetCategoriesWithSubCategories();
        Task<BLLResponse> GetCategoryById(string id);
    }
}