using PersonalAssetsMobile.UIModels;
using PersonalAssetsMobile.ViewModels.Category;
using PersonalAssetsMobile.Views.Category;

namespace PersonalAssetsMobile.Views;

public partial class CategoryList : ContentPage
{
    public CategoryList(CategoryListVM categoryListVM)
    {
        InitializeComponent();

        BindingContext = categoryListVM;
    }

    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var TappedItem = e.Item as CategoryUI;

        Shell.Current.GoToAsync($"{nameof(CategoryDisplay)}?Id={TappedItem.Id}", true);

    }
}