using PersonalAssetsMobile.UIModels;

namespace PersonalAssetsMobile.ViewModels.Category
{
    public class CategoryDisplayVM : ViewModelBase, IQueryAttributable
    {
        CategoryUI categoryUI;

        public CategoryUI CategoryUI
        {
            get => categoryUI;
            set
            {
                if (categoryUI != value)
                {
                    categoryUI = value;
                    OnPropertyChanged(nameof(CategoryUI));
                }
            }
        }

        int Id;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Id =  Convert.ToInt32(query["Id"]);
            Console.WriteLine("teste");
        }
    }
}
