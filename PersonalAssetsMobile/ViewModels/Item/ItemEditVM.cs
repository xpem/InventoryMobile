using PersonalAssetsMobile.UIModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssetsMobile.ViewModels.Item
{
    public class ItemEditVM : ViewModelBase
    {
        public ObservableCollection<UIItemStatus> ItemsStatusObsList { get; set; }

        public ObservableCollection<UIAcquisitionType> AcquisitionTypeList { get; private set; }

        UIItemStatus itemStatus;

        public UIItemStatus ItemStatus
        {
            get => itemStatus; set
            {
                if (itemStatus != value)
                {
                    itemStatus = value;

                    OnPropertyChanged(nameof(ItemStatus));
                }
            }
        }


        public ItemEditVM()
        {

            ItemsStatusObsList = new ObservableCollection<UIItemStatus>();

            foreach (var _itemStatus in UIModels.ItemsStatusList.itemsStatus)
            {
                ItemsStatusObsList.Add(_itemStatus);
            }

            AcquisitionTypeList= new ObservableCollection<UIAcquisitionType>();

            foreach(var _acquisitionType in UIModels.UIAcquisitionTypeList.UIAcquisitionTypes)
            {
                AcquisitionTypeList.Add(_acquisitionType);
            }
        }
    }
}
