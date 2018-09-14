#if __ANDROID__
using System;
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
    public static class SectionConstants
    {
        private static FontStyleItem ButtonFontStyle => (FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R2C1S];
        private static FontStyleItem SectionFontStyle => (FontStyleItem)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.R2C3];

        public static Dictionary<string, int> TopPaddings => 
                Sizes.PaddingsCollection.AddDefault(Fields.Default, (int)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.TopPadding]);
        public static Dictionary<string, int> BottomPaddings =>  
                Sizes.PaddingsCollection.AddDefault(Fields.Default, (int)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.BottomPadding]);
        public static Dictionary<string, int> LeftPaddings =>  
                Sizes.PaddingsCollection.AddDefault(Fields.Default, (int)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.LeftPadding]);
        public static Dictionary<string, int> RightPaddings =>  
                Sizes.PaddingsCollection.AddDefault(Fields.Default, (int)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.RightPadding]);
        public static Dictionary<string, int> BorderWidth =>
                Sizes.BorderWidthCollection.AddDefault(Fields.Default, (int)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.BorderWidth]);
        public static Dictionary<string, Color> BorderColors => 
                Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor4]);
        public static Dictionary<string, Color> BackgroundsColors => 
                Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor5]);
        public static Dictionary<string, Color> ButtonTextColors => 
                Colors.MainColorsCollection.AddDefault(Fields.Default, ButtonFontStyle.Color);
        public static Dictionary<string, Color> SectionTextColors => 
                Colors.MainColorsCollection.AddDefault(Fields.Default, SectionFontStyle.Color);
        public static Dictionary<string, float> ButtonTextSizes => 
                Sizes.TextSizeCollection.AddDefault(Fields.Default, ButtonFontStyle.Size);
        public static Dictionary<string, float> SectionTextSizes => 
                Sizes.TextSizeCollection.AddDefault(Fields.Default, SectionFontStyle.Size);
        public static Dictionary<string, float> ButtonLetterSpacings => 
                Sizes.LetterSpacingCollection.AddDefault(Fields.Default, ButtonFontStyle.LetterSpacing);
        public static Dictionary<string, float> SectionLetterSpacings => 
                Sizes.LetterSpacingCollection.AddDefault(Fields.Default, SectionFontStyle.LetterSpacing);
        public static Dictionary<string, string> ButtonFonts => 
                Fonts.GetSectionFonts().AddDefault(Fields.Default, "Fonts/Roboto-Bold.ttf");
         public static Dictionary<string, string> SectionFonts =>
                Fonts.GetSectionFonts().AddDefault(Fields.Default, "Fonts/Roboto-Bold.ttf");
    }
}

#endif