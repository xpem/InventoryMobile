using PersonalAssetsMobile.ViewModels.Item;

namespace PersonalAssetsMobile.Views.Item;

public partial class ItemEdit : ContentPage
{
    public ItemEdit(ItemEditVM itemEditVM)
    {
        InitializeComponent();

        BindingContext = itemEditVM;
    }
}