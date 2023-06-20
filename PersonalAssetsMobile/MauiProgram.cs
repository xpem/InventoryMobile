using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PersonalAssetsMobile.Services;
using PersonalAssetsMobile.Services.Interfaces;
using PersonalAssetsMobile.ViewModels;
using PersonalAssetsMobile.ViewModels.Category;
using PersonalAssetsMobile.ViewModels.Category.SubCategory;
using PersonalAssetsMobile.ViewModels.Item;
using PersonalAssetsMobile.ViewModels.Item.Selectors;
using PersonalAssetsMobile.Views;
using PersonalAssetsMobile.Views.Category;
using PersonalAssetsMobile.Views.Category.SubCategory;
using PersonalAssetsMobile.Views.Item;
using PersonalAssetsMobile.Views.Item.Selectors;

namespace PersonalAssetsMobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        //todo
        //
        //cadastro de items com dependencia
        //implementar um dark theme padrão
        //implementar auto complete no campo "loja"
        //implementar mecanismo para funcionamento offline.
        //opção de cadastro de categoria e subcategoria nas telas de seleção
        //tela com filtros?

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("FA6Free-Solid-900.otf", "Icons");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        #region Dependency injections

        #region front

        builder.Services.AddTransient<AppShell>();
        builder.Services.AddTransient<AppShellVM>();

        builder.Services.AddTransient<SignIn>();
        builder.Services.AddTransient<SignInVM>();

        builder.Services.AddTransient<SignUp>();
        builder.Services.AddTransient<SignUpVM>();

        builder.Services.AddTransient<UpdatePassword>();
        builder.Services.AddTransient<UpdatePasswordVM>();

        builder.Services.AddTransient<Main>();
        builder.Services.AddTransient<MainVM>();

        builder.Services.AddTransient<CategoryList>();
        builder.Services.AddTransient<CategoryListVM>();

        builder.Services.AddTransient<CategoryEdit>();
        builder.Services.AddTransient<CategoryEditVM>();

        builder.Services.AddTransient<CategoryDisplay>();
        builder.Services.AddTransient<CategoryDisplayVM>();

        builder.Services.AddTransient<SubCategoryEdit>();
        builder.Services.AddTransient<SubCategoryEditVM>();

        builder.Services.AddTransient<ItemEdit>();
        builder.Services.AddTransient<ItemEditVM>();

        builder.Services.AddTransient<CategorySelector>();
        builder.Services.AddTransient<CategorySelectorVM>();

        builder.Services.AddTransient<SubCategorySelector>();
        builder.Services.AddTransient<SubCategorySelectorVM>();

        #endregion

        #region services

        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
        builder.Services.AddScoped<IItemSituationService, ItemSituationService>();
        builder.Services.AddScoped<IAcquisitionTypeService, AcquisitionTypeService>();
        builder.Services.AddScoped<IItemService, ItemService>();
        #endregion

        #endregion


        return builder.Build();
    }
}
