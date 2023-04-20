using ApiRepos;
using Models;

namespace ApiDAL
{
    public static class CategoryApiDAL
    {
        public static async Task<Response> GetCategories(string userToken) =>
            await HttpClientFunctions.GetAsync(ApiKeys.ApiUri + "/category", userToken);
    }
}
