#if __ANDROID__
using System.Collections.Generic;
using Android.Graphics;
using EOS.UI.Droid.Themes;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.Helpers;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Shared.Sandbox.ControlConstants.Android
{
    public static class WorkTimeConstants
    {
        private static FontStyleItem TitleFontStyle => (FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R2C2];
        private static FontStyleItem DayFontStyle => (FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R1C3];
        private static FontStyleItem CurrentDayFontStyle => (FontStyleItem) EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R1C6S];

        public static Dictionary<string, string> TitleFonts =>
            Fonts.GetWorktimeTitleFonts().AddDefault(Fields.Default, "Fonts/Roboto-Bold.ttf");

        public static Dictionary<string, string> DayFonts =>
            Fonts.GetWorktimeDayFonts().AddDefault(Fields.Default, "Fonts/Roboto-Bold.ttf");

        public static Dictionary<string, float> TitleTextSizes =>
            Sizes.TextSizeCollection.AddDefault(Fields.Default, TitleFontStyle.Size);

        public static Dictionary<string, float> DayTextSizes =>
            Sizes.TextSizeCollection.AddDefault(Fields.Default, DayFontStyle.Size);

        public static Dictionary<string, Color> TitleColors =>
           Colors.FontColorsCollection.AddDefault(Fields.Default, TitleFontStyle.Color);

        public static Dictionary<string, Color> DayColors =>
            Colors.FontColorsCollection.AddDefault(Fields.Default, DayFontStyle.Color);

        public static Dictionary<string, Color> CurrentDayColors =>
            Colors.FontColorsCollection.AddDefault(Fields.Default, CurrentDayFontStyle.Color);

        public static Dictionary<string, Color> CurrentDayBackgroundColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.BrandPrimaryColor]);

        public static Dictionary<string, Color> DayEvenBackgroundColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor5]);

        public static Dictionary<string, Color> DividersColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor4]);

        public static Dictionary<string, Color> CurrentDayDividerColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor4]);
    }
}

#endif