using System;
using System.Globalization;
using Xamarin.Forms;

namespace WaspApp.Converters
{
    public class DayOfWeekConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            if (!(value is DayOfWeek)) throw new InvalidCastException("Unable to cast " + value + " to a DayOfWeek.");

            switch ((DayOfWeek)value)
            {
                case DayOfWeek.Sunday: return Content.Resources.DateTimeFormats.Sunday;
                case DayOfWeek.Monday: return Content.Resources.DateTimeFormats.Monday;
                case DayOfWeek.Tuesday: return Content.Resources.DateTimeFormats.Tuesday;
                case DayOfWeek.Wednesday: return Content.Resources.DateTimeFormats.Wednesday;
                case DayOfWeek.Thursday: return Content.Resources.DateTimeFormats.Thursday;
                case DayOfWeek.Friday: return Content.Resources.DateTimeFormats.Friday;
                case DayOfWeek.Saturday: return Content.Resources.DateTimeFormats.Saturday;
                default: return Content.Resources.Labels.Unknown;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
