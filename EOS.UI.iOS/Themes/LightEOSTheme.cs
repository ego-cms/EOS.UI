﻿using System.Collections.Generic;
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
            { EOSConstants.PrimaryColorDisabled, UIColor.LightGray },
            { EOSConstants.PrimaryColorPressed, UIColor.Gray },
            { EOSConstants.SecondaryColor, UIColor.Black },
            { EOSConstants.SecondaryColorDisabled, UIColor.LightTextColor},
            { EOSConstants.SecondaryColorPressed, UIColor.LightTextColor},
            { EOSConstants.TextSize, 17 },
            { EOSConstants.Font, UIFont.SystemFontOfSize(17)},
            { EOSConstants.CornerRadius, 3 },
            { EOSConstants.LetterSpacing, 1 },
            { EOSConstants.HintTextColor, UIColor.LightGray },
            { EOSConstants.HintTextColorDisabled, UIColor.Gray },
            { EOSConstants.LeftImageFocused, "account-circle" },
            { EOSConstants.LeftImageUnfocused, "account-key" },
            { EOSConstants.LeftImageDisabled, "account-off" },
            { EOSConstants.UnderlineColorFocused, UIColor.White },
            { EOSConstants.UnderlineColorUnfocused, UIColor.DarkGray },
            { EOSConstants.UnderlineColorDisabled, UIColor.LightGray },
            { EOSConstants.CalendarImage, "icCalendar"},
            { EOSConstants.FabProgressPreloaderImage, "icPreloader"},
            { EOSConstants.FabProgressPrimaryColor, UIColor.FromRGB(255, 92, 73)},
            { EOSConstants.FabProgressPressedColor, UIColor.FromRGB(255, 92, 73)},
            { EOSConstants.FabProgressDisabledColor, UIColor.FromRGB(255, 92, 73)},
            { EOSConstants.FabProgressSize, 50},
            { EOSConstants.CircleProgressShown, true},
            { EOSConstants.FabShadow, new ShadowConfig(){
                        Color = UIColor.Black.CGColor,
                        Offset = new CGSize(0,0),
                        Radius = 2,
                        Opacity = 0.9f
                    }}
        };
    }
}
