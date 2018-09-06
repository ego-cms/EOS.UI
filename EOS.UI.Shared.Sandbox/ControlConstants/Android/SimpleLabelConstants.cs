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
    public static class SimpleLabelConstants
    {
        private static FontStyleItem FontStyle => (FontStyleItem) EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R2C1S];

        public static Dictionary<string, string> SimpleLabelFonts =>
               Fonts.GetGhostButtonSimpleLabelFonts().AddDefault(Fields.Default, "Fonts/Roboto-Bold.ttf");
        public static Dictionary<string, Color> FontColors =>
               Colors.FontColorsCollection.AddDefault(Fields.Default, FontStyle.Color);
        public static Dictionary<string, float> TextSizes =>
               Sizes.TextSizeCollection.AddDefault(Fields.Default, FontStyle.Size);
        public static Dictionary<string, float> LetterSpacings =>
               Sizes.LetterSpacingCollection.AddDefault(Fields.Default, FontStyle.LetterSpacing);
    }
}

#endif