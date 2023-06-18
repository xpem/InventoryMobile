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
            SetValue(TextProperty, value);
            //if (value is not null)
            //{
            //    string valueFromString = OnlyDigits().Replace(value.ToString(), "");
            //    string _value;

            //    if (valueFromString.Length <= 0)
            //    { _value = "0"; }
            //    else
            //    {
            //        if (string.IsNullOrEmpty(valueFromString)) _value = "0";
            //        //if (!long.TryParse(valueFromString, out long valueLong))
            //        //{
            //        //    _value = "0";
            //        //}
            //        else
            //        {
            //            if (Convert.ToDecimal(valueFromString) <= 0)
            //            {
            //                _value = "0";
            //            }
            //            else
            //            {

            //                var decValue = (Convert.ToDecimal(valueFromString) / 100m);
            //                var currencyFormatValue = decValue.ToString("N2", new CultureInfo("pt-BR", false));

            //                //if (valueFromString.Length > 2)
            //                //{
            //                _value = currencyFormatValue;
            //                //}else
            //                //{
            //                //    _value = valueFromString;
            //                //}
            //            }
            //        }
            //    }

            //    SetValue(TextProperty, "0"+_value);

            //    Debug.WriteLine("Passou aqui 2" + value);

            //Cursor = value.Length;
            //}
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

        string valueFromString = OnlyDigits().Replace(this.EntryCurrency.Text.ToString(), "");
        string _value;

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

                    var decValue = (Convert.ToDecimal(valueFromString) / 100m);
                    var currencyFormatValue = decValue.ToString("N2", new CultureInfo("pt-BR", false));

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
        this.EntryCurrency.Text = _value;
        this.EntryCurrency.CursorPosition = this.EntryCurrency.Text?.Length ?? 0;
    }

}
