using BLL;
using Models;

namespace PersonalAssetsMobile.Services
{
    public static class CategoryService
    {
        public static async Task<List<Category>> GetCategories()
        {
            var token = await SecureStorage.Default.GetAsync("TOKEN");

            var resp = await CategoryBLL.GetCategories(token);

            if (resp is not null && resp.Success)
                return resp.Content as List<Category>;

            return null;
        }

        public static async Task<Category> GetCategoryById(int id)
        {
            var token = await SecureStorage.Default.GetAsync("TOKEN");

            var resp = await CategoryBLL.GetCategoryById(token, id.ToString());

            if (resp.Success)
                return resp.Content as Category;

            return null;
            //return (false, null, resp.ErrorMessage);
        }

        public static async Task<(bool, string)> AddCategory(Category category)
        {
            var token = await SecureStorage.Default.GetAsync("TOKEN");

            var resp = await CategoryBLL.AddCategory(token, category);

            if (resp.Success)
            {
                return (true, "Categoria Adicionada!");
            }
            else return (false, "Ocorreu um erro ao tentar adicionar a categoria");
        }
    }
}
