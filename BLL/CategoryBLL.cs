using ApiDAL;
using BLL.Handlers;
using Models;
using Models.Responses;
using System.Text.Json.Nodes;

namespace BLL
{
    public class CategoryBLL(ICategoryApiDAL categoryApiDAL) : ICategoryBLL
    {
        public async Task<BLLResponse> GetCategories() =>
            ApiResponseHandler.Handler<List<Models.Category>>(await categoryApiDAL.GetCategories());


        public async Task<BLLResponse> GetCategoriesWithSubCategories() =>
            ApiResponseHandler.Handler<List<Models.Category>>(await categoryApiDAL.GetCategoriesWithSubCategories());


        public async Task<BLLResponse> GetCategoryById(string id) =>
            ApiResponseHandler.Handler<Models.Category>(await categoryApiDAL.GetCategoryById(id));


        public async Task<BLLResponse> AddCategory(Category category)
        {
            var resp = await categoryApiDAL.AddCategory(category);

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
                        SystemDefault = jResp["SystemDefault"]?.GetValue<bool>()
                    };

                    return new BLLResponse() { Success = resp.Success, Content = categoryResp };
                }
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public async Task<BLLResponse> AltCategory(Category category)
        {
            var resp = await categoryApiDAL.AltCategory(category);

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
                            SystemDefault = jResp["SystemDefault"]?.GetValue<bool>()
                        };

                        return new BLLResponse() { Success = resp.Success, Content = categoryResp };
                    }
                }
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public async Task<BLLResponse> DelCategory(int id)
        {
            var resp = await categoryApiDAL.DelCategory(id);

            if (resp is not null && resp.Content is not null)
                return new BLLResponse() { Success = resp.Success, Content = resp.Content };

            return new BLLResponse() { Success = false, Content = null };
        }
    }
}
