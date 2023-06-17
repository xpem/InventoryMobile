using PersonalAssetsMobile.Views;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels
{
    public class AppShellVM : BindableObject//: ViewModelBase
    {

        string email;

        public string Email { get => email; set { if (email != value) { email = value; OnPropertyChanged(nameof(Email)); } } }

        public ICommand SignOutCommand => new Command(async (e) =>
        {
            //clean user info stored info.
            Preferences.Default.Clear();
            SecureStorage.Default.RemoveAll();

            await Shell.Current.GoToAsync($"//{nameof(SignIn)}");
        });

        public AppShellVM()
        {
            var user = LocalDbDAL.UserLocalDAl.GetUser().Result;

            if (user is not null)
                Email = user.Email;
        }
    }
}
