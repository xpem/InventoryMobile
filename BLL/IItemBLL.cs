using Models;
using Models.Responses;
using System.Text.Json.Nodes;

namespace BLL
{
    public interface IItemBLL
    {
        Task<BLLResponse> AddItem(Item item);
        Task<BLLResponse> AltItem(Item item);
        Item BuildItemResponse(JsonNode jResp);
        Task<BLLResponse> DelItem(int id);
        Task<BLLResponse> GetItemByIdAsync(string id);
        Task<BLLResponse> GetItemsAsync();
    }
}