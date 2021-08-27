using System;

namespace WaspApp.Converters
{
    public class BooleanToStringConverter : Xamarin.Forms.IValueConverter
    {
        public string True { get; set; }
        public string False { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is bool)) throw new InvalidCastException("Unable to cast " + value + " to a boolean.");
            return (bool)value ? True : False;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
