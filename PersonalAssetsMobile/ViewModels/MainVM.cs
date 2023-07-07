using Models;
using PersonalAssetsMobile.Resources.Fonts.Icons;
using PersonalAssetsMobile.Services.Interfaces;
using PersonalAssetsMobile.UIModels;
using PersonalAssetsMobile.Utils;
using PersonalAssetsMobile.Views.Item;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Windows.Input;
using System.Xml.Linq;

namespace PersonalAssetsMobile.ViewModels
{
    public class MainVM : ViewModelBase
    {
        //  public ObservableCollection<ItemGroup> Items { get; } = new();
        readonly Color BgButtonSelectedColor = Color.FromArgb("#29A0B1");

        List<UIItem> ListAllItems;

        ObservableCollection<UIItem> itemsObsList;

        public ObservableCollection<UIItem> ItemsObsList
        {
            get => itemsObsList; set
            {
                itemsObsList = value;
                OnPropertyChanged(nameof(ItemsObsList));
            }
        }

        //public ObservableCollection<UICategory> Categories { get; set; }


        public ObservableCollection<UIItemSituation> ItemsSituationObsList { get; set; }

        //UIItem itemUI;

        //public UIItem ItemUI
        //{
        //    get => itemUI;
        //    set
        //    {
        //        if (itemUI != value)
        //        {
        //            itemUI = value;

        //            if (itemUI is not null)
        //            {
        //                Shell.Current.GoToAsync($"{nameof(ItemEdit)}?Id={itemUI.Id}", true);
        //            }
        //            else
        //            {
        //                throw new Exception("Id de item nulo");
        //            }
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        // List<UIItemSituation> SelectedUIItemsStatus { get; set; }

        UIItemSituation SelectedUIItemsStatus { get; set; }

        public ICommand ItemSituationSelectdCommand => new Command((e) =>
            {
                var itemSituation = e as UIItemSituation;

                var bgcolor = itemSituation.BackgoundColor;

                if (!bgcolor.Equals(BgButtonSelectedColor))
                {
                    //if (SelectedUIItemsStatus.Count > 1)
                    //{
                    //ItemsSituationObsList.Where(x => x.Id == itemSituation.Id).First().BackgoundColor = Color.FromArgb("#919191");                    
                    //SelectedUIItemsStatus.Remove(itemSituation);
                    //}
                    ItemsSituationObsList.Where(x => x.Id == SelectedUIItemsStatus.Id).ToList().ForEach(y => y.BackgoundColor = Color.FromArgb("#919191"));
                    ItemsSituationObsList.Where(x => x.Id == itemSituation.Id).First().BackgoundColor = BgButtonSelectedColor;
                    SelectedUIItemsStatus = itemSituation;
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

            ItemsObsList = new();

            foreach (var i in ListAllItems.Where(x => x.SituationId == SelectedUIItemsStatus.Id))//SelectedUIItemsStatus.Any(y => y.Id == x.SituationId)))
            {
                ItemsObsList.Add(i);
            }

            IsBusy = false;
        }

        readonly IItemSituationService itemSituationService;

        readonly IItemService itemService;

        public MainVM(IItemSituationService _itemSituationService, IItemService _itemService)
        {
            itemSituationService = _itemSituationService;
            itemService = _itemService;
            //ItemsStatus = new();
            //foreach (var _status in ItemsStatusList.itemsStatus)
            //{
            //    ItemsStatus.Add(_status);
            //}
        }

        public ICommand OnAppearingCommand => new Command(async (e) =>
        {
            if (isOn)
            {
                IsBusy = true;
                try
                {
                    Color backgoundColor;

                    List<Models.ItemSituation> itemSituationList = await itemSituationService.GetItemSituation();
                    var listItems = await itemService.GetItems();

                    if (itemSituationList is not null && itemSituationList.Count > 0)
                    {
                        if (SelectedUIItemsStatus is null)
                        {
                            ItemsSituationObsList = new();
                            string textSituationItem;

                            for (int i = 0; i < itemSituationList.Count; i++)
                            {
                                if (itemSituationList[i].Sequence is 1)
                                    backgoundColor = Color.FromArgb("#29A0B1");
                                else
                                    backgoundColor = Color.FromArgb("#919191");
                                var teste = listItems.Where(x => x.Situation == itemSituationList[i].Id);

                                textSituationItem = $"{itemSituationList[i].Name} ({listItems.Where(x => x.Situation == itemSituationList[i].Id).Count()})";

                                ItemsSituationObsList.Add(new UIItemSituation() { Id = itemSituationList[i].Id, Name = textSituationItem, BackgoundColor = backgoundColor });
                            }

                            OnPropertyChanged(nameof(ItemsSituationObsList));

                            SelectedUIItemsStatus = ItemsSituationObsList.First();
                        }

                        //AcquisitionTypeList = new ObservableCollection<UIAcquisitionType>();

                        //foreach (var _acquisitionType in UIModels.UIAcquisitionTypeList.UIAcquisitionTypes)
                        //{
                        //    AcquisitionTypeList.Add(_acquisitionType);
                        //}


                        //FilterItemsList();

                        ItemsObsList = new();
                        ListAllItems = new();

                        string IconUniCode;

                        foreach (var item in listItems)
                        {
                            string categoryAndSubCategory = "";

                            categoryAndSubCategory = item.Category.Name;

                            if (item.Category.SubCategory is not null)
                                categoryAndSubCategory += "/" + item.Category.SubCategory.Name;

                            if (item.Category.SubCategory is null || item.Category.SubCategory.IconName is null)
                                IconUniCode = Icons.Tag;
                            else
                                IconUniCode = SubCategoryIconsList.GetIconCode(item.Category.SubCategory.IconName);


                            UIItem uIItem = new()
                            {
                                Id = item.Id,
                                Name = item.Name,
                                CategoryAndSubCategory = categoryAndSubCategory,
                                CategoryColor = Color.FromArgb(item.Category.Color),
                                SituationId = item.Situation.Value,
                                SubCategoryIcon = IconUniCode,
                            };

                            ListAllItems.Add(uIItem);

                            if (item.Situation == SelectedUIItemsStatus.Id)
                                itemsObsList.Add(uIItem);

                            //if (SelectedUIItemsStatus.Exists(x => x.Id == item.Situation))
                            //    ItemsObsList.Add(uIItem);
                        }

                        IsBusy = false;
                    }
                }
                catch (Exception ex) { throw ex; }
            }
        });
    }
}
