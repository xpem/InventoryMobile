using ApiRepos;
using Models.Responses;

namespace ApiDAL
{
    public interface IItemSituationDAL
    {
        Task<ApiResponse> GetItemSituation();
    }

    public class ItemSituationDAL(IHttpClientFunctions httpClientFunctions) : IItemSituationDAL
    {
        public async Task<ApiResponse> GetItemSituation()
            => await httpClientFunctions.AuthRequest(Models.RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/itemsituation");
    }
}
