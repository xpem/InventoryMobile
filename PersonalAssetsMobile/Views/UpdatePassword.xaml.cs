using PersonalAssetsMobile.ViewModels;

namespace PersonalAssetsMobile.Views;

public partial class UpdatePassword : ContentPage
{
    public UpdatePassword(UpdatePasswordVM updatePasswordVM)
    {
        InitializeComponent();
        BindingContext = updatePasswordVM;
    }
}