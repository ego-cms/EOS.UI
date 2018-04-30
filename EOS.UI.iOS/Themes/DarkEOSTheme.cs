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
            { EOSConstants.PrimaryColor, UIColor.Black },
            { EOSConstants.SecondaryColor, UIColor.White },
            { EOSConstants.DisabledTextColor, UIColor.LightTextColor},
            { EOSConstants.PressedStateTextColor, UIColor.LightTextColor},
            { EOSConstants.TextSize, 17 },
            { EOSConstants.Font, UIFont.SystemFontOfSize(17) },
            { EOSConstants.CornerRadius, 3 },
            { EOSConstants.LetterSpacing, 1 },
        };
    }
}
