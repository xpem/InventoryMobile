using PersonalAssetsMobile.UIModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssetsMobile.ViewModels
{

    public class CategoryListVM : ViewModelBase
    {
        public ObservableCollection<UICategory> Categories { get; } = new();

        public CategoryListVM()
        {
            List<UICategory> listCategories = new()
            {
                new UICategory{Name="Casa", Color = Color.FromArgb("#E2808A")},
                new UICategory{Name="Carro", Color = Color.FromArgb("#F1CCD7")},
                new UICategory{Name="Moto", Color = Color.FromArgb("#2E765E")},
                new UICategory{Name="Vestimenta", Color = Color.FromArgb("#638C80")},
            };

            foreach (var i in listCategories)
            {
                Categories.Add(i);
            }
        }

    }
}
