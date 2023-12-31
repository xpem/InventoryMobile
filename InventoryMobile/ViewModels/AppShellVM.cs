using BLL.Interface;
using InventoryMobile.Views;
using Models;
using System.Windows.Input;

namespace InventoryMobile.ViewModels
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

        public AppShellVM(User user)
        {
            if (user is not null)
                Email = user.Email;
        }
    }
}
