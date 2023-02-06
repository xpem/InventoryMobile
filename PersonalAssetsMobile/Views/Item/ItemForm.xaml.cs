using PersonalAssetsMobile.ViewModels.Item;

namespace PersonalAssetsMobile.Views.Item;

public partial class ItemForm : ContentPage
{
    public ItemForm(ItemFormVM itemFormVM)
    {
        InitializeComponent();

        BindingContext = itemFormVM;
    }
}