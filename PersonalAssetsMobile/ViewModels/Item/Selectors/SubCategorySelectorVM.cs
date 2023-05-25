using System.Windows.Input;

namespace PersonalAssetsMobile.ViewModels.Item.Selectors
{
    public class SubCategorySelectorVM : ViewModelBase, IQueryAttributable
    {
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Category", out object value))
            {
                var category = value as Models.Category;

                Console.WriteLine(category);
            }
        }

        public ICommand TestCommand => new Command(async () => await Shell.Current.GoToAsync("../..", true));

    }
}
