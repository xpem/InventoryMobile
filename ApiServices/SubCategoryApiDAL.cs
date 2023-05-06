using ApiRepos;
using Models;
using Models.Responses;
using System.Text.Json;

namespace ApiDAL
{
    public static class SubCategoryApiDAL
    {
        public static async Task<ApiResponse> GetSubCategoriesByCategoryId(string userToken, string subCategoryId) =>
           await HttpClientFunctions.GetAsync(ApiKeys.ApiUri + "/subcategory/category/" + subCategoryId, userToken);

        public static async Task<ApiResponse> AddSubCategory(string userToken, SubCategory subCategory)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { subCategory.Name, IconName = subCategory.IconName, Category = new { Id = subCategory.CategoryId } });

                return await HttpClientFunctions.PostAsync(ApiKeys.ApiUri + "/subcategory", json, userToken);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
