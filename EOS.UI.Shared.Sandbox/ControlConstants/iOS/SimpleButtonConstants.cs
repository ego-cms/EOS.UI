#if __IOS__
using System;
using System.Collections.Generic;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Helpers;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.Helpers;
using UIKit;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Shared.Sandbox.ControlConstants.iOS
{
    public static class SimpleButtonConstants
    {
        public static Dictionary<string, UIColor> BackgroundColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (UIColor)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.BrandPrimaryColor]);
        public static Dictionary<string, UIColor> DisabledBackgroundColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (UIColor)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor4S]);
        public static Dictionary<string, UIColor> PressedBackgroundColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (UIColor)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.BrandPrimaryColorVariant1]);
        public static Dictionary<string, UIColor> ShadowColors =>
        Colors.MainColorsCollection.AddDefault(Fields.Default, ((ShadowConfig)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.SimpleButtonShadow]).Color);
        public static Dictionary<string, UIColor> FontColors =>
            Colors.FontColorsCollection.AddDefault(Fields.Default, ((FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R3C5S]).Color);
        public static Dictionary<string, UIColor> DisabledFontColors =>
           Colors.FontColorsCollection.AddDefault(Fields.Default, ((FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R3C4S]).Color);
        public static Dictionary<string, float> LetterSpacings =>
            Sizes.LetterSpacingCollection.AddDefault(Fields.Default, ((FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R3C5S]).LetterSpacing);
        public static Dictionary<string, float> TextSizes =>
            Sizes.TextSizeCollection.AddDefault(Fields.Default, ((FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R3C5S]).Size);
        public static Dictionary<string, int> CornerRadiusCollection =>
            Sizes.CornerRadiusCollection.AddDefault(Fields.Default, (int)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.ButtonCornerRadius]);
        public static Dictionary<string, UIFont> SimpleButtonFonts =>
            Fonts.GetGhostButtonSimpleLabelFonts().AddDefault(Fields.Default, ((FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R3C5S]).Font);
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
                nfloat a, r, g, b;
                var shadowConfig = (ShadowConfig)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.SimpleButtonShadow];
                shadowConfig.Color.GetRGBA(out r, out g, out b, out a);
                return Shadow.OpacityCollection.AddDefault(Fields.Default, a);
            }
        }
    }
}
#endif
