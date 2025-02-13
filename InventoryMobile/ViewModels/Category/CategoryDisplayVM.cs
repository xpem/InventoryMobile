﻿using InventoryMobile.Utils;
using InventoryMobile.Views.Category;
using InventoryMobile.Views.Category.SubCategory;
using Models.Responses;
using Services.Interface;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace InventoryMobile.ViewModels.Category
{
    public partial class CategoryDisplayVM(ICategoryBLL categoryBLL, ISubCategoryService subCategoryBLL) : ViewModelBase, IQueryAttributable
    {
        public int CurrentPage { get; set; }

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

        public ObservableCollection<UIModels.UISubCategory> SubCategoryObsCol { get; set; } = [];

        public ICommand CategoryEditCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(CategoryEdit)}?Id={Id}", true));

        public ICommand AddSubCategoryCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(SubCategoryEdit)}", true, new Dictionary<string, object> { { "Category", (new Models.DTO.Category() { Id = Id, Name = Name }) } }));
        public ICommand SubCategoryEditCommand => new Command(async (e) => await Shell.Current.GoToAsync($"{nameof(SubCategoryEdit)}?Id={e}", true, new Dictionary<string, object> { { "Category", (new Models.DTO.Category() { Id = Id, Name = Name }) } }));

        public ICommand DeleteCategoryCommand => new Command(async () =>
        {
            if (SubCategoryObsCol.Count > 0)
            {
                _ = Application.Current.MainPage.DisplayAlert("Aviso", "Não é possivel excluir uma categoria que tenha subcategorias relacionadas", null, "Ok");
            }
            else
            {
                if (await Application.Current.MainPage.DisplayAlert("Confirmação", "Deseja excluir esta Categoria?", "Sim", "Cancelar"))
                {
                    bool success = false;
                    string message = null;
                    ServResp resp = await categoryBLL.DelCategoryAsync(Id);

                    if (resp.Success)
                    {
                        success = resp.Success;
                        message = resp.Content as string;
                    }

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

        public ICommand LoadMoreCommand => new Command(async () =>
        {
            CurrentPage++;
            await LoadSubCategories(CurrentPage);
        });

        public ICommand DeleteSubCategoryCommand => new Command(async (e) =>
        {
            if (await Application.Current.MainPage.DisplayAlert("Confirmação", "Deseja excluir esta Sub Categoria?", "Sim", "Cancelar"))
            {
                bool success = false;
                string message = null;
                var resp = await subCategoryBLL.DelSubCategory(Convert.ToInt32(e));

                if (resp.Success)
                {
                    success = true;
                    message = "Sub Categoria Excluída!";
                }

                if (success)
                {
                    if (!await Application.Current.MainPage.DisplayAlert("Aviso", "Sub Categoria excluída!", null, "Ok"))
                    {
                        UIModels.UISubCategory sub = SubCategoryObsCol.Where(x => x.Id == Convert.ToInt32(e)).First();
                        SubCategoryObsCol.Remove(sub);
                        OnPropertyChanged(nameof(SubCategoryObsCol));
                        //await Shell.Current.GoToAsync("..");
                    }
                }
                else
                {
                    if (message != null)
                        await Application.Current.MainPage.DisplayAlert("Aviso", message, null, "Ok");
                    else
                        throw new Exception("Houve um erro ao tentar excluir a Sub Categoria");
                }
            }
        });

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Id = Convert.ToInt32(query["Id"]);
        }

        public ICommand OnAppearingCommand => new Command(async (e) =>
        {
            Models.DTO.Category category = null;

            ServResp resp = await categoryBLL.GetCategoryByIdAsync(Id.ToString());

            if (resp.Success)
                category = resp.Content as Models.DTO.Category;

            //Category no longer exists in the db
            if (category == null) await Shell.Current.GoToAsync("..");

            CategoryColor = Color.FromArgb(category.Color);
            Name = category.Name;
            SystemDefault = category.SystemDefault.Value;
            SubCategoryObsCol = [];

            CurrentPage = 1;

            _ = LoadSubCategories(CurrentPage);
                      
            OnPropertyChanged(nameof(SubCategoryObsCol));
        });

        private async Task LoadSubCategories(int pageNumber)
        {
            IsBusy = true;

            List<Models.DTO.SubCategoryDTO> subCategoryList = await subCategoryBLL.GetByCategoryIdAsync(((App)App.Current).Uid, pageNumber, Id);

            if (subCategoryList != null && subCategoryList.Count > 0)
                foreach (var subCategory in subCategoryList)
                    SubCategoryObsCol.Add(new UIModels.UISubCategory() { Id = subCategory.Id, Icon = SubCategoryIconsList.GetIconCode(subCategory.IconName), Name = subCategory.Name, SystemDefault = !subCategory.SystemDefault.Value });

            IsBusy = false;
        }
    }
}
