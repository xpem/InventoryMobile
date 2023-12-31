using InventoryMobile.ViewModels;

namespace InventoryMobile.Views;

public partial class SignIn : ContentPage
{
	public SignIn(SignInVM signInVM)
	{
		InitializeComponent();

        BindingContext = signInVM;
    }
}