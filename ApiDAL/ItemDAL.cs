using ApiRepos;
using Models.Responses;
using System.Text.Json;

namespace ApiDAL
{
    public static class ItemDAL
    {
        public static async Task<ApiResponse> AddItem(Models.Item item)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { item.Name, item.TechnicalDescription, item.AcquisitionDate, item.PurchaseValue, item.PurchaseStore, item.ResaleValue, item.Situation, item.Comment, item.AcquisitionType });

                return await HttpClientFunctions.AuthRequest(Models.RequestsTypes.Post, ApiKeys.ApiUri + "/item", json);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
