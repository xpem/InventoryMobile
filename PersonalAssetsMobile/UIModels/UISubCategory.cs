using PersonalAssetsMobile.Views.Category.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersonalAssetsMobile.UIModels
{
   public class UISubCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public bool SystemDefault { get; set; }

        public ICommand SubCategoryEditCommand => new Command(async (e) => await Shell.Current.GoToAsync($"{nameof(SubCategoryEdit)}?Id={e}", true));

    }
}
