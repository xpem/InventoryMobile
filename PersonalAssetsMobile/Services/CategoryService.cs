using BLL;
using Models;
using PersonalAssetsMobile.Services.Interfaces;

namespace PersonalAssetsMobile.Services
{
    public class CategoryService : ICategoryService
    {

        public async Task<List<Category>> GetCategories()
        {
            var resp = await CategoryBLL.GetCategories();

            if (resp is not null && resp.Success)
                return resp.Content as List<Category>;

            return null;
        }

        public async Task<List<Category>> GetCategoriesWithSubCategories()
        {
            var resp = await CategoryBLL.GetCategoriesWithSubCategories();

            if (resp is not null && resp.Success)
                return resp.Content as List<Category>;

            return null;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var resp = await CategoryBLL.GetCategoryById(id.ToString());

            if (resp.Success)
                return resp.Content as Category;

            return null;
        }

        public async Task<(bool, string)> AddCategory(Category category)
        {
            var resp = await CategoryBLL.AddCategory(category);

            if (resp.Success)
                return (true, "Categoria Adicionada!");
            else return (false, "Ocorreu um erro ao tentar adicionar a categoria");
        }

        public async Task<(bool, string)> AltCategory(Category category)
        {
            var resp = await CategoryBLL.AltCategory(category);

            if (resp.Success)
                return (true, "Categoria Atualizada!");
            else return (false, "Ocorreu um erro ao tentar atualizar a categoria");
        }

        public async Task<(bool, string)> DelCategory(int id)
        {
            var resp = await CategoryBLL.DelCategory(id);

            if (resp.Success)
                return (true, "Categoria Excluída!");
            else return (false, "Ocorreu um erro ao tentar excluir a categoria");
        }
    }
}
