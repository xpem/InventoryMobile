using ApiDAL;
using ApiDAL.Interfaces;
using BLL.Handlers;
using Models;
using Models.Responses;
using System.Text.Json.Nodes;

namespace BLL
{
    public class SubCategoryBLL(ISubCategoryApiDAL subCategoryApiDAL) : ISubCategoryBLL
    {
        public async Task<BLLResponse> GetSubCategoriesByCategoryId(int categoryId)
        {
            var resp = await subCategoryApiDAL.GetSubCategoriesByCategoryId(categoryId.ToString());

            return ApiResponseHandler.Handler<List<SubCategory>>(resp);
        }

        public async Task<BLLResponse> GetSubCategoryById(string id)
        {
            var resp = await subCategoryApiDAL.GetSubCategoryById(id);

            return ApiResponseHandler.Handler<SubCategory>(resp);
        }

        public async Task<BLLResponse> InsertSubCategory(SubCategory subCategory)
        {
            var resp = await subCategoryApiDAL.AddSubCategory(subCategory);

            if (resp is not null && resp.Success && resp.Content is not null and string)
            {
                var jResp = JsonNode.Parse(resp.Content as string);
                if (jResp is not null)
                {
                    SubCategory subCategoryResp = new()
                    {
                        Id = jResp["Id"]?.GetValue<int>() ?? 0,
                        Name = jResp["Name"]?.GetValue<string>(),
                        IconName = jResp["IconName"]?.GetValue<string>(),
                        SystemDefault = jResp["SystemDefault"]?.GetValue<bool>()
                    };

                    return new BLLResponse() { Success = resp.Success, Content = subCategoryResp };
                }
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public async Task<BLLResponse> AltSubCategory(SubCategory subCategory)
        {
            var resp = await subCategoryApiDAL.AltSubCategory(subCategory);

            if (resp is not null && resp.Content is not null and string)
            {
                if (resp.Success)
                {
                    var jResp = JsonNode.Parse(resp.Content as string);
                    if (jResp is not null)
                    {
                        SubCategory subCategoryResp = new()
                        {
                            Id = jResp["Id"]?.GetValue<int>() ?? 0,
                            Name = jResp["Name"]?.GetValue<string>(),
                            IconName = jResp["IconName"]?.GetValue<string>(),
                            SystemDefault = jResp["SystemDefault"]?.GetValue<bool>()
                        };

                        return new BLLResponse() { Success = resp.Success, Content = subCategoryResp };
                    }
                }
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public async Task<BLLResponse> DelSubCategory(int id)
        {
            var resp = await subCategoryApiDAL.DelSubCategory(id);

            if (resp is not null && resp.Content is not null)
                return new BLLResponse() { Success = resp.Success, Content = resp.Content };

            return new BLLResponse() { Success = false, Content = null };
        }
    }
}
