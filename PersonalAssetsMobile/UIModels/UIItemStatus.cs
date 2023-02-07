namespace PersonalAssetsMobile.UIModels
{
    public class UIItemStatus : BindableObject
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
    }

    public class ItemsStatusList
    {
        public static List<UIItemStatus> itemsStatus { get; } = new List<UIItemStatus>() {
            new UIItemStatus() { Id = 1, Name = "Em Uso",BackgoundColor =Color.FromArgb("#919191") },
              new UIItemStatus() { Id = 2, Name = "Emprestado",BackgoundColor=Color.FromArgb("#919191") },
                new UIItemStatus() { Id = 3, Name = "Revendido", BackgoundColor = Color.FromArgb("#919191") },
                  new UIItemStatus() { Id = 4, Name = "Guardado" , BackgoundColor = Color.FromArgb("#919191")},
        };
    }
}
