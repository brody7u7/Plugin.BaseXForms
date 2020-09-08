using Color = Xamarin.Forms.Color;

namespace Plugin.BaseXForms.Styles
{
    public static class Colors
    {
        // Common
        public static Color Primary = Color.FromHex("#24AB73");
        public static Color PrimaryDark = Color.FromHex("#4CD964");
        public static Color CommonBlack = Color.FromHex("#000000");
        public static Color CommonWhite = Color.FromHex("#FFFFFF");
        public static Color CommonGrayLight = Color.FromHex("#C1C2C3");
        public static Color CommonGray = Color.FromHex("#212B36");
        public static Color OKColor = Color.Green;
        public static Color Precaution = Color.FromHex("#FD9726");
        public static Color Danger = Color.FromHex("#B00020");
        public static Color Disabled = Color.LightGray;
        public static Color Divider = Color.FromHex("#F0F0F0");
        public static Color EntryBorderColor = Color.Gray;
        public static Color EntryBackgroundColor = Color.White;
        public static Color IconTappedBackColor = Color.FromHex("#33FFFFFF");

        // Backgrounds
        public static Color PrimaryLightBackground = Color.FromHex("#FFFFFF");
        public static Color SecondaryLightBackground = Color.FromHex("#F2F2F2");
        public static Color CellLightBackground = Color.FromHex("#FFFFFF");
        public static Color PopupLightBackground = Color.FromRgba(254, 254, 254, 0.7);
        public static Color PrimaryDarkBackground = Color.FromHex("#121212");
        public static Color SecondaryDarkBackground = Color.FromHex("#121212");
        public static Color CellDarkBackground = Color.FromHex("#202020");
        public static Color PopupDarkBackground = Color.FromRgba(18, 18, 18, 0.7);

        // Texts
        public static Color TextDarkPrimary = new Color(CommonBlack.R, CommonBlack.G, CommonBlack.B, 1);
        public static Color TextDarkSecondary = new Color(CommonBlack.R, CommonBlack.G, CommonBlack.B, 0.87);
        public static Color TextDarkHint = new Color(CommonBlack.R, CommonBlack.G, CommonBlack.B, 0.6);
        public static Color TextDarkDisabled = new Color(CommonBlack.R, CommonBlack.G, CommonBlack.B, 0.38);
        public static Color TextLightPrimary = new Color(CommonWhite.R, CommonWhite.G, CommonWhite.B, 1);
        public static Color TextLightSecondary = new Color(CommonWhite.R, CommonWhite.G, CommonWhite.B, 0.87);
        public static Color TextLightHint = new Color(CommonWhite.R, CommonWhite.G, CommonWhite.B, 0.6);
        public static Color TextLightDisabled = new Color(CommonWhite.R, CommonWhite.G, CommonWhite.B, 0.38);
    }
}
