using BLL;
using Plugin.Connectivity;

namespace InventoryMobile.ViewModels
{
    public class ViewModelBase : BindableObject
    {

        bool isBusy;

        public bool isOn = true;

        public bool IsNotBusy => !isBusy;

        public bool IsBusy
        {
            get => isBusy; set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged(nameof(IsBusy));
                    OnPropertyChanged(nameof(IsNotBusy));
                }
            }
        }



    }
}
