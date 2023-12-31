using Models;

namespace InventoryMobile.Services.Interfaces
{
    public interface IAcquisitionTypeService
    {
        Task<List<AcquisitionType>> GetAcquisitionType();
    }
}
