using ApiDAL.Interfaces;
using BLL.Handlers;
using Models;
using Models.Responses;
using Services.Interface;
using System.Text.Json.Nodes;

namespace BLL
{
    public class CategoryBLL(ICategoryApiDAL categoryApiDAL) : ICategoryBLL
    {
        public async Task<ServResp> GetCategoriesAsync() => ApiResponseHandler.Handler<List<Models.Category>>(await categoryApiDAL.GetCategoriesAsync());

        public async Task<ServResp> GetCategoriesWithSubCategoriesAsync() => ApiResponseHandler.Handler<List<Models.Category>>(await categoryApiDAL.GetCategoriesWithSubCategoriesAsync());

        public async Task<ServResp> GetCategoryByIdAsync(string id) => ApiResponseHandler.Handler<Models.Category>(await categoryApiDAL.GetCategoryByIdAsync(id));

        public async Task<ServResp> AddCategoryAsync(Category category)
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

                    return new ServResp() { Success = resp.Success, Content = categoryResp };
                }
                else return new ServResp() { Success = false, Content = resp.Content };
            }

            return new ServResp() { Success = false, Content = null };
        }

        public async Task<ServResp> AltCategoryAsync(Category category)
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

                        return new ServResp() { Success = resp.Success, Content = categoryResp };
                    }
                }
                else return new ServResp() { Success = false, Content = resp.Content };
            }

            return new ServResp() { Success = false, Content = null };
        }

        public async Task<ServResp> DelCategoryAsync(int id)
        {
            var resp = await categoryApiDAL.DelCategoryAsync(id);

            if (resp is not null && resp.Content is not null)
                return new ServResp() { Success = resp.Success, Content = resp.Content };

            return new ServResp() { Success = false, Content = null };
        }
    }
}
