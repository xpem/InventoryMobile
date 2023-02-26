using PersonalAssetsMobile.UIModels;
using PersonalAssetsMobile.Views.Category;
using Services.Category;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels.Category
{
    public class CategoryListVM : ViewModelBase
    {
        public ObservableCollection<UICategory> Categories { get; } = new();

        public ICommand CategoryEditCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(CategoryEdit)}"));

        readonly ICategoryServices categoryServices;

        public CategoryListVM(ICategoryServices _categoryServices)
        {
            categoryServices = _categoryServices;
        }

        public ICommand OnAppearingCommand => new Command(async (e) =>
        {
            foreach (var i in await categoryServices.GetCategoriesAsync())
            {
                Categories.Add(new UICategory() { Id = i.Id, Name = i.Name, Color = Color.FromArgb(i.Color) });
            }
        });
    }
}
