namespace InventoryMobile.UIModels
{
    public class UIItemSituation : BindableObject
    {
        int id;

        public int Id
        {
            get => id; set
            {
                if (id != value) { id = value; OnPropertyChanged(); }
            }
        }

        string name;
        public string Name
        {
            get => name; set
            {
                if (name != value) { name = value; OnPropertyChanged(); }
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
}
