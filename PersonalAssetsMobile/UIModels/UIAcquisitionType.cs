using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssetsMobile.UIModels
{
    public class UIAcquisitionType : BindableObject
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

    public class UIAcquisitionTypeList {

        public static List<UIAcquisitionType> UIAcquisitionTypes = new List<UIAcquisitionType>()
        {
            new UIAcquisitionType()
            {
                Id= 1,Name="Compra"
            },
            new UIAcquisitionType()
            {
                Id= 1,Name="Emprestimo"
            },
            new UIAcquisitionType()
            {
                Id= 1,Name="Doação"
            },
            new UIAcquisitionType()
            {
                Id= 1,Name="Presente"
            },
        };
    }

}
