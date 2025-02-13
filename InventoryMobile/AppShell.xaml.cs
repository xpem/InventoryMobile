﻿using InventoryMobile.ViewModels;
using InventoryMobile.Views;
using InventoryMobile.Views.Category;
using InventoryMobile.Views.Category.SubCategory;
using InventoryMobile.Views.Item;
using InventoryMobile.Views.Item.Selectors;

namespace InventoryMobile;

public partial class AppShell : Shell
{
    private readonly AppShellVM AppShellVM;

    public AppShell(AppShellVM appShellVM)
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(SignUp), typeof(SignUp));

        Routing.RegisterRoute(nameof(UpdatePassword), typeof(UpdatePassword));

        Routing.RegisterRoute(nameof(Main), typeof(Main));

        Routing.RegisterRoute(nameof(CategoryList), typeof(CategoryList));

        Routing.RegisterRoute(nameof(CategoryEdit), typeof(CategoryEdit));

        Routing.RegisterRoute(nameof(CategoryDisplay), typeof(CategoryDisplay));

        Routing.RegisterRoute(nameof(SubCategoryEdit), typeof(SubCategoryEdit));

        Routing.RegisterRoute(nameof(ItemEdit), typeof(ItemEdit));

        Routing.RegisterRoute(nameof(CategorySelector), typeof(CategorySelector));

        Routing.RegisterRoute(nameof(SubCategorySelector), typeof(SubCategorySelector));

        Routing.RegisterRoute(nameof(ItemDisplay), typeof(ItemDisplay));

        Routing.RegisterRoute(nameof(FirstSync), typeof(FirstSync));

        BindingContext = AppShellVM = appShellVM;
    }

    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        var previousRouteString = args?.Previous?.Location?.OriginalString;
        var currentRouteString = args?.Current?.Location?.OriginalString;

        if (previousRouteString != null && previousRouteString.Equals("//SignIn/FirstSyncProcess") &&
            currentRouteString.Equals("//Main"))
        {
            AppShellVM.AtualizaUser();
        }

        base.OnNavigated(args);
    }
}
