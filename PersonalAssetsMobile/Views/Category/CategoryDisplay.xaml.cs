using InventoryMobile.ViewModels.Category;

namespace InventoryMobile.Views.Category;

public partial class CategoryDisplay : ContentPage
{
    public CategoryDisplay(CategoryDisplayVM categoryDisplayVM)
    {
        InitializeComponent();

        BindingContext = categoryDisplayVM;
    }

    private void DeleteSubCategoryBtn_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string ID = button.CommandParameter.ToString();

        var vm = (CategoryDisplayVM)BindingContext;
        vm.DeleteSubCategoryCommand.Execute(ID);
    }

    private void BtnSubCategoryEdit_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string ID = button.CommandParameter.ToString();

        var vm = (CategoryDisplayVM)BindingContext;
        vm.SubCategoryEditCommand.Execute(ID);
    }
}