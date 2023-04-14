using PersonalAssetsMobile.ViewModels.Category.SubCategory;

namespace PersonalAssetsMobile.Views.Category.SubCategory;

public partial class SubCategoryEdit : ContentPage
{
    public SubCategoryEdit(SubCategoryEditVM subCategoryEditVM)
    {
        InitializeComponent();

        BindingContext = subCategoryEditVM;
    }
}