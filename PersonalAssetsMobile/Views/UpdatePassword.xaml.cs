using InventoryMobile.ViewModels;

namespace InventoryMobile.Views;

public partial class UpdatePassword : ContentPage
{
    public UpdatePassword(UpdatePasswordVM updatePasswordVM)
    {
        InitializeComponent();
        BindingContext = updatePasswordVM;
    }
}