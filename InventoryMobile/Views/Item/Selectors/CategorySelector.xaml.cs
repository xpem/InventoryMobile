using InventoryMobile.UIModels;
using InventoryMobile.ViewModels.Item.Selectors;
using InventoryMobile.Views.Item.Selectors;
using System.Xml.Linq;

namespace InventoryMobile.Views.Item;

public partial class CategorySelector : ContentPage
{
    readonly CategorySelectorVM categorySelectorVM;

    public CategorySelector(CategorySelectorVM _categorySelectorVM)
    {
        InitializeComponent();

        BindingContext = categorySelectorVM = _categorySelectorVM;
    }

    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var TappedItem = e.Item as UICategory;
        if (TappedItem.HaveSubcategories)
        {
            var categoryObj = categorySelectorVM.CategoriesObsList.FirstOrDefault(c => c.Id == TappedItem.Id);
            Shell.Current.GoToAsync($"{nameof(SubCategorySelector)}", true, new Dictionary<string, object> { { "Category", categoryObj } });
        }
    }
    private async void ViewCell_Tapped(object sender, EventArgs e)
    {
        var cell = sender as ViewCell;
        cell.View.Opacity = 0.5;
        await cell.View.FadeTo(0.5, 2000);
        _ = cell.View.FadeTo(1, 1000).ConfigureAwait(false);
    }

}