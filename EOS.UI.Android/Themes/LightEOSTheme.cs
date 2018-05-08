using System.Collections.Generic;
using Android.Graphics;
using EOS.UI.Android;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace UIFrameworks.Android.Themes
{
    public class LightEOSTheme : IEOSTheme
    {
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
            { EOSConstants.PrimaryColor, Color.White },
            { EOSConstants.SecondaryColor, Color.Black },
            { EOSConstants.SecondaryColorDisabled, Color.LightGray},
            { EOSConstants.SecondaryColorPressed, Color.LightGray},
            { EOSConstants.TertiaryColor, Color.Blue },
            { EOSConstants.QuaternaryColor, Color.LightGray },
            { EOSConstants.TextSize, 17f },
            { EOSConstants.SecondaryTextSize, 17f },
            { EOSConstants.Font, "Fonts/OpenSansRegular.ttf" },
            { EOSConstants.SecondaryFont, "Fonts/OpenSansRegular.ttf" },
            { EOSConstants.CornerRadius, 5f },
            { EOSConstants.LetterSpacing, 0.1f },
            { EOSConstants.SecondaryLetterSpacing, 0.1f },
            { EOSConstants.HintTextColor, Color.LightGray },
            { EOSConstants.HintTextColorDisabled, Color.Gray },
            { EOSConstants.LeftImageFocused, Resource.Drawable.AccountCircle },
            { EOSConstants.LeftImageUnfocused, Resource.Drawable.AccountKey },
            { EOSConstants.LeftImageDisabled, Resource.Drawable.AccountOff },
            { EOSConstants.UnderlineColorFocused, Color.White },
            { EOSConstants.UnderlineColorUnfocused, Color.DarkGray },
            { EOSConstants.UnderlineColorDisabled, Color.LightGray },
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