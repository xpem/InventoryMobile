using ApiDAL;
using BLL.Handlers;
using Models;
using System.Text.Json.Nodes;

namespace BLL
{
    public class SubCategoryBLL
    {
        public static async Task<BLLResponse> GetSubCategoriesByCategoryId(int categoryId)
        {
            var resp = await SubCategoryApiDAL.GetSubCategoriesByCategoryId(categoryId.ToString());

            return ApiResponseHandler.Handler<List<SubCategory>>(resp);
        }

        public static async Task<BLLResponse> GetSubCategoryById(string id)
        {
            var resp = await SubCategoryApiDAL.GetSubCategoryById(id);

            return ApiResponseHandler.Handler<SubCategory>(resp);
        }

        public static async Task<BLLResponse> AddSubCategory(SubCategory subCategory)
        {
            var resp = await SubCategoryApiDAL.AddSubCategory(subCategory);

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

        public static async Task<BLLResponse> AltSubCategory(SubCategory subCategory)
        {
            var resp = await SubCategoryApiDAL.AltSubCategory(subCategory);

            if (resp is not null && resp.Content is not null)
            {
                if (resp.Success)
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
                }
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public static async Task<BLLResponse> DelSubCategory(int id)
        {
            var resp = await SubCategoryApiDAL.DelSubCategory(id);

            if (resp is not null && resp.Content is not null)
                return new BLLResponse() { Success = resp.Success, Content = resp.Content };

            return new BLLResponse() { Success = false, Content = null };
        }
    }
}
