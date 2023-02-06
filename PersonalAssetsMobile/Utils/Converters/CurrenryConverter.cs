using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonalAssetsMobile.Utils.Converters
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object Value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Value is null || Value is 0) { return "0"; }
            else return Value.ToString();
            //return Decimal.Parse(Value.ToString()).ToString("N2", new CultureInfo("pt-BR", false));
        }

        public object ConvertBack(object Value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueFromString = Regex.Replace(Value.ToString(), @"\D", "");

            if (valueFromString.Length <= 0)
                return 0m;

            long valueLong;
            if (!long.TryParse(valueFromString, out valueLong))
                return 0m;

            if (valueLong <= 0)
                return 0m;

            return valueLong / 100m;
        }
    }
}
