using PersonalAssetsMobile.ViewModels.Item.Selectors;

namespace PersonalAssetsMobile.Views.Item;

public partial class CategorySelector : ContentPage
{
	public CategorySelector(CategorySelectorVM categorySelectorVM)
	{
		InitializeComponent();

        BindingContext = categorySelectorVM;
	}

    //private async void Button_Clicked(object sender, EventArgs e)
    //{
    //    await Shell.Current.GoToAsync($"..?SelectedCategory=Teste");
    //}

    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
    }
}