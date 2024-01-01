using Models;
using Models.Responses;

namespace ApiDAL.Interfaces
{
    public interface ISubCategoryApiDAL
    {
        Task<ApiResponse> AddSubCategory(SubCategory subCategory);
        Task<ApiResponse> AltSubCategory(SubCategory subCategory);
        Task<ApiResponse> DelSubCategory(int id);
        Task<ApiResponse> GetSubCategoriesByCategoryId(string subCategoryId);
        Task<ApiResponse> GetSubCategoryById(string id);
    }
}