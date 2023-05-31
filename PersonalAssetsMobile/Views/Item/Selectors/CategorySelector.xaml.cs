using PersonalAssetsMobile.UIModels;
using PersonalAssetsMobile.ViewModels.Item.Selectors;
using PersonalAssetsMobile.Views.Item.Selectors;
using System.Xml.Linq;

namespace PersonalAssetsMobile.Views.Item;

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
            var categoryObj = categorySelectorVM.Categorylist.FirstOrDefault(c => c.Id == TappedItem.Id);
            Shell.Current.GoToAsync($"{nameof(SubCategorySelector)}", true, new Dictionary<string, object> { { "Category", categoryObj } });
        }
    }
}