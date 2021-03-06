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
    public static class GhostButtonConstants
    {
        private static FontStyleItem FontStyle => (FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R2C1S];

        public static Dictionary<string, UIFont> GhostButtonFonts =>
            Fonts.GetGhostButtonSimpleLabelFonts().AddDefault(Fields.Default, FontStyle.Font);
        public static Dictionary<string, float> LetterSpacings =>
            Sizes.LetterSpacingCollection.AddDefault(Fields.Default, FontStyle.LetterSpacing);
        public static Dictionary<string, float> TextSizes =>
            Sizes.TextSizeCollection.AddDefault(Fields.Default, FontStyle.Size);
        public static Dictionary<string, UIColor> FontColors =>
            Colors.GetGhostButtonFontColors().AddDefault(Fields.Default, FontStyle.Color);
        public static Dictionary<string, UIColor> DisabledFontColors =>
            Colors.GetGhostButtonFontColors().AddDefault(Fields.Default, (UIColor)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor3S]);
        public static Dictionary<string, UIColor> RippleColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (UIColor)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.RippleColor]);

    }
}

#endif
