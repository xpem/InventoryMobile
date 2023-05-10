using Models;

namespace PersonalAssetsMobile.Services
{
    public interface ISubCategoryService
    {
        Task<List<Models.SubCategory>> GetSubCategoriesByCategoryId(int categoryId);

        Task<SubCategory> GetSubCategoryById(int id);

        Task<(bool, string)> AddSubcategory(SubCategory subCategory);

        Task<(bool, string)> AltSubCategory(SubCategory subCategory);
    }
}
