using PersonalAssetsMobile.Models;
using PersonalAssetsMobile.ViewModels;
using System.Collections.ObjectModel;

namespace PersonalAssetsMobile.Views;

public partial class CategoryList : ContentPage
{



    public CategoryList(CategoryListVM categoryListVM)
	{
		InitializeComponent();

		BindingContext= categoryListVM;
    }
}