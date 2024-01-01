using Models;
using Models.Responses;

namespace BLL
{
    public interface ICategoryBLL
    {
        Task<BLLResponse> AddCategoryAsync(Category category);
        Task<BLLResponse> AltCategoryAsync(Category category);
        Task<BLLResponse> DelCategoryAsync(int id);
        Task<BLLResponse> GetCategoriesAsync();
        Task<BLLResponse> GetCategoriesWithSubCategoriesAsync();
        Task<BLLResponse> GetCategoryByIdAsync(string id);
    }
}