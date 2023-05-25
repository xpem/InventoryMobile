using ApiDAL;
using BLL.Handlers;
using Models;
using System.Text.Json.Nodes;

namespace BLL
{
    public class CategoryBLL
    {
        public static async Task<BLLResponse> GetCategories()
        {
            var resp = await CategoryApiDAL.GetCategories();

            return ApiResponseHandler.Handler<List<Models.Category>>(resp);
        }

        public static async Task<BLLResponse> GetCategoriesWithSubCategories()
        {
            var resp = await CategoryApiDAL.GetCategoriesWithSubCategories();

            return ApiResponseHandler.Handler<List<Models.Category>>(resp);
        }

        public static async Task<BLLResponse> GetCategoryById(string id)
        {
            var resp = await CategoryApiDAL.GetCategoryById(id);

            return ApiResponseHandler.Handler<Models.Category>(resp);
        }

        public static async Task<BLLResponse> AddCategory(Category category)
        {
            var resp = await CategoryApiDAL.AddCategory(category);

            if (resp is not null && resp.Content is not null)
            {
                var jResp = JsonNode.Parse(resp.Content);
                if (resp.Success && jResp is not null)
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

        public static async Task<BLLResponse> AltCategory(Category category)
        {
            var resp = await CategoryApiDAL.AltCategory(category);

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

        public static async Task<BLLResponse> DelCategory(int id)
        {
            var resp = await CategoryApiDAL.DelCategory(id);

            if (resp is not null && resp.Content is not null)
                return new BLLResponse() { Success = resp.Success, Content = resp.Content };

            return new BLLResponse() { Success = false, Content = null };
        }
    }
}
