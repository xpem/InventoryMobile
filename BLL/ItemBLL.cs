using ApiDAL;
using BLL.Handlers;
using Models;
using Models.Responses;
using System.Text.Json.Nodes;

namespace BLL
{
    public class ItemBLL
    {

        public static async Task<BLLResponse> GetItems() =>
            ApiResponseHandler.Handler<List<Models.Item>>(await ItemDAL.GetItems());

        public static async Task<BLLResponse> GetItemById(string id) =>
            ApiResponseHandler.Handler<Item>(await ItemDAL.GetItemById(id));

        public static async Task<BLLResponse> AddItem(Models.Item item)
        {
            var resp = await ItemDAL.AddItem(item);

            if (resp is not null && resp.Success && resp.Content is not null)
            {
                var jResp = JsonNode.Parse(resp.Content);

                if (jResp is not null)
                {
                    Item itemResp = new()
                    {
                        Id = jResp["Id"]?.GetValue<int>() ?? 0,
                        Name = jResp["Name"]?.GetValue<string>(),
                        TechnicalDescription = jResp["TechnicalDescription"]?.GetValue<string>(),
                        AcquisitionDate = jResp["AcquisitionDate"]?.GetValue<DateTime>() ?? DateTime.MinValue,
                        AcquisitionType = jResp["AcquisitionType"]?.GetValue<Int32>() ?? Int32.MinValue,
                        PurchaseStore = jResp["PurchaseStore"]?.GetValue<string>(),
                        PurchaseValue = jResp["PurchaseValue"]?.GetValue<string>(),
                        ResaleValue = jResp["ResaleValue"]?.GetValue<string>(),
                        //CreatedAt = jResp["CreatedAt"]?.GetValue<DateTime>(),
                        //UpdatedAt = jResp["UpdatedAt"]?.GetValue<DateTime>(),
                        Comment = jResp["Comment"]?.GetValue<string>(),
                        Situation = jResp["Situation"]?.GetValue<Int32>(),
                    };

                    return new BLLResponse() { Success = resp.Success, Content = itemResp };
                }
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public static async Task<BLLResponse> AltItem(Models.Item item)
        {
            var resp = await ItemDAL.AltItem(item);

            if (resp is not null && resp.Success && resp.Content is not null)
            {
                JsonNode? jResp = JsonNode.Parse(resp.Content);

                if (jResp is not null)
                    return new BLLResponse() { Success = resp.Success, Content = BuildItemResponse(jResp) };
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public static async Task<BLLResponse> DelItem(int id)
        {
            var resp = await ItemDAL.DelItem(id);

            if (resp is not null && resp.Content is not null)
                return new BLLResponse() { Success = resp.Success, Content = resp.Content };

            return new BLLResponse() { Success = false, Content = null };
        }

        public static Item BuildItemResponse(JsonNode jResp) => new()
        {
            Id = jResp["Id"]?.GetValue<int>() ?? 0,
            Name = jResp["Name"]?.GetValue<string>(),
            TechnicalDescription = jResp["TechnicalDescription"]?.GetValue<string>(),
            AcquisitionDate = jResp["AcquisitionDate"]?.GetValue<DateTime>() ?? DateTime.MinValue,
            AcquisitionType = jResp["AcquisitionType"]?.GetValue<Int32>() ?? Int32.MinValue,
            PurchaseStore = jResp["PurchaseStore"]?.GetValue<string>(),
            PurchaseValue = jResp["PurchaseValue"]?.GetValue<string>(),
            ResaleValue = jResp["ResaleValue"]?.GetValue<string>(),
            //CreatedAt = jResp["CreatedAt"]?.GetValue<DateTime>(),
            //UpdatedAt = jResp["UpdatedAt"]?.GetValue<DateTime>(),
            Comment = jResp["Comment"]?.GetValue<string>(),
            Situation = jResp["Situation"]?.GetValue<Int32>()
        };
    }
}
