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

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set
        {
            string valueFromString = Regex.Replace(value.ToString(), @"\D", "");
            string _value = "";
            if (valueFromString.Length <= 0)
            { _value = "0"; }
            else
            {
                long valueLong;
                if (!long.TryParse(valueFromString, out valueLong))
                {
                    _value = "0";
                }
                else
                {
                    if (valueLong <= 0)
                    {
                        _value = "0";
                    }
                    else
                    {
                        //if (valueFromString.Length > 2)
                        //{
                            _value = Decimal.Parse((valueLong / 100m).ToString()).ToString("N2", new CultureInfo("pt-BR", false));
                        //}else
                        //{
                        //    _value = valueFromString;
                        //}
                    }
                }               
            }
            SetValue(TextProperty, _value);

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