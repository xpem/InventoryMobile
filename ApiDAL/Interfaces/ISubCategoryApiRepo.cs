using Models.DTO;
using Models.Responses;

namespace ApiRepos.Interfaces
{
    public interface ISubCategoryApiRepo
    {
        Task<ApiResponse> AddSubCategory(SubCategory subCategory);

        Task<ApiResponse> AltSubCategory(SubCategory subCategory);

        Task<ApiResponse> DelSubCategory(int id);

        Task<ApiResponse> GetSubCategoriesByCategoryId(string subCategoryId);

        Task<ApiResponse> GetSubCategoryById(string id);

        Task<ApiResponse> GetByLastUpdateAsync(DateTime lastUpdate, int page);
    }
}