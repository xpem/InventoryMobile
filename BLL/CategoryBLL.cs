using ApiDAL.Interfaces;
using BLL.Handlers;
using Models;
using Models.Responses;
using System.Text.Json.Nodes;

namespace BLL
{
    public class CategoryBLL(ICategoryApiDAL categoryApiDAL) : ICategoryBLL
    {
        public async Task<BLLResponse> GetCategoriesAsync() => ApiResponseHandler.Handler<List<Models.Category>>(await categoryApiDAL.GetCategoriesAsync());

        public async Task<BLLResponse> GetCategoriesWithSubCategoriesAsync() => ApiResponseHandler.Handler<List<Models.Category>>(await categoryApiDAL.GetCategoriesWithSubCategoriesAsync());

        public async Task<BLLResponse> GetCategoryByIdAsync(string id) => ApiResponseHandler.Handler<Models.Category>(await categoryApiDAL.GetCategoryByIdAsync(id));

        public async Task<BLLResponse> AddCategoryAsync(Category category)
        {
            var resp = await categoryApiDAL.AddCategoryAsync(category);

            if (resp is not null && resp.Content is not null and string)
            {
                var jResp = JsonNode.Parse(resp.Content as string);
                if (resp.Success && jResp is not null)
                {
                    Models.Category categoryResp = new()
                    {
                        Id = jResp["Id"]?.GetValue<int>() ?? 0,
                        Name = jResp["Name"]?.GetValue<string>(),
                        Color = jResp["Color"]?.GetValue<string>(),
                        SystemDefault = jResp["SystemDefault"]?.GetValue<bool>()
                    };

                    return new BLLResponse() { Success = resp.Success, Content = categoryResp };
                }
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public async Task<BLLResponse> AltCategoryAsync(Category category)
        {
            var resp = await categoryApiDAL.AltCategoryAsync(category);

            if (resp is not null && resp.Content is not null and string)
            {
                if (resp.Success)
                {
                    var jResp = JsonNode.Parse(resp.Content as string);
                    if (jResp is not null)
                    {
                        Models.Category categoryResp = new()
                        {
                            Id = jResp["Id"]?.GetValue<int>() ?? 0,
                            Name = jResp["Name"]?.GetValue<string>(),
                            Color = jResp["Color"]?.GetValue<string>(),
                            SystemDefault = jResp["SystemDefault"]?.GetValue<bool>()
                        };

                        return new BLLResponse() { Success = resp.Success, Content = categoryResp };
                    }
                }
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public async Task<BLLResponse> DelCategoryAsync(int id)
        {
            var resp = await categoryApiDAL.DelCategoryAsync(id);

            if (resp is not null && resp.Content is not null)
                return new BLLResponse() { Success = resp.Success, Content = resp.Content };

            return new BLLResponse() { Success = false, Content = null };
        }
    }
}
