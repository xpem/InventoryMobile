using BLL;
using PersonalAssetsMobile.Services.Interfaces;

namespace PersonalAssetsMobile.Services
{
    public class ItemService: IItemService
    {
        public async Task<(bool,string)> AddItem(Models.Item item)
        {
            var resp = await ItemBLL.AddItem(item);

            if (resp.Success)
                return (true, "Item Adicionado!");
            else return (false, "Ocorreu um erro ao tentar adicionar o item");
        }
    }
}
