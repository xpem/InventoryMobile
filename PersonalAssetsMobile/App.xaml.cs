using BLL;
using BLL.Interface;
using InventoryMobile.Views;
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

            Models.User user = userBLL.GetUserLocal();
            MainPage = new AppShell(new ViewModels.AppShellVM());

            bool isOn = false;

            if (CrossConnectivity.Current.IsConnected)
            {
                Task.Run(async () => isOn = await checkServerBLL.CheckServer()).Wait();

                if (!isOn)
                    _ = Application.Current.MainPage.DisplayAlert("Aviso", "Não foi possivel se conectar a internet", null, "Ok");
            }
            else _ = Application.Current.MainPage.DisplayAlert("Aviso", "Não foi possivel se conectar a internet", null, "Ok");


            if (user != null)
                Shell.Current.GoToAsync($"//{nameof(Main)}");
        }
        catch { throw; }
    }
}
