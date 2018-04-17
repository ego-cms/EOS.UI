using System.Collections.Generic;
using Android.Graphics;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace UIFrameworks.Android.Themes
{
    public class LightEOSTheme : IEOSTheme
    {
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
            { EOSConstants.BackgroundColor, Color.White },
            { EOSConstants.TextColor, Color.Black },
            { EOSConstants.TextSize, 17 },
            { EOSConstants.Font, "Fonts/capture_it_dark" },
            { EOSConstants.CornerRadius, 3 },
            { EOSConstants.LetterSpacing, 1 },
        };
    }
}