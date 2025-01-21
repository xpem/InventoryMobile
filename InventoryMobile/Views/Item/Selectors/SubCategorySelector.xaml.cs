using InventoryMobile.UIModels;
using InventoryMobile.ViewModels.Item.Selectors;
using Models.DTO;

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

            List<SubCategory> subCategories = [subCategoryObj];

            Models.DTO.Category category = new()
            {
                Id = subCategorySelectorVM.Category.Id,
                Name = subCategorySelectorVM.Category.Name,
                SubCategories = subCategories
            };

            Shell.Current.GoToAsync($"../..", true,
                new Dictionary<string, object> { { "SelectedCategory", category } });
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