using BLL;
using Models;
using InventoryMobile.Services.Interfaces;
using Models.Responses;

namespace InventoryMobile.Services
{
    public class ItemService(IItemBLL itemBLL) : IItemService
    {

        public async Task<List<Models.Item>> GetItems()
        {
            BLLResponse resp = await itemBLL.GetItems();

            if (resp is not null && resp.Success)
            {
                var itemListResp = resp.Content as List<Item>;
                itemListResp = (from item in itemListResp orderby item.CreatedAt descending select item).ToList();

                if (itemListResp.Count > 0)
                    return itemListResp;
                else return null;
            }

            return null;
        }

        public async Task<Item> GetItemById(int id)
        {
            BLLResponse resp = await itemBLL.GetItemById(id.ToString());

            if (resp is not null && resp.Success)
                return resp.Content as Models.Item;

            return null;
        }

        public async Task<(bool, string)> AddItem(Models.Item item)
        {
            var resp = await itemBLL.AddItem(item);

            if (resp.Success)
                return (true, "Item Adicionado!");
            else return (false, "Ocorreu um erro ao tentar adicionar o item");
        }

        public async Task<(bool, string)> AltItem(Models.Item item)
        {
            var resp = await itemBLL.AltItem(item);

            if (resp.Success)
                return (true, "Item Atualizada!");
            else return (false, "Ocorreu um erro ao tentar alterar o item");
        }

        public async Task<(bool, string)> DelItem(int id)
        {
            var resp = await itemBLL.DelItem(id);

            if (resp.Success)
                return (true, "Item Excluído!");
            else return (false, "Ocorreu um erro ao tentar excluir o item");
        }
    }
}
