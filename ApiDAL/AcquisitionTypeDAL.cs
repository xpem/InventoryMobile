using ApiRepos;
using Models.Responses;

namespace ApiDAL
{
    public static class AcquisitionTypeDAL
    {
        public static async Task<ApiResponse> GetAcquisitionType() => await HttpClientFunctions.AuthRequest(Models.RequestsTypes.Get, ApiKeys.ApiUri + "/acquisitiontype");
    }
}
