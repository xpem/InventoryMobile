using BLL;
using Models;

namespace PersonalAssetsMobile.Services
{
    public static class CategoryService
    {
        public static async Task<(bool, List<Category>, string)> GetCategories()
        {
            var token = await SecureStorage.Default.GetAsync("TOKEN");

            var resp = await CategoryBLL.GetCategory(token);

            if (resp is not null && resp.Success)
                return (true, resp.Content as List<Category>, null);

            return (false, null, "Ocorreu um Erro não identificado");
        }
    }
}
