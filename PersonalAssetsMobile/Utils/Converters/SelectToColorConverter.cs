using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssetsMobile.Utils.Converters
{
    public class SelectedToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Gray100
            Color result = Color.FromArgb("#ACACAC");

            if (value != null && parameter != null && ((Border)parameter).BindingContext != null && value == ((Border)parameter).BindingContext)
            {
                //Gray300
                result = Color.FromArgb("#E1E1E1");
            }

            return result;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
