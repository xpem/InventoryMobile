using Services;
using Services.User;

namespace PersonalAssetsMobile;

public partial class App : Application
{
	public App()
	{
        BuildDbService.BuildSQLiteDb();

        InitializeComponent();

        if (UserService.GetUserLocalDb() is null)        
            MainPage = new AppShell();
	}
}
