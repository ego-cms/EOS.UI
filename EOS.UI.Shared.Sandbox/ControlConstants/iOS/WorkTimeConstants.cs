#if __IOS__
using System.Collections.Generic;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.Helpers;
using UIKit;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Shared.Sandbox.ControlConstants.iOS
{
    public static class WorkTimeConstants
    {
        private static FontStyleItem TitleFontStyle => (FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R2C2];
        private static FontStyleItem DayFontStyle => (FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R1C3];
        private static FontStyleItem CurrentDayFontStyle => (FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R1C6];

        public static Dictionary<string, UIFont> TitleFonts =>
            Fonts.GetWorkTimeTitleFonts().AddDefault(Fields.Default, TitleFontStyle.Font);

        public static Dictionary<string, UIFont> DayFonts =>
            Fonts.GetWorkTimeDayFonts().AddDefault(Fields.Default, DayFontStyle.Font);

        public static Dictionary<string, float> TitleTextSizes =>
            Sizes.TextSizeCollection.AddDefault(Fields.Default, TitleFontStyle.Size);

        public static Dictionary<string, float> DayTextSizes =>
            Sizes.TextSizeCollection.AddDefault(Fields.Default, DayFontStyle.Size);
        
         public static Dictionary<string, UIColor> TitleColors =>
            Colors.FontColorsCollection.AddDefault(Fields.Default, TitleFontStyle.Color);

        public static Dictionary<string, UIColor> DayColors =>
            Colors.FontColorsCollection.AddDefault(Fields.Default, DayFontStyle.Color);

        public static Dictionary<string, UIColor> CurrentDayColors =>
            Colors.FontColorsCollection.AddDefault(Fields.Default, CurrentDayFontStyle.Color);

        public static Dictionary<string, UIColor> CurrentDayBackgroundColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (UIColor)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.BrandPrimaryColor]);

        public static Dictionary<string, UIColor> DayEvenBackgroundColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (UIColor)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor5]);

        public static Dictionary<string, UIColor> DevidersColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (UIColor)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor4]);

        public static Dictionary<string, UIColor> CurrentDayDeviderColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (UIColor)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor4]);
    }
}

#endif