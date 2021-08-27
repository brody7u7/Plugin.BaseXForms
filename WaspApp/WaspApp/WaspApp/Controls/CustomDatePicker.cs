using WaspApp.Styles;
using Xamarin.Forms;

namespace WaspApp.Controls
{
    public class CustomDatePicker : DatePicker
    {
        /// <summary>
        /// Set or get the border color
        /// </summary>
        public Color BorderColor { get { return (Color)GetValue(BorderColorProperty); } set { SetValue(BorderColorProperty, value); } }
        public static BindableProperty BorderColorProperty = BindableProperty.Create(
            nameof(BorderColor),
            typeof(Color),
            typeof(CustomDatePicker),
            Colors.EntryBorderColor);

        public Color FocusedBorderColor { get { return (Color)GetValue(FocusedBorderColorProperty); } set { SetValue(FocusedBorderColorProperty, value); } }
        public static BindableProperty FocusedBorderColorProperty = BindableProperty.Create(
            nameof(FocusedBorderColor),
            typeof(Color),
            typeof(CustomDatePicker),
            Colors.PrimaryColor);

        public Color DisabledTextColor { get { return (Color)GetValue(DisabledTextColorProperty); } set { SetValue(DisabledTextColorProperty, value); } }
        public static BindableProperty DisabledTextColorProperty = BindableProperty.Create(
            nameof(DisabledTextColor),
            typeof(Color),
            typeof(CustomDatePicker),
            Colors.DisabledColor);

        public new Color BackgroundColor { get { return (Color)GetValue(BackgroundColorProperty); } set { SetValue(BackgroundColorProperty, value); } }
        public new static BindableProperty BackgroundColorProperty = BindableProperty.Create(
            nameof(BackgroundColor),
            typeof(Color),
            typeof(CustomDatePicker),
            Colors.EntryBackgroundColor);

        /// <summary>
        /// From 0 to 100 of radius
        /// </summary>
        public int BorderRadius { get { return (int)GetValue(BorderRadiusProperty); } set { SetValue(BorderRadiusProperty, value); } }
        public static BindableProperty BorderRadiusProperty = BindableProperty.Create(
            nameof(BorderRadius),
            typeof(int),
            typeof(CustomDatePicker),
            0);

        public int BorderStroke { get { return (int)GetValue(BorderStrockeProperty); } set { SetValue(BorderStrockeProperty, value); } }
        public static BindableProperty BorderStrockeProperty = BindableProperty.Create(
           nameof(BorderStroke),
           typeof(int),
           typeof(CustomDatePicker),
           1);

        public CustomDatePickerBorderType BorderType { get { return (CustomDatePickerBorderType)GetValue(BorderTypeProperty); } set { SetValue(BorderTypeProperty, value); } }
        public static BindableProperty BorderTypeProperty = BindableProperty.Create(
           nameof(BorderType),
           typeof(CustomDatePickerBorderType),
           typeof(CustomDatePicker),
           CustomDatePickerBorderType.Frame);

        public CustomDatePicker() { }
    }

    public enum CustomDatePickerBorderType
    {
        Frame, Line, None
    }
}
