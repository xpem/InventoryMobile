using PersonalAssetsMobile.Services.Interfaces;
using PersonalAssetsMobile.UIModels;
using PersonalAssetsMobile.ViewModels.Category;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels.Item.Selectors
{
    public class CategorySelectorVM : BindableObject
    {
        readonly ICategoryService categoryService;

        public CategorySelectorVM(ICategoryService _categoryService) { categoryService = _categoryService; }

        public ObservableCollection<UICategory> Categories { get; set; } = new();

        public ICommand OnAppearingCommand => new Command(async (e) =>
        {
            Categories = new();

            List<Models.Category> list = await categoryService.GetCategories();
            Categories.Add(new UICategory() { Id = 0, Name = "[Sem Categoria]", Color = Color.FromArgb("#2F9300") });

            if (list != null && list.Count > 0)
                foreach (var i in list)
                {
                    Categories.Add(new UICategory() { Id = i.Id, Name = i.Name, Color = Color.FromArgb(i.Color) });
                }

            OnPropertyChanged(nameof(Categories));
        });
    }
}
