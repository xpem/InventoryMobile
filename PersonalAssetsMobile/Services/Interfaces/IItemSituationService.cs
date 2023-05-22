using Models;

namespace PersonalAssetsMobile.Services.Interfaces
{
    public interface IItemSituationService
    {
        Task<List<ItemSituation>> GetItemSituation();
    }
}
