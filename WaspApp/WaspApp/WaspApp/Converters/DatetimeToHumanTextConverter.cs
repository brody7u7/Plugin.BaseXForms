using WaspApp.Content.Resources;
using System;

namespace WaspApp.Converters
{
    public enum DatetimeToHumanTextStyle
    {
        /// <summary>
        /// M/d/yyyy
        /// </summary>
        ShortDate,
        /// <summary>
        /// dddd, MMMM d, yyyy
        /// </summary>
        LongDate,
        /// <summary>
        /// h:mm tt
        /// </summary>
        ShortTime,
        /// <summary>
        /// h:mm:ss tt
        /// </summary>
        LongTime,
        /// <summary>
        /// HH:mm dd/MM/yyyy
        /// </summary>
        ShortDateTime,
        /// <summary>
        /// Specify the amount of time lapsed from the given datetime.
        /// </summary>
        Ago,
        /// <summary>
        /// hh:mm tt
        /// </summary>
        Hour,
        /// <summary>
        /// Specify amount of years lapsed from the given datetime.
        /// </summary>
        Age,
        /// <summary>
        /// Specify a custom pattern. Used together with DatetimeToHumanTextConverter.CustomPattern property.
        /// </summary>
        Custom
    }

    public enum DatetimeToHumanTextInputType
    {
        DateTime, UnixTimeSecond, UnixTimeMillisecond
    }

    public class DatetimeToHumanTextConverter : Xamarin.Forms.IValueConverter
    {
        public DatetimeToHumanTextStyle Style { get; set; } = DatetimeToHumanTextStyle.ShortDate;
        public DatetimeToHumanTextInputType InputType { get; set; } = DatetimeToHumanTextInputType.DateTime;
        public string CustomPattern { get; set; }
        public bool FromUtc { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter != null)
            {
                if (parameter is DatetimeToHumanTextStyle)
                    Style = (DatetimeToHumanTextStyle)parameter;
                else if (parameter is DatetimeToHumanTextInputType)
                    InputType = (DatetimeToHumanTextInputType)parameter;
                else if (parameter is string)
                    CustomPattern = (string)parameter;
            }
                
            DateTime? date;
            if (InputType == DatetimeToHumanTextInputType.DateTime)
                date = value as DateTime?;
            else if (InputType == DatetimeToHumanTextInputType.UnixTimeSecond)
                date = DateTimeOffset.FromUnixTimeSeconds(((long)value)).DateTime;
            else
                date = DateTimeOffset.FromUnixTimeMilliseconds(((long)value)).DateTime;
            
            if (date == null)
                return Labels.NA;
            
            if (!date.HasValue)
            {
                DateTime temp;
                if (value is string && DateTime.TryParse((string)value, out temp))
                    date = temp;
                else
                    return Labels.NA;
            }

            if (FromUtc)
                date = date.Value.ToLocalTime();
            
            switch (Style)
            {
                case DatetimeToHumanTextStyle.ShortDate: return date.Value.ToShortDateString();
                case DatetimeToHumanTextStyle.LongDate: return date.Value.ToLongDateString();
                case DatetimeToHumanTextStyle.ShortTime: return date.Value.ToShortTimeString();
                case DatetimeToHumanTextStyle.LongTime: return date.Value.ToLongTimeString();
                case DatetimeToHumanTextStyle.ShortDateTime: return date.Value.ToString("HH:mm dd/MM/yyyy");
                case DatetimeToHumanTextStyle.Ago: return DateTimeAgo(date.Value);
                case DatetimeToHumanTextStyle.Hour: return date.Value.ToString("hh:mm tt");
                case DatetimeToHumanTextStyle.Age: return CalculateAge(date.Value);
                case DatetimeToHumanTextStyle.Custom: return date.Value.ToString(CustomPattern);
                default: return date.Value.ToShortDateString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DateTime.Now;
        }

        private string DateTimeAgo(DateTime date)
        {
            TimeSpan span = DateTime.Now.Subtract(date);
            int differenceDays = (int)span.TotalDays;

            if (differenceDays <= 0)
            {
                // TODO Get time ago
                return DateTimeFormats.Now;
            }
            else if (differenceDays < 7)
            {
                if (differenceDays == 1)
                    return DateTimeFormats.DayAgoFormat;
                else
                    return string.Format(DateTimeFormats.DaysAgoFormat, differenceDays);
            }
            else if (differenceDays < 31)
            {
                double weeks = Math.Ceiling((double)differenceDays / 7);
                if (weeks == 1)
                    return DateTimeFormats.WeekAgoFormat;
                else
                    return string.Format(DateTimeFormats.WeeksAgoFormat, weeks);
            }
            else if (differenceDays < 365)
            {
                double months = Math.Ceiling((double)differenceDays / 31);
                if (months == 1)
                    return DateTimeFormats.MonthAgoFormat;
                else
                    return string.Format(DateTimeFormats.MonthsAgoFormat, months);
            }
            else
            {
                double years = Math.Ceiling((double)differenceDays / 365);
                if (years == 1)
                    return DateTimeFormats.YearAgoFormat;
                else
                    return string.Format(DateTimeFormats.YearsAgoFormat, years);
            }
        }

        private string CalculateAge(DateTime date)
        {
            int age = System.Convert.ToInt32((DateTime.Today - date).TotalDays / 365.2425);
            return string.Format(DateTimeFormats.YearsOldFormat, age);
        }
    }
}
