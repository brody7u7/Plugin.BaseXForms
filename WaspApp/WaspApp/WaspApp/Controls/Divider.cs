using WaspApp.Styles;
using Xamarin.Forms;

namespace WaspApp.Controls
{
    public class Divider : BoxView
    {
        public Divider()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.Start;
            Color = Colors.DividerColor;
            Margin = new Thickness(0, 4);
            HeightRequest = 1;
        }
    }

    public class DividerVertical : BoxView
    {
        public DividerVertical()
        {
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.Start;
            Color = Colors.DividerColor;
            Margin = new Thickness(4, 0);
            WidthRequest = 1;
        }
    }
}
