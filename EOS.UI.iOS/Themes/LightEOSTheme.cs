using System.Collections.Generic;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Themes
{
    public class LightEOSTheme : IEOSTheme
    {
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
            { EOSConstants.BackgroundColor, UIColor.White },
            { EOSConstants.TextColor, UIColor.Black },
            { EOSConstants.DisabledTextColor, UIColor.LightTextColor},
            { EOSConstants.PressedStateTextColor, UIColor.LightTextColor},
            { EOSConstants.TextSize, 17 },
            { EOSConstants.Font, UIFont.SystemFontOfSize(17)},
            { EOSConstants.CornerRadius, 3 },
            { EOSConstants.LetterSpacing, 1 },
        };
    }
}
