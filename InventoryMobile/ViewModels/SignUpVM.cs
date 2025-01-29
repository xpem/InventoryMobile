using InventoryMobile.Utils;
using Services.Interface;
using System.Windows.Input;

namespace InventoryMobile.ViewModels
{
    public class SignUpVM(IUserService userBLL) : ViewModelBase
    {
        string name, email, password, confirmPassword;
        bool btnIsEnabled = true;

        public string Name { get => name; set { if (name != value) { name = value; OnPropertyChanged(nameof(Name)); } } }

        public string Email { get => email; set { if (email != value) { email = value; OnPropertyChanged(nameof(Email)); } } }

        public string Password { get => password; set { if (password != value) { password = value; OnPropertyChanged(nameof(Password)); } } }

        public string ConfirmPassword { get => confirmPassword; set { if (confirmPassword != value) { confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); } } }

        public bool BtnIsEnabled { get => btnIsEnabled; set { if (btnIsEnabled != value) { btnIsEnabled = value; OnPropertyChanged(nameof(BtnIsEnabled)); } } }

        public ICommand SignUpCommand => new Command(async () =>
        {
            if (!(Connectivity.NetworkAccess == NetworkAccess.Internet))
            {
                _ = await Application.Current.MainPage.DisplayAlert("Aviso", "Sem conexão com a internet", null, "Ok");
                return;
            }

            if (VerifyFileds())
            {
                btnIsEnabled = false;

                //
                Models.Responses.ServResp resp = userBLL.AddUser(name, email, password);

                if (!resp.Success)
                    await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possível cadastrar o usuário!", null, "Ok");
                else
                {
                    bool res = await Application.Current.MainPage.DisplayAlert("Aviso", "Usuário cadastrado!", null, "Ok");

                    if (!res)
                        await Shell.Current.GoToAsync("..");
                }
            }
        });

        private bool VerifyFileds()
        {
            //usar contracts??

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
            else if (!ConfirmPassword.Equals(Password, StringComparison.CurrentCultureIgnoreCase))
                validInformation = false;

            if (!validInformation)
                _ = Application.Current.MainPage.DisplayAlert("Aviso", "Preencha os campos e confirme a senha corretamente", null, "Ok");

            return validInformation;
        }

    }
}
