using PersonalAssetsMobile.ViewModels.Category;

namespace PersonalAssetsMobile.Views.Category;

public partial class CategoryEdit : ContentPage
{
    public CategoryEdit(CategoryEditVM categoryEditVM)
    {
        InitializeComponent();
        BindingContext = categoryEditVM;
    }
}