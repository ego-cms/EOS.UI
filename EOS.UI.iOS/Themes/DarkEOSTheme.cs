using System.Collections.Generic;
using CoreGraphics;
using EOS.UI.iOS.Helpers;
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
            { EOSConstants.PrimaryColorDisabled, UIColor.Gray },
            { EOSConstants.PrimaryColorPressed, UIColor.LightGray },
            { EOSConstants.SecondaryColor, UIColor.White },
            { EOSConstants.SecondaryColorDisabled, UIColor.LightGray},
            { EOSConstants.SecondaryColorPressed, UIColor.LightGray},
            { EOSConstants.TertiaryColor, UIColor.Blue },
            { EOSConstants.QuaternaryColor, UIColor.LightGray },
            { EOSConstants.TextSize, 17 },
            { EOSConstants.SecondaryTextSize, 22 },
            { EOSConstants.Font, UIFont.SystemFontOfSize(17) },
            { EOSConstants.SecondaryFont, UIFont.SystemFontOfSize(17) },
            { EOSConstants.CornerRadius, 3 },
            { EOSConstants.LetterSpacing, 1 },
            { EOSConstants.SecondaryLetterSpacing, 2 },
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

            { EOSConstants.BorderWidth, 2 },
            { EOSConstants.SectionTitle, "Dark section" },
            { EOSConstants.SectionActionTitle, "View All" },
            { EOSConstants.LeftPadding, 5 },
            { EOSConstants.TopPadding, 5 },
            { EOSConstants.RightPadding, 5 },
            { EOSConstants.BottomPadding, 5 },
            { EOSConstants.HasSectionBorder, false },
            { EOSConstants.HasSectionAction, true }
        };
    }
}
