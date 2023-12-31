using InventoryMobile.UIModels;
using InventoryMobile.ViewModels.Item.Selectors;

namespace InventoryMobile.Views.Item.Selectors;

public partial class SubCategorySelector : ContentPage
{
    readonly SubCategorySelectorVM subCategorySelectorVM;

    public SubCategorySelector(SubCategorySelectorVM _subCategorySelectorVM)
    {
        InitializeComponent();

        BindingContext = subCategorySelectorVM = _subCategorySelectorVM;
    }

    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var TappedItem = e.Item as UISubCategory;

        if (TappedItem is not null)
        {
            var subCategoryObj = subCategorySelectorVM.Category.SubCategories.FirstOrDefault(c => c.Id == TappedItem.Id);

            List<Models.SubCategory> subCategories = new()
            {
                subCategoryObj
            };

            Models.Category category = new()
            {
                Id = subCategorySelectorVM.Category.Id,
                Name = subCategorySelectorVM.Category.Name,
                SubCategories = subCategories
            };

            Shell.Current.GoToAsync($"../..", true,
                new Dictionary<string, object> { { "SelectedCategory", category } });
        }
    }
}