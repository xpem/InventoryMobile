using PersonalAssetsMobile.ViewModels.Category;

namespace PersonalAssetsMobile.Views.Category;

public partial class CategoryDisplay : ContentPage
{
	public CategoryDisplay(CategoryDisplayVM categoryDisplayVM)
	{
		InitializeComponent();

		BindingContext = categoryDisplayVM;
	}
}