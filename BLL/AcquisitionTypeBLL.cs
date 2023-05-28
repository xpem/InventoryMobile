using ApiDAL;
using BLL.Handlers;
using Models;

namespace BLL
{
    public class AcquisitionTypeBLL
    {
        public static async Task<BLLResponse> GetAcquisitionType()
        {
            var resp = await AcquisitionTypeDAL.GetAcquisitionType();

            return ApiResponseHandler.Handler<List<AcquisitionType>>(resp);
        }
    }
}
