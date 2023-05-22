using ApiDAL;
using BLL.Handlers;
using Models;

namespace BLL
{
    public class ItemSituationBLL
    {
        public static async Task<BLLResponse> GetItemSituation()
        {
            var resp = await ItemSituationDAL.GetItemSituation();

            return ApiResponseHandler.Handler<List<ItemSituation>>(resp);
        }
    }
}
