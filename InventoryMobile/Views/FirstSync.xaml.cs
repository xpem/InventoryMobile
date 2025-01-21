using InventoryMobile.ViewModels;

namespace InventoryMobile.Views;

public partial class FirstSync : ContentPage
{
	public FirstSync(FirstSyncVM firstSyncVM)
	{
		InitializeComponent();

		BindingContext = firstSyncVM;
	}
}