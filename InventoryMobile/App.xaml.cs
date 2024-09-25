using BLL;
using BLL.Interface;
using DbContextDAL;
using InventoryMobile.Views;
using Models.DTO;
using Plugin.Connectivity;

namespace InventoryMobile;

public partial class App : Application
{
    public App(IBuildDbBLL buildDbBLL, IUserBLL userBLL, ICheckServerBLL checkServerBLL)
    {
        try
        {
            buildDbBLL.Init();

            InitializeComponent();

            User user = null;

            Task.Run(async () => user = await userBLL.GetUserLocalAsync()).Wait();

            MainPage = new AppShell(new ViewModels.AppShellVM(userBLL, user));

            if (CrossConnectivity.Current.IsConnected)
            {
                if (user != null)
                    Shell.Current.GoToAsync($"//{nameof(Main)}");
            }
        }
        catch { throw; }
    }
}
