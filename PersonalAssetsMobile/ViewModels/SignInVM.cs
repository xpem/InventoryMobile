using PersonalAssetsMobile.Views;
using Plugin.Connectivity;
using Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ICommand SignUpCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(SignUp)}"));

        public ICommand UpdatePasswordCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(UpdatePassword)}"));

        public ICommand SignInCommand => new Command(async () =>
        {
            IsBusy = true;

            try
            {
                if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                {
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        if (Password.Length > 3)
                        {
                            btnSignInText = "Acessando...";
                            BtnSignInEnabled = false;

                            BtnSignInEnabled = true;
                            btnSignInText = "Acessar";
                            IsBusy = false;
                        }
                    }
                }
            }
            catch { throw; }


            await Shell.Current.GoToAsync($"//{nameof(Main)}");

            IsBusy = false;
        });
    }
}
