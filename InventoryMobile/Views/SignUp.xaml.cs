using InventoryMobile.ViewModels;

namespace InventoryMobile.Views;

public partial class SignUp : ContentPage
{
	public SignUp(SignUpVM signUpVM)
	{
		InitializeComponent();

		BindingContext= signUpVM;
	}
}