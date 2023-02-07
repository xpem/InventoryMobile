using PersonalAssetsMobile.ViewModels;

namespace PersonalAssetsMobile.Views;

public partial class Main : ContentPage
{
    private readonly MainVM vm;

    public Main(MainVM mainVM)
    {
        InitializeComponent();
        BindingContext = vm = mainVM;
    }

    private void BtnItemStatusSelected_Clicked(object sender, EventArgs e)
    {
        var view = sender as View;
        vm.ItemStatusSelectdCommand.Execute(view.BindingContext);
    }

    private void BtnCategorySelected_Clicked(object sender, EventArgs e)
    {
        var view = sender as View;
        vm.CategorySelectedCommand.Execute(view.BindingContext);
    }
}