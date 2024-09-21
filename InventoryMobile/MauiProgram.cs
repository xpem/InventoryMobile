using ApiDAL;
using ApiDAL.Interfaces;
using BLL;
using BLL.Interface;
using CommunityToolkit.Maui;
using DbContextDAL;
using DbContextDAL.Interface;
using InventoryMobile.ViewModels;
using InventoryMobile.ViewModels.Category;
using InventoryMobile.ViewModels.Category.SubCategory;
using InventoryMobile.ViewModels.Item;
using InventoryMobile.ViewModels.Item.Selectors;
using InventoryMobile.Views;
using InventoryMobile.Views.Category;
using InventoryMobile.Views.Category.SubCategory;
using InventoryMobile.Views.Item;
using InventoryMobile.Views.Item.Selectors;
using Microsoft.Extensions.Logging;
using Models;

namespace InventoryMobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        //todo
        //
        //efetuar build de distribuição local do app para testes
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

        if (!System.IO.Directory.Exists(FilePaths.ImagesPath))
            System.IO.Directory.CreateDirectory(FilePaths.ImagesPath);

        #region Dependency injections

        builder.Services.AddFrontServices();

        builder.Services.AddDbContext<InventoryDbContextDAL>();

        builder.Services.AddBLLServices();

        #region DAL

        builder.Services.AddScoped<IUserDAL, UserDAL>();

        #endregion

        builder.Services.AddApiDALServices();

        #endregion


        return builder.Build();
    }

    public static IServiceCollection AddFrontServices(this IServiceCollection services)
    {
        services.AddTransient<AppShell>();
        services.AddTransient<AppShellVM>();

        services.AddTransient<SignIn>();
        services.AddTransient<SignInVM>();

        services.AddTransient<SignUp>();
        services.AddTransient<SignUpVM>();

        services.AddTransient<UpdatePassword>();
        services.AddTransient<UpdatePasswordVM>();

        services.AddTransient<Main>();
        services.AddTransient<MainVM>();

        services.AddTransient<CategoryList>();
        services.AddTransient<CategoryListVM>();

        services.AddTransient<CategoryEdit>();
        services.AddTransient<CategoryEditVM>();

        services.AddTransient<CategoryDisplay>();
        services.AddTransient<CategoryDisplayVM>();

        services.AddTransient<SubCategoryEdit>();
        services.AddTransient<SubCategoryEditVM>();

        services.AddTransient<ItemEdit>();
        services.AddTransient<ItemEditVM>();

        services.AddTransient<CategorySelector>();
        services.AddTransient<CategorySelectorVM>();

        services.AddTransient<SubCategorySelector>();
        services.AddTransient<SubCategorySelectorVM>();

        services.AddTransient<ItemDisplay>();
        services.AddTransient<ItemDisplayVM>();

        return services;
    }

    public static IServiceCollection AddApiDALServices(this IServiceCollection services)
    {
        services.AddScoped<IHttpClientFunctions, HttpClientFunctions>();
        services.AddScoped<IHttpClientWithFileFunctions, HttpClientWithFileFunctions>();

        services.AddScoped<IUserApiDAL, UserApiDAL>();
        services.AddScoped<IItemApiDAL, ItemApiDAL>();
        services.AddScoped<IItemSituationApiDAL, ItemSituationApiDAL>();
        services.AddScoped<ICategoryApiDAL, CategoryApiDAL>();
        services.AddScoped<ISubCategoryApiDAL, SubCategoryApiDAL>();
        services.AddScoped<IAcquisitionTypeApiDAL, AcquisitionTypeApiDAL>();

        return services;
    }

    public static IServiceCollection AddBLLServices(this IServiceCollection services)
    {
        services.AddScoped<IBuildDbBLL, BuildDbBLL>();
        services.AddScoped<IUserBLL, UserBLL>();
        services.AddScoped<ICheckServerBLL, CheckServerBLL>();
        services.AddScoped<IItemBLL, ItemBLL>();
        services.AddScoped<IItemSituationBLL, ItemSituationBLL>();
        services.AddScoped<ICategoryBLL, CategoryBLL>();
        services.AddScoped<ISubCategoryBLL, SubCategoryBLL>();
        services.AddScoped<IAcquisitionTypeBLL, AcquisitionTypeBLL>();

        return services;
    }


}
