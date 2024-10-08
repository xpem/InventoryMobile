using InventoryMobile.UIModels;
using InventoryMobile.ViewModels;
using InventoryMobile.Views.Item;

namespace InventoryMobile.Views;

public partial class Main : ContentPage
{
    private readonly MainVM vm;

    public Main(MainVM mainVM)
    {
        InitializeComponent();
        BindingContext = vm = mainVM;
    }

    private void BtnItemSituationSelected_Clicked(object sender, EventArgs e)
    {
        var view = sender as View;
        vm.ItemSituationSelectdCommand.Execute(view.BindingContext);
    }

    private void ItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var TappedItem = e.Item as UIItem;

        //Shell.Current.GoToAsync($"{nameof(ItemEdit)}?Id={TappedItem.Id}", true);
        Shell.Current.GoToAsync($"{nameof(ItemDisplay)}?Id={TappedItem.Id}", true);
    }

    private async void ViewCell_Tapped(object sender, EventArgs e)
    {
        var cell = sender as ViewCell;
        cell.View.Opacity = 0.5;
        await cell.View.FadeTo(0.5, 2000);
        _ = cell.View.FadeTo(1, 1000).ConfigureAwait(false);
    }

    //private void BtnCategorySelected_Clicked(object sender, EventArgs e)
    //{
    //    var view = sender as View;
    //    vm.CategorySelectedCommand.Execute(view.BindingContext);
    //}
}