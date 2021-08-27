using System;

namespace WaspApp.Converters
{
    public class UnixTimeToDatetimeConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var time = (long)value;
            DateTime date = DateTimeOffset.FromUnixTimeMilliseconds(time).DateTime;
            return date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var dt = (DateTime)value;
            return dt.Ticks;
        }
    }
}
