using ApiRepos;
using Models;

namespace ApiDAL
{
    public static class SubCategoryApiDAL
    {
        //public static async Task<int?> AddSubCategory(string name, string icon, string categoryId)
        //{
        //    try
        //    {
        //        string json = JsonSerializer.Serialize(new { Name = name, Icon = icon, Category = new { Id = categoryId } });

        //        Response resp = await HttpClientFunctions.PostAsync(ApiKeys.ApiUri + "/subcategory", json);

        //        if (resp is not null && resp.Success && resp.Content is not null)
        //        {
        //            int id = Convert.ToInt32(resp.Content["Id"]?.GetValue<int>().ToString());

        //            return id;
        //        }
        //        else return null;
        //    }
        //    catch (Exception ex) { throw ex; }
        //}

        public static async Task<Response> GetSubCategories(string userToken)
        {
            return await HttpClientFunctions.GetAsync(ApiKeys.ApiUri + "/subcategory", userToken);
        }
    }
}
