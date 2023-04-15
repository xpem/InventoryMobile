using PersonalAssetsMobile.Views;
using Services;
using Services.User;

namespace PersonalAssetsMobile;

public partial class App : Application
{
    public App()
    {
        BuildDbService.BuildSQLiteDb();

        InitializeComponent();

        MainPage = new AppShell();

        if (UserService.GetUserLocalDb() is not null)
            Shell.Current.GoToAsync($"//{nameof(Main)}");
    }
}
