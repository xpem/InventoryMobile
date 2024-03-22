using ApiDAL;
using BLL.Handlers;
using Models;
using Models.ItemModels;
using Models.Responses;
using System.Text.Json.Nodes;

namespace BLL
{
    public interface IItemBLL
    {
        Task<BLLResponse> AddItemAsync(Item item);
        Task<BLLResponse> AltItemAsync(Item item);
        Task<BLLResponse> DelItemAsync(int id);
        Task<BLLResponse> GetItemByIdAsync(string id);
        Task<List<Item>> GetItemsAllAsync();
    }

    public class ItemBLL(IItemApiDAL itemApiDAL) : IItemBLL
    {

        public async Task<List<Item>> GetItemsAllAsync()
        {
            ApiResponse totalsResp = await itemApiDAL.GetTotalItensAsync();
            List<Item> items = [];

            var itemTotalsBLLResponse = ApiResponseHandler.Handler<ItemTotals>(totalsResp);

            if (itemTotalsBLLResponse.Success)
            {
                ItemTotals? itemTotals = itemTotalsBLLResponse.Content as ItemTotals;

                for (int i = 1; i <= itemTotals?.TotalPages; i++)
                {
                    ApiResponse resp = await itemApiDAL.GetPaginatedItemsAsync(i);
                    var paginatedItemsBLLResponse = ApiResponseHandler.Handler<List<Item>>(resp);

                    if (paginatedItemsBLLResponse.Success)
                        if (paginatedItemsBLLResponse.Content is List<Item> pageItems)
                            items.AddRange(pageItems);
                }

                return items;
            }
            else throw new Exception("totalsResp success false, error:" + itemTotalsBLLResponse.Error);
        }

        public async Task<BLLResponse> GetItemByIdAsync(string id)
        {
            ApiResponse resp = await itemApiDAL.GetItemByIdAsync(id);
            return ApiResponseHandler.Handler<Item>(resp);
        }

        public async Task<BLLResponse> AddItemAsync(Item item)
        {
            ApiResponse? resp = await itemApiDAL.AddItemAsync(item);

            if (resp is not null && resp.Success && resp.Content is not null)
            {
                JsonNode? jResp = JsonNode.Parse(resp.Content);

                if (jResp is not null)
                    return new BLLResponse() { Success = resp.Success, Content = null };
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public async Task<BLLResponse> AltItemAsync(Item item)
        {
            ApiResponse? resp = await itemApiDAL.AltItemAsync(item);

            if (resp is not null && resp.Success && resp.Content is not null)
            {
                JsonNode? jResp = JsonNode.Parse(resp.Content);

                if (jResp is not null)
                    return new BLLResponse() { Success = resp.Success, Content = null };
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public async Task<BLLResponse> DelItemAsync(int id)
        {
            ApiResponse? resp = await itemApiDAL.DelItemAsync(id);

            if (resp is not null && !resp.Success && !string.IsNullOrEmpty(resp.Content))
                return new BLLResponse() { Success = false, Content = resp.Content };

            return new BLLResponse() { Success = true, Content = null };
        }

    }
}
