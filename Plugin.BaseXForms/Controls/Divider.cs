using Plugin.BaseXForms.Styles;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plugin.BaseXForms.Controls
{
    public class Divider : BoxView
    {
        public Divider()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.Start;
            Color = Colors.Divider;
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
            Color = Colors.Divider;
            Margin = new Thickness(4, 0);
            WidthRequest = 1;
        }
    }
}
