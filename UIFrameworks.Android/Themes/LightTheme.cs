using System.Collections.Generic;
using Android.Graphics;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace UIFrameworks.Android.Themes
{
    public class LightTheme : ITheme
    {
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
            { Constants.BackgroundColor, Color.White },
            { Constants.TextColor, Color.Black },
            { Constants.TextSize, 17 },
            { Constants.Font, "Fonts/capture_it_dark" },
            { Constants.CornerRadius, 3 },
            { Constants.LetterSpacing, 1 },
        };
    }
}