using System;

namespace Plugin.BaseXForms.Converters
{
    public class InvertedBooleanConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }

            throw new InvalidCastException("Unable to cast " + value + " to a boolean.");
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }

            throw new InvalidCastException("Unable to cast " + value + " to a boolean.");
        }
    }
}