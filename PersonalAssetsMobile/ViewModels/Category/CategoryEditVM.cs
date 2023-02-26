using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels.Category
{
    public class CategoryEditVM : ViewModelBase
    {
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

        bool colorPickerVisible;

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

        bool buttonColorVisible;

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

        public ICommand ShowColorPickerCommand => new Command(() => ShowColorPicker());

        public ICommand DefineColorCommand => new Command((e) => DefineColor(Color.FromArgb(e as string)));

        public void ShowColorPicker()
        {
            ColorPickerVisible = true;
            ButtonColorVisible = false;
        }

        public void DefineColor(Color color) {
            CategoryColor = color;
            ColorPickerVisible = false;
            ButtonColorVisible = true;
        }


        public CategoryEditVM()
        {
            ColorPickerVisible = false;
            ButtonColorVisible = true;

            categoryColor = Color.FromArgb("#a3e4d7");
        }
    }
}
