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

    private async void ViewCell_Tapped(object sender, EventArgs e)
    {
        var cell = sender as ViewCell;
        cell.View.Opacity = 0.5;
        await cell.View.FadeTo(0.5, 2000);
        _ = cell.View.FadeTo(1, 1000).ConfigureAwait(false);
    }
}