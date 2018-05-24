﻿using System;
using System.Collections.Generic;
using EOS.UI.Shared.Helpers;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

#if __IOS__
using CoreGraphics;
using EOS.UI.iOS.Helpers;
using UIKit;
#endif

#if __ANDROID__
using Android.Graphics;
using EOS.UI.Android;
#endif

namespace EOS.UI.Shared.Themes.Themes
{
    public class LightEOSTheme : IEOSTheme
    {
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
#if __IOS__
            { EOSConstants.BrandPrimaryColor, UIColor.FromRGB(60,109,240) },
            { EOSConstants.BrandPrimaryColorDisabled, UIColor.LightGray },
            { EOSConstants.BrandPrimaryColorPressed, UIColor.Gray },
            { EOSConstants.BrandSecondaryColor, UIColor.Black },
            { EOSConstants.BrandSecondaryColorDisabled, UIColor.LightTextColor},
            { EOSConstants.BrandSecondaryColorPressed, UIColor.LightTextColor},
            { EOSConstants.SemanticSuccessColor, UIColor.FromRGB(0,170,94)},
            { EOSConstants.SemanticErrorColor, UIColor.FromRGB(255,92,73)},
            { EOSConstants.SemanticWarningColor, UIColor.FromRGB(254,213,0)},
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
#endif

#if __ANDROID__
            { EOSConstants.BrandPrimaryColor, Color.Rgb(60,109,240) },
            { EOSConstants.BrandPrimaryColorDisabled, Color.LightCoral},
            { EOSConstants.BrandPrimaryColorPressed, Color.LightBlue},
            { EOSConstants.BrandSecondaryColor, Color.Black },
            { EOSConstants.BrandSecondaryColorDisabled, Color.LightGray},
            { EOSConstants.BrandSecondaryColorPressed, Color.LightGray},
            { EOSConstants.SemanticSuccessColor, Color.Rgb(0,170,94)},
            { EOSConstants.SemanticErrorColor, Color.Rgb(255,92,73)},
            { EOSConstants.SemanticWarningColor, Color.Rgb(254,213,0)},
            { EOSConstants.TextSize, 17f },
            { EOSConstants.Font, "Fonts/OpenSansRegular.ttf" },
            { EOSConstants.CornerRadius, 5f },
            { EOSConstants.LetterSpacing, 0.1f },
            { EOSConstants.HintTextColor, Color.LightGray },
            { EOSConstants.HintTextColorDisabled, Color.Gray },
            { EOSConstants.LeftImageFocused, Resource.Drawable.AccountCircle },
            { EOSConstants.LeftImageUnfocused, Resource.Drawable.AccountKey },
            { EOSConstants.LeftImageDisabled, Resource.Drawable.AccountOff },
            { EOSConstants.UnderlineColorFocused, Color.White },
            { EOSConstants.UnderlineColorUnfocused, Color.DarkGray },
            { EOSConstants.UnderlineColorDisabled, Color.LightGray },
            { EOSConstants.CalendarImage, Resource.Drawable.icCalendar },
            { EOSConstants.FabProgressPreloaderImage, Resource.Drawable.icPreloader },
            { EOSConstants.FabProgressPrimaryColor, new Color(255, 92, 73) },
            { EOSConstants.FabProgressDisabledColor, new Color(255, 92, 73) },
            { EOSConstants.FabProgressPressedColor, new Color(255, 92, 73) },
            { EOSConstants.CircleProgressShown, true}
#endif
        };
    }
}
