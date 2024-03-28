using ApiDAL;
using BLL.Handlers;
using Models.ItemModels;
using Models.Responses;

namespace BLL
{
    public interface IItemSituationBLL
    {
        Task<BLLResponse> GetItemSituation();
    }

    public class ItemSituationBLL(IItemSituationApiDAL itemSituationDAL) : IItemSituationBLL
    {
        public async Task<BLLResponse> GetItemSituation()
        {
            ApiResponse resp = await itemSituationDAL.GetItemSituation();

            return ApiResponseHandler.Handler<List<ItemSituation>>(resp);
        }
    }
}
