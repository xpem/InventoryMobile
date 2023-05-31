using ApiDAL;
using Models;
using System.Text.Json.Nodes;

namespace BLL
{
    public class ItemBLL
    {
        public static async Task<BLLResponse> AddItem(Models.Item item)
        {
            var resp = await ItemDAL.AddItem(item);

            if (resp is not null && resp.Success && resp.Content is not null)
            {
                var jResp = JsonNode.Parse(resp.Content);
                if (jResp is not null)
                {
                    //SubCategory subCategoryResp = new()
                    //{
                    //    Id = jResp["Id"]?.GetValue<int>() ?? 0,
                    //    Name = jResp["Name"]?.GetValue<string>(),
                    //    IconName = jResp["IconName"]?.GetValue<string>(),
                    //    SystemDefault = jResp["SystemDefault"]?.GetValue<int>()
                    //};

                    return new BLLResponse() { Success = resp.Success };
                }
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }
    }
}
