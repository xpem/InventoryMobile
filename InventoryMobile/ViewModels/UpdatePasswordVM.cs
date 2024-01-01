using BLL.Interface;
using InventoryMobile.Utils;
using System.Windows.Input;

namespace InventoryMobile.ViewModels
{
    public class UpdatePasswordVM(IUserBLL userBLL) : ViewModelBase
    {
        string email;
        string btnSendEmailText = "Enviar Email";

        public string Email { get => email; set { if (email != value) { email = value; OnPropertyChanged(nameof(Email)); } } }

        public string BtnSendEmailText { get => btnSendEmailText; set { if (btnSendEmailText != value) { btnSendEmailText = value; OnPropertyChanged(nameof(BtnSendEmailText)); } } }

        public ICommand UpdatePasswordCommand => new Command(async () =>
        {
            BtnSendEmailText = "Processando...";
            IsBusy = true;

            if (!(Connectivity.NetworkAccess == NetworkAccess.Internet))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Sem conexão com a internet", null, "Ok");
                return;
            }

            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Digite um email válido", null, "Ok");
                return;
            }
            else if (!Validations.ValidateEmail(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Digite um email válido", null, "Ok");
                return;
            }
            else
            {
                userBLL.RecoverPassword(Email);

                await Application.Current.MainPage.DisplayAlert("Aviso", "Email de alteração de senha enviado!", null, "Ok");

                await Shell.Current.GoToAsync("..");
            }

            BtnSendEmailText = "Enviar Email";
            IsBusy = false;
        });
    }
}
