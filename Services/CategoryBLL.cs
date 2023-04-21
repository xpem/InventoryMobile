using ApiDAL;
using Models;

namespace BLL
{
    public class CategoryBLL
    {
        public static async Task<BLLResponse> GetCategories(string token)
        {
            var resp = await CategoryApiDAL.GetCategories(token);

            return ApiResponseHandler.Handler<List<Models.Category>>(resp);
        }

        public static async Task<BLLResponse> GetCategoryById(string token, string id)
        {
            var resp = await CategoryApiDAL.GetCategoryById(token, id);

            return ApiResponseHandler.Handler<Models.Category>(resp);
        }
    }
}
