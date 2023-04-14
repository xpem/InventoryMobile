using PersonalAssetsMobile.UIModels;
using PersonalAssetsMobile.Views.Category;
using Services.Category;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels.Category
{
    public class CategoryListVM : ViewModelBase
    {
        public ObservableCollection<CategoryUI> Categories { get; set; } = new();

        public ICommand CategoryAddCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(CategoryEdit)}"));

        readonly ICategoryServices categoryServices;

        public CategoryListVM(ICategoryServices _categoryServices)
        {
            categoryServices = _categoryServices;
        }

        public ICommand OnAppearingCommand => new Command(async (e) =>
        {
            Categories = new();
            foreach (var i in await categoryServices.GetCategoriesAsync())
            {
                Categories.Add(new CategoryUI() { Id = i.Id, Name = i.Name, Color = Color.FromArgb(i.Color) });
            }
            OnPropertyChanged(nameof(Categories));
        });
    }
}
