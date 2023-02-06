using PersonalAssetsMobile.Models;
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
        public ObservableCollection<Category> Categories { get; } = new();

        public CategoryListVM()
        {
            List<Category> listCategories = new()
            {
                new Category{Name="Casa", Color = Color.FromArgb("#E2808A")},
                new Category{Name="Carro", Color = Color.FromArgb("#F1CCD7")},
                new Category{Name="Moto", Color = Color.FromArgb("#2E765E")},
                new Category{Name="Vestimenta", Color = Color.FromArgb("#638C80")},
            };

            foreach (var i in listCategories)
            {
                Categories.Add(i);
            }
        }

    }
}
