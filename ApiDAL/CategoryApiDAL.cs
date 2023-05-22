using ApiRepos;
using Models;
using Models.Responses;
using System.Text.Json;

namespace ApiDAL
{
    public static class CategoryApiDAL
    {

        public static async Task<ApiResponse> GetCategories() =>
            await HttpClientFunctions.AuthRequest(RequestsTypes.Get, ApiKeys.ApiUri + "/category");

        public static async Task<ApiResponse> GetCategoryById(string id) =>
            await HttpClientFunctions.AuthRequest(RequestsTypes.Get, ApiKeys.ApiUri + "/category/" + id);

        public static async Task<ApiResponse> AddCategory(Category category)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { category.Name, category.Color });

                return await HttpClientFunctions.AuthRequest(RequestsTypes.Post, ApiKeys.ApiUri + "/category", json);
            }
            catch (Exception ex) { throw ex; }
        }

        public static async Task<ApiResponse> AltCategory(Category category)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { category.Name, category.Color });

                return await HttpClientFunctions.AuthRequest(RequestsTypes.Put, ApiKeys.ApiUri + "/category/" + category.Id, json);
            }
            catch (Exception ex) { throw ex; }
        }

        public static async Task<ApiResponse> DelCategory(int id)
        {
            try
            {
                return await HttpClientFunctions.AuthRequest(RequestsTypes.Delete, ApiKeys.ApiUri + "/category/" + id);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
