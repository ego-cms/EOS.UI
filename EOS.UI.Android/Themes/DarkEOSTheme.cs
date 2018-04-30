using System.Collections.Generic;
using Android.Graphics;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace UIFrameworks.Android.Themes
{
    public class DarkEOSTheme : IEOSTheme
    {
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
            { EOSConstants.BackgroundColor, Color.Black },
            { EOSConstants.TextColor, Color.White },
            { EOSConstants.DisabledTextColor, Color.LightGray},
            { EOSConstants.PressedStateTextColor, Color.LightGray},
            { EOSConstants.TextSize, 22f },
            { EOSConstants.Font, "Fonts/OpenSansRegular.ttf" },
            { EOSConstants.CornerRadius, 20f },
            { EOSConstants.LetterSpacing, 0.5f },
        };
    }
}