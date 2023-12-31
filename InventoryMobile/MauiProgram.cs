using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using InventoryMobile.Services;
using InventoryMobile.Services.Interfaces;
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
using BLL;
using DbContextDAL;
using DbContextDAL.Interface;
using BLL.Interface;
using ApiDAL.Interfaces;
using ApiDAL;

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

        #region UIServices

        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
        builder.Services.AddScoped<IAcquisitionTypeService, AcquisitionTypeService>();
        builder.Services.AddScoped<IItemService, ItemService>();

        #endregion

        builder.Services.AddDbContext<InventoryDbContextDAL>();

        #region BLL

        builder.Services.AddScoped<IBuildDbBLL, BuildDbBLL>();
        builder.Services.AddScoped<IUserBLL, UserBLL>();
        builder.Services.AddScoped<ICheckServerBLL, CheckServerBLL>();
        builder.Services.AddScoped<IItemBLL, ItemBLL>();
        builder.Services.AddScoped<IItemSituationBLL, ItemSituationBLL>();

        #endregion

        #region DAL

        builder.Services.AddScoped<IUserDAL, UserDAL>();
        builder.Services.AddScoped<IItemSituationDAL, ItemSituationDAL>();

        #endregion

        #region ApiDAL

        builder.Services.AddScoped<IHttpClientFunctions, HttpClientFunctions>();
        builder.Services.AddScoped<IUserApiDAL, UserApiDAL>();
        builder.Services.AddScoped<IItemDAL, ItemDAL>();

        #endregion

        #endregion


        return builder.Build();
    }
}
