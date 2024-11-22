using ApiDAL;
using BLL.Handlers;
using Models;
using Models.Responses;

namespace BLL
{
    public interface IAcquisitionTypeBLL
    {
        Task<ServResp> GetAcquisitionType();
    }

    public class AcquisitionTypeBLL(IAcquisitionTypeApiDAL acquisitionTypeApiDAL) : IAcquisitionTypeBLL
    {
        public async Task<ServResp> GetAcquisitionType()
        {
            var resp = await acquisitionTypeApiDAL.GetAcquisitionType();

            return ApiResponseHandler.Handler<List<AcquisitionType>>(resp);
        }
    }
}
