using System;

namespace WaspApp.Converters
{
    public class StringToBooleanConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if (!(value is string)) throw new InvalidCastException("Unable to cast " + value + " to a string.");
            return !string.IsNullOrEmpty((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
