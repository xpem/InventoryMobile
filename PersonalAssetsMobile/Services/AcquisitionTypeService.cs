using BLL;
using Models;
using InventoryMobile.Services.Interfaces;

namespace InventoryMobile.Services
{
    public class AcquisitionTypeService(IAcquisitionTypeBLL acquisitionTypeBLL) : IAcquisitionTypeService
    {
        public async Task<List<AcquisitionType>> GetAcquisitionType()
        {
            var resp = await acquisitionTypeBLL.GetAcquisitionType();

            if (resp is not null && resp.Success)
                return resp.Content as List<AcquisitionType>;

            return null;
        }
    }
}
