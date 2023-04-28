using PersonalAssetsMobile.Resources.Fonts.Icons;
using PersonalAssetsMobile.Services;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels.Category
{
    public class CategoryEditVM : ViewModelBase, IQueryAttributable
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

        string name, btnInsertText, btnInsertIcon;

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

        public string BtnInsertText { get => btnInsertText; set { if (value != btnInsertText) { btnInsertText = value; OnPropertyChanged(nameof(BtnInsertText)); } } }

        public string BtnInsertIcon { get => btnInsertIcon; set { if (value != btnInsertIcon) { btnInsertIcon = value; OnPropertyChanged(nameof(BtnInsertIcon)); } } }

        bool colorPickerVisible, buttonColorVisible, btnInsertIsEnabled = true;

        public bool ColorPickerVisible
        {
            get => colorPickerVisible; set
            {
                if (colorPickerVisible != value)
                {
                    colorPickerVisible = value;

                    OnPropertyChanged(nameof(ColorPickerVisible));
                }
            }
        }

        public bool ButtonColorVisible
        {
            get => buttonColorVisible;
            set
            {
                if (buttonColorVisible != value)
                {
                    buttonColorVisible = value;

                    OnPropertyChanged(nameof(ButtonColorVisible));
                }
            }
        }

        public bool BtnInsertIsEnabled { get => btnInsertIsEnabled; set { if (value != btnInsertIsEnabled) { btnInsertIsEnabled = value; OnPropertyChanged(nameof(BtnInsertIsEnabled)); } } }

        public ICommand ShowColorPickerCommand => new Command(() => ShowColorPicker());

        public void ShowColorPicker()
        {
            ColorPickerVisible = true;
            ButtonColorVisible = false;
        }

        public ICommand DefineColorCommand => new Command((e) => DefineColor(Color.FromArgb(e as string)));

        public void DefineColor(Color color)
        {
            CategoryColor = color;
            ColorPickerVisible = false;
            ButtonColorVisible = true;
        }

        readonly ICategoryService categoryService;

        public CategoryEditVM(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            ColorPickerVisible = false;
            ButtonColorVisible = true;

            if (query.Count > 0)
            {
                Id = Convert.ToInt32(query["Id"]);
                Models.Category category = await categoryService.GetCategoryById(Id);

                CategoryColor = Color.FromArgb(category.Color);

                Name = category.Name;

                BtnInsertIcon = Icons.Pen;
                BtnInsertText = "Atualizar";
            }
            else
            {
                CategoryColor = Color.FromArgb("#a3e4d7");

                Id = 0;
                Name = "";
                BtnInsertIcon = Icons.Plus;
                BtnInsertText = "Cadastrar";
            }
        }

        public ICommand InsertCommand => new Command(async (e) => { await InsertCategory(); });

        public async Task InsertCategory()
        {
            try
            {
                if (await VerifyFields())
                {
                    BtnInsertIsEnabled = false;

                    Models.Category category = new()
                    {
                        Name = Name,
                        Color = CategoryColor.ToArgbHex(),
                    };

                    string mensagem = "";

                    if (Id > 0)
                    {
                        category.Id = Id;

                        (_, mensagem) = await categoryService.AltCategory(category);
                    }
                    else
                        (_, mensagem) = await categoryService.AddCategory(category);

                    bool resposta = await Application.Current.MainPage.DisplayAlert("Aviso", mensagem, null, "Ok");

                    if (!resposta)
                        await Shell.Current.GoToAsync("..");
                }

                BtnInsertIsEnabled = true;
            }
            catch (Exception ex) { throw ex; }
        }

        private async Task<bool> VerifyFields()
        {
            bool valid = true;

            if (string.IsNullOrEmpty(Name))
            {
                valid = false;
                _ = Application.Current.MainPage.DisplayAlert("Aviso", "Digite um Nome válido", null, "Ok");
            }

            return valid;
        }
    }
}
