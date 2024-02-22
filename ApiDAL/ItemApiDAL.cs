using ApiRepos;
using Models;
using Models.Responses;
using System.Text.Json;

namespace ApiDAL
{
    public interface IItemApiDAL
    {
        Task<ApiResponse> AddItemAsync(Item item);
        Task<ApiResponse> AltItemAsync(Item item);
        Task<ApiResponse> DelItemAsync(int id);
        Task<ApiResponse> GetItemByIdAsync(string id);
        Task<ApiResponse> GetItemsAsync();
    }

    public class ItemApiDAL(IHttpClientFunctions httpClientFunctions) : IItemApiDAL
    {
        public async Task<ApiResponse> GetItemsAsync() =>
            await httpClientFunctions.AuthRequestAsync(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/item");

        public async Task<ApiResponse> GetItemByIdAsync(string id) =>
           await httpClientFunctions.AuthRequestAsync(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/item/" + id);

        public async Task<ApiResponse> AddItemAsync(Models.Item item)
        {
            string json = BuildItemJson(item);

            return await httpClientFunctions.AuthRequestAsync(Models.RequestsTypes.Post, ApiKeys.ApiAddress + "/Inventory/item", json);
        }

        private static string BuildItemJson(Models.Item item) =>
            JsonSerializer.Serialize(new
            {
                item.Name,
                item.TechnicalDescription,
                item.AcquisitionDate,
                item.PurchaseValue,
                item.PurchaseStore,
                item.ResaleValue,
                SituationId = item.Situation?.Id,
                item.Comment,
                item.AcquisitionType,
                Category = new { CategoryId = item.Category?.Id, SubCategoryId = item.Category?.SubCategory is not null ? (int?)item.Category.SubCategory.Id : null }
            });

        public async Task<ApiResponse> AltItemAsync(Models.Item item)
        {
            try
            {
                string json = BuildItemJson(item);

                return await httpClientFunctions.AuthRequestAsync(Models.RequestsTypes.Put, ApiKeys.ApiAddress + "/Inventory/item/" + item.Id, json);
            }
            catch (Exception ex) { throw; }
        }

        public async Task<ApiResponse> DelItemAsync(int id)
        {
            try
            {
                return await httpClientFunctions.AuthRequestAsync(RequestsTypes.Delete, ApiKeys.ApiAddress + "/Inventory/item/" + id);
            }
            catch (Exception ex) { throw; }
        }

    }
}
