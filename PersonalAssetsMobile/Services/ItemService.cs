using BLL;
using Models;
using PersonalAssetsMobile.Services.Interfaces;

namespace PersonalAssetsMobile.Services
{
    public class ItemService : IItemService
    {

        public async Task<List<Models.Item>> GetItems()
        {
            BLLResponse resp = await ItemBLL.GetItems();

            if (resp is not null && resp.Success)
                return resp.Content as List<Models.Item>;

            return null;
        }

        public async Task<Item> GetItemById(int id)
        {
            BLLResponse resp = await ItemBLL.GetItemById(id.ToString());

            if (resp is not null && resp.Success)
                return resp.Content as Models.Item;

            return null;
        }

        public async Task<(bool, string)> AddItem(Models.Item item)
        {
            var resp = await ItemBLL.AddItem(item);

            if (resp.Success)
                return (true, "Item Adicionado!");
            else return (false, "Ocorreu um erro ao tentar adicionar o item");
        }

        public async Task<(bool, string)> AltItem(Models.Item item)
        {
            var resp = await ItemBLL.AltItem(item);

            if (resp.Success)
                return (true, "Item Atualizada!");
            else return (false, "Ocorreu um erro ao tentar adicionar o item");
        }
    }
}
