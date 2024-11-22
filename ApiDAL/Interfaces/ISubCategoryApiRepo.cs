using Models;
using Models.Responses;

namespace ApiDAL.Interfaces
{
    public interface ISubCategoryApiRepo
    {
        Task<ApiResponse> AddSubCategory(Models.SubCategory subCategory);

        Task<ApiResponse> AltSubCategory(Models.SubCategory subCategory);

        Task<ApiResponse> DelSubCategory(int id);

        Task<ApiResponse> GetSubCategoriesByCategoryId(string subCategoryId);

        Task<ApiResponse> GetSubCategoryById(string id);

        Task<ApiResponse> GetByLastUpdateAsync(DateTime lastUpdate, int page);
    }
}