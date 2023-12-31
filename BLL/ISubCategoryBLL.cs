using Models;
using Models.Responses;

namespace BLL
{
    public interface ISubCategoryBLL
    {
        Task<BLLResponse> AddSubCategory(SubCategory subCategory);
        Task<BLLResponse> AltSubCategory(SubCategory subCategory);
        Task<BLLResponse> DelSubCategory(int id);
        Task<BLLResponse> GetSubCategoriesByCategoryId(int categoryId);
        Task<BLLResponse> GetSubCategoryById(string id);
    }
}