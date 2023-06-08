using PersonalAssetsMobile.Services.Interfaces;
using PersonalAssetsMobile.UIModels;
using PersonalAssetsMobile.Views.Item;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels.Item
{
    public class ItemEditVM : ViewModelBase, IQueryAttributable
    {
        #region fields

        int CategoryId { get; set; }

        int? SubCategoryId { get; set; }

        string name;
        string description;
        string categoryName;
        string acquisitionStore;
        string commentary;

        public string Name { get => name; set { if (name != value) { name = value; OnPropertyChanged(nameof(Name)); } } }

        public string Description
        {
            get => description; set { if (description != value) { description = value; OnPropertyChanged(nameof(Description)); } }
        }

        public string CategoryName
        {
            get => categoryName; set { if (categoryName != value) { categoryName = value; OnPropertyChanged(nameof(CategoryName)); } }
        }

        DateTime acquisitionDate;

        public DateTime AcquisitionDate
        {
            get => acquisitionDate; set { if (acquisitionDate != value) { acquisitionDate = value; OnPropertyChanged(nameof(AcquisitionDate)); } }
        }

        decimal acquisitionValue;

        public decimal AcquisitionValue
        {
            get => acquisitionValue; set { if (acquisitionValue != value) { acquisitionValue = value; OnPropertyChanged(nameof(AcquisitionValue)); } }
        }

        public string AcquisitionStore
        {
            get => acquisitionStore; set { if (acquisitionStore != value) { acquisitionStore = value; OnPropertyChanged(nameof(AcquisitionStore)); } }
        }

        public string Commentary
        {
            get => commentary; set { if (commentary != value) { commentary = value; OnPropertyChanged(nameof(Commentary)); } }
        }

        int pkrItemSituationSelectedIndex, pkrAcquisitionTypeSelectedIndex;

        public int PkrItemSituationSelectedIndex { get => pkrItemSituationSelectedIndex; set { pkrItemSituationSelectedIndex = value; OnPropertyChanged(nameof(PkrItemSituationSelectedIndex)); } }

        public int PkrAcquisitionTypeSelectedIndex { get => pkrAcquisitionTypeSelectedIndex; set { pkrAcquisitionTypeSelectedIndex = value; OnPropertyChanged(nameof(PkrAcquisitionTypeSelectedIndex)); } }

        #endregion

        #region commands

        public ICommand CategorySelectorCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(CategorySelector)}", true));

        public ICommand AddItemCommand => new Command(async () => await AddItem());

        #endregion

        #region Components Behaviors

        bool btnAddIsEnabled = true;

        public bool BtnAddIsEnabled { get => btnAddIsEnabled; set { if (value != btnAddIsEnabled) { btnAddIsEnabled = value; OnPropertyChanged(); } } }

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
        readonly IItemService itemService;

        public ItemEditVM(IItemSituationService _itemSituationService, IAcquisitionTypeService _acquisitionTypeService, IItemService _itemService)
        {
            itemSituationService = _itemSituationService;
            acquisitionTypeService = _acquisitionTypeService;
            itemService = _itemService;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            //backing of Category Selection Function
            if (query.ContainsKey("SelectedCategory") && query.TryGetValue("SelectedCategory", out object selectedCategory))
            {
                Models.Category modelSelectedCategory = selectedCategory as Models.Category;
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
                DateTime acquisitionDate = DateTime.Now;

                CategoryName = "Selecione";
                Name = Description = string.Empty;
                AcquisitionValue = decimal.Zero;

                AcquisitionDate = new DateTime(acquisitionDate.Year, acquisitionDate.Month, acquisitionDate.Day);

                #region load lists

                ItemsSituationObsList = new();

                List<Models.ItemSituation> itemSituationList = await itemSituationService.GetItemSituation();

                ItemsSituationObsList.Add(new UIItemSituation() { Id = -1, Name = "Selecione" });

                foreach (Models.ItemSituation itemSituation in itemSituationList)
                    ItemsSituationObsList.Add(new UIItemSituation() { Id = itemSituation.Id, Name = itemSituation.Name });

                OnPropertyChanged(nameof(ItemsSituationObsList));

                PkrItemSituationSelectedIndex = 0;

                AcquisitionTypeObsList = new();

                List<Models.AcquisitionType> acquisitionTypeList = await acquisitionTypeService.GetAcquisitionType();

                AcquisitionTypeObsList.Add(new UIAcquisitionType() { Id = -1, Name = "Selecione" });

                foreach (Models.AcquisitionType acquisitionType in acquisitionTypeList)
                    AcquisitionTypeObsList.Add(new UIAcquisitionType() { Id = acquisitionType.Id, Name = acquisitionType.Name });

                OnPropertyChanged(nameof(AcquisitionTypeObsList));

                PkrAcquisitionTypeSelectedIndex = 0;

                #endregion
            }
        }

        private async Task AddItem()
        {
            try
            {
                if (await Validate())
                {
                    BtnAddIsEnabled = false;

                    Models.Item item = new()
                    {
                        Name = Name.Trim(),
                        AcquisitionDate = AcquisitionDate,
                        AcquisitionType = AcquisitionTypeObsList[pkrAcquisitionTypeSelectedIndex].Id,
                        Comment = Commentary?.Trim(),
                        PurchaseStore = AcquisitionStore?.Trim(),
                        PurchaseValue = AcquisitionValue,
                        Situation = ItemsSituationObsList[pkrItemSituationSelectedIndex].Id,
                        ResaleValue = 0,
                        TechnicalDescription = Description.Trim(),
                        Category = new Models.Category() { Id = CategoryId, SubCategory = SubCategoryId is not null ? new Models.SubCategory() { Id = SubCategoryId.Value } : null },
                    };

                    string message = "";

                    (_, message) = await itemService.AddItem(item);

                    bool resposta = await Application.Current.MainPage.DisplayAlert("Aviso", message, null, "Ok");

                    if (!resposta)
                        await Shell.Current.GoToAsync("..");

                    BtnAddIsEnabled = true;
                }
            }
            catch (Exception ex) { throw ex; }
        }


        private async Task<bool> Validate()
        {
            bool valid = true;

            if (string.IsNullOrEmpty(Name))
                valid = false;

            if (!valid) { await Application.Current.MainPage.DisplayAlert("Aviso", "preencha com um nome válido", null, "Ok"); }
            else
            {
                if (PkrItemSituationSelectedIndex is 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Aviso", "Selecione uma situação válida", null, "Ok");
                    valid = false;
                }
                if (pkrAcquisitionTypeSelectedIndex is 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Aviso", "Selecione um tipo de aquisição válida", null, "Ok");
                    valid = false;
                }
            }

            return valid;
        }
    }
}
