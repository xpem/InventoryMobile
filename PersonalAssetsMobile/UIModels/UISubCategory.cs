using PersonalAssetsMobile.Services;
using PersonalAssetsMobile.Views.Category.SubCategory;
using System.Windows.Input;

namespace PersonalAssetsMobile.UIModels
{
    public class UISubCategory : BindableObject
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public bool SystemDefault { get; set; }
    }
}
