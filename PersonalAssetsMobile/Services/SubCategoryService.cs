using BLL;
using Models;
using PersonalAssetsMobile.Services.Interfaces;

namespace PersonalAssetsMobile.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        public async Task<List<Models.SubCategory>> GetSubCategoriesByCategoryId(int categoryId)
        {
            var resp = await SubCategoryBLL.GetSubCategoriesByCategoryId(categoryId);

            if (resp is not null && resp.Success && resp.Content is not null)
                return resp.Content as List<SubCategory>;

            return null;
        }

        public async Task<SubCategory> GetSubCategoryById(int id)
        {
            var resp = await SubCategoryBLL.GetSubCategoryById(id.ToString());

            if (resp.Success)
                return resp.Content as SubCategory;

            return null;
        }

        public async Task<(bool, string)> AddSubcategory(SubCategory subCategory)
        {
            var resp = await SubCategoryBLL.AddSubCategory(subCategory);

            if (resp.Success)
                return (true, "Sub Categoria Adicionada!");
            else return (false, "Ocorreu um erro ao tentar adicionar a sub categoria");
        }

        public async Task<(bool, string)> AltSubCategory(SubCategory subCategory)
        {
            var resp = await SubCategoryBLL.AltSubCategory(subCategory);

            if (resp.Success)
                return (true, "Sub Categoria Atualizada!");
            else return (false, "Ocorreu um erro ao tentar alterar a sub categoria");
        }

        public async Task<(bool, string)> DelSubCategory(int id)
        {
            var resp = await SubCategoryBLL.DelSubCategory(id);

            if (resp.Success)
                return (true, "Sub Categoria Excluída!");
            else return (false, "Ocorreu um erro ao tentar excluir a sub categoria");
        }
    }
}
