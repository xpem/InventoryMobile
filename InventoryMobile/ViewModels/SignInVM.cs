using InventoryMobile.Views;
using Services.Interface;
using System.Windows.Input;

namespace InventoryMobile.ViewModels
{
    public partial class SignInVM(IUserService userBLL) : ViewModelBase
    {

        string email = "", password = "", btnSignInText = "Acessar";

        bool btnSignInEnabled = true;

        public string Email { get => email; set { if (email != value) { email = value; OnPropertyChanged(nameof(Email)); } } }

        public string Password { get => password; set { if (password != value) { password = value; OnPropertyChanged(nameof(Password)); } } }

        public string BtnSignInText { get => btnSignInText; set { if (btnSignInText != value) { btnSignInText = value; OnPropertyChanged(nameof(BtnSignInText)); } } }

        public bool BtnSignInEnabled { get => btnSignInEnabled; set { if (btnSignInEnabled != value) { btnSignInEnabled = value; OnPropertyChanged(nameof(BtnSignInEnabled)); } } }

        public ICommand SignUpCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(SignUp)}"));

        public ICommand UpdatePasswordCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(UpdatePassword)}"));

        public ICommand SignInCommand => new Command(async () =>
        {
            IsBusy = true;

            try
            {
                if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        if (Password.Length > 3)
                        {
                            btnSignInText = "Acessando...";
                            BtnSignInEnabled = false;

                            var resp = await userBLL.SignIn(Email, Password);

                            if (resp.Success)
                            {
                                if (resp.Content is not null and int)
                                    ((App)App.Current).Uid = (int)resp.Content;

                                await Shell.Current.GoToAsync($"{nameof(FirstSync)}", false);

                                //await Shell.Current.GoToAsync($"//{nameof(Main)}");

                            }
                            else
                            {
                                string errorMessage = "";

                                if (resp.Error == Models.Responses.ErrorTypes.WrongEmailOrPassword)
                                    errorMessage = "Email/senha incorretos";
                                else if (resp.Error == Models.Responses.ErrorTypes.ServerUnavaliable)
                                    errorMessage = "Servidor indisponível, favor entrar em contato com o desenvolvedor.";
                                else errorMessage = "Erro não mapeado, favor entrar em contato com o desenvolvedor.";

                                await Application.Current.MainPage.DisplayAlert("Aviso", errorMessage, null, "Ok");
                            }

                            BtnSignInEnabled = true;
                            btnSignInText = "Acessar";
                            IsBusy = false;
                        }
                        else
                            await Application.Current.MainPage.DisplayAlert("Aviso", "Digite uma senha com mais de 3 dígitos", null, "Continuar");
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Aviso", "É necessário ter acesso a internet para efetuar o acesso.", null, "Ok");
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Aviso", "Insira seu email e senha.", null, "Continuar");
            }
            catch (Exception ex)
            {
                throw;
            }

            IsBusy = false;
        });
    }
}
