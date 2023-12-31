using InventoryMobile.UIModels;
using InventoryMobile.ViewModels.Category;
using InventoryMobile.Views.Category;

namespace InventoryMobile.Views;

public partial class CategoryList : ContentPage
{
    public CategoryList(CategoryListVM categoryListVM)
    {
        InitializeComponent();

        BindingContext = categoryListVM;
    }

    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var TappedItem = e.Item as UICategory;

        Shell.Current.GoToAsync($"{nameof(CategoryDisplay)}?Id={TappedItem.Id}", true);

    }
}