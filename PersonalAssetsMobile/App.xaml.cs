using PersonalAssetsMobile.Views;
using Services;

namespace PersonalAssetsMobile;

public partial class App : Application
{
    public App()
    {
        BuildDbService.BuildSQLiteDb();

        InitializeComponent();

        MainPage = new AppShell(new ViewModels.AppShellVM());
    }

    protected override async void OnStart()
    {
        if (!string.IsNullOrEmpty(Preferences.Default.Get("ID", string.Empty)))
            await Shell.Current.GoToAsync($"//{nameof(Main)}");

        base.OnStart();
    }
}
