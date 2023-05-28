using BLL;
using Models;
using PersonalAssetsMobile.Services.Interfaces;

namespace PersonalAssetsMobile.Services
{
    public class AcquisitionTypeService : IAcquisitionTypeService
    {
        public async Task<List<AcquisitionType>> GetAcquisitionType()
        {
            var resp = await AcquisitionTypeBLL.GetAcquisitionType();

            if (resp is not null && resp.Success)
                return resp.Content as List<AcquisitionType>;

            return null;
        }
    }
}
