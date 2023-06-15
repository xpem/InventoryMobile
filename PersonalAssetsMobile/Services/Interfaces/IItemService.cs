using Models;

namespace PersonalAssetsMobile.Services.Interfaces
{
    public interface IItemService
    {
        Task<List<Models.Item>> GetItems();

        Task<Item> GetItemById(int id);

        Task<(bool, string)> AddItem(Models.Item item);

        Task<(bool, string)> AltItem(Models.Item item);
    }
}
