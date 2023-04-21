using ApiRepos;
using Models;
using Models.Responses;

namespace ApiDAL
{
    public static class SubCategoryApiDAL
    {
        public static async Task<ApiResponse> GetSubCategoriesByCategoryId(string userToken, string categoryId) =>
           await HttpClientFunctions.GetAsync(ApiKeys.ApiUri + "/subcategory/category/" + categoryId, userToken);

        //public static async Task<ApiResponse> GetSubCategoryById(string userToken, string id) =>
        //    await HttpClientFunctions.GetAsync(ApiKeys.ApiUri + "/category/" + id, userToken);
    }
}
