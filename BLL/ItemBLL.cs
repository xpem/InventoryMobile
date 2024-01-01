using ApiDAL;
using BLL.Handlers;
using Models;
using Models.Responses;
using System.Text.Json.Nodes;

namespace BLL
{
    public class ItemBLL(IItemApiDAL itemDAL) : IItemBLL
    {

        public async Task<BLLResponse> GetItems() => ApiResponseHandler.Handler<List<Models.Item>>(await itemDAL.GetItems());

        public async Task<BLLResponse> GetItemById(string id) =>
            ApiResponseHandler.Handler<Item>(await itemDAL.GetItemById(id));

        public async Task<BLLResponse> AddItem(Models.Item item)
        {
            var resp = await itemDAL.AddItem(item);

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
                        PurchaseValue = jResp["PurchaseValue"]?.GetValue<decimal>(),
                        ResaleValue = jResp["ResaleValue"]?.GetValue<decimal>(),
                        //CreatedAt = jResp["CreatedAt"]?.GetValue<DateTime>(),
                        //UpdatedAt = jResp["UpdatedAt"]?.GetValue<DateTime>(),
                        Comment = jResp["Comment"]?.GetValue<string>(),
                        Situation = new ItemSituation() { Id = jResp["Situation"]?.GetValue<int>() ?? 0 },
                    };

                    return new BLLResponse() { Success = resp.Success, Content = itemResp };
                }
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public async Task<BLLResponse> AltItem(Models.Item item)
        {
            var resp = await itemDAL.AltItem(item);

            if (resp is not null && resp.Success && resp.Content is not null)
            {
                JsonNode? jResp = JsonNode.Parse(resp.Content);

                if (jResp is not null)
                    return new BLLResponse() { Success = resp.Success, Content = BuildItemResponse(jResp) };
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public async Task<BLLResponse> DelItem(int id)
        {
            var resp = await itemDAL.DelItem(id);

            if (resp is not null && resp.Content is not null)
                return new BLLResponse() { Success = resp.Success, Content = resp.Content };

            return new BLLResponse() { Success = false, Content = null };
        }

        public Item BuildItemResponse(JsonNode jResp) => new()
        {
            Id = jResp["Id"]?.GetValue<int>() ?? 0,
            Name = jResp["Name"]?.GetValue<string>(),
            TechnicalDescription = jResp["TechnicalDescription"]?.GetValue<string>(),
            AcquisitionDate = jResp["AcquisitionDate"]?.GetValue<DateTime>() ?? DateTime.MinValue,
            AcquisitionType = jResp["AcquisitionType"]?.GetValue<Int32>() ?? Int32.MinValue,
            PurchaseStore = jResp["PurchaseStore"]?.GetValue<string>(),
            PurchaseValue = jResp["PurchaseValue"]?.GetValue<decimal>(),
            ResaleValue = jResp["ResaleValue"]?.GetValue<decimal>(),
            //CreatedAt = jResp["CreatedAt"]?.GetValue<DateTime>(),
            //UpdatedAt = jResp["UpdatedAt"]?.GetValue<DateTime>(),
            Comment = jResp["Comment"]?.GetValue<string>(),
            Situation = new ItemSituation() { Id = jResp["Situation"]?.GetValue<int>() ?? 0 }
        };
    }
}
