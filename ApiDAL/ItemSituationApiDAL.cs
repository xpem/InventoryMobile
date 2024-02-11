using ApiRepos;
using Models.Responses;

namespace ApiDAL
{
    public interface IItemSituationApiDAL
    {
        Task<ApiResponse> GetItemSituation();
    }

    public class ItemSituationApiDAL(IHttpClientFunctions httpClientFunctions) : IItemSituationApiDAL
    {
        public async Task<ApiResponse> GetItemSituation()
            => await httpClientFunctions.AuthRequestAsync(Models.RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/itemsituation");
    }
}
