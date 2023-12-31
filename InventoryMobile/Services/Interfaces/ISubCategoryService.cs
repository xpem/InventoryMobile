using Models;

namespace InventoryMobile.Services.Interfaces
{
    public interface ISubCategoryService
    {
        Task<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId);

        Task<SubCategory> GetSubCategoryById(int id);

        Task<(bool, string)> AddSubcategory(SubCategory subCategory);

        Task<(bool, string)> AltSubCategory(SubCategory subCategory);

        Task<(bool, string)> DelSubCategory(int id);
    }
}
