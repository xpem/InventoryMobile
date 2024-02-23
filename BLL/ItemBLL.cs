using ApiDAL;
using BLL.Handlers;
using Models;
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
        Task<BLLResponse> GetItemsAsync();
    }

    public class ItemBLL(IItemApiDAL itemApiDAL) : IItemBLL
    {
        public async Task<BLLResponse> GetItemsAsync()
        {
            ApiResponse resp = await itemApiDAL.GetItemsAsync();
            return ApiResponseHandler.Handler<List<Models.Item>>(resp);
        }

        public async Task<BLLResponse> GetItemByIdAsync(string id)
        {
            ApiResponse resp = await itemApiDAL.GetItemByIdAsync(id);
            return ApiResponseHandler.Handler<Item>(resp);
        }

        public async Task<BLLResponse> AddItemAsync(Models.Item item)
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

        public async Task<BLLResponse> AltItemAsync(Models.Item item)
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
