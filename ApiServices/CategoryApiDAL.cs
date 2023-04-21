using ApiRepos;
using Models;
using Models.Responses;

namespace ApiDAL
{
    public static class CategoryApiDAL
    {
        public static async Task<ApiResponse> GetCategories(string userToken) =>
            await HttpClientFunctions.GetAsync(ApiKeys.ApiUri + "/category", userToken);

        public static async Task<ApiResponse> GetCategoryById(string userToken, string id) =>
            await HttpClientFunctions.GetAsync(ApiKeys.ApiUri + "/category/" + id, userToken);
    }
}
