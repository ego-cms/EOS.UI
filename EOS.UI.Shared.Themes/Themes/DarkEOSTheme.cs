using System;
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
//using R = EOS.UI.Android.Resource;
using EOS.UI.Android;
#endif

namespace EOS.UI.Shared.Themes.Themes
{
    public class DarkEOSTheme : IEOSTheme
    {
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
#if __IOS__
            { EOSConstants.PrimaryColor, UIColor.Black },
            { EOSConstants.PrimaryColorDisabled, UIColor.Gray },
            { EOSConstants.PrimaryColorPressed, UIColor.LightGray },
            { EOSConstants.SecondaryColor, UIColor.White },
            { EOSConstants.SecondaryColorDisabled, UIColor.LightTextColor},
            { EOSConstants.SecondaryColorPressed, UIColor.LightTextColor},
            { EOSConstants.TextSize, 17 },
            { EOSConstants.Font, UIFont.SystemFontOfSize(17) },
            { EOSConstants.CornerRadius, 3 },
            { EOSConstants.LetterSpacing, 1 },
            { EOSConstants.HintTextColor, UIColor.Gray },
            { EOSConstants.HintTextColorDisabled, UIColor.LightGray },
            { EOSConstants.LeftImageFocused, "account-circle" },
            { EOSConstants.LeftImageUnfocused, "account-key" },
            { EOSConstants.LeftImageDisabled, "account-off" },
            { EOSConstants.UnderlineColorFocused, UIColor.Black },
            { EOSConstants.UnderlineColorUnfocused, UIColor.DarkGray },
            { EOSConstants.UnderlineColorDisabled, UIColor.LightGray },
            { EOSConstants.CalendarImage, "icCalendar"},
            { EOSConstants.FabProgressPreloaderImage, "icPreloader"},
            { EOSConstants.FabProgressPrimaryColor, UIColor.FromRGB(255, 92, 73)},
            { EOSConstants.FabProgressPressedColor, UIColor.FromRGB(255, 92, 73)},
            { EOSConstants.FabProgressDisabledColor, UIColor.FromRGB(255, 92, 73)},
            { EOSConstants.FabProgressSize, 50},
            { EOSConstants.CircleProgressShown, true},
            { EOSConstants.FabShadow,  new ShadowConfig(){
                        Color = UIColor.Black.CGColor,
                        Offset = new CGSize(0,0),
                        Radius = 1,
                        Opacity = 0.7f
                    }},
#endif

#if __ANDROID__
            { EOSConstants.PrimaryColor, Color.Black },
            { EOSConstants.PrimaryColorDisabled, Color.LightBlue},
            { EOSConstants.PrimaryColorPressed, Color.LightCoral},
            { EOSConstants.SecondaryColor, Color.White },
            { EOSConstants.SecondaryColorDisabled, Color.LightGray},
            { EOSConstants.SecondaryColorPressed, Color.LightGray},
            { EOSConstants.TextSize, 22f },
            { EOSConstants.Font, "Fonts/OpenSansRegular.ttf" },
            { EOSConstants.CornerRadius, 20f },
            { EOSConstants.LetterSpacing, 0.5f },
            { EOSConstants.HintTextColor, Color.Gray },
            { EOSConstants.HintTextColorDisabled, Color.LightGray },
            { EOSConstants.LeftImageFocused, Resource.Drawable.AccountCircle },
            { EOSConstants.LeftImageUnfocused, Resource.Drawable.AccountKey },
            { EOSConstants.LeftImageDisabled, Resource.Drawable.AccountOff },
            { EOSConstants.UnderlineColorFocused, Color.Black },
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
