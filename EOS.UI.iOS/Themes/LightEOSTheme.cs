using System.Collections.Generic;
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
            { EOSConstants.SecondaryColorDisabled, UIColor.LightGray},
            { EOSConstants.SecondaryColorPressed, UIColor.LightGray},
            { EOSConstants.TertiaryColor, UIColor.Blue },
            { EOSConstants.QuaternaryColor, UIColor.LightGray },
            { EOSConstants.TextSize, 17 },
            { EOSConstants.SecondaryTextSize, 17 },
            { EOSConstants.Font, UIFont.SystemFontOfSize(17)},
            { EOSConstants.SecondaryFont, UIFont.SystemFontOfSize(17) },
            { EOSConstants.CornerRadius, 3 },
            { EOSConstants.LetterSpacing, 1 },
            { EOSConstants.SecondaryLetterSpacing, 1 },
            { EOSConstants.HintTextColor, UIColor.LightGray },
            { EOSConstants.HintTextColorDisabled, UIColor.Gray },
            { EOSConstants.LeftImageFocused, "AccountCircle" },
            { EOSConstants.LeftImageUnfocused, "AccountKey" },
            { EOSConstants.LeftImageDisabled, "AccountOff" },
            { EOSConstants.UnderlineColorFocused, UIColor.White },
            { EOSConstants.UnderlineColorUnfocused, UIColor.DarkGray },
            { EOSConstants.UnderlineColorDisabled, UIColor.LightGray },
            { EOSConstants.BorderWidth, 2 },
            { EOSConstants.SectionTitle, "Light section" },
            { EOSConstants.SectionActionTitle, "View All" },
            { EOSConstants.LeftPadding, 5 },
            { EOSConstants.TopPadding, 7 },
            { EOSConstants.RightPadding, 7 },
            { EOSConstants.BottomPadding, 5 },
            { EOSConstants.HasSectionBorder, true },
            { EOSConstants.HasSectionAction, true }
        };
    }
}
