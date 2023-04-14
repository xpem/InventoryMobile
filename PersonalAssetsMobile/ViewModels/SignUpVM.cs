using Models;
using PersonalAssetsMobile.Utils;
using Plugin.Connectivity;
using Services.User;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels
{
    public class SignUpVM : ViewModelBase
    {

        string name;

        public string Name { get => name; set { if (name != value) { name = value; OnPropertyChanged(nameof(Name)); } } }

        string email;

        public string Email { get => email; set { if (email != value) { email = value; OnPropertyChanged(nameof(Email)); } } }

        string password;

        public string Password { get => password; set { if (password != value) { password = value; OnPropertyChanged(nameof(Password)); } } }

        string confirmPassword;

        public string ConfirmPassword { get => confirmPassword; set { if (confirmPassword != value) { confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); } } }

        bool btnIsEnabled = true;

        public bool BtnIsEnabled { get => btnIsEnabled; set { if (btnIsEnabled != value) { btnIsEnabled = value; OnPropertyChanged(nameof(BtnIsEnabled)); } } }

        public ICommand AddUserCommand => new Command(async () =>
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                _ = await Application.Current.MainPage.DisplayAlert("Aviso", "Sem conexão com a internet", null, "Ok");
                return;
            }

            if (VerifyFileds())
            {
                btnIsEnabled = false;

                //
                User user = await UserService.AddUser(name, email, password);

                if (user != null)
                {
                    if (user.Error != null)
                    {
                        if (user.Error == ErrorType.Email_Exists)
                            await Application.Current.MainPage.DisplayAlert("Aviso", "Email já cadastrado!", null, "Ok");
                        else
                            await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possível cadastrar o usuário!", null, "Ok");
                    }
                    else
                    {
                        bool res = await Application.Current.MainPage.DisplayAlert("Aviso", "Usuário cadastrado!", null, "Ok");

                        if (!res)
                            await Shell.Current.GoToAsync("..");
                    }
                }
            }
        });

        private bool VerifyFileds()
        {
            bool validInformation = true;

            if (string.IsNullOrEmpty(Name))
                validInformation = false;

            if (string.IsNullOrEmpty(Email))
                validInformation = false;
            else if (!Validations.ValidateEmail(Email))
            {
                _ = Application.Current.MainPage.DisplayAlert("Aviso", "Digite um email válido", null, "Ok");
                return false;
            }

            if (string.IsNullOrEmpty(Password))
                validInformation = false;
            else if (Password.Length < 4)
                validInformation = false;

            if (string.IsNullOrEmpty(ConfirmPassword))
                validInformation = false;
            else if (ConfirmPassword.ToUpper() != Password.ToUpper())
                validInformation = false;

            if (!validInformation)
                _ = Application.Current.MainPage.DisplayAlert("Aviso", "Preencha os campos e confirme a senha corretamente", null, "Ok");


            return validInformation;
        }
    }
}
