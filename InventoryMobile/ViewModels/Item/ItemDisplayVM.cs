using BLL;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMobile.ViewModels.Item
{
    public class ItemDisplayVM(IItemBLL itemBLL) : ViewModelBase, IQueryAttributable
    {
        int ItemId { get; set; }

        string name, description, categoryAndSubCategory, acquisitionStore, commentary, situation;

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

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            DateTime itemAcquisitionDate = DateTime.Now;

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
                    //AcquisitionValue = item.PurchaseValue.ToString();

                    string _categoryAndSubCategory = item.Category.Name;

                    if (item.Category.SubCategory is not null)
                    {
                        _categoryAndSubCategory += "/" + item.Category.SubCategory.Name;
                    }

                    Situation = item.Situation.Name;

                    CategoryAndSubCategory = _categoryAndSubCategory;
                    Description = item.TechnicalDescription;
                    //Commentary = item.Comment;

                    //PkrItemSituationSelectedIndex = ItemsSituationObsList.IndexOf(ItemsSituationObsList.Where(s => s.Id == item.Situation.Id).FirstOrDefault());
                    //PkrAcquisitionTypeSelectedIndex = AcquisitionTypeObsList.IndexOf(AcquisitionTypeObsList.Where(s => s.Id == item.AcquisitionType).FirstOrDefault());
                    //ResaleValue = item.ResaleValue.ToString();
                    //AcquisitionStore = item.PurchaseStore;
                }
            }
            // AcquisitionDate = new DateTime(itemAcquisitionDate.Year, itemAcquisitionDate.Month, itemAcquisitionDate.Day);

        }
    }
}
