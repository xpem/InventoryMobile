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

    private async void ViewCell_Tapped(object sender, EventArgs e)
    {
        var cell = sender as ViewCell;
        cell.View.Opacity = 0.5;
        await cell.View.FadeTo(0.5, 2000);
        _ = cell.View.FadeTo(1, 1000).ConfigureAwait(false);
    }
}