using InventoryMobile.ViewModels.Item;

namespace InventoryMobile.Views.Item;

public partial class ItemDisplay : ContentPage
{
	public ItemDisplay(ItemDisplayVM itemDisplayVM)
	{
		InitializeComponent();

		BindingContext = itemDisplayVM;
	}
}