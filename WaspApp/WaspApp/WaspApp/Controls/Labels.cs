using WaspApp.Styles;
using Xamarin.Forms;

namespace WaspApp.Controls
{
    public abstract class LabelBase : Label
    {
        public LabelBase(Color textColorLightTheme, Color textColorDarkTheme, double fontSize)
        {
            //this.SetAppThemeColor(TextColorProperty, textColorLightTheme, textColorDarkTheme);
            TextColor = textColorDarkTheme;
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
