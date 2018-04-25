using System.Collections.Generic;
using Android.Graphics;
using EOS.UI.Android;
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
            { EOSConstants.TextSize, 17f },
            { EOSConstants.Font, "Fonts/OpenSansRegular.ttf" },
            { EOSConstants.CornerRadius, 5f },
            { EOSConstants.LetterSpacing, 0.1f },
            { EOSConstants.HintTextColor, Color.Red },
            { EOSConstants.HintTextSize, 10f },
            { EOSConstants.LeftImageFocused, Resource.Drawable.DrawableLeftFocused },
            { EOSConstants.LeftImageUnfocused, Resource.Drawable.DrawableLeftFocused },
        };
    }
}