using PersonalAssetsMobile.Views;
using PersonalAssetsMobile.Views.Category;
using PersonalAssetsMobile.Views.Item;

namespace PersonalAssetsMobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(SignUp), typeof(SignUp));

        Routing.RegisterRoute(nameof(UpdatePassword), typeof(UpdatePassword));

        Routing.RegisterRoute(nameof(Main), typeof(Main));

        Routing.RegisterRoute(nameof(CategoryList), typeof(CategoryList));

        Routing.RegisterRoute(nameof(CategoryEdit),typeof(CategoryEdit));

        Routing.RegisterRoute(nameof(ItemEdit), typeof(ItemEdit));

    }
}
