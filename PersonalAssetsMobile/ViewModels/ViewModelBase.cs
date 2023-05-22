using BLL;
using Plugin.Connectivity;

namespace PersonalAssetsMobile.ViewModels
{
    public class ViewModelBase : BindableObject
    {

        bool isBusy;

        public bool isOn = true;

        public bool IsBusy
        {
            get => isBusy; set { if (isBusy != value) { isBusy = value; OnPropertyChanged(nameof(IsBusy)); } }
        }

        public bool IsNotBusy => !isBusy;

        public ViewModelBase()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Task.Run(async () => isOn = await CheckServerBLL.CheckServer()).Wait();

                if (!isOn)
                    _ = Application.Current.MainPage.DisplayAlert("Aviso", "Não foi possivel se conectar a internet", null, "Ok");
            }
            else _ = Application.Current.MainPage.DisplayAlert("Aviso", "Não foi possivel se conectar a internet", null, "Ok");

        }
    }
}
