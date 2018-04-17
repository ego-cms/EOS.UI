using System.Collections.Generic;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Themes
{
    public class DarkEOSTheme : IEOSTheme
    {
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
            { EOSConstants.BackgroundColor, UIColor.Black },
            { EOSConstants.TextColor, UIColor.White },
            { EOSConstants.TextSize, 17 },
            { EOSConstants.Font, "Fonts/capture_it_light.ttf" },
            { EOSConstants.CornerRadius, 3 },
            { EOSConstants.LetterSpacing, 1 },
        };
    }
}
