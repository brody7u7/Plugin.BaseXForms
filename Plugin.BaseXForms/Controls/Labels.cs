using System;
using System.Collections.Generic;
using System.Text;
using Plugin.BaseXForms.Styles;
using Xamarin.Forms;

namespace Plugin.BaseXForms.Controls
{
    public class Labels
    {
        public abstract class LabelBase : Label
        {
            public LabelBase(Color textColorLightTheme, Color textColorDarkTheme, double fontSize)
            {
                this.SetAppThemeColor(TextColorProperty, textColorLightTheme, textColorDarkTheme);
                FontSize = fontSize;
            }
        }

        public class LabelPrimary : LabelBase
        {
            public LabelPrimary() : base(Colors.TextDarkPrimary, Colors.TextLightPrimary, Fonts.MediumSize) { }
        }
        public class LabelSecondary : LabelBase
        {
            public LabelSecondary() : base(Colors.TextDarkSecondary, Colors.TextLightSecondary, Fonts.SmallSize) { }
        }
        public class LabelHint : LabelBase
        {
            public LabelHint() : base(Colors.TextDarkHint, Colors.TextLightHint, Fonts.ExtraSmallSize) { }
        }
    }
}
