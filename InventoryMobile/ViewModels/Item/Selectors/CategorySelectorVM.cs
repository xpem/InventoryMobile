using InventoryMobile.UIModels;
using Models.Responses;
using Services.Interface;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace InventoryMobile.ViewModels.Item.Selectors
{
    public class CategorySelectorVM(ICategoryBLL categoryBLL) : ViewModelBase
    {
        public ObservableCollection<UICategory> CategoriesObsList { get; set; }

        public ICommand OnAppearingCommand => new Command(async (e) =>
        {
            CategoriesObsList = [];
            List<Models.Category> Categorylist = [];

            ServResp resp = await categoryBLL.GetCategoriesWithSubCategoriesAsync();

            if (resp is not null && resp.Success)
                Categorylist = resp.Content as List<Models.Category>;

            CategoriesObsList.Add(new UICategory() { Id = -1, Name = "[Sem Categoria]", Color = Color.FromArgb("#2F9300"), HaveSubcategories = false });

            if (Categorylist != null && Categorylist.Count > 0)
                foreach (var i in Categorylist)
                    CategoriesObsList.Add(new UICategory() { Id = i.Id.Value, Name = i.Name, Color = Color.FromArgb(i.Color), HaveSubcategories = i.SubCategories.Count > 0, SubCategories = i.SubCategories });

            OnPropertyChanged(nameof(CategoriesObsList));
        });
    }
}
