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
    public static class CircleProgressConstants
    {
        private static FontStyleItem FontStyle => (FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R1C1];

        public static Dictionary<string, Color> CircleProgressColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.BrandPrimaryColor]);
        public static Dictionary<string, Color> AlternativeColors =>
           Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.BrandPrimaryColor]);
        public static Dictionary<string, Color> FillColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor4]);
        public static Dictionary<string, string> CircleProgressFonts =>
            Fonts.GetCircleProgressFonts().AddDefault(Fields.Default, "Fonts/Roboto-Bold.ttf");
        public static Dictionary<string, float> TextSizes =>
            Sizes.TextSizeCollection.AddDefault(Fields.Default, FontStyle.Size);
    }
}
#endif
