using ApiDAL;
using BLL.Handlers;
using Models;
using System.Text.Json.Nodes;

namespace BLL
{
    public class CategoryBLL
    {
        public static async Task<BLLResponse> GetCategories() =>
            ApiResponseHandler.Handler<List<Models.Category>>(await CategoryApiDAL.GetCategories());


        public static async Task<BLLResponse> GetCategoriesWithSubCategories() =>
            ApiResponseHandler.Handler<List<Models.Category>>(await CategoryApiDAL.GetCategoriesWithSubCategories());


        public static async Task<BLLResponse> GetCategoryById(string id) =>
            ApiResponseHandler.Handler<Models.Category>(await CategoryApiDAL.GetCategoryById(id));


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
