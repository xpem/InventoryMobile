using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PersonalAssetsMobile.Components;

public partial class BorderedCurrencyEntry : ContentView
{
    public BorderedCurrencyEntry()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
    propertyName: nameof(Text), returnType: typeof(string), declaringType: typeof(BorderedEntry), defaultValue: null, defaultBindingMode: BindingMode.TwoWay);

    [GeneratedRegex("\\D")]
    private static partial Regex OnlyDigits();

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set
        {
            string valueFromString = OnlyDigits().Replace(value.ToString(), "");
            string _value="0";

            if (valueFromString.Length <= 0)
            { _value = "0"; }
            else
            {
                if (string.IsNullOrEmpty(valueFromString)) _value = "0";
                //if (!long.TryParse(valueFromString, out long valueLong))
                //{
                //    _value = "0";
                //}
                else
                {
                    if (Convert.ToDecimal(valueFromString) <= 0)
                    {
                        _value = "0";
                    }
                    else
                    { 
                        Debug.WriteLine(valueFromString);

                        var decValue = (Convert.ToDecimal(valueFromString) / 100m);
                        Debug.WriteLine(decValue);
                        var currencyFormatValue = decValue.ToString("N2", new CultureInfo("pt-BR", false));
                        Debug.WriteLine(currencyFormatValue);
                        
                        
                        //if (valueFromString.Length > 2)
                        //{
                        _value = currencyFormatValue;
                        //}else
                        //{
                        //    _value = valueFromString;
                        //}
                    }
                }
            }
            Debug.WriteLine("Passou aqui" + _value);
            SetValueCore(TextProperty, _value);

            //Cursor = value.Length;
        }
    }

    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
       propertyName: nameof(LabelText), returnType: typeof(string), declaringType: typeof(BorderedEntry), defaultValue: null, defaultBindingMode: BindingMode.OneWay);

    public string LabelText { get => (string)GetValue(LabelTextProperty); set { SetValue(LabelTextProperty, value); } }

    //public static readonly BindableProperty CursorProperty = BindableProperty.Create(
    // propertyName: nameof(Cursor), returnType: typeof(int), declaringType: typeof(BorderedEntry), defaultValue: null, defaultBindingMode: BindingMode.OneWay);

    //public int Cursor { get => (int)GetValue(CursorProperty); set { SetValue(CursorProperty, value); } }


    private void EntryCurrency_TextChanged(object sender, TextChangedEventArgs e)
    {
        this.EntryCurrency.CursorPosition = this.EntryCurrency.Text?.Length ?? 0;

    }

}
