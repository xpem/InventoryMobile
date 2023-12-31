using BLL;
using Models;
using InventoryMobile.Services.Interfaces;
using Models.Responses;

namespace InventoryMobile.Services
{
    public class CategoryService(ICategoryBLL categoryBLL) : ICategoryService
    {

        public async Task<List<Category>> GetCategories()
        {
            BLLResponse resp = await categoryBLL.GetCategories();

            if (resp is not null && resp.Success)
                return resp.Content as List<Category>;

            return null;
        }

        public async Task<List<Category>> GetCategoriesWithSubCategories()
        {
            BLLResponse resp = await categoryBLL.GetCategoriesWithSubCategories();

            if (resp is not null && resp.Success)
                return resp.Content as List<Category>;

            return null;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            BLLResponse resp = await categoryBLL.GetCategoryById(id.ToString());

            if (resp.Success)
                return resp.Content as Category;

            return null;
        }

        public async Task<(bool, string)> AddCategory(Category category)
        {
            BLLResponse resp = await categoryBLL.AddCategory(category);

            if (resp.Success)
                return (true, "Categoria Adicionada!");
            else return (false, "Ocorreu um erro ao tentar adicionar a categoria");
        }

        public async Task<(bool, string)> AltCategory(Category category)
        {
            BLLResponse resp = await categoryBLL.AltCategory(category);

            if (resp.Success)
                return (true, "Categoria Atualizada!");
            else return (false, "Ocorreu um erro ao tentar atualizar a categoria");
        }

        public async Task<(bool, string)> DelCategory(int id)
        {
            BLLResponse resp = await categoryBLL.DelCategory(id);

            if (resp.Success)
                return (true, "Categoria Excluída!");
            else return (false, "Ocorreu um erro ao tentar excluir a categoria");
        }
    }
}
