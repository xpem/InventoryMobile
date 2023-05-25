using PersonalAssetsMobile.Services.Interfaces;
using PersonalAssetsMobile.UIModels;
using PersonalAssetsMobile.ViewModels.Category;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels.Item.Selectors
{
    public class CategorySelectorVM : ViewModelBase
    {
        readonly ICategoryService categoryService;

        public CategorySelectorVM(ICategoryService _categoryService) { categoryService = _categoryService; }

        public ObservableCollection<UICategory> CategoriesObsList { get; set; }

        public List<Models.Category> Categorylist { get; set; }

        public ICommand OnAppearingCommand => new Command(async (e) =>
        {
            CategoriesObsList = new();
            Categorylist = await categoryService.GetCategoriesWithSubCategories();
            CategoriesObsList.Add(new UICategory() { Id = -1, Name = "[Sem Categoria]", Color = Color.FromArgb("#2F9300"), HaveSubcategories = false });

            if (Categorylist != null && Categorylist.Count > 0)
                foreach (var i in Categorylist)
                    CategoriesObsList.Add(new UICategory() { Id = i.Id, Name = i.Name, Color = Color.FromArgb(i.Color), HaveSubcategories = i.SubCategories.Count > 0 });


            OnPropertyChanged(nameof(CategoriesObsList));
        });
    }
}
