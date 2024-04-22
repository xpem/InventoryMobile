using BLL.Interface;
using InventoryMobile.Views;
using Models.DTO;
using System.Windows.Input;

namespace InventoryMobile.ViewModels
{
    public class AppShellVM : BindableObject//: ViewModelBase
    {
        IUserBLL UserBLL { get; set; }

        string email;

        public string Email { get => email; set { if (email != value) { email = value; OnPropertyChanged(nameof(Email)); } } }

        public ICommand SignOutCommand => new Command(async (e) =>
        {
            //clean user info stored info.
            UserBLL.RemoveUserLocal();

            await Shell.Current.GoToAsync($"//{nameof(SignIn)}");
        });

        public AppShellVM(IUserBLL userBLL,User user)
        {
            UserBLL = userBLL;

            if (user is not null)
                Email = user.Email;
        }
    }
}
