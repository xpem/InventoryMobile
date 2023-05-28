using Models;

namespace PersonalAssetsMobile.Services.Interfaces
{
    public interface IAcquisitionTypeService
    {
        Task<List<AcquisitionType>> GetAcquisitionType();
    }
}
