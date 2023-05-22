using PersonalAssetsMobile.Views;

namespace PersonalAssetsMobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell(new ViewModels.AppShellVM());
    }

    protected override async void OnStart()
    {
        await LocalDbDAL.BuildDbDAL.BuildDB();

        if ((await LocalDbDAL.UserLocalDAl.GetUser()) is not null)
            await Shell.Current.GoToAsync($"//{nameof(Main)}");

        base.OnStart();
    }
}
