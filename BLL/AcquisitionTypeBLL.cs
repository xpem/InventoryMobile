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

    public class AcquisitionTypeBLL(IAcquisitionTypeApiDAL acquisitionTypeApiDAL) : IAcquisitionTypeBLL
    {
        public async Task<BLLResponse> GetAcquisitionType()
        {
            var resp = await acquisitionTypeApiDAL.GetAcquisitionType();

            return ApiResponseHandler.Handler<List<AcquisitionType>>(resp);
        }
    }
}
