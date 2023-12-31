using Models;
using Models.Responses;

namespace ApiDAL
{
    public interface ICategoryApiDAL
    {
        Task<ApiResponse> AddCategory(Category category);
        Task<ApiResponse> AltCategory(Category category);
        Task<ApiResponse> DelCategory(int id);
        Task<ApiResponse> GetCategories();
        Task<ApiResponse> GetCategoriesWithSubCategories();
        Task<ApiResponse> GetCategoryById(string id);
    }
}