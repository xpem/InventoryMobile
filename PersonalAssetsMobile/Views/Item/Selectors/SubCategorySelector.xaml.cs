using PersonalAssetsMobile.ViewModels.Item.Selectors;

namespace PersonalAssetsMobile.Views.Item.Selectors;

public partial class SubCategorySelector : ContentPage
{
    public SubCategorySelector(SubCategorySelectorVM subCategorySelectorVM)
    {
        InitializeComponent();

        BindingContext = subCategorySelectorVM;
    }
}