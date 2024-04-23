using BLL;
using InventoryMobile.UIModels;
using InventoryMobile.Views.Item;
using Models.ItemModels;
using Models.Responses;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace InventoryMobile.ViewModels.Item
{
    public class ItemDisplayVM(IItemBLL itemBLL) : ViewModelBase, IQueryAttributable
    {
        int ItemId { get; set; }

        string name, description, categoryAndSubCategory, acquisitionStore, acquisitionTypeName, commentary, situation, resaleValue, acquisitionDate, withdrawalDate, acquisitionValue;

        bool resaleSituation, withdrawalDateIsVisible, crvwIsVisible;

        ObservableCollection<UIImagePath> imagePathsObsCol;

        public ObservableCollection<UIImagePath> ImagePathsObsCol
        {
            get => imagePathsObsCol; set
            {
                imagePathsObsCol = value;
                OnPropertyChanged(nameof(ImagePathsObsCol));
            }
        }

        public bool WithdrawalDateIsVisible { get => withdrawalDateIsVisible; set { if (value != withdrawalDateIsVisible) { withdrawalDateIsVisible = value; OnPropertyChanged(nameof(WithdrawalDateIsVisible)); } } }

        public bool CrvwIsVisible
        {
            get => crvwIsVisible; set
            {
                if (value != crvwIsVisible)
                {
                    crvwIsVisible = value; OnPropertyChanged(nameof(CrvwIsVisible));
                }
            }
        }

        public string AcquisitionTypeName
        {
            get => acquisitionTypeName;
            set { if (acquisitionTypeName != value) { acquisitionTypeName = value; OnPropertyChanged(nameof(AcquisitionTypeName)); } }
        }

        public string WithdrawalDate
        {
            get => withdrawalDate;
            set { if (withdrawalDate != value) { withdrawalDate = value; OnPropertyChanged(nameof(WithdrawalDate)); } }
        }

        public string ResaleValue
        {
            get => resaleValue; set { if (resaleValue != value) { resaleValue = value; OnPropertyChanged(nameof(ResaleValue)); } }
        }

        public string AcquisitionValue
        {
            get => acquisitionValue; set { if (acquisitionValue != value) { acquisitionValue = value; OnPropertyChanged(nameof(AcquisitionValue)); } }
        }

        public string AcquisitionDate
        {
            get => acquisitionDate; set
            {
                if (acquisitionDate != value)
                {
                    acquisitionDate = value; OnPropertyChanged(nameof(AcquisitionDate));
                }
            }
        }

        public string AcquisitionStore
        {
            get => acquisitionStore; set
            {
                if (acquisitionStore != value)
                {
                    acquisitionStore = value; OnPropertyChanged(nameof(AcquisitionStore));
                }
            }
        }

        public bool ResaleSituation
        {
            get => resaleSituation;
            set { if (value != resaleSituation) { resaleSituation = value; OnPropertyChanged(nameof(ResaleSituation)); } }
        }

        public string Commentary
        {
            get => commentary;
            set { if (value != commentary) { commentary = value; OnPropertyChanged(nameof(Commentary)); } }
        }

        public string Name
        {
            get => name;
            set { if (name != value) { name = value; OnPropertyChanged(nameof(Name)); } }
        }

        public string Description
        {
            get => description; set { if (description != value) { description = value; OnPropertyChanged(nameof(Description)); } }
        }

        public string Situation
        {
            get => situation; set { if (situation != value) { situation = value; OnPropertyChanged(nameof(Situation)); } }
        }

        public string CategoryAndSubCategory
        {
            get => categoryAndSubCategory; set { if (categoryAndSubCategory != value) { categoryAndSubCategory = value; OnPropertyChanged(nameof(CategoryAndSubCategory)); } }
        }

        public ICommand EditCommand => new Command(() => Shell.Current.GoToAsync($"{nameof(ItemEdit)}?Id={ItemId}", true));

        public ICommand DelItemCommand => new Command(async () => await DeleteItem());

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            IsBusy = true;
            if (query.ContainsKey("Id") && query.TryGetValue("Id", out object itemId))
            {
                ItemId = Convert.ToInt32(itemId);
                Models.ItemModels.Item item;

                BLLResponse resp = await itemBLL.GetItemByIdAsync(ItemId.ToString());

                //binding do Imagesource no front p ver se funciona

                if (resp is not null && resp.Success)
                {

                    item = resp.Content as Models.ItemModels.Item;
                    Name = item.Name;
                    //AcquisitionValue = item.PurchaseValue.ToString();

                    string _categoryAndSubCategory = item.Category.Name;

                    if (item.Category.SubCategory is not null)
                    {
                        _categoryAndSubCategory += "/" + item.Category.SubCategory.Name;
                    }

                    Situation = item.Situation.Name;

                    CategoryAndSubCategory = _categoryAndSubCategory;
                    Description = item.TechnicalDescription;
                    WithdrawalDateIsVisible = ResaleSituation = false;

                    WithdrawalDate = string.Format("dd/MM/yyyy", item.WithdrawalDate != null ? item.WithdrawalDate.Value : DateTime.Now);
                    // AcquisitionType = item.AcquisitionType;

                    if (item.Situation.Id == OutSituationsIds.ResaleStatusId)
                    {
                        ResaleSituation = true;
                        ResaleValue = item.ResaleValue.ToString();
                    }

                    if (OutSituationsIds.OutSituations.Contains(item.Situation.Id))
                    {
                        WithdrawalDateIsVisible = true;
                        WithdrawalDate = item.WithdrawalDate.Value.ToString("d");
                    }

                    AcquisitionTypeName = item.AcquisitionType.Name;
                    AcquisitionDate = item.AcquisitionDate.ToString("d");
                    AcquisitionValue = item.PurchaseValue.ToString();
                    AcquisitionStore = item.PurchaseStore;

                    Commentary = item.Comment;

                    ImagePathsObsCol = [];

                    ItemFiles listImagePaths = await itemBLL.GetItemImages(ItemId, item.Image1, item.Image2);

                    if (listImagePaths != null)
                    {
                        if (listImagePaths.Image1 != null)
                            ImagePathsObsCol.Add(new UIImagePath(listImagePaths.Image1));

                        if (listImagePaths.Image2 != null)
                            ImagePathsObsCol.Add(new UIImagePath(listImagePaths.Image2));
                    }

                    if (imagePathsObsCol.Count > 0) { CrvwIsVisible = true; }
                    else { CrvwIsVisible = false; }
                }
            }
            IsBusy = false;
        }

        private async Task DeleteItem()
        {
            if (await Application.Current.MainPage.DisplayAlert("Confirmação", "Deseja excluir este Item?", "Sim", "Cancelar"))
            {
                IsBusy = true;

                await itemBLL.DelItemAsync(ItemId);

                IsBusy = false;

                if (!await Application.Current.MainPage.DisplayAlert("Aviso", "Item excluído!", null, "Ok"))
                    await Shell.Current.GoToAsync("..");
            }
        }
    }
}
