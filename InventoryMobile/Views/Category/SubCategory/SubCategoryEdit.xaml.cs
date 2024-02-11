using InventoryMobile.ViewModels.Category.SubCategory;

namespace InventoryMobile.Views.Category.SubCategory;

public partial class SubCategoryEdit : ContentPage
{
    public SubCategoryEdit(SubCategoryEditVM subCategoryEditVM)
    {
        InitializeComponent();

        BindingContext = subCategoryEditVM;
    }
}