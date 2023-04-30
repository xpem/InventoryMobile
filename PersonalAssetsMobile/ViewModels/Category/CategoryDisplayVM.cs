using BLL;
using PersonalAssetsMobile.Services;
using PersonalAssetsMobile.UIModels;
using PersonalAssetsMobile.Views.Category;
using PersonalAssetsMobile.Views.Category.SubCategory;
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

                    OnPropertyChanged(nameof(SystemDefault));
                }
            }
        }

        public ObservableCollection<UIModels.UISubCategory> SubCategoryObsCol { get; set; } = new();

        public ICommand CategoryEditCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(CategoryEdit)}?Id={Id}", true));

        public ICommand SubCategoryEditCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(SubCategoryEdit)}?CategoryId={Id}", true));

        public ICommand DeleteCategoryCommand => new Command(async (e) =>
        {
            if (SubCategoryObsCol.Count > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Não é possivel excluir uma categoria que tenha subcategorias relacionadas", null, "Ok");
            }
            else
            {
                if (await Application.Current.MainPage.DisplayAlert("Confirmação", "Deseja excluir esta Categoria?", "Sim", "Cancelar"))
                {
                    (var success, var message) = await categoryService.DelCategory(Id);

                    if (success)
                    {
                        if (!await Application.Current.MainPage.DisplayAlert("Aviso", "Categoria excluída!", null, "Ok"))
                            await Shell.Current.GoToAsync("..");
                    }
                    else
                    {
                        if (message != null)
                            await Application.Current.MainPage.DisplayAlert("Aviso", message, null, "Ok");
                        else
                            throw new Exception("Houve um erro ao tentar excluir A Categoria");
                    }
                }
            }
        });

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Id = Convert.ToInt32(query["Id"]);
        }

        readonly ICategoryService categoryService;

        public CategoryDisplayVM(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        public ICommand OnAppearingCommand => new Command(async (e) =>
        {
            Models.Category category = await categoryService.GetCategoryById(Id);

            //Category no longer exists in the db
            if(category == null) await Shell.Current.GoToAsync("..");

            CategoryColor = Color.FromArgb(category.Color);
            Name = category.Name;
            SystemDefault = category.SystemDefault != 1;
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
