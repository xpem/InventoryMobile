using ApiRepos;
using Models;
using Models.Responses;
using System.Text.Json;

namespace ApiDAL
{
    public class SubCategoryApiDAL(IHttpClientFunctions httpClientFunctions)
    {
        public async Task<ApiResponse> GetSubCategoriesByCategoryId(string subCategoryId) =>
            await httpClientFunctions.AuthRequest(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/subcategory/category/" + subCategoryId);

        public async Task<ApiResponse> GetSubCategoryById(string id) =>
            await httpClientFunctions.AuthRequest(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/subcategory/" + id);

        public async Task<ApiResponse> AltSubCategory(SubCategory subCategory)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { subCategory.Name, subCategory.IconName, });

                return await httpClientFunctions.AuthRequest(RequestsTypes.Put, ApiKeys.ApiAddress + "/Inventory/subcategory/" + subCategory.Id, json);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<ApiResponse> AddSubCategory(SubCategory subCategory)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { subCategory.Name, subCategory.IconName, subCategory.CategoryId });

                return await httpClientFunctions.AuthRequest(RequestsTypes.Post, ApiKeys.ApiAddress + "/Inventory/subcategory", json);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<ApiResponse> DelSubCategory(int id) => await httpClientFunctions.AuthRequest(RequestsTypes.Delete, ApiKeys.ApiAddress + "/Inventory/subCategory/" + id);
    }
}
