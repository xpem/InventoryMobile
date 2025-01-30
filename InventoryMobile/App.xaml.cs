using BLL;
using BLL.Interface;
using InventoryMobile.Infra.Services;
using InventoryMobile.Views;
using Models.DTO;
using Services.Interface;

namespace InventoryMobile;

public partial class App : Application
{
    public int Uid { get; set; }

    public App(IBuildDbService buildDbService, IUserService userBLL, ISyncService syncService)
    {
        try
        {
            buildDbService.Init();

            InitializeComponent();

            User user = userBLL.GetAsync().Result;

            if (user != null)
            {
                Uid = user.Id;
                syncService.StartThread();
            }

            MainPage = new AppShell(new ViewModels.AppShellVM(userBLL, user, buildDbService, syncService));

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (user != null)
                    Shell.Current.GoToAsync($"//{nameof(Main)}");
            }
        }
        catch { throw; }
    }
}
