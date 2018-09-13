#if __ANDROID__
using System;
using System.Collections.Generic;
using Android.Graphics;
using EOS.UI.Droid.Themes;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.Helpers;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Shared.Sandbox.ControlConstants.Android
{
    public static class CircleMenuConstants
    {
        public static Dictionary<string, Color> FocusedBackgroundColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.BrandPrimaryColor]);
        public static Dictionary<string, Color> UnfocusedBackgroundColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor6S]);
        public static Dictionary<string, Color> FocusedIconColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor6S]);
        public static Dictionary<string, Color> UnfocusedIconColors =>
            Colors.MainColorsCollection.AddDefault(Fields.Default, (Color)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.NeutralColor1S]);
    }
}
#endif