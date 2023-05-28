using PersonalAssetsMobile.Services;
using PersonalAssetsMobile.Services.Interfaces;
using PersonalAssetsMobile.UIModels;
using PersonalAssetsMobile.Views.Item;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels.Item
{
    public class ItemEditVM : ViewModelBase, IQueryAttributable
    {
        #region fields

        int CategoryId { get; set; }

        int? SubCategoryId { get; set; }

        string name;

        public string Name
        {
            get => name; set { if (name != value) { name = value; OnPropertyChanged(nameof(Name)); } }
        }

        string description;

        public string Description
        {
            get => description; set { if (description != value) { description = value; OnPropertyChanged(nameof(Description)); } }
        }

        string categoryName;

        public string CategoryName
        {
            get => categoryName; set { if (categoryName != value) { categoryName = value; OnPropertyChanged(nameof(CategoryName)); } }
        }

        DateTime acquisitionDate;

        public DateTime AcquisitionDate
        {
            get => acquisitionDate; set { if (acquisitionDate != value) { acquisitionDate = value; OnPropertyChanged(nameof(AcquisitionDate)); } }
        }

        int pkrItemSituationSelectedIndex, pkrAcquisitionTypeSelectedIndex;

        public int PkrItemSituationSelectedIndex { get => pkrItemSituationSelectedIndex; set { pkrItemSituationSelectedIndex = value; OnPropertyChanged(nameof(PkrItemSituationSelectedIndex)); } }

        public int PkrAcquisitionTypeSelectedIndex { get => pkrAcquisitionTypeSelectedIndex; set { pkrAcquisitionTypeSelectedIndex = value; OnPropertyChanged(nameof(PkrAcquisitionTypeSelectedIndex)); } }

        #endregion

        #region commands

        public ICommand CategorySelectorCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(CategorySelector)}", true));

        #endregion

        public ObservableCollection<UIItemSituation> ItemsSituationObsList { get; set; }

        public ObservableCollection<UIAcquisitionType> AcquisitionTypeObsList { get; private set; }

        UIItemSituation itemSituation;

        public UIItemSituation ItemSituation
        {
            get => itemSituation; set
            {
                if (itemSituation != value)
                {
                    itemSituation = value;

                    OnPropertyChanged(nameof(ItemSituation));
                }
            }
        }

        readonly IItemSituationService itemSituationService;
        readonly IAcquisitionTypeService acquisitionTypeService;

        public ItemEditVM(IItemSituationService _itemSituationService, IAcquisitionTypeService _acquisitionTypeService)
        {
            itemSituationService = _itemSituationService;
            acquisitionTypeService = _acquisitionTypeService;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {

            DateTime acquisitionDate = DateTime.Now;
            //backing of Category Selection Function
            if (query.ContainsKey("SelectedCategory") && query.TryGetValue("SelectedCategory", out object selectedCategory))
            {
                var modelSelectedCategory = selectedCategory as Models.Category;
                CategoryId = modelSelectedCategory.Id;

                if (modelSelectedCategory?.SubCategories?.Count > 0)
                {
                    CategoryName = modelSelectedCategory.Name + "/" + modelSelectedCategory.SubCategories[0].Name;
                    SubCategoryId = modelSelectedCategory.SubCategories[0].Id;
                }
                else
                {
                    CategoryName = modelSelectedCategory.Name;
                }
            }
            else
            {
                CategoryName = "Selecione";
                Name = Description = string.Empty;
            }

            AcquisitionDate = new DateTime(acquisitionDate.Year, acquisitionDate.Month, acquisitionDate.Day);

            #region load lists
            ItemsSituationObsList = new();

            var itemSituationList = await itemSituationService.GetItemSituation();

            ItemsSituationObsList.Add(new UIItemSituation() { Id = 0, Name = "Selecione" });

            foreach (var itemSituation in itemSituationList)
                ItemsSituationObsList.Add(new UIItemSituation() { Id = itemSituation.Id, Name = itemSituation.Name });

            OnPropertyChanged(nameof(ItemsSituationObsList));

            PkrItemSituationSelectedIndex = 0;

            AcquisitionTypeObsList = new();

            var acquisitionTypeList = await acquisitionTypeService.GetAcquisitionType();

            AcquisitionTypeObsList.Add(new UIAcquisitionType() { Id = 0, Name = "Selecione" });

            foreach (var acquisitionType in acquisitionTypeList)
                AcquisitionTypeObsList.Add(new UIAcquisitionType() { Id = acquisitionType.Id, Name = acquisitionType.Name });

            OnPropertyChanged(nameof(AcquisitionTypeObsList));

            PkrAcquisitionTypeSelectedIndex = 0;

            //add

            #endregion
        }
    }
}
