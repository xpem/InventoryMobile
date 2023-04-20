using ApiDAL;
using Models;
using System.Text.Json;

namespace BLL
{
    public class CategoryBLL
    {
        public static async Task<BLLResponse> GetCategory(string token)
        {
            var resp = await CategoryApiDAL.GetCategories(token);

            try
            {
                if (resp is not null && resp.Content is not null)
                {
                    var list = JsonSerializer.Deserialize<List<Models.Category>>(resp.Content);

                    return new BLLResponse() { Success = true, Content = list };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new BLLResponse() { Success = false, Error = ErrorTypes.Unknown };
        }
    }
}
