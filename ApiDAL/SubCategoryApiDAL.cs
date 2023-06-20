using ApiRepos;
using Models;
using Models.Responses;
using System.Text.Json;

namespace ApiDAL
{
    public static class SubCategoryApiDAL
    {
        public static async Task<ApiResponse> GetSubCategoriesByCategoryId(string subCategoryId) =>
            await HttpClientFunctions.AuthRequest(RequestsTypes.Get, ApiKeys.ApiUri + "/subcategory/category/" + subCategoryId);

        public static async Task<ApiResponse> GetSubCategoryById(string id) =>
            await HttpClientFunctions.AuthRequest(RequestsTypes.Get, ApiKeys.ApiUri + "/subcategory/" + id);

        public static async Task<ApiResponse> AltSubCategory(SubCategory subCategory)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { subCategory.Name, subCategory.IconName, });

                return await HttpClientFunctions.AuthRequest(RequestsTypes.Put, ApiKeys.ApiUri + "/subcategory/" + subCategory.Id, json);
            }
            catch (Exception ex) { throw ex; }
        }

        public static async Task<ApiResponse> AddSubCategory(SubCategory subCategory)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { subCategory.Name, subCategory.IconName, subCategory.CategoryId });

                return await HttpClientFunctions.AuthRequest(RequestsTypes.Post, ApiKeys.ApiUri + "/subcategory", json);
            }
            catch (Exception ex) { throw ex; }
        }

        public static async Task<ApiResponse> DelSubCategory(int id)
        {
            try
            {
                return await HttpClientFunctions.AuthRequest(RequestsTypes.Delete, ApiKeys.ApiUri + "/subCategory/" + id);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
