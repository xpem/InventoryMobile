using PersonalAssetsMobile.ViewModels;

namespace PersonalAssetsMobile.Views;

public partial class SignIn : ContentPage
{
	public SignIn(SignInVM signInVM)
	{
		InitializeComponent();

        BindingContext = signInVM;
    }
}