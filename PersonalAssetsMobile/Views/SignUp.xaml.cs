using PersonalAssetsMobile.ViewModels;

namespace PersonalAssetsMobile.Views;

public partial class SignUp : ContentPage
{
	public SignUp(SignUpVM signUpVM)
	{
		InitializeComponent();

		BindingContext= signUpVM;
	}
}