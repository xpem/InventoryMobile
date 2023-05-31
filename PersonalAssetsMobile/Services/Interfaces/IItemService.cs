namespace PersonalAssetsMobile.Services.Interfaces
{
    public interface IItemService
    {
        Task<(bool, string)> AddItem(Models.Item item);
    }
}
