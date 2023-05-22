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
            if (!string.IsNullOrEmpty(Preferences.Default.Get("EMAIL", string.Empty)))
                Email = Preferences.Default.Get("EMAIL", "");
        }
    }
}
