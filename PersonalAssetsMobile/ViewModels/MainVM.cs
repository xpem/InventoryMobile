using PersonalAssetsMobile.UIModels;
using PersonalAssetsMobile.Views.Item;
using System;
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


        public ObservableCollection<UIItemStatus> ItemsStatus { get; set; }

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
                        Shell.Current.GoToAsync($"{nameof(ItemForm)}?Key={itemUI.Id}", true);
                    }
                    else
                    {
                        throw new Exception("Id de item nulo");
                    }
                    OnPropertyChanged();
                }
            }
        }


        List<UIItemStatus> SelectedUIItemsStatus = new();

        public ICommand ItemStatusSelectdCommand => new Command((e) =>
        {
            var itemStatus = e as UIItemStatus;

            var bgcolor = itemStatus.BackgoundColor;

            if (bgcolor.Equals(BgButtonSelectedColor))
            {
                if (SelectedUIItemsStatus.Count > 1)
                {
                    ItemsStatus.Where(x => x.Id == itemStatus.Id).First().BackgoundColor = Color.FromArgb("#919191");
                    SelectedUIItemsStatus.Remove(itemStatus);
                }
            }
            else
            {
                ItemsStatus.Where(x => x.Id == itemStatus.Id).First().BackgoundColor = BgButtonSelectedColor;
                SelectedUIItemsStatus.Add(itemStatus);
            }

            FilterItemsList();

            OnPropertyChanged(nameof(ItemsStatus));

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


        private void FilterItemsList()
        {
            IsBusy = true;

            Items = new();

            foreach (var i in ItemList.ListItems.Where(x => SelectedUIItemsStatus.Any(y => y.Id == x.StatusId)))
            {
                Items.Add(i);
            }

            IsBusy = false;
        }

        public MainVM()
        {
            ItemsStatus = new();
            foreach (var _status in ItemsStatusList.itemsStatus)
            {
                ItemsStatus.Add(_status);
            }

            //Categories = new();
            //foreach (var _category in CategoryList.List)
            //{
            //    Categories.Add(_category);
            //}

            SelectedUIItemsStatus.Add(ItemsStatus.First());

            FilterItemsList();

            //Items = new();

            //foreach (var i in ItemList.ListItems)
            //{
            //    Items.Add(i);
            //}
        }
    }
}
