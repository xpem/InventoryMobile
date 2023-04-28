using ApiDAL;
using Models;
using System.Text.Json.Nodes;

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

        public static async Task<BLLResponse> AddCategory(string token, Category category)
        {
            var resp = await CategoryApiDAL.AddCategory(token, category);

            if (resp is not null && resp.Content is not null)
            {
                var jResp = JsonNode.Parse(resp.Content);
                if (jResp is not null)
                {
                    Models.Category categoryResp = new()
                    {
                        Id = jResp["Id"]?.GetValue<int>() ?? 0,
                        Name = jResp["Name"]?.GetValue<string>(),
                        Color = jResp["Color"]?.GetValue<string>(),
                        SystemDefault = jResp["SystemDefault"]?.GetValue<int>()
                    };

                    return new BLLResponse() { Success = resp.Success, Content = categoryResp };
                }
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public static async Task<BLLResponse> AltCategory(string token, Category category)
        {
            var resp = await CategoryApiDAL.AltCategory(token, category);

            if (resp is not null && resp.Content is not null)
            {
                if (resp.Success)
                {
                    var jResp = JsonNode.Parse(resp.Content);
                    if (jResp is not null)
                    {
                        Models.Category categoryResp = new()
                        {
                            Id = jResp["Id"]?.GetValue<int>() ?? 0,
                            Name = jResp["Name"]?.GetValue<string>(),
                            Color = jResp["Color"]?.GetValue<string>(),
                            SystemDefault = jResp["SystemDefault"]?.GetValue<int>()
                        };

                        return new BLLResponse() { Success = resp.Success, Content = categoryResp };
                    }
                }
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }
    }
}
