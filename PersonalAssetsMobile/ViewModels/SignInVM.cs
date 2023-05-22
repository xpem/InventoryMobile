using PersonalAssetsMobile.Services;
using PersonalAssetsMobile.Views;
using Plugin.Connectivity;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels
{
    public class SignInVM : ViewModelBase
    {

        string email, password, btnSignInText = "Acessar";

        bool btnSignInEnabled = false;

        public string Email { get => email; set { if (email != value) { email = value; OnPropertyChanged(nameof(Email)); } } }

        public string Password { get => password; set { if (password != value) { password = value; OnPropertyChanged(nameof(Password)); } } }

        public string BtnSignInText { get => btnSignInText; set { if (btnSignInText != value) { btnSignInText = value; OnPropertyChanged(nameof(BtnSignInText)); } } }

        public bool BtnSignInEnabled { get => btnSignInEnabled; set { if (btnSignInEnabled != value) { btnSignInEnabled = value; OnPropertyChanged(nameof(BtnSignInEnabled)); } } }


        public SignInVM()
        {
            Email = Password = "";
        }

        public ICommand SignUpCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(SignUp)}"));

        public ICommand UpdatePasswordCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(UpdatePassword)}"));

        public ICommand SignInCommand => new Command(async () =>
        {
            IsBusy = true;

            try
            {
                if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                {
                    if (isOn)
                    {
                        if (Password.Length > 3)
                        {
                            btnSignInText = "Acessando...";
                            BtnSignInEnabled = false;

                            string message = await UserService.SignIn(email, password);

                            if (message is null)
                                await Shell.Current.GoToAsync($"//{nameof(Main)}");
                            else
                                await Application.Current.MainPage.DisplayAlert("Aviso", message, null, "Ok");

                            BtnSignInEnabled = true;
                            btnSignInText = "Acessar";
                            IsBusy = false;
                        }
                        else
                            await Application.Current.MainPage.DisplayAlert("Aviso", "Digite uma senha com mais de 3 dígitos", null, "Continuar");
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Aviso", "É necessário ter acesso a internet para efetuar o primeiro acesso.", null, "Ok");
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Aviso", "Insira seu email e senha.", null, "Continuar");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            IsBusy = false;
        });
    }
}
