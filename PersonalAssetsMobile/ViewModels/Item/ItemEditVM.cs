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
        string name;

        public string Name
        {
            get => name; set
            {
                if (name != value) { name = value; OnPropertyChanged(nameof(Name)); }
            }
        }

        string description;

        public string Description
        {
            get => description; set
            {
                if (description != value) { description = value; OnPropertyChanged(nameof(Description)); }
            }
        }

        string categoryName;

        public string CategoryName
        {
            get => categoryName; set
            {
                if (categoryName != value) { categoryName = value; OnPropertyChanged(nameof(CategoryName)); }
            }
        }
        #endregion

        #region commands

        public ICommand CategorySelectorCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(CategorySelector)}", true));

        #endregion

        public ObservableCollection<UIItemSituation> ItemsSituationObsList { get; set; }

        public ObservableCollection<UIAcquisitionType> AcquisitionTypeList { get; private set; }

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

        public ItemEditVM(IItemSituationService _itemSituationService)
        {
            itemSituationService = _itemSituationService;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            //backing of Category Selection Function
            if (query.ContainsKey("SelectedCategory") && query.TryGetValue("SelectedCategory", out object categorySelected))
            {
                CategoryName = categorySelected.ToString();
            }
            else
            {
                #region load lists
                ItemsSituationObsList = new();

                var itemSituationList = await itemSituationService.GetItemSituation();

                if (itemSituationList != null && itemSituationList.Count > 0)
                    for (int i = 0; i < itemSituationList.Count; i++)
                    {
                        ItemsSituationObsList.Add(new UIItemSituation() { Id = itemSituationList[i].Id, Name = itemSituationList[i].Name });
                    }

                AcquisitionTypeList = new ObservableCollection<UIAcquisitionType>();

                foreach (var _acquisitionType in UIModels.UIAcquisitionTypeList.UIAcquisitionTypes)
                {
                    AcquisitionTypeList.Add(_acquisitionType);
                }


                //add
                CategoryName = "Selecione";
                Name = Description = string.Empty;

                OnPropertyChanged(nameof(ItemsSituationObsList));

                #endregion

            }
        }
    }
}
