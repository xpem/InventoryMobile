using PersonalAssetsMobile.Resources.Fonts.Icons;
using PersonalAssetsMobile.Services.Interfaces;
using PersonalAssetsMobile.Utils;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels.Category.SubCategory
{
    public class SubCategoryEditVM : ViewModelBase, IQueryAttributable
    {
        readonly ISubCategoryService subCategoryService;

        public SubCategoryEditVM(ISubCategoryService subCategoryService)
        {
            this.subCategoryService = subCategoryService;
        }

        int CategoryId, Id;

        //Color categoryColor;

        //public Color CategoryColor
        //{
        //    get => categoryColor; set
        //    {
        //        if (categoryColor != value)
        //        {
        //            categoryColor = value;

        //            OnPropertyChanged(nameof(CategoryColor));
        //        }
        //    }
        //}

        string categoryName, name, icon;

        public string CategoryName
        {
            get => categoryName; set
            {
                if (categoryName != value)
                {
                    categoryName = value;

                    OnPropertyChanged(nameof(CategoryName));
                }
            }
        }

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

        public string Icon
        {
            get => icon; set
            {
                if (icon != value)
                {
                    icon = value;

                    OnPropertyChanged(nameof(Icon));
                }
            }
        }

        bool iconPickerVisible;

        public bool IconPickerVisible
        {
            get => iconPickerVisible; set
            {
                if (iconPickerVisible != value)
                {
                    iconPickerVisible = value;

                    OnPropertyChanged(nameof(IconPickerVisible));
                }
            }
        }

        bool buttonIconVisible, btnInsertIsEnabled = true;

        public bool ButtonIconVisible
        {
            get => buttonIconVisible;
            set
            {
                if (buttonIconVisible != value)
                {
                    buttonIconVisible = value;

                    OnPropertyChanged(nameof(ButtonIconVisible));
                }
            }
        }

        string btnConfirmationIcon, btnConfirmationText;

        public string BtnConfirmationIcon
        {
            get => btnConfirmationIcon;
            set
            {
                if (btnConfirmationIcon != value)
                {
                    btnConfirmationIcon = value;

                    OnPropertyChanged(nameof(BtnConfirmationIcon));
                }
            }
        }

        public string BtnConfirmationText
        {
            get => btnConfirmationText;
            set
            {
                if (btnConfirmationText != value)
                {
                    btnConfirmationText = value;

                    OnPropertyChanged(nameof(BtnConfirmationText));
                }
            }
        }

        public bool BtnInsertIsEnabled { get => btnInsertIsEnabled; set { if (value != btnInsertIsEnabled) { btnInsertIsEnabled = value; OnPropertyChanged(nameof(BtnInsertIsEnabled)); } } }

        public ICommand ShowIconPickerCommand => new Command(() => ShowIconPicker());

        public ICommand DefineIconCommand => new Command((e) => DefineIcon(e as string));

        public ICommand AddCommand => new Command(async (e) => await InsertSubCategory());

        public async Task InsertSubCategory()
        {
            try
            {
                if (await VerifyFields())
                {
                    BtnInsertIsEnabled = false;

                    Models.SubCategory subCategory = new()
                    {
                        Name = Name,
                        IconName = SubCategoryIconsList.GetIconName(icon),
                        CategoryId = CategoryId
                    };

                    string message = "";

                    if (Id > 0)
                    {
                        subCategory.Id = Id;

                        (_, message) = await subCategoryService.AltSubCategory(subCategory);
                    }
                    else
                        (_, message) = await subCategoryService.AddSubcategory(subCategory);

                    bool resposta = await Application.Current.MainPage.DisplayAlert("Aviso", message, null, "Ok");

                    if (!resposta)
                        await Shell.Current.GoToAsync("..");


                    BtnInsertIsEnabled = true;
                }

            }
            catch (Exception ex) { throw ex; }
        }

        private async Task<bool> VerifyFields()
        {
            bool valid = true;

            if (string.IsNullOrEmpty(Name))
            {
                valid = false;
                _ = await Application.Current.MainPage.DisplayAlert("Aviso", "Digite um Nome válido", null, "Ok");
            }

            return valid;
        }

        public void ShowIconPicker()
        {
            IconPickerVisible = true;
            ButtonIconVisible = false;
            Icon = Icons.Tag;
        }

        public void DefineIcon(string iconDefinition)
        {
            Icon = iconDefinition;
            IconPickerVisible = false;
            ButtonIconVisible = true;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            IconPickerVisible = false;
            ButtonIconVisible = true;

            if (query.TryGetValue("Id", out object _id))
                Id = Convert.ToInt32(_id);

            if (query.TryGetValue("Category", out object value))
            {
                var category = value as Models.Category;
                CategoryName = category.Name;
                CategoryId = category.Id;
            }

            if (Id != 0)
            {
                Models.SubCategory subcategory = await subCategoryService.GetSubCategoryById(Id);

                Name = subcategory.Name;
                CategoryId = subcategory.CategoryId;
                Icon = subcategory.IconName;

                BtnConfirmationText = "Alterar";
                BtnConfirmationIcon = Icons.Pen;
            }
            else
            {
                BtnConfirmationText = "Cadastrar";
                BtnConfirmationIcon = Icons.Plus;
            }

            Icon ??= Icons.Tag;
        }
    }
}
