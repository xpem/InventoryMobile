using ApiDAL.Handlers;
using ApiRepos;
using LocalDbDAL;
using Models.Responses;

namespace ApiDAL
{
    public static class ItemSituationDAL
    {
        public static async Task<ApiResponse> GetItemSituation() => await HttpClientFunctions.AuthRequest(Models.RequestsTypes.Get, ApiKeys.ApiUri + "/itemsituation");
    }
}
