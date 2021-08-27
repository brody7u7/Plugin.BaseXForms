using Xamarin.Forms;

namespace WaspApp.Controls
{
    public class CustomSearchBar : SearchBar
    {
        public Color IconColor { get { return (Color)GetValue(IconColorProperty); } set { SetValue(IconColorProperty, value); } }
        public static readonly BindableProperty IconColorProperty = BindableProperty.Create(
            nameof(IconColor),
            typeof(Color),
            typeof(CustomSearchBar),
            Color.Black);
    }
}
