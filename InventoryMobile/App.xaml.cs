using BLL;
using BLL.Interface;
using InventoryMobile.Views;
using Models.DTO;

namespace InventoryMobile;

public partial class App : Application
{
    public App(IBuildDbBLL buildDbBLL, IUserService userBLL, ICheckServerBLL checkServerBLL)
    {
        try
        {
            buildDbBLL.Init();

            InitializeComponent();

            User user = null;

            Task.Run(async () => user = await userBLL.GetLocalAsync()).Wait();

            MainPage = new AppShell(new ViewModels.AppShellVM(userBLL, user));

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (user != null)
                    Shell.Current.GoToAsync($"//{nameof(Main)}");
            }
        }
        catch { throw; }
    }
}
