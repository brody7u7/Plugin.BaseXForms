using System;
using WaspApp.Styles;
using Xamarin.Forms;

namespace WaspApp.Controls
{
    public class MonthYearPickerView : View
    {
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
           propertyName: nameof(FontSize),
           returnType: typeof(double),
           declaringType: typeof(MonthYearPickerView),
           defaultValue: Device.GetNamedSize(NamedSize.Medium, typeof(Label)));

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: nameof(TextColor),
            returnType: typeof(Color),
            declaringType: typeof(MonthYearPickerView),
            defaultValue: Color.White);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty InfiniteScrollProperty = BindableProperty.Create(
            propertyName: nameof(InfiniteScroll),
            returnType: typeof(bool),
            declaringType: typeof(MonthYearPickerView),
            defaultValue: true);

        public bool InfiniteScroll
        {
            get => (bool)GetValue(InfiniteScrollProperty);
            set => SetValue(InfiniteScrollProperty, value);
        }

        public static readonly BindableProperty DateProperty = BindableProperty.Create(
            propertyName: nameof(Date),
            returnType: typeof(DateTime),
            declaringType: typeof(MonthYearPickerView),
            defaultValue: default,
            defaultBindingMode: BindingMode.TwoWay);

        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        public static readonly BindableProperty MaxDateProperty = BindableProperty.Create(
            propertyName: nameof(MaxDate),
            returnType: typeof(DateTime),
            declaringType: typeof(MonthYearPickerView),
            defaultValue: DateTime.MaxValue,
            defaultBindingMode: BindingMode.TwoWay);

        public DateTime MaxDate
        {
            get => (DateTime)GetValue(MaxDateProperty);
            set => SetValue(MaxDateProperty, value);
        }

        public static readonly BindableProperty MinDateProperty = BindableProperty.Create(
            propertyName: nameof(MinDate),
            returnType: typeof(DateTime),
            declaringType: typeof(MonthYearPickerView),
            defaultValue: DateTime.MinValue,
            defaultBindingMode: BindingMode.TwoWay);

        public DateTime MinDate
        {
            get => (DateTime)GetValue(MinDateProperty);
            set => SetValue(MinDateProperty, value);
        }

        public Color BorderColor { get { return (Color)GetValue(BorderColorProperty); } set { SetValue(BorderColorProperty, value); } }
        public static BindableProperty BorderColorProperty = BindableProperty.Create(
            nameof(BorderColor),
            typeof(Color),
            typeof(MonthYearPickerView),
            Colors.EntryBorderColor);

        public Color FocusedBorderColor { get { return (Color)GetValue(FocusedBorderColorProperty); } set { SetValue(FocusedBorderColorProperty, value); } }
        public static BindableProperty FocusedBorderColorProperty = BindableProperty.Create(
            nameof(FocusedBorderColor),
            typeof(Color),
            typeof(MonthYearPickerView),
            Colors.PrimaryColor);

        public Color DisabledTextColor { get { return (Color)GetValue(DisabledTextColorProperty); } set { SetValue(DisabledTextColorProperty, value); } }
        public static BindableProperty DisabledTextColorProperty = BindableProperty.Create(
            nameof(DisabledTextColor),
            typeof(Color),
            typeof(MonthYearPickerView),
            Colors.DisabledColor);

        /// <summary>
        /// 0% - 100%
        /// </summary>
        public int BorderRadius { get { return (int)GetValue(BorderRadiusProperty); } set { SetValue(BorderRadiusProperty, value); } }
        public static BindableProperty BorderRadiusProperty = BindableProperty.Create(
            nameof(BorderRadius),
            typeof(int),
            typeof(MonthYearPickerView),
            0);

        public int BorderStroke { get { return (int)GetValue(BorderStrockeProperty); } set { SetValue(BorderStrockeProperty, value); } }
        public static BindableProperty BorderStrockeProperty = BindableProperty.Create(
           nameof(BorderStroke),
           typeof(int),
           typeof(MonthYearPickerView),
           1);

        public MonthYearPickerViewBorderType BorderType { get { return (MonthYearPickerViewBorderType)GetValue(BorderTypeProperty); } set { SetValue(BorderTypeProperty, value); } }
        public static BindableProperty BorderTypeProperty = BindableProperty.Create(
           nameof(BorderType),
           typeof(MonthYearPickerViewBorderType),
           typeof(MonthYearPickerView),
           MonthYearPickerViewBorderType.Frame);
    }

    public enum MonthYearPickerViewBorderType
    {
        Frame, Line, None
    }
}
