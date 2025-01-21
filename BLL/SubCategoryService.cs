using ApiDAL;
using ApiRepos.Interfaces;
using BLL.Handlers;
using LocalRepos;
using LocalRepos.Interface;
using Models.DTO;
using Models.Responses;
using Services.Interface;
using System.Text.Json.Nodes;

namespace Services
{
    public class SubCategoryService(ISubCategoryApiRepo subCategoryApiRepo, ISubCategoryRepo subCategoryRepo) : ISubCategoryService, ISyncServiceBase
    {
        public async Task<ServResp> GetSubCategoriesByCategoryId(int categoryId)
        {
            var resp = await subCategoryApiRepo.GetSubCategoriesByCategoryId(categoryId.ToString());

            return ApiResponseHandler.Handler<List<SubCategory>>(resp);
        }

        public async Task<ServResp> GetSubCategoryById(string id)
        {
            var resp = await subCategoryApiRepo.GetSubCategoryById(id);

            return ApiResponseHandler.Handler<SubCategory>(resp);
        }

        public async Task<ServResp> InsertSubCategory(SubCategory subCategory)
        {
            var resp = await subCategoryApiRepo.AddSubCategory(subCategory);

            if (resp is not null && resp.Success && resp.Content is not null and string)
            {
                var jResp = JsonNode.Parse((string)resp.Content);
                if (jResp is not null)
                {
                    SubCategory subCategoryResp = new()
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

        public async Task<ServResp> AltSubCategory(SubCategory subCategory)
        {
            var resp = await subCategoryApiRepo.AltSubCategory(subCategory);

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
            await new SyncServiceBase<SubCategoryService>(this).ApiToLocalSync(uid, lastUpdate);
        }

        public Task<int> CreateAsync(DTOBase entity)
        {
            return subCategoryRepo.CreateAsync(entity as SubCategory);
        }

        public Task<int> UpdateAsync(DTOBase entity)
        {
            return subCategoryRepo.UpdateAsync(entity as SubCategory);
        }


        public async Task<List<DTOBase>?> GetByLastUpdateAsync(DateTime lastUpdate, int page)
        {
            var apiResp = await subCategoryApiRepo.GetByLastUpdateAsync(lastUpdate, page);

            var resp = ApiResponseHandler.Handler<List<SubCategory>>(apiResp);

            if (resp is not null && resp.Success && resp.Content is not null)
            {
                var listResp = resp.Content as List<Models.DTO.SubCategory>;

                return listResp?.Select(x => x as DTOBase).ToList();
            }
            else throw new Exception("Error getting SubCategories from API");
        }

        public async Task<DTOBase?> GetByIdAsync(int id)
        {
            return (await subCategoryRepo.GetByIdAsync(id)) as DTOBase;
        }
    }
}
