using ApiRepos;
using Models;
using Models.Responses;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace ApiDAL
{
    public static class ItemDAL
    {
        public static async Task<ApiResponse> GetItems() =>
            await HttpClientFunctions.AuthRequest(RequestsTypes.Get, ApiKeys.ApiUri + "/item");

        public static async Task<ApiResponse> GetItemById(string id) =>
           await HttpClientFunctions.AuthRequest(RequestsTypes.Get, ApiKeys.ApiUri + "/item/" + id);

        public static async Task<ApiResponse> AddItem(Models.Item item)
        {
            try
            {
                string json = JsonSerializer.Serialize(new
                {
                    item.Name,
                    item.TechnicalDescription,
                    item.AcquisitionDate,
                    item.PurchaseValue,
                    item.PurchaseStore,
                    item.ResaleValue,
                    Situation = new { Id = item.Situation },
                    item.Comment,
                    item.AcquisitionType,
                    Category = new { item.Category?.Id, SubCategory = item.Category?.SubCategory is not null ? new { item.Category.SubCategory.Id } : null }
                });

                return await HttpClientFunctions.AuthRequest(Models.RequestsTypes.Post, ApiKeys.ApiUri + "/item", json);
            }
            catch (Exception ex) { throw ex; }
        }

        public static async Task<ApiResponse> AltItem(Models.Item item)
        {
            try
            {
                string json = JsonSerializer.Serialize(new
                {
                    item.Name,
                    item.TechnicalDescription,
                    item.AcquisitionDate,
                    item.PurchaseValue,
                    item.PurchaseStore,
                    item.ResaleValue,
                    Situation = new { Id = item.Situation },
                    item.Comment,
                    item.AcquisitionType,
                    Category = new { item.Category?.Id, SubCategory = item.Category?.SubCategory is not null ? new { item.Category.SubCategory.Id } : null }
                });

                return await HttpClientFunctions.AuthRequest(Models.RequestsTypes.Put, ApiKeys.ApiUri + "/item/" + item.Id, json);
            }
            catch (Exception ex) { throw ex; }
        }


    }
}
