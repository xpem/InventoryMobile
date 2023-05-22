using PersonalAssetsMobile.Services.Interfaces;
using PersonalAssetsMobile.UIModels;
using PersonalAssetsMobile.Views.Item;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels
{
    public class MainVM : ViewModelBase
    {
        //  public ObservableCollection<ItemGroup> Items { get; } = new();
        readonly Color BgButtonSelectedColor = Color.FromArgb("#29A0B1");

        ObservableCollection<UIItem> items;

        public ObservableCollection<UIItem> Items
        {
            get => items; set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        //public ObservableCollection<UICategory> Categories { get; set; }


        public ObservableCollection<UIItemSituation> ItemsSituationObsList { get; set; }

        UIItem itemUI;

        public UIItem ItemUI
        {
            get => itemUI;
            set
            {
                if (itemUI != value)
                {
                    itemUI = value;

                    if (itemUI is not null)
                    {
                        Shell.Current.GoToAsync($"{nameof(ItemEdit)}?Key={itemUI.Id}", true);
                    }
                    else
                    {
                        throw new Exception("Id de item nulo");
                    }
                    OnPropertyChanged();
                }
            }
        }

        readonly List<UIItemSituation> SelectedUIItemsStatus = new();

        public ICommand ItemSituationSelectdCommand => new Command((e) =>
        {
            var itemSituation = e as UIItemSituation;

            var bgcolor = itemSituation.BackgoundColor;

            if (bgcolor.Equals(BgButtonSelectedColor))
            {
                if (SelectedUIItemsStatus.Count > 1)
                {
                    ItemsSituationObsList.Where(x => x.Id == itemSituation.Id).First().BackgoundColor = Color.FromArgb("#919191");
                    SelectedUIItemsStatus.Remove(itemSituation);
                }
            }
            else
            {
                ItemsSituationObsList.Where(x => x.Id == itemSituation.Id).First().BackgoundColor = BgButtonSelectedColor;
                SelectedUIItemsStatus.Add(itemSituation);
            }

            FilterItemsList();

            OnPropertyChanged(nameof(ItemsSituationObsList));

        });

        //public ICommand CategorySelectedCommand => new Command((e) =>
        //{
        //    var category = e as UICategory;

        //    var bgcolor = category.BackgoundColor;

        //    if (bgcolor.Equals(BgButtonSelectedColor))
        //        Categories.Where(x => x.Id == category.Id).First().BackgoundColor = Color.FromArgb("#919191");
        //    else
        //        Categories.Where(x => x.Id == category.Id).First().BackgoundColor = BgButtonSelectedColor;

        //    OnPropertyChanged(nameof(Categories));

        //});

        public ICommand ItemAddCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(ItemEdit)}"));

        private void FilterItemsList()
        {
            IsBusy = true;

            Items = new();

            //foreach (var i in ItemList.ListItems.Where(x => SelectedUIItemsStatus.Any(y => y.Id == x.StatusId)))
            //{
            //    Items.Add(i);
            //}

            IsBusy = false;
        }

        readonly IItemSituationService itemSituationService;

        public MainVM(IItemSituationService _itemSituationService)
        {
            itemSituationService = _itemSituationService;
            //ItemsStatus = new();
            //foreach (var _status in ItemsStatusList.itemsStatus)
            //{
            //    ItemsStatus.Add(_status);
            //}
        }

        public ICommand OnAppearingCommand => new Command(async (e) =>
        {
            ItemsSituationObsList = new();

            if (isOn)
            {
                List<Models.ItemSituation> itemSituationList = await itemSituationService.GetItemSituation();
                Color backgoundColor;

                if (itemSituationList is not null && itemSituationList.Count > 0)
                {                    
                    for (int i = 0; i < itemSituationList.Count; i++)
                    {
                        if (itemSituationList[i].Sequence is 1)
                            backgoundColor = Color.FromArgb("#29A0B1");
                        else
                            backgoundColor = Color.FromArgb("#919191");

                        ItemsSituationObsList.Add(new UIItemSituation() { Id = itemSituationList[i].Id, Name = itemSituationList[i].Name, BackgoundColor = backgoundColor });
                    }

                    SelectedUIItemsStatus.Add(ItemsSituationObsList.First());

                    FilterItemsList();

                    //AcquisitionTypeList = new ObservableCollection<UIAcquisitionType>();

                    //foreach (var _acquisitionType in UIModels.UIAcquisitionTypeList.UIAcquisitionTypes)
                    //{
                    //    AcquisitionTypeList.Add(_acquisitionType);
                    //}

                    OnPropertyChanged(nameof(ItemsSituationObsList));
                }
            }
        });
    }
}
