using BLL;
using Models;
using PersonalAssetsMobile.Services.Interfaces;

namespace PersonalAssetsMobile.Services
{
    public class ItemSituationService : IItemSituationService
    {
        public async Task<List<ItemSituation>> GetItemSituation()
        {
            var resp = await ItemSituationBLL.GetItemSituation();

            if (resp is not null && resp.Success)
                return resp.Content as List<ItemSituation>;

            return null;
        }
    }
}
