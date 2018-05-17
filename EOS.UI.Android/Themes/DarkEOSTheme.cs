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
        };
    }
}