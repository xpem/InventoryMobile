using ApiDAL;
using Models;
using System.Text.Json.Nodes;

namespace BLL
{
    public class SubCategoryBLL
    {
        public static async Task<BLLResponse> GetSubCategoriesByCategoryId(string token,int categoryId)
        {
            var resp = await SubCategoryApiDAL.GetSubCategoriesByCategoryId(token,categoryId.ToString());

            return ApiResponseHandler.Handler<List<SubCategory>>(resp);
        }

        public static async Task<BLLResponse> AddSubCategory(string token, SubCategory subCategory)
        {
            var resp = await SubCategoryApiDAL.AddSubCategory(token, subCategory);

            if (resp is not null && resp.Success && resp.Content is not null)
            {
                var jResp = JsonNode.Parse(resp.Content);
                if (jResp is not null)
                {
                    SubCategory subCategoryResp = new()
                    {
                        Id = jResp["Id"]?.GetValue<int>() ?? 0,
                        Name = jResp["Name"]?.GetValue<string>(),
                        IconName = jResp["IconName"]?.GetValue<string>(),
                        SystemDefault = jResp["SystemDefault"]?.GetValue<int>()
                    };

                    return new BLLResponse() { Success = resp.Success, Content = subCategoryResp };
                }
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }
    }
}
