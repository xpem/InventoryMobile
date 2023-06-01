using Models;
using PersonalAssetsMobile.UIModels;
using PersonalAssetsMobile.Utils;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels.Item.Selectors
{
    public class SubCategorySelectorVM : ViewModelBase, IQueryAttributable
    {
        int CategoryId;

        Color categoryColor;

        public Color CategoryColor
        {
            get => categoryColor; set { if (categoryColor != value) { categoryColor = value; OnPropertyChanged(nameof(CategoryColor)); } }
        }

        string categoryName;

        public string CategoryName
        {
            get => categoryName; set { if (categoryName != value) { categoryName = value; OnPropertyChanged(nameof(CategoryName)); } }
        }

        public ObservableCollection<UISubCategory> SubCategoryObsList { get; set; }

        public Models.Category Category { get; set; }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Category", out object value))
            {
                Category = value as Models.Category;

                CategoryName = Category.Name;
                CategoryColor = Color.FromArgb(Category.Color);
                CategoryId = Category.Id;

                SubCategoryObsList = new();

                if (Category.SubCategories != null && Category.SubCategories.Count > 0)
                    foreach (var subCategory in Category.SubCategories)
                        SubCategoryObsList.Add(
                            new UISubCategory()
                            {
                                Id = subCategory.Id,
                                Icon = SubCategoryIconsList.GetIconCode(subCategory.IconName),
                                Name = subCategory.Name,
                                SystemDefault = subCategory.SystemDefault != 1
                            });

                OnPropertyChanged(nameof(SubCategoryObsList));
            }
        }

        public ICommand SelectCategoryCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync($"../..", true, new Dictionary<string, object> { { "SelectedCategory", new Models.Category() { Id = CategoryId, Name = categoryName } } });
        });

        //Shell.Current.GoToAsync($"{nameof(SubCategorySelector)}", true, new Dictionary<string, object> { { "Category", categoryObj

    }
}
