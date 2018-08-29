#if __IOS__
using System;
using System.Collections.Generic;
using System.Linq;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.Helpers;
using UIKit;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Shared.Sandbox.ControlConstants.iOS
{
    public static class BadgeLabelConstants
    {
        public static Dictionary<string, UIColor> BackgroundColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (UIColor)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.BrandPrimaryColor]);
        public static Dictionary<string, UIColor> FontColors =>
            Colors.FontColorsCollection.AddDefault(Fields.Default, ((FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R2C5S]).Color);
        public static Dictionary<string, float> LetterSpacings =>
            Sizes.LetterSpacingCollection.AddDefault(Fields.Default, ((FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R2C5S]).LetterSpacing);
        public static Dictionary<string, float> TextSizes =>
            Sizes.TextSizeCollection.AddDefault(Fields.Default, ((FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R2C5S]).Size);
        public static Dictionary<string, int> CornerRadiusCollection =>
            Sizes.CornerRadiusCollection.AddDefault(Fields.Default, (int)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.LabelCornerRadius]);
        public static Dictionary<string, UIFont> BadgeLabelFonts =>
            Fonts.GetButtonLabelFonts().AddDefault(Fields.Default, ((FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R2C5S]).Font);
    }
}
#endif