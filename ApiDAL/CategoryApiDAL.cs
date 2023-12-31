using ApiRepos;
using Models;
using Models.Responses;
using System.Text.Json;

namespace ApiDAL
{
    public class CategoryApiDAL(IHttpClientFunctions httpClientFunctions) : ICategoryApiDAL
    {

        public async Task<ApiResponse> GetCategories() =>
            await httpClientFunctions.AuthRequest(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/category");

        public async Task<ApiResponse> GetCategoriesWithSubCategories() =>
            await httpClientFunctions.AuthRequest(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/category/subcategory");

        public async Task<ApiResponse> GetCategoryById(string id) =>
            await httpClientFunctions.AuthRequest(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/category/" + id);

        public async Task<ApiResponse> AddCategory(Category category)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { category.Name, category.Color });

                return await httpClientFunctions.AuthRequest(RequestsTypes.Post, ApiKeys.ApiAddress + "/Inventory/category", json);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<ApiResponse> AltCategory(Category category)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { category.Name, category.Color });

                return await httpClientFunctions.AuthRequest(RequestsTypes.Put, ApiKeys.ApiAddress + "/Inventory/category/" + category.Id, json);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<ApiResponse> DelCategory(int id)
        {
            try
            {
                return await httpClientFunctions.AuthRequest(RequestsTypes.Delete, ApiKeys.ApiAddress + "/Inventory/category/" + id);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
