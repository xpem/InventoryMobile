using BLL;
using PersonalAssetsMobile.Services;
using PersonalAssetsMobile.UIModels;
using PersonalAssetsMobile.Views.Category;
using PersonalAssetsMobile.Views.Category.SubCategory;
using Services.Category;
using Services.Category.SubCategory;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels.Category
{
    public class CategoryDisplayVM : ViewModelBase, IQueryAttributable
    {
        int Id;

        Color categoryColor;

        public Color CategoryColor
        {
            get => categoryColor; set
            {
                if (categoryColor != value)
                {
                    categoryColor = value;

                    OnPropertyChanged(nameof(CategoryColor));
                }
            }
        }

        string name;

        public string Name
        {
            get => name; set
            {
                if (name != value)
                {
                    name = value;

                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        bool systemDefault;

        public bool SystemDefault
        {
            get => systemDefault; set
            {
                if (systemDefault != value)
                {
                    systemDefault = value;

                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public ObservableCollection<UIModels.UISubCategory> SubCategoryObsCol { get; set; } = new();

        public ICommand CategoryEditCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(CategoryEdit)}?Id={Id}", true));

        public ICommand SubCategoryEditCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(SubCategoryEdit)}?CategoryId={Id}", true));

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Id = Convert.ToInt32(query["Id"]);
        }

        public ICommand OnAppearingCommand => new Command(async (e) =>
        {
            Models.Category category = await CategoryService.GetCategoryById(Id);

            CategoryColor = Color.FromArgb(category.Color);
            Name = category.Name;
            SystemDefault = category.SystemDefault == 1;
            SubCategoryObsCol = new();

            List<Models.SubCategory> subCategoryList = new();//await subCategoryServices.GetSubCategoriesAsync(Id);

            foreach (var subCategory in subCategoryList)
            {
                SubCategoryObsCol.Add(new UIModels.UISubCategory() { Id = subCategory.Id, Icon = subCategory.Icon, Name = subCategory.Name, SystemDefault = !subCategory.SystemDefault });
            }

            OnPropertyChanged(nameof(SubCategoryObsCol));
        });
    }
}
