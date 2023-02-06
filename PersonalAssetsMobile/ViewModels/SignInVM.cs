using PersonalAssetsMobile.Views;
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

        string email, password;

        public string Email { get => email; set { if (email != value) { email = value; OnPropertyChanged(); } } }
        public string Password { get => password; set { if (password != value) { password = value; OnPropertyChanged(); } } }

        public ICommand SignUpCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(SignUp)}"));

        public ICommand UpdatePasswordCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(UpdatePassword)}"));

        public ICommand SignInCommand => new Command(async () =>
        {
            IsBusy = true;

            await Shell.Current.GoToAsync($"//{nameof(Main)}");

            IsBusy = false;

        });

    }
}
