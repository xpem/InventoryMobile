using PersonalAssetsMobile.Views;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels
{
    public class AppShellVM : ViewModelBase
    {
        public ICommand SignOutCommand => new Command(async (e) =>
        {
            //clean user info stored info.
            Preferences.Default.Clear();
            SecureStorage.Default.RemoveAll();
            
            await Shell.Current.GoToAsync($"//{nameof(SignIn)}");
        });
    }
}
