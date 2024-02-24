using BLL;
using InventoryMobile.Resources.Fonts.Icons;
using InventoryMobile.UIModels;
using InventoryMobile.Views.Item;
using Models;
using Models.Responses;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace InventoryMobile.ViewModels.Item
{
    public class ItemEditVM(IItemBLL itemBLL, IItemSituationBLL itemSituationBLL, IAcquisitionTypeBLL acquisitionTypeBLL) : ViewModelBase, IQueryAttributable
    {

        int ItemId { get; set; }

        int CategoryId { get; set; }

        int? SubCategoryId { get; set; }

        const int resaleStatusId = 5;
        readonly int[] outSituations = [4, 5, 3, 7];

        #region fields

        string name, description, categoryName, acquisitionStore, commentary;

        public string Name
        {
            get => name;
            set { if (name != value) { name = value; OnPropertyChanged(nameof(Name)); } }
        }

        public string Description
        {
            get => description; set { if (description != value) { description = value; OnPropertyChanged(nameof(Description)); } }
        }

        public string CategoryName
        {
            get => categoryName; set { if (categoryName != value) { categoryName = value; OnPropertyChanged(nameof(CategoryName)); } }
        }

        DateTime acquisitionDate, withdrawalDate;

        public DateTime AcquisitionDate
        {
            get => acquisitionDate;
            set { if (acquisitionDate != value) { acquisitionDate = value; OnPropertyChanged(nameof(AcquisitionDate)); } }
        }

        public DateTime WithdrawalDate
        {
            get => withdrawalDate;
            set { if (withdrawalDate != value) { withdrawalDate = value; OnPropertyChanged(nameof(WithdrawalDate)); } }
        }

        string acquisitionValue, resaleValue;

        public string AcquisitionValue
        {
            get => acquisitionValue; set { if (acquisitionValue != value) { acquisitionValue = value; OnPropertyChanged(nameof(AcquisitionValue)); } }
        }

        public string ResaleValue
        {
            get => resaleValue; set { if (resaleValue != value) { resaleValue = value; OnPropertyChanged(nameof(ResaleValue)); } }
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

        public int PkrItemSituationSelectedIndex
        {
            get => pkrItemSituationSelectedIndex;
            set
            {
                if (pkrItemSituationSelectedIndex != value)
                {
                    pkrItemSituationSelectedIndex = value;
                    if (pkrItemSituationSelectedIndex > 0)
                    {
                        if (ItemsSituationObsList[pkrItemSituationSelectedIndex].Id == resaleStatusId)
                        {
                            StlResaleValueIsVisible = true;
                            ResaleValue = "0";
                            OnPropertyChanged(nameof(ResaleValue));
                        }
                        else StlResaleValueIsVisible = false;

                        if (outSituations.Contains(ItemsSituationObsList[pkrItemSituationSelectedIndex].Id))
                        {
                            StlWithdrawalDateIsVisible = true;
                            WithdrawalDate = DateTime.Now;
                            OnPropertyChanged(nameof(ResaleValue));
                        }
                        else StlWithdrawalDateIsVisible = false;
                    }
                }
                OnPropertyChanged(nameof(PkrItemSituationSelectedIndex));
            }
        }

        public int PkrAcquisitionTypeSelectedIndex { get => pkrAcquisitionTypeSelectedIndex; set { pkrAcquisitionTypeSelectedIndex = value; OnPropertyChanged(nameof(PkrAcquisitionTypeSelectedIndex)); } }

        #endregion

        #region commands

        public ICommand CategorySelectorCommand => new Command(async () => await Shell.Current.GoToAsync($"{nameof(CategorySelector)}", true));

        public ICommand AddItemCommand => new Command(async () => await AltItem());

        public ICommand DelItemCommand => new Command(async () => await DeleteItem());

        #endregion

        #region Components Behaviors

        string btnInsertText, btnInsertIcon;

        public string BtnInsertText { get => btnInsertText; set { if (value != btnInsertText) { btnInsertText = value; OnPropertyChanged(nameof(BtnInsertText)); } } }

        public string BtnInsertIcon { get => btnInsertIcon; set { if (value != btnInsertIcon) { btnInsertIcon = value; OnPropertyChanged(nameof(BtnInsertIcon)); } } }

        bool btnInsertIsEnabled = true, btnDeleteIsVisible, stlResaleValueIsVisible, stlWithdrawalDateIsVisible;

        public bool BtnInsertIsEnabled { get => btnInsertIsEnabled; set { if (value != btnInsertIsEnabled) { btnInsertIsEnabled = value; OnPropertyChanged(nameof(BtnInsertIsEnabled)); } } }

        public bool BtnDeleteIsVisible { get => btnDeleteIsVisible; set { if (value != btnDeleteIsVisible) { btnDeleteIsVisible = value; OnPropertyChanged(nameof(BtnDeleteIsVisible)); } } }

        public bool StlResaleValueIsVisible { get => stlResaleValueIsVisible; set { if (value != stlResaleValueIsVisible) { stlResaleValueIsVisible = value; OnPropertyChanged(nameof(StlResaleValueIsVisible)); } } }

        public bool StlWithdrawalDateIsVisible { get => stlWithdrawalDateIsVisible; set { if (value != stlWithdrawalDateIsVisible) { stlWithdrawalDateIsVisible = value; OnPropertyChanged(nameof(StlWithdrawalDateIsVisible)); } } }

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
                DateTime itemAcquisitionDate = DateTime.Now;

                ItemsSituationObsList = [];
                List<ItemSituation> itemSituationList = [];

                var respItemSituationList = await itemSituationBLL.GetItemSituation();

                if (respItemSituationList is not null && respItemSituationList.Success)
                    itemSituationList = respItemSituationList.Content as List<ItemSituation>;

                ItemsSituationObsList.Add(new UIItemSituation() { Id = -1, Name = "Selecione" });

                foreach (Models.ItemSituation itemSituation in itemSituationList)
                    ItemsSituationObsList.Add(new UIItemSituation() { Id = itemSituation.Id, Name = itemSituation.Name });

                OnPropertyChanged(nameof(ItemsSituationObsList));

                AcquisitionTypeObsList = [];

                List<AcquisitionType> acquisitionTypeList = [];

                var respAcquisitionTypeList = await acquisitionTypeBLL.GetAcquisitionType();

                if (respAcquisitionTypeList is not null && respAcquisitionTypeList.Success)
                    acquisitionTypeList = respAcquisitionTypeList.Content as List<AcquisitionType>;

                AcquisitionTypeObsList.Add(new UIAcquisitionType() { Id = -1, Name = "Selecione" });

                foreach (Models.AcquisitionType acquisitionType in acquisitionTypeList)
                    AcquisitionTypeObsList.Add(new UIAcquisitionType() { Id = acquisitionType.Id, Name = acquisitionType.Name });

                OnPropertyChanged(nameof(AcquisitionTypeObsList));

                if (query.ContainsKey("Id") && query.TryGetValue("Id", out object itemId))
                {
                    ItemId = Convert.ToInt32(itemId);
                    Models.Item item;

                    BLLResponse resp = await itemBLL.GetItemByIdAsync(ItemId.ToString());

                    if (resp is not null && resp.Success)
                    {
                        item = resp.Content as Models.Item;

                        itemAcquisitionDate = item.AcquisitionDate;
                        Name = item.Name;
                        AcquisitionValue = item.PurchaseValue.ToString();

                        string categoryAndSubCategory = item.Category.Name;
                        CategoryId = item.Category.Id;

                        if (item.Category.SubCategory is not null)
                        {
                            categoryAndSubCategory += "/" + item.Category.SubCategory.Name;
                            SubCategoryId = item.Category.SubCategory.Id;
                        }

                        CategoryName = categoryAndSubCategory;
                        Description = item.TechnicalDescription;
                        Commentary = item.Comment;

                        PkrItemSituationSelectedIndex = ItemsSituationObsList.IndexOf(ItemsSituationObsList.Where(s => s.Id == item.Situation.Id).FirstOrDefault());
                        PkrAcquisitionTypeSelectedIndex = AcquisitionTypeObsList.IndexOf(AcquisitionTypeObsList.Where(s => s.Id == item.AcquisitionType).FirstOrDefault());
                        ResaleValue = item.ResaleValue.ToString();
                        WithdrawalDate = item.WithdrawalDate != null ? item.WithdrawalDate.Value : DateTime.Now;
                        AcquisitionStore = item.PurchaseStore;
                    }

                    BtnInsertIcon = Icons.Pen;
                    BtnInsertText = "Atualizar";
                    BtnDeleteIsVisible = true;
                }
                else
                {
                    CategoryName = "Selecione";
                    Name = Description = string.Empty;
                    AcquisitionValue = "0";

                    BtnInsertIcon = Icons.Plus;
                    BtnInsertText = "Cadastrar";
                    BtnDeleteIsVisible = false;

                    PkrItemSituationSelectedIndex = 0;

                    PkrAcquisitionTypeSelectedIndex = 0;
                }

                AcquisitionDate = itemAcquisitionDate;
            }
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

        private static decimal CurrencyValueParse(string currencyValue) =>
            decimal.Parse(currencyValue.Replace(".", ""), NumberStyles.Number, new NumberFormatInfo() { NumberDecimalSeparator = "," });

        private async Task AltItem()
        {
            try
            {
                if (await Validate())
                {
                    BtnInsertIsEnabled = false;

                    decimal decAquisitionValue = CurrencyValueParse(AcquisitionValue);
                    decimal decResaleValue = ItemsSituationObsList[pkrItemSituationSelectedIndex].Id == resaleStatusId ? CurrencyValueParse(ResaleValue) : 0;

                    Models.Item item = new()
                    {
                        Name = Name.Trim(),
                        AcquisitionDate = new DateTime(AcquisitionDate.Year, AcquisitionDate.Month, AcquisitionDate.Day).Date,
                        AcquisitionType = AcquisitionTypeObsList[pkrAcquisitionTypeSelectedIndex].Id,
                        Comment = Commentary?.Trim(),
                        PurchaseStore = AcquisitionStore?.Trim(),
                        PurchaseValue = decAquisitionValue,
                        Situation = new ItemSituation() { Id = ItemsSituationObsList[pkrItemSituationSelectedIndex].Id },
                        ResaleValue = StlResaleValueIsVisible ? decResaleValue : null,
                        TechnicalDescription = Description.Trim(),
                        Category = new Models.Category() { Id = CategoryId, SubCategory = SubCategoryId is not null ? new Models.SubCategory() { Id = SubCategoryId.Value } : null },
                        WithdrawalDate = StlWithdrawalDateIsVisible ? WithdrawalDate : null

                    };

                    string message = "";
                    BLLResponse resp;

                    if (ItemId > 0)
                    {
                        item.Id = ItemId;

                        resp = await itemBLL.AltItemAsync(item);
                    }
                    else
                        resp = await itemBLL.AddItemAsync(item);


                    if (resp.Success)
                    {
                        if (ItemId > 0)
                            message = "Item Atualizada!";
                        else
                            message = "Item Adicionado!";
                    }
                    else if (resp.Content != null)
                        message = resp.Content as string;

                    bool resposta = await Application.Current.MainPage.DisplayAlert("Aviso", message, null, "Ok");

                    if (!resposta)
                        await Shell.Current.GoToAsync("..");

                    BtnInsertIsEnabled = true;
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
