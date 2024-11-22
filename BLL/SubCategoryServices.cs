using ApiDAL;
using ApiDAL.Interfaces;
using BLL.Handlers;
using Models;
using Models.Responses;
using Services.Interface;
using System.Text.Json.Nodes;

namespace BLL
{
    public class SubCategoryServices(ISubCategoryApiRepo subCategoryApiRepo) : ISubCategoryBLL, ISyncServiceBase
    {
        public async Task<ServResp> GetSubCategoriesByCategoryId(int categoryId)
        {
            var resp = await subCategoryApiRepo.GetSubCategoriesByCategoryId(categoryId.ToString());

            return ApiResponseHandler.Handler<List<Models.SubCategory>>(resp);
        }

        public async Task<ServResp> GetSubCategoryById(string id)
        {
            var resp = await subCategoryApiRepo.GetSubCategoryById(id);

            return ApiResponseHandler.Handler<Models.SubCategory>(resp);
        }

        public async Task<ServResp> InsertSubCategory(Models.SubCategory subCategory)
        {
            var resp = await subCategoryApiRepo.AddSubCategory(subCategory);

            if (resp is not null && resp.Success && resp.Content is not null and string)
            {
                var jResp = JsonNode.Parse(resp.Content as string);
                if (jResp is not null)
                {
                    Models.SubCategory subCategoryResp = new()
                    {
                        Id = jResp["Id"]?.GetValue<int>() ?? 0,
                        Name = jResp["Name"]?.GetValue<string>(),
                        IconName = jResp["IconName"]?.GetValue<string>(),
                        SystemDefault = jResp["SystemDefault"]?.GetValue<bool>()
                    };

                    return new ServResp() { Success = resp.Success, Content = subCategoryResp };
                }
                else return new ServResp() { Success = false, Content = resp.Content };
            }

            return new ServResp() { Success = false, Content = null };
        }

        public async Task<ServResp> GetByLastUpdateAsync(DateTime lastUpdate, int page)
        {
            ApiResponse resp = await subCategoryApiRepo.GetByLastUpdateAsync(lastUpdate, page);

            return ApiResponseHandler.Handler<List<SubCategory>>(resp);
        }

        public async Task<ServResp> AltSubCategory(Models.SubCategory subCategory)
        {
            var resp = await subCategoryApiRepo.AltSubCategory(subCategory);

            if (resp is not null && resp.Content is not null and string)
            {
                if (resp.Success)
                {
                    var jResp = JsonNode.Parse(resp.Content as string);
                    if (jResp is not null)
                    {
                        Models.SubCategory subCategoryResp = new()
                        {
                            Id = jResp["Id"]?.GetValue<int>() ?? 0,
                            Name = jResp["Name"]?.GetValue<string>(),
                            IconName = jResp["IconName"]?.GetValue<string>(),
                            SystemDefault = jResp["SystemDefault"]?.GetValue<bool>()
                        };

                        return new ServResp() { Success = resp.Success, Content = subCategoryResp };
                    }
                }
                else return new ServResp() { Success = false, Content = resp.Content };
            }

            return new ServResp() { Success = false, Content = null };
        }

        public async Task<ServResp> DelSubCategory(int id)
        {
            var resp = await subCategoryApiRepo.DelSubCategory(id);

            if (resp is not null && resp.Content is not null)
                return new ServResp() { Success = resp.Success, Content = resp.Content };

            return new ServResp() { Success = false, Content = null };
        }

        public Task LocalToApiSync(int uid, DateTime lastUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task ApiToLocalSync(int uid, DateTime lastUpdate)
        {
            int page = 1;
            DateTime? localLastUpdate;

            while (true)
            {
                ServResp respByLastUpdate = await GetByLastUpdateAsync(lastUpdate, page);
                if ((respByLastUpdate is not null) && respByLastUpdate.Success && respByLastUpdate.Content is not null)
                {
                    List<SubCategory>? subCategories = respByLastUpdate.Content as List<SubCategory>;

                    if (subCategories is not null)
                    {
                        foreach (SubCategory subCategory in subCategories)
                        {
                            if(subCategory is null) throw new ArgumentNullException(nameof(subCategory));

                            subCategory.UserId = uid;
                            localLastUpdate = null;

                            //if (subCategory.Id is not null)
                            //    localLastUpdate = await bookDAL.GetUpdatedAtByIdAsync(subCategory.Id.Value);
                            //else
                            //    throw new ArgumentNullException(nameof(subCategory.Id));

                            //continuar

                        }
                    }
                }
            }
        }
    }
}
