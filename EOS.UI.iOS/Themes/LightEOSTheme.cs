using System.Collections.Generic;
using CoreGraphics;
using EOS.UI.iOS.Helpers;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Themes
{
    public class LightEOSTheme : IEOSTheme
    {
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
            { EOSConstants.PrimaryColor, UIColor.White },
            { EOSConstants.SecondaryColor, UIColor.Black },
            { EOSConstants.DisabledTextColor, UIColor.LightTextColor},
            { EOSConstants.PressedStateTextColor, UIColor.LightTextColor},
            { EOSConstants.TextSize, 17 },
            { EOSConstants.Font, UIFont.SystemFontOfSize(17)},
            { EOSConstants.CornerRadius, 3 },
            { EOSConstants.LetterSpacing, 1 },
            { EOSConstants.HintTextColor, UIColor.LightGray },
            { EOSConstants.HintTextColorDisabled, UIColor.Gray },
            { EOSConstants.LeftImageFocused, "AccountCircle" },
            { EOSConstants.LeftImageUnfocused, "AccountKey" },
            { EOSConstants.LeftImageDisabled, "AccountOff" },
            { EOSConstants.UnderlineColorFocused, UIColor.White },
            { EOSConstants.UnderlineColorUnfocused, UIColor.DarkGray },
            { EOSConstants.UnderlineColorDisabled, UIColor.LightGray },
            { EOSConstants.CalendarImage, "calendar"},
            { EOSConstants.FabProgressPreloaderImage, "preloader"},
            { EOSConstants.FabProgressPrimaryColor, UIColor.FromRGB(255, 92, 73)},
            { EOSConstants.FabProgressPressedColor, UIColor.FromRGB(255, 92, 73)},
            { EOSConstants.FabProgressDisabledColor, UIColor.FromRGB(255, 92, 73)},
			{ EOSConstants.FabShadow, new ShadowConfig(){
                        Color = UIColor.LightGray.CGColor,
                        Offset = new CGSize(0,0),
                        Radius = 5,
                        Opacity = 0.7f
                    }},
            { EOSConstants.FabProgressSize, 50},
        };
    }
}
