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
    public static class InputConstants
    {
        private static FontStyleItem FontStyle => (FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R4C2];

        public static Dictionary<string, string> InputFonts =>
            Fonts.GetInputFonts().AddDefault(Fields.Default, "Fonts/Roboto-Regular.ttf");
        public static Dictionary<string, float> LetterSpacings =>
            Sizes.LetterSpacingCollection.AddDefault(Fields.Default, FontStyle.LetterSpacing);
        public static Dictionary<string, float> TextSizes =>
            Sizes.TextSizeCollection.AddDefault(Fields.Default, FontStyle.Size);
        public static Dictionary<string, Color> FontColors =>
            Colors.FontColorsCollection.AddDefault(Fields.Default, FontStyle.Color);
        public static Dictionary<string, Color> DisabledFontColors =>
        Colors.FontColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor3]);
        public static Dictionary<string, Color> PlaceholderColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor2]);
        public static Dictionary<string, Color> DisabledPlaceholderColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor3]);
        public static Dictionary<string, Color> IconColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor2]);
        public static Dictionary<string, Color> PopulatedIconColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.BrandPrimaryColor]);
        public static Dictionary<string, Color> UnderlineColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor3]);
        public static Dictionary<string, Color> PopulatedUnderlineColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor3]);
        public static Dictionary<string, Color> FocusedColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.BrandPrimaryColor]);
        public static Dictionary<string, Color> DisabledColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.DisabledInputColor]);
    }
}

#endif
