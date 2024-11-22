using Models;
using Models.Responses;

namespace Services.Interface
{
    public interface ISubCategoryBLL
    {
        Task<ServResp> InsertSubCategory(SubCategory subCategory);
        Task<ServResp> AltSubCategory(SubCategory subCategory);
        Task<ServResp> DelSubCategory(int id);
        Task<ServResp> GetSubCategoriesByCategoryId(int categoryId);
        Task<ServResp> GetSubCategoryById(string id);
    }
}