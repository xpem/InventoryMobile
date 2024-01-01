using BLL;
using InventoryMobile.UIModels;
using InventoryMobile.Views.Category;
using Models.Responses;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace InventoryMobile.ViewModels.Category
{
    public class CategoryListVM(ICategoryBLL categoryBLL) : ViewModelBase
    {
        public ObservableCollection<UICategory> Categories { get; set; } = [];

        public ICommand CategoryAddCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(CategoryEdit)}"));

        public ICommand OnAppearingCommand => new Command(async (e) =>
        {
            Categories = [];

            List<Models.Category> list = [];
                        
            BLLResponse resp = await categoryBLL.GetCategoriesAsync();

            if (resp is not null && resp.Success)
                list = resp.Content as List<Models.Category>;

            if (list != null && list.Count > 0)
                foreach (var i in list)
                {
                    Categories.Add(new UICategory() { Id = i.Id, Name = i.Name, Color = Color.FromArgb(i.Color) });
                }

            OnPropertyChanged(nameof(Categories));
        });
    }
}
