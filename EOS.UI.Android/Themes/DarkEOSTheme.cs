using System.Collections.Generic;
using Android.Graphics;
using EOS.UI.Android;
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
            { EOSConstants.TextSize, 22f },
            { EOSConstants.Font, "Fonts/OpenSansRegular.ttf" },
            { EOSConstants.CornerRadius, 20f },
            { EOSConstants.LetterSpacing, 0.5f },
            { EOSConstants.HintTextColor, Color.Red },
            { EOSConstants.HintTextSize, 10f },
            { EOSConstants.LeftImageFocused, Resource.Drawable.DrawableLeftFocused },
            { EOSConstants.LeftImageUnfocused, Resource.Drawable.DrawableLeftFocused },
        };
    }
}