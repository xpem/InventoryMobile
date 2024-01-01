using ApiRepos;
using Models.Responses;

namespace ApiDAL
{
    public interface IAcquisitionTypeApiDAL
    {
        Task<ApiResponse> GetAcquisitionType();
    }

    public class AcquisitionTypeApiDAL(IHttpClientFunctions httpClientFunctions) : IAcquisitionTypeApiDAL
    {
        public async Task<ApiResponse> GetAcquisitionType() => await httpClientFunctions.AuthRequestAsync(Models.RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/acquisitiontype");
    }
}
