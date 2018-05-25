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
            { EOSConstants.BrandPrimaryTextColor, UIColor.White },
            { EOSConstants.BrandSecondaryTextColor, UIColor.Black },
            { EOSConstants.BrandSecondaryColorDisabled, UIColor.LightTextColor},
            { EOSConstants.BrandSecondaryColorPressed, UIColor.LightTextColor},
            { EOSConstants.BrandPrimaryColorVariant1, UIColor.FromRGB(49,81,183) },
            { EOSConstants.SemanticSuccessColor, UIColor.FromRGB(0,170,94)},
            { EOSConstants.SemanticErrorColor, UIColor.FromRGB(255,92,73)},
            { EOSConstants.SemanticWarningColor, UIColor.FromRGB(254,213,0)},
            { EOSConstants.NeutralColor1, UIColor.FromRGB(52,51,52)},
            { EOSConstants.NeutralColor2, UIColor.FromRGB(119,118,119)},
            { EOSConstants.NeutralColor3, UIColor.FromRGB(192,191,192)},
            { EOSConstants.NeutralColor4, UIColor.FromRGB(234,234,234)},
            { EOSConstants.NeutralColor5, UIColor.FromRGB(246,246,246)},
            { EOSConstants.NeutralColor6, UIColor.FromRGB(255,255,255)},
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
            { EOSConstants.BrandPrimaryTextColor, Color.White },
            { EOSConstants.BrandSecondaryTextColor, Color.Black },
            { EOSConstants.BrandSecondaryColorDisabled, Color.LightGray},
            { EOSConstants.BrandSecondaryColorPressed, Color.LightGray},
            { EOSConstants.BrandPrimaryColorVariant1, Color.Rgb(49,81,183) },
            { EOSConstants.SemanticSuccessColor, Color.Rgb(0,170,94)},
            { EOSConstants.SemanticErrorColor, Color.Rgb(255,92,73)},
            { EOSConstants.SemanticWarningColor, Color.Rgb(254,213,0)},
            { EOSConstants.NeutralColor1, Color.Rgb(52,51,52)},
            { EOSConstants.NeutralColor2, Color.Rgb(119,118,119)},
            { EOSConstants.NeutralColor3, Color.Rgb(192,191,192)},
            { EOSConstants.NeutralColor4, Color.Rgb(234,234,234)},
            { EOSConstants.NeutralColor5, Color.Rgb(246,246,246)},
            { EOSConstants.NeutralColor6, Color.Rgb(255,255,255)},
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
            { EOSConstants.CircleProgressShown, true}
#endif
        };
    }
}
