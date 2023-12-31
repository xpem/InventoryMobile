using ApiDAL;
using BLL.Handlers;
using Models;
using Models.Responses;

namespace BLL
{
    public interface IAcquisitionTypeBLL
    {
        Task<BLLResponse> GetAcquisitionType();
    }

    public class AcquisitionTypeBLL(IAcquisitionTypeDAL acquisitionTypeDAL) : IAcquisitionTypeBLL
    {
        public async Task<BLLResponse> GetAcquisitionType()
        {
            var resp = await acquisitionTypeDAL.GetAcquisitionType();

            return ApiResponseHandler.Handler<List<AcquisitionType>>(resp);
        }
    }
}
