namespace PersonalAssetsMobile.Services.Interfaces
{
    public interface IItemService
    {
        Task<List<Models.Item>> GetItems();

        Task<(bool, string)> AddItem(Models.Item item);
    }
}
