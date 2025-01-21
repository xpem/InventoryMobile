using ApiDAL;
using ApiRepos.Interfaces;
using Models;
using Models.DTO;
using Models.Responses;
using System.Text.Json;

namespace ApiRepos
{
    public class CategoryApiDAL(IHttpClientFunctions httpClientFunctions) : ICategoryApiDAL
    {
        public async Task<ApiResponse> GetCategoriesAsync() =>
            await httpClientFunctions.AuthRequestAsync(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/category");

        public async Task<ApiResponse> GetCategoriesWithSubCategoriesAsync() =>
            await httpClientFunctions.AuthRequestAsync(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/category/subcategory");

        public async Task<ApiResponse> GetCategoryByIdAsync(string id) =>
            await httpClientFunctions.AuthRequestAsync(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/category/" + id);

        public async Task<ApiResponse> AddCategoryAsync(Category category)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { category.Name, category.Color });

                return await httpClientFunctions.AuthRequestAsync(RequestsTypes.Post, ApiKeys.ApiAddress + "/Inventory/category", json);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<ApiResponse> AltCategoryAsync(Category category)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { category.Name, category.Color });

                return await httpClientFunctions.AuthRequestAsync(RequestsTypes.Put, ApiKeys.ApiAddress + "/Inventory/category/" + category.Id, json);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<ApiResponse> DelCategoryAsync(int id)
        {
            try
            {
                return await httpClientFunctions.AuthRequestAsync(RequestsTypes.Delete, ApiKeys.ApiAddress + "/Inventory/category/" + id);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
