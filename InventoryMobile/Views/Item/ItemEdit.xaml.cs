using InventoryMobile.ViewModels.Item;

namespace InventoryMobile.Views.Item;

public partial class ItemEdit : ContentPage
{
    public ItemEdit(ItemEditVM itemEditVM)
    {
        InitializeComponent();

        BindingContext = itemEditVM;
    }
}