using ApiRepos;
using Models.Responses;

namespace ApiDAL
{
    public interface IAcquisitionTypeDAL
    {
        Task<ApiResponse> GetAcquisitionType();
    }

    public class AcquisitionTypeDAL(IHttpClientFunctions httpClientFunctions) : IAcquisitionTypeDAL
    {
        public async Task<ApiResponse> GetAcquisitionType() => await httpClientFunctions.AuthRequest(Models.RequestsTypes.Get, ApiKeys.ApiAddress + "/acquisitiontype");
    }
}
