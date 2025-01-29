using BLL;
using InventoryMobile.Resources.Fonts.Icons;
using InventoryMobile.UIModels;
using InventoryMobile.Utils;
using InventoryMobile.Views;
using InventoryMobile.Views.Item;
using Models.Exceptions;
using Models.ItemModels;
using Services.Interface;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace InventoryMobile.ViewModels
{
    public class MainVM(IItemBLL itemBLL, IItemSituationBLL itemSituationBLL, IUserService userBLL) : ViewModelBase
    {
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

        public ObservableCollection<UIItemSituation> ItemsSituationObsList { get; set; }

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

        public ICommand ItemAddCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(ItemEdit)}"));

        private void FilterItemsList()
        {
            IsBusy = true;

            ItemsObsList = [];

            foreach (var i in ListAllItems.Where(x => x.SituationId == SelectedUIItemsStatus.Id))//SelectedUIItemsStatus.Any(y => y.Id == x.SituationId)))
            {
                ItemsObsList.Add(i);
            }

            IsBusy = false;
        }

        public ICommand OnAppearingCommand => new Command(async (e) =>
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                IsBusy = true;
                try
                {
                    Color backgoundColor;

                    List<ItemSituation> itemSituationList = [];

                    Models.Responses.ServResp respItemSituation = await itemSituationBLL.GetItemSituation();

                    if (respItemSituation is not null && respItemSituation.Success)
                        itemSituationList = respItemSituation.Content as List<ItemSituation>;

                    var respItems = await itemBLL.GetItemsAllAsync();

                    List<Models.ItemModels.Item> itemList = [];
                    if (respItems is not null)
                    {
                        itemList = respItems;
                        itemList = [.. (from item in itemList orderby item.CreatedAt descending select item)];
                    }

                    if (itemSituationList is not null && itemSituationList.Count > 0)
                    {
                        ItemsSituationObsList = [];
                        string textSituationItem;

                        for (int i = 0; i < itemSituationList.Count; i++)
                        {
                            if (itemSituationList[i].Sequence is 1)
                                backgoundColor = Color.FromArgb("#29A0B1");
                            else
                                backgoundColor = Color.FromArgb("#919191");

                            textSituationItem = $"{itemSituationList[i].Name} ({itemList.Where(x => x.Situation.Id == itemSituationList[i].Id).Count()})";

                            ItemsSituationObsList.Add(new UIItemSituation() { Id = itemSituationList[i].Id.Value, Name = textSituationItem, BackgoundColor = backgoundColor });
                        }

                        if (SelectedUIItemsStatus is null)
                        {
                            OnPropertyChanged(nameof(ItemsSituationObsList));

                            SelectedUIItemsStatus = ItemsSituationObsList.First();
                        }

                        //AcquisitionTypeList = new ObservableCollection<UIAcquisitionType>();

                        //foreach (var _acquisitionType in UIModels.UIAcquisitionTypeList.UIAcquisitionTypes)
                        //{
                        //    AcquisitionTypeList.Add(_acquisitionType);
                        //}


                        //FilterItemsList();

                        ItemsObsList = [];
                        ListAllItems = [];

                        string IconUniCode;
                        if (itemList is not null)
                            foreach (var item in itemList)
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
                                    Id = item.Id.Value,
                                    Name = item.Name,
                                    CategoryAndSubCategory = categoryAndSubCategory,
                                    CategoryColor = Color.FromArgb(item.Category.Color),
                                    SituationId = item.Situation.Id.Value,
                                    SubCategoryIcon = IconUniCode,
                                };

                                ListAllItems.Add(uIItem);

                                if (item.Situation.Id == SelectedUIItemsStatus.Id)
                                    itemsObsList.Add(uIItem);

                                //if (SelectedUIItemsStatus.Exists(x => x.Id == item.Situation))
                                //    ItemsObsList.Add(uIItem);
                            }

                        IsBusy = false;
                    }
                }
                catch (SignInFailException ex)
                {
                    userBLL.RemoveUserLocal();

                    await Shell.Current.GoToAsync($"//{nameof(SignIn)}");
                }
                catch (Exception ex) { throw ex; }
            }
        });
    }
}
