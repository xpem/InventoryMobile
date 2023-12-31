using InventoryMobile.ViewModels.Category;

namespace InventoryMobile.Views.Category;

public partial class CategoryEdit : ContentPage
{
    public CategoryEdit(CategoryEditVM categoryEditVM)
    {
        InitializeComponent();
        BindingContext = categoryEditVM;
    }
}