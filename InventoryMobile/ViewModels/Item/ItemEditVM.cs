using BLL;
using InventoryMobile.Resources.Fonts.Icons;
using InventoryMobile.UIModels;
using InventoryMobile.Views.Item;
using Models;
using Models.DTO;
using Models.ItemModels;
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

        public enum MediaPickerType { pick, capture }

        #region fields

        string name, description, categoryName, acquisitionStore, commentary, acquisitionValue, resaleValue, btnInsertText, btnInsertIcon;

        bool btnPickItemImageIsEnabled;

        DateTime acquisitionDate, withdrawalDate;

        int pkrItemSituationSelectedIndex, pkrAcquisitionTypeSelectedIndex;

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

        public string AcquisitionValue
        {
            get => acquisitionValue; set { if (acquisitionValue != value) { acquisitionValue = value; OnPropertyChanged(nameof(AcquisitionValue)); } }
        }

        public bool BtnPickItemImageIsEnabled
        {
            get => btnPickItemImageIsEnabled;
            set
            {
                if (btnPickItemImageIsEnabled != value)
                {
                    btnPickItemImageIsEnabled = value; OnPropertyChanged(nameof(BtnPickItemImageIsEnabled));
                }
            }
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

        public ICommand PickItemImageCommand => new Command(async () => await TakeItemImage(MediaPickerType.pick));

        public ICommand CaptureItemImageCommand => new Command(async () => await TakeItemImage(MediaPickerType.capture));

        public ICommand DelItemImageCommand => new Command((e) => DelItemImage(Guid.Parse(e as string)));

        #endregion

        #region Components Behaviors

        public string BtnInsertText { get => btnInsertText; set { if (value != btnInsertText) { btnInsertText = value; OnPropertyChanged(nameof(BtnInsertText)); } } }

        public string BtnInsertIcon { get => btnInsertIcon; set { if (value != btnInsertIcon) { btnInsertIcon = value; OnPropertyChanged(nameof(BtnInsertIcon)); } } }

        bool btnInsertIsEnabled = true, btnDeleteIsVisible, stlResaleValueIsVisible, stlWithdrawalDateIsVisible, crvwIsVisible, vSLAddImageIsVisible = true;

        public bool BtnInsertIsEnabled { get => btnInsertIsEnabled; set { if (value != btnInsertIsEnabled) { btnInsertIsEnabled = value; OnPropertyChanged(nameof(BtnInsertIsEnabled)); } } }

        public bool BtnDeleteIsVisible { get => btnDeleteIsVisible; set { if (value != btnDeleteIsVisible) { btnDeleteIsVisible = value; OnPropertyChanged(nameof(BtnDeleteIsVisible)); } } }

        public bool StlResaleValueIsVisible { get => stlResaleValueIsVisible; set { if (value != stlResaleValueIsVisible) { stlResaleValueIsVisible = value; OnPropertyChanged(nameof(StlResaleValueIsVisible)); } } }

        public bool StlWithdrawalDateIsVisible { get => stlWithdrawalDateIsVisible; set { if (value != stlWithdrawalDateIsVisible) { stlWithdrawalDateIsVisible = value; OnPropertyChanged(nameof(StlWithdrawalDateIsVisible)); } } }

        public bool VSLAddImageIsVisible { get => vSLAddImageIsVisible; set { if (value != vSLAddImageIsVisible) { vSLAddImageIsVisible = value; OnPropertyChanged(nameof(VSLAddImageIsVisible)); } } }

        public bool CrvwIsVisible { get => crvwIsVisible; set { if (value != crvwIsVisible) { crvwIsVisible = value; OnPropertyChanged(nameof(CrvwIsVisible)); } } }

        #endregion

        FixedIndexesImagePaths ImagePaths { get; set; } = new();

        ObservableCollection<UIImagePath> imagePathsObsCol;

        public ObservableCollection<UIImagePath> ImagePathsObsCol
        {
            get => imagePathsObsCol; set
            {
                imagePathsObsCol = value;
                OnPropertyChanged(nameof(ImagePathsObsCol));
            }
        }

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
                Models.DTO.Category modelSelectedCategory = selectedCategory as Models.DTO.Category;
                CategoryId = modelSelectedCategory.Id.Value;

                if (modelSelectedCategory?.SubCategories?.Count > 0)
                {
                    CategoryName = modelSelectedCategory.Name + "/" + modelSelectedCategory.SubCategories[0].Name;
                    SubCategoryId = modelSelectedCategory.SubCategories[0].Id.Value;
                }
                else
                {
                    CategoryName = modelSelectedCategory.Name;
                }
            }
            else
            {
                IsBusy = true;
                DateTime itemAcquisitionDate = DateTime.Now;

                ItemsSituationObsList = [];
                List<ItemSituation> itemSituationList = [];

                var respItemSituationList = await itemSituationBLL.GetItemSituation();

                if (respItemSituationList is not null && respItemSituationList.Success)
                    itemSituationList = respItemSituationList.Content as List<ItemSituation>;

                ItemsSituationObsList.Add(new UIItemSituation() { Id = -1, Name = "Selecione" });

                foreach (ItemSituation itemSituation in itemSituationList)
                    ItemsSituationObsList.Add(new UIItemSituation() { Id = itemSituation.Id.Value, Name = itemSituation.Name });

                OnPropertyChanged(nameof(ItemsSituationObsList));

                AcquisitionTypeObsList = [];

                List<AcquisitionType> acquisitionTypeList = [];

                var respAcquisitionTypeList = await acquisitionTypeBLL.GetAcquisitionType();

                if (respAcquisitionTypeList is not null && respAcquisitionTypeList.Success)
                    acquisitionTypeList = respAcquisitionTypeList.Content as List<AcquisitionType>;

                AcquisitionTypeObsList.Add(new UIAcquisitionType() { Id = -1, Name = "Selecione" });

                foreach (AcquisitionType acquisitionType in acquisitionTypeList)
                    AcquisitionTypeObsList.Add(new UIAcquisitionType() { Id = acquisitionType.Id.Value, Name = acquisitionType.Name });

                OnPropertyChanged(nameof(AcquisitionTypeObsList));

                if (query.ContainsKey("Id") && query.TryGetValue("Id", out object itemId))
                {
                    ItemId = Convert.ToInt32(itemId);
                    Models.ItemModels.Item item;

                    ServResp resp = await itemBLL.GetItemByIdAsync(ItemId.ToString());

                    if (resp is not null && resp.Success)
                    {
                        item = resp.Content as Models.ItemModels.Item;

                        itemAcquisitionDate = item.AcquisitionDate;
                        Name = item.Name;
                        AcquisitionValue = item.PurchaseValue.ToString();

                        string categoryAndSubCategory = item.Category.Name;
                        CategoryId = item.Category.Id.Value;

                        if (item.Category.SubCategory is not null)
                        {
                            categoryAndSubCategory += "/" + item.Category.SubCategory.Name;
                            SubCategoryId = item.Category.SubCategory.Id.Value;
                        }

                        CategoryName = categoryAndSubCategory;
                        Description = item.TechnicalDescription;
                        Commentary = item.Comment;

                        PkrItemSituationSelectedIndex = ItemsSituationObsList.IndexOf(ItemsSituationObsList.Where(s => s.Id == item.Situation.Id).FirstOrDefault());
                        PkrAcquisitionTypeSelectedIndex = AcquisitionTypeObsList.IndexOf(AcquisitionTypeObsList.Where(s => s.Id == item.AcquisitionType.Id).FirstOrDefault());
                        ResaleValue = item.ResaleValue.ToString();
                        WithdrawalDate = item.WithdrawalDate != null ? item.WithdrawalDate.Value : DateTime.Now;
                        AcquisitionStore = item.PurchaseStore;

                        ImagePathsObsCol = [];

                        var responseItemImages = await itemBLL.GetItemImages(ItemId, item.Image1, item.Image2);

                        if (responseItemImages != null)
                        {
                            if (responseItemImages.Image1 != null)
                            {
                                string image1ExternalId = item.Image1;
                                ImagePaths.Image1 = new(responseItemImages.Image1.ImageFilePath, fileName: responseItemImages.Image1.FileName, image1ExternalId);
                            }

                            if (responseItemImages.Image2 != null)
                            {
                                string image2ExternalId = item.Image2;
                                ImagePaths.Image2 = new(responseItemImages.Image2.ImageFilePath, fileName: responseItemImages.Image2.FileName, image2ExternalId);
                            }
                        }

                        BuildImagePathsObsColAsync();
                    }

                    BtnInsertIcon = Icons.Pen;
                    BtnInsertText = "Atualizar";
                    BtnDeleteIsVisible = true;
                    BtnPickItemImageIsEnabled = true;
                }
                else
                {
                    CategoryName = "Selecione";
                    Name = Description = string.Empty;
                    AcquisitionValue = "0";

                    BtnInsertIcon = Icons.Plus;
                    BtnInsertText = "Cadastrar";
                    BtnDeleteIsVisible = false;
                    BtnPickItemImageIsEnabled = true;

                    PkrItemSituationSelectedIndex = 0;

                    PkrAcquisitionTypeSelectedIndex = 0;
                }

                AcquisitionDate = itemAcquisitionDate;
                IsBusy = false;
            }
        }

        private void BuildImagePathsObsColAsync()
        {
            ImagePathsObsCol = [];

            if (ImagePaths != null)
            {
                if (ImagePaths.Image1 != null)
                    ImagePathsObsCol.Add(ImagePaths.Image1);

                if (ImagePaths.Image2 != null)
                    ImagePathsObsCol.Add(ImagePaths.Image2);
            }

            if (ImagePathsObsCol.Count > 0)
            {
                CrvwIsVisible = true;

                if (ImagePathsObsCol.Count == 2)
                    BtnPickItemImageIsEnabled = false;
                else
                    BtnPickItemImageIsEnabled = true;
            }
            else
            {
                CrvwIsVisible = false;
                BtnPickItemImageIsEnabled = true;
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

                    Models.ItemModels.Item item = new()
                    {
                        Name = Name.Trim(),
                        AcquisitionDate = new DateTime(AcquisitionDate.Year, AcquisitionDate.Month, AcquisitionDate.Day).Date,
                        AcquisitionType = new AcquisitionType() { Id = AcquisitionTypeObsList[pkrAcquisitionTypeSelectedIndex].Id },
                        Comment = Commentary?.Trim(),
                        PurchaseStore = AcquisitionStore?.Trim(),
                        PurchaseValue = decAquisitionValue,
                        Situation = new ItemSituation() { Id = ItemsSituationObsList[pkrItemSituationSelectedIndex].Id },
                        ResaleValue = StlResaleValueIsVisible ? decResaleValue : null,
                        TechnicalDescription = Description.Trim(),
                        Category = new Models.DTO.Category() { Id = CategoryId, SubCategory = SubCategoryId is not null ? new SubCategory() { Id = SubCategoryId.Value } : null },
                        WithdrawalDate = StlWithdrawalDateIsVisible ? WithdrawalDate : null
                    };

                    string message = "";
                    ServResp resp;

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
                        {
                            message = "Item Adicionado!";

                            if (resp.Content is Models.ItemModels.Item)
                            {
                                Models.ItemModels.Item AddedItem = resp.Content as Models.ItemModels.Item;
                                ItemId = AddedItem.Id.Value;
                            }

                            if (ItemId == 0) throw new Exception("Id do item zerado após criação de item");
                        }

                        ItemFilesToUpload itemFilesToUpload = new();

                        if (ImagePaths.Image1 != null || ImagePaths.Image2 != null)
                        {
                            if (ImagePaths.Image1 != null)
                                itemFilesToUpload.Image1 = new() { FileName = ImagePaths.Image1.FileName, FileId = 1, ImageFilePath = ImagePaths.Image1.ImageFilePath };

                            if (ImagePaths.Image2 != null)
                                itemFilesToUpload.Image2 = new() { FileName = ImagePaths.Image2.FileName, FileId = 2, ImageFilePath = ImagePaths.Image2.ImageFilePath };

                            var respAddItemImages = await itemBLL.AddItemImageAsync(ItemId, itemFilesToUpload);

                            if (respAddItemImages is not null && respAddItemImages.Success)
                            {
                                var itemFileNames = respAddItemImages.Content as Models.ItemModels.ItemFileNames;
                            }
                        }
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

        public void DelItemImage(Guid id)
        {
            UIImagePath itemImage;

            if (ImagePaths.Image1 is not null && ImagePaths.Image1.Id == id)
            {
                itemImage = ImagePaths.Image1;

                ImagePaths.Image1 = null;

                BuildImagePathsObsColAsync();

                ItemEditVM.DeleteLocalFile(itemImage.ImageFilePath);

                if (itemImage.ExternalFileName is not null)
                {
                    _ = itemBLL.DelItemImageAsync(ItemId, itemImage.ExternalFileName);
                }
            }
            else if (ImagePaths.Image2 is not null)
            {
                itemImage = ImagePaths.Image2;
                ImagePaths.Image2 = null;

                BuildImagePathsObsColAsync();
                ItemEditVM.DeleteLocalFile(itemImage.ImageFilePath);

                if (itemImage.ExternalFileName is not null)
                {
                    _ = itemBLL.DelItemImageAsync(ItemId, itemImage.ExternalFileName);
                }
            }
        }

        private static void DeleteLocalFile(string imageFilePath) => File.Delete(Path.Combine(imageFilePath));

        public async Task TakeItemImage(MediaPickerType mediaPickerType)
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo;

                if (mediaPickerType is MediaPickerType.pick)
                    photo = await MediaPicker.Default.PickPhotoAsync();
                else
                    photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    string tempFileName = (new Guid()).ToString();
                    int fileIdx = 1;
                    if (ImagePathsObsCol is not null && ImagePathsObsCol.Count == 1 && ImagePaths.Image1 is not null)
                    { fileIdx++; }

                    tempFileName += Path.GetExtension(photo.FileName);

                    string tempLocalFilePath = Path.Combine(FilePaths.ImagesPath, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();

                    using FileStream localFileStream = File.OpenWrite(tempLocalFilePath);

                    await sourceStream.CopyToAsync(localFileStream);

                    ImagePaths ??= new();

                    if (fileIdx == 1)
                        ImagePaths.Image1 = new(tempLocalFilePath, photo.FileName);
                    else
                        ImagePaths.Image2 = new(tempLocalFilePath, photo.FileName);

                    BuildImagePathsObsColAsync();
                }
            }
        }
    }
}
