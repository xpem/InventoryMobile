using ApiDAL;
using Models;
using System.Text.Json;

namespace BLL.Category.SubCategory
{
    public class SubCategoryBLL
    {
        public async Task<BLLResponse> GetSubCategories(string token)
        {
            var resp = await SubCategoryApiDAL.GetSubCategories(token);

            if (resp is not null && resp.Success && resp.Content is not null)
            {
                var subCategoriesList = JsonSerializer.Deserialize<List<Models.SubCategory>>(resp.Content);

                return new BLLResponse() { Success = true, Content = subCategoriesList };
            }

            return new BLLResponse() { Success = false, Error = ErrorTypes.Unknown };

        }
    }
}
