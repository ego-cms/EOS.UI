using System.Collections.Generic;
using Android.Graphics;
using EOS.UI.Android;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace UIFrameworks.Android.Themes
{
    public class DarkEOSTheme : IEOSTheme
    {
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
            { EOSConstants.PrimaryColor, Color.Black },
            { EOSConstants.SecondaryColor, Color.White },
            { EOSConstants.SecondaryColorDisabled, Color.LightGray},
            { EOSConstants.SecondaryColorPressed, Color.LightGray},
            { EOSConstants.TertiaryColor, Color.AliceBlue },
            { EOSConstants.QuaternaryColor, Color.LightGray },
            { EOSConstants.TextSize, 22f },
            { EOSConstants.SecondaryTextSize, 22f },
            { EOSConstants.Font, "Fonts/OpenSansRegular.ttf" },
            { EOSConstants.SecondaryFont, "Fonts/OpenSansRegular.ttf" },
            { EOSConstants.CornerRadius, 20f },
            { EOSConstants.LetterSpacing, 0.5f },
            { EOSConstants.SecondaryLetterSpacing, 0.5f },
            { EOSConstants.HintTextColor, Color.Gray },
            { EOSConstants.HintTextColorDisabled, Color.LightGray },
            { EOSConstants.LeftImageFocused, Resource.Drawable.AccountCircle },
            { EOSConstants.LeftImageUnfocused, Resource.Drawable.AccountKey },
            { EOSConstants.LeftImageDisabled, Resource.Drawable.AccountOff },
            { EOSConstants.UnderlineColorFocused, Color.Black },
            { EOSConstants.UnderlineColorUnfocused, Color.DarkGray },
            { EOSConstants.UnderlineColorDisabled, Color.LightGray },
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