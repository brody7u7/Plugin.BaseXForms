using Xamarin.Forms;
using WaspApp.Styles;

namespace WaspApp.Controls
{
    public class CustomPicker : Picker
    {
        /// <summary>
        /// Set or get the border color
        /// </summary>
        public Color BorderColor { get { return (Color)GetValue(BorderColorProperty); } set { SetValue(BorderColorProperty, value); } }
        public static BindableProperty BorderColorProperty = BindableProperty.Create(
            nameof(BorderColor),
            typeof(Color),
            typeof(CustomPicker),
            Colors.EntryBorderColor);

        public Color FocusedBorderColor { get { return (Color)GetValue(FocusedBorderColorProperty); } set { SetValue(FocusedBorderColorProperty, value); } }
        public static BindableProperty FocusedBorderColorProperty = BindableProperty.Create(
            nameof(FocusedBorderColor),
            typeof(Color),
            typeof(CustomPicker),
            Colors.PrimaryColor);

        public Color DisabledTextColor { get { return (Color)GetValue(DisabledTextColorProperty); } set { SetValue(DisabledTextColorProperty, value); } }
        public static BindableProperty DisabledTextColorProperty = BindableProperty.Create(
            nameof(DisabledTextColor),
            typeof(Color),
            typeof(CustomPicker),
            Colors.DisabledColor);

        public new Color BackgroundColor { get { return (Color)GetValue(BackgroundColorProperty); } set { SetValue(BackgroundColorProperty, value); } }
        public new static BindableProperty BackgroundColorProperty = BindableProperty.Create(
            nameof(BackgroundColor),
            typeof(Color),
            typeof(CustomPicker),
            Colors.EntryBackgroundColor);

        /// <summary>
        /// From 0 to 100 of radius
        /// </summary>
        public int BorderRadius { get { return (int)GetValue(BorderRadiusProperty); } set { SetValue(BorderRadiusProperty, value); } }
        public static BindableProperty BorderRadiusProperty = BindableProperty.Create(
            nameof(BorderRadius),
            typeof(int),
            typeof(CustomPicker),
            0);
        
        public int BorderStroke { get { return (int)GetValue(BorderStrockeProperty); } set { SetValue(BorderStrockeProperty, value); } }
        public static BindableProperty BorderStrockeProperty = BindableProperty.Create(
           nameof(BorderStroke),
           typeof(int),
           typeof(CustomPicker),
           1);

        public CustomPickerBorderType BorderType { get { return (CustomPickerBorderType)GetValue(BorderTypeProperty); } set { SetValue(BorderTypeProperty, value); } }
        public static BindableProperty BorderTypeProperty = BindableProperty.Create(
           nameof(BorderType),
           typeof(CustomPickerBorderType),
           typeof(CustomPicker),
           CustomPickerBorderType.Frame);

        public CustomPicker() { }
    }

    public enum CustomPickerBorderType
    {
        Frame, Line, None
    }
}
