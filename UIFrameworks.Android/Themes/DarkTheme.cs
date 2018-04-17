using System.Collections.Generic;
using Android.Graphics;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace UIFrameworks.Android.Themes
{
    public class DarkTheme : ITheme
    {
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
            { Constants.BackgroundColor, Color.Black },
            { Constants.TextColor, Color.White },
            { Constants.TextSize, 17 },
            { Constants.Font, "Fonts/capture_it_light.ttf" },
            { Constants.CornerRadius, 3 },
            { Constants.LetterSpacing, 1 },
        };
    }
}