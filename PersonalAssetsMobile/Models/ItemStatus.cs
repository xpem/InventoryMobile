using PersonalAssetsMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersonalAssetsMobile.Models
{
    public class ItemStatus : BindableObject
    {
        int id;

        public int Id
        {
            get => id; set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        string name;
        public string Name
        {
            get => name; set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }
    }

    public class ItemsStatusList
    {
        public static List<ItemStatus> itemsStatus { get; } = new List<ItemStatus>() {
            new ItemStatus() { Id = 1, Name = "Em Uso" },
              new ItemStatus() { Id = 2, Name = "Emprestado" },
                new ItemStatus() { Id = 3, Name = "Revendido" },
                  new ItemStatus() { Id = 4, Name = "Guardado" },
        };
    }
}
