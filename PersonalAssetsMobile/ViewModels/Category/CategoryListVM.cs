using PersonalAssetsMobile.Services;
using PersonalAssetsMobile.UIModels;
using PersonalAssetsMobile.Views.Category;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels.Category
{
    public class CategoryListVM : ViewModelBase
    {
        public ObservableCollection<UICategory> Categories { get; set; } = new();

        public ICommand CategoryAddCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(CategoryEdit)}"));

        readonly ICategoryService categoryService;

        public CategoryListVM(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        public ICommand OnAppearingCommand => new Command(async (e) =>
        {
            Categories = new();

            List<Models.Category> list = await categoryService.GetCategories();

            foreach (var i in list)
            {
                Categories.Add(new UICategory() { Id = i.Id, Name = i.Name, Color = Color.FromArgb(i.Color) });
            }

            OnPropertyChanged(nameof(Categories));
        });
    }
}
