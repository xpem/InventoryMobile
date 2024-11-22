using Models;
using Models.Responses;

namespace Services.Interface
{
    public interface ICategoryBLL
    {
        Task<ServResp> AddCategoryAsync(Category category);
        Task<ServResp> AltCategoryAsync(Category category);
        Task<ServResp> DelCategoryAsync(int id);
        Task<ServResp> GetCategoriesAsync();
        Task<ServResp> GetCategoriesWithSubCategoriesAsync();
        Task<ServResp> GetCategoryByIdAsync(string id);
    }
}