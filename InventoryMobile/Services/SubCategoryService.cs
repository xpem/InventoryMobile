using BLL;
using Models;
using InventoryMobile.Services.Interfaces;

namespace InventoryMobile.Services
{
    public class SubCategoryService(ISubCategoryBLL subCategoryBLL) : ISubCategoryService
    {
        public async Task<List<Models.SubCategory>> GetSubCategoriesByCategoryId(int categoryId)
        {
            var resp = await subCategoryBLL.GetSubCategoriesByCategoryId(categoryId);

            if (resp is not null && resp.Success && resp.Content is not null)
                return resp.Content as List<SubCategory>;

            return null;
        }

        public async Task<SubCategory> GetSubCategoryById(int id)
        {
            var resp = await subCategoryBLL.GetSubCategoryById(id.ToString());

            if (resp.Success)
                return resp.Content as SubCategory;

            return null;
        }

        public async Task<(bool, string)> AddSubcategory(SubCategory subCategory)
        {
            var resp = await subCategoryBLL.AddSubCategory(subCategory);

            if (resp.Success)
                return (true, "Sub Categoria Adicionada!");
            else return (false, "Ocorreu um erro ao tentar adicionar a sub categoria");
        }

        public async Task<(bool, string)> AltSubCategory(SubCategory subCategory)
        {
            var resp = await subCategoryBLL.AltSubCategory(subCategory);

            if (resp.Success)
                return (true, "Sub Categoria Atualizada!");
            else return (false, "Ocorreu um erro ao tentar alterar a sub categoria");
        }

        public async Task<(bool, string)> DelSubCategory(int id)
        {
            var resp = await subCategoryBLL.DelSubCategory(id);

            if (resp.Success)
                return (true, "Sub Categoria Excluída!");
            else return (false, "Ocorreu um erro ao tentar excluir a sub categoria");
        }
    }
}
