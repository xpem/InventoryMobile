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


        /// <summary>
        /// define a cor de fundo do obj da listagem na main page.
        /// </summary>
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
}
