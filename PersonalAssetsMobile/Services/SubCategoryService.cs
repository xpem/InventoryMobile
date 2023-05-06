using BLL;
using Models;

namespace PersonalAssetsMobile.Services
{
    public class SubCategoryService : ServiceBase, ISubCategoryService
    {
        public async Task<List<Models.SubCategory>> GetSubCategoriesByCategoryId(int categoryId)
        {
            var resp = await SubCategoryBLL.GetSubCategoriesByCategoryId(Token, categoryId);

            if (resp is not null && resp.Success && resp.Content is not null)
                return resp.Content as List<SubCategory>;

            return null;
        }

        public async Task<(bool, string)> AddSubcategory(SubCategory subCategory)
        {
            var resp = await SubCategoryBLL.AddSubCategory(Token, subCategory);

            if (resp.Success)
                return (true, "Sub Categoria Adicionada!");
            else return (false, "Ocorreu um erro ao tentar adicionar a sub categoria");
        }
    }
}
