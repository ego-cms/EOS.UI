#if __ANDROID__
using System.Collections.Generic;
using Android.Graphics;
using EOS.UI.Droid.Themes;
using EOS.UI.Shared.Helpers;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.Helpers;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Shared.Sandbox.ControlConstants.Android
{
    public static class SimpleButtonConstants
    {
        private static FontStyleItem FontStyle => (FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R3C5S];

        public const int LeftPadding = 15;
        public const int RightPadding = 15;
        public const int TopPadding = 0;
        public const int BottomPadding = 0;

        public static Dictionary<string, Color> BackgroundColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.BrandPrimaryColor]);
        public static Dictionary<string, Color> DisabledBackgroundColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor4S]);
        public static Dictionary<string, Color> PressedBackgroundColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.BrandPrimaryColorVariant1]);
        public static Dictionary<string, Color> ShadowColors =>
        Colors.MainColorsCollection.AddDefault(Fields.Default, ((ShadowConfig)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.SimpleButtonShadow]).Color);
        public static Dictionary<string, Color> FontColors =>
            Colors.FontColorsCollection.AddDefault(Fields.Default, FontStyle.Color);
        public static Dictionary<string, Color> DisabledFontColors =>
           Colors.FontColorsCollection.AddDefault(Fields.Default, ((FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R3C4S]).Color);
        public static Dictionary<string, Color> RippleColors =>
        Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.BrandPrimaryColorVariant1]);
        public static Dictionary<string, float> LetterSpacings =>
            Sizes.LetterSpacingCollection.AddDefault(Fields.Default, FontStyle.LetterSpacing);
        public static Dictionary<string, float> TextSizes =>
            Sizes.TextSizeCollection.AddDefault(Fields.Default, FontStyle.Size);
        public static Dictionary<string, int> CornerRadiusCollection =>
            Sizes.CornerRadiusCollection.AddDefault(Fields.Default, (int)(float)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.ButtonCornerRadius]);
        public static Dictionary<string, string> SimpleButtonFonts =>
            Fonts.GetButtonBadgeFonts().AddDefault(Fields.Default, "Fonts/Roboto-Medium.ttf");
        public static Dictionary<string, int> ShadowOffsetXCollection =>
            Shadow.OffsetCollection.AddDefault(Fields.Default, (int)((ShadowConfig)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.SimpleButtonShadow]).Offset.X);
        public static Dictionary<string, int> ShadowOffsetYCollection =>
            Shadow.OffsetCollection.AddDefault(Fields.Default, (int)((ShadowConfig)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.SimpleButtonShadow]).Offset.Y);
        public static Dictionary<string, int> ShadowRadiusCollection =>
            Shadow.RadiusCollection.AddDefault(Fields.Default, (int)((ShadowConfig)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.SimpleButtonShadow]).Blur);
        public static Dictionary<string, double> ShadowOpacityCollection
        {
            get
            {
                var shadowConfig = (ShadowConfig) EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.SimpleButtonShadow];
                return Shadow.OpacityCollection.AddDefault(Fields.Default, shadowConfig.Color.A);
            }
        }
    }
}
#endif
