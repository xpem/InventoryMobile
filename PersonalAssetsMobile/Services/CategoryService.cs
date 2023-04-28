using BLL;
using Models;

namespace PersonalAssetsMobile.Services
{
    public class CategoryService : ServiceBase, ICategoryService
    {

        public async Task<List<Category>> GetCategories()
        {
            var resp = await CategoryBLL.GetCategories(Token);

            if (resp is not null && resp.Success)
                return resp.Content as List<Category>;

            return null;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var resp = await CategoryBLL.GetCategoryById(Token, id.ToString());

            if (resp.Success)
                return resp.Content as Category;

            return null;
        }

        public async Task<(bool, string)> AddCategory(Category category)
        {
            var resp = await CategoryBLL.AddCategory(Token, category);

            if (resp.Success)
            {
                return (true, "Categoria Adicionada!");
            }
            else return (false, "Ocorreu um erro ao tentar adicionar a categoria");
        }

        public async Task<(bool, string)> AltCategory(Category category)
        {
            var resp = await CategoryBLL.AltCategory(Token, category);

            if (resp.Success)
            {
                return (true, "Categoria Atualizada!");
            }
            else return (false, "Ocorreu um erro ao tentar atualizar a categoria");
        }
    }
}
