﻿using PersonalAssetsMobile.Resources.Fonts.Icons;
using PersonalAssetsMobile.Services;
using Services.Category;
using Services.Category.SubCategory;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels.Category.SubCategory
{
    public class SubCategoryEditVM : ViewModelBase, IQueryAttributable
    {
        int CategoryId, Id;
        readonly ISubCategoryServices subCategoryServices;

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

        string categoryName;

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

        string icon;

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

        bool buttonIconVisible;

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

        string btnConfirmationIcon;

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

        string btnConfirmationText;

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

        public ICommand ShowIconPickerCommand => new Command(() => ShowIconPicker());

        public ICommand DefineIconCommand => new Command((e) => DefineIcon(e as string));

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

        public SubCategoryEditVM(ISubCategoryServices _subCategoryServices)
        {
            subCategoryServices = _subCategoryServices;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            IconPickerVisible = false;
            ButtonIconVisible = true;
            
            if (query.TryGetValue("CategoryId", out object _categoryId))
                CategoryId = Convert.ToInt32(_categoryId);
            else if (query.TryGetValue("Id", out object _id))
                Id = Convert.ToInt32(_id);

            if (Id != 0)
            {
                Models.SubCategory subcategory = await subCategoryServices.GetSubCategoryAsync(Id);

                Name = subcategory.Name;
                CategoryId = subcategory.CategoryId;
                Icon = subcategory.Icon;

                btnConfirmationText = "Alterar";
                btnConfirmationIcon = Icons.Pen;
            }
            else
            {
                btnConfirmationText = "Cadastrar";
                btnConfirmationIcon = Icons.Plus;
            }

            Icon ??= Icons.Tag;


            Models.Category category = await CategoryService.GetCategoryById(CategoryId); //CategoryColor = Color.FromArgb(category.Color);

            CategoryName = category.Name;

        }
    }
}
