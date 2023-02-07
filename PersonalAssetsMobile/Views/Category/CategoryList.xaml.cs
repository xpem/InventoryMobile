using PersonalAssetsMobile.ViewModels;

namespace PersonalAssetsMobile.Views;

public partial class CategoryList : ContentPage
{



    public CategoryList(CategoryListVM categoryListVM)
	{
		InitializeComponent();

		BindingContext= categoryListVM;
    }
}