using ApiRepos;
using Models;
using Models.Responses;
using System.Text.Json;

namespace ApiDAL
{
    public static class CategoryApiDAL
    {
        public static async Task<ApiResponse> GetCategories(string userToken) =>
            await HttpClientFunctions.GetAsync(ApiKeys.ApiUri + "/category", userToken);

        public static async Task<ApiResponse> GetCategoryById(string userToken, string id) =>
            await HttpClientFunctions.GetAsync(ApiKeys.ApiUri + "/category/" + id, userToken);

        public static async Task<ApiResponse> AddCategory(string userToken, Category category)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { category.Name, category.Color });

                return await HttpClientFunctions.PostAsync(ApiKeys.ApiUri + "/category", json, userToken);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
