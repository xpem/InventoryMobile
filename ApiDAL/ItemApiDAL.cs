using ApiRepos;
using Models;
using Models.ItemModels;
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
        Task<ApiResponse> GetPaginatedItemsAsync(int page);
        Task<ApiResponse> GetTotalItensAsync();
        Task<ApiResponse> GetItemImageAsync(int id, int imageIndex);
    }

    public class ItemApiDAL(IHttpClientFunctions httpClientFunctions, IHttpClientWithFileFunctions httpClientWithFileFunctions) : IItemApiDAL
    {
        public async Task<ApiResponse> GetTotalItensAsync() => await httpClientFunctions.AuthRequestAsync(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/item/totals");

        public async Task<ApiResponse> GetPaginatedItemsAsync(int page) =>
            await httpClientFunctions.AuthRequestAsync(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/item?page=" + page);

        public async Task<ApiResponse> GetItemByIdAsync(string id) =>
           await httpClientFunctions.AuthRequestAsync(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/item/" + id);

        public async Task<ApiResponse> GetItemImageAsync(int id, int imageIndex) =>
            await httpClientWithFileFunctions.AuthRequestAsync(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/item/" + id + "/image/" + imageIndex);

        public async Task<ApiResponse> AddItemAsync(Item item)
        {
            string json = BuildItemJson(item);

            return await httpClientFunctions.AuthRequestAsync(Models.RequestsTypes.Post, ApiKeys.ApiAddress + "/Inventory/item", json);
        }

        private static string BuildItemJson(Item item) =>
            JsonSerializer.Serialize(new
            {
                item.Name,
                item.TechnicalDescription,
                AcquisitionDate = DateOnly.FromDateTime(item.AcquisitionDate),
                item.PurchaseValue,
                item.PurchaseStore,
                item.ResaleValue,
                SituationId = item.Situation?.Id,
                item.Comment,
                AcquisitionType = item.AcquisitionType?.Id,
                Category = new { CategoryId = item.Category?.Id, SubCategoryId = item.Category?.SubCategory is not null ? (int?)item.Category.SubCategory.Id : null },
                WithdrawalDate = item.WithdrawalDate != null ? (DateOnly?)DateOnly.FromDateTime(item.WithdrawalDate.Value) : null,
            });

        public async Task<ApiResponse> AltItemAsync(Item item)
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
