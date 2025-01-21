using BLL;
using BLL.Interface;
using InventoryMobile.Infra.Services;
using InventoryMobile.Views;
using Models.DTO;
using Services;
using System.Windows.Input;
using System.Xml.Linq;

namespace InventoryMobile.ViewModels
{
    public partial class AppShellVM : BindableObject
    {
        IUserService UserService { get; set; }

        public ISyncService SyncService { get; set; }

        public IBuildDbService BuildDbService { get; set; }

        string email, name;

        public string Email { get => email; set { if (email != value) { email = value; OnPropertyChanged(nameof(Email)); } } }

        public string Name { get => name; set { if (name != value) { name = value; OnPropertyChanged(nameof(Name)); } } }

        public AppShellVM(IUserService userBLL, User user, IBuildDbService buildDbService, ISyncService syncService)
        {
            UserService = userBLL;
            SyncService = syncService;

            if (user is not null)
                Email = user.Email;
            BuildDbService = buildDbService;
        }

        public ICommand SignOutCommand => new Command(async (e) =>
        {
            bool resp = await Application.Current.MainPage.DisplayAlert("Confirmação", "Deseja sair e retornar a tela inicial?", "Sim", "Cancelar");

            if (resp)
            {
                //finalize sync thread process
                SyncService.ThreadIsRunning = false;

                SyncService.Timer?.Dispose();

                ((App)App.Current).Uid = 0;

                await BuildDbService.CleanLocalDatabase();

                _ = Shell.Current.GoToAsync($"//{nameof(SignIn)}");
            }
        });

        public async void AtualizaUser()
        {
            User user = await UserService.GetLocalAsync();

            if (user is not null)
            {
                Name = user.Name;
                Email = user.Email;
            }
        }
    }
}
