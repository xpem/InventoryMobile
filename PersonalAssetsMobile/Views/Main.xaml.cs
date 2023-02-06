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

    private void Button_Clicked(object sender, EventArgs e)
    {
        var view = sender as View;
        vm.SelectdItemStatusCommand.Execute(view.BindingContext);
    }
}