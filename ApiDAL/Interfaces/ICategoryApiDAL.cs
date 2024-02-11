using Models;
using Models.Responses;

namespace ApiDAL.Interfaces
{
    public interface ICategoryApiDAL
    {
        Task<ApiResponse> AddCategoryAsync(Category category);
        Task<ApiResponse> AltCategoryAsync(Category category);
        Task<ApiResponse> DelCategoryAsync(int id);
        Task<ApiResponse> GetCategoriesAsync();
        Task<ApiResponse> GetCategoriesWithSubCategoriesAsync();
        Task<ApiResponse> GetCategoryByIdAsync(string id);
    }
}