using ApiDAL;
using BLL.Handlers;
using Models;
using Models.ItemModels;
using Models.Responses;
using System.Text.Json.Nodes;

namespace BLL
{
    public interface IItemBLL
    {
        Task<BLLResponse> AddItemAsync(Item item);
        Task<BLLResponse> AltItemAsync(Item item);
        Task<BLLResponse> DelItemAsync(int id);
        Task<BLLResponse> GetItemByIdAsync(string id);
        Task<List<Item>> GetItemsAllAsync();
        Task<ItemFilesToUpload> GetItemImages(int itemId, string itemImage1, string itemImage2);
        Task<BLLResponse> AddItemImageAsync(int id, ItemFilesToUpload itemFilesToUpload);
        Task<BLLResponse> DelItemImageAsync(int id, string filename);
    }

    public class ItemBLL(IItemApiDAL itemApiDAL) : IItemBLL
    {
        public async Task<List<Item>> GetItemsAllAsync()
        {
            ApiResponse totalsResp = await itemApiDAL.GetTotalItensAsync();
            List<Item> items = [];

            var itemTotalsBLLResponse = ApiResponseHandler.Handler<ItemTotals>(totalsResp);

            if (itemTotalsBLLResponse.Success)
            {
                ItemTotals? itemTotals = itemTotalsBLLResponse.Content as ItemTotals;

                for (int i = 1; i <= itemTotals?.TotalPages; i++)
                {
                    ApiResponse resp = await itemApiDAL.GetPaginatedItemsAsync(i);
                    var paginatedItemsBLLResponse = ApiResponseHandler.Handler<List<Item>>(resp);

                    if (paginatedItemsBLLResponse.Success)
                        if (paginatedItemsBLLResponse.Content is List<Item> pageItems)
                            items.AddRange(pageItems);
                }

                return items;
            }
            else throw new Exception("totalsResp success false, error:" + itemTotalsBLLResponse.Error);
        }

        public async Task<BLLResponse> GetItemByIdAsync(string id)
        {
            ApiResponse resp = await itemApiDAL.GetItemByIdAsync(id);
            return ApiResponseHandler.Handler<Item>(resp);
        }

        public async Task<BLLResponse> AddItemAsync(Item item)
        {
            ApiResponse? resp = await itemApiDAL.AddItemAsync(item);

            if (resp is not null && resp.Success && resp.Content is not null and string)
            {
                return ApiResponseHandler.Handler<Item>(resp);
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public async Task<BLLResponse> AltItemAsync(Item item)
        {
            ApiResponse? resp = await itemApiDAL.AltItemAsync(item);

            if (resp is not null && resp.Success && resp.Content is not null and string)
            {
                JsonNode? jResp = JsonNode.Parse(resp.Content as string);

                if (jResp is not null)
                    return new BLLResponse() { Success = resp.Success, Content = null };
                else return new BLLResponse() { Success = false, Content = resp.Content };
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public async Task<BLLResponse> DelItemAsync(int id)
        {
            ApiResponse? resp = await itemApiDAL.DelItemAsync(id);

            if (resp is not null && !resp.Success && !string.IsNullOrEmpty(resp.Content as string))
                return new BLLResponse() { Success = false, Content = resp.Content.ToString() };

            return new BLLResponse() { Success = true, Content = null };
        }

        public async Task<BLLResponse> DelItemImageAsync(int id, string filename)
        {
            ApiResponse? resp = await itemApiDAL.DelItemImageAsync(id, filename);

            if (resp is not null && !resp.Success && !string.IsNullOrEmpty(resp.Content as string))
            {
                return new BLLResponse() { Success = false, Content = resp.Content.ToString() };
            }

            //BLLResponse itemResp = ApiResponseHandler.Handler<Item>(resp);
            return new BLLResponse() { Success = true, Content = null };
        }

        public async Task<ItemFilesToUpload> GetItemImages(int itemId, string itemImage1, string itemImage2)
        {
            ItemFilesToUpload itemFilesToUpload = new();

            if (itemImage1 != null)
            {
                var resItemImage = await GetImageItemAsync(itemId, 1, itemImage1, FilePaths.ImagesPath);

                if (resItemImage is not null)
                    itemFilesToUpload.Image1 = resItemImage;
            }

            if (itemImage2 != null)
            {
                var resItemImage = await GetImageItemAsync(itemId, 2, itemImage2, FilePaths.ImagesPath);

                if (resItemImage is not null)
                    itemFilesToUpload.Image2 = resItemImage;
            }

            return itemFilesToUpload;
        }

        public async Task<BLLResponse> AddItemImageAsync(int id, ItemFilesToUpload itemFilesToUpload)
        {
            ApiResponse resp = await itemApiDAL.AddItemImage(id, itemFilesToUpload);

            if (resp != null && resp.Content is not null)
            {
                var respBllResp = ApiResponseHandler.Handler<ItemFileNames>(resp);

                if (respBllResp is not null && respBllResp.Success)
                {
                    var itemFileNames = respBllResp.Content as Models.ItemModels.ItemFileNames;
                    if (itemFileNames is not null)
                    {
                        if (itemFileNames.Image1 is not null)
                        {
                            var newPath = Path.Combine(FilePaths.ImagesPath, itemFileNames.Image1);
                            System.IO.File.Move(itemFilesToUpload.Image1.ImageFilePath, newPath);

                            itemFilesToUpload.Image1.ImageFilePath = Path.Combine(FilePaths.ImagesPath, itemFileNames.Image1);
                        }

                        if (itemFileNames.Image2 is not null)
                        {
                            var newPath = Path.Combine(FilePaths.ImagesPath, itemFileNames.Image2);
                            System.IO.File.Move(itemFilesToUpload.Image2.ImageFilePath, newPath);

                            itemFilesToUpload.Image2.ImageFilePath = Path.Combine(FilePaths.ImagesPath, itemFileNames.Image2);
                        }

                        return new BLLResponse() { Success = true };
                    }
                }
            }

            return new BLLResponse() { Success = false };

        }

        private async Task<ImageFile?> GetImageItemAsync(int id, int idx, string fileName, string filePath)
        {
            bool exists = System.IO.Directory.Exists(filePath);

            if (!exists)
                System.IO.Directory.CreateDirectory(filePath);

            string filePathAndName = Path.Combine(filePath, fileName);
            ImageFile imageFile;

            if (File.Exists(filePathAndName))
            {
                using var fs = new FileStream(filePathAndName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                using MemoryStream memoryStream = new();
                fs.CopyTo(memoryStream);
                imageFile = new() { FileName = fileName, FileId = idx, ImageFilePath = filePathAndName };

                return imageFile;
            }

            ApiResponse resp = await itemApiDAL.GetItemImageAsync(id, fileName);

            if (resp is not null && resp.Content is not null and Stream)
            {
                using var fs = new FileStream(filePathAndName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                ((Stream)resp.Content).CopyTo(fs);

                imageFile = new() { FileName = fs.Name, FileId = idx, ImageFilePath = filePathAndName };

                await ((Stream)resp.Content).DisposeAsync();

                return imageFile;
            }

            return null;
        }
    }
}
