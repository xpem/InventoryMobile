using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssetsMobile.UIModels
{
    public class UICategory : BindableObject
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

        Color backgoundColor;

        public Color BackgoundColor
        {
            get => backgoundColor; set
            {
                if (backgoundColor != value)
                {
                    backgoundColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color Color { get; set; }
    }

    public class CategoryList
    {
        public static List<UICategory> List { get; } = new()
    {
        new UICategory { Id = 1, Name = "Casa", Color = Color.FromArgb("#EF7C8E"),BackgoundColor=Color.FromArgb("#919191") },
        new UICategory { Id = 2, Name = "Carro", Color = Color.FromArgb("#EF7C8E"),BackgoundColor=Color.FromArgb("#919191") },
        new UICategory { Id = 3, Name = "Moto", Color = Color.FromArgb("#EF7C8E"),BackgoundColor=Color.FromArgb("#919191") },
        new UICategory { Id = 4, Name = "Vestimenta", Color = Color.FromArgb("#EF7C8E"),BackgoundColor=Color.FromArgb("#919191") },
    };
    }

}
