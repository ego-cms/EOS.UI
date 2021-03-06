﻿using System.Collections.Generic;
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
            { EOSConstants.PrimaryColorDisabled, Color.LightCoral},
            { EOSConstants.PrimaryColorPressed, Color.LightBlue},
            { EOSConstants.SecondaryColor, Color.Black },
            { EOSConstants.SecondaryColorDisabled, Color.LightGray},
            { EOSConstants.SecondaryColorPressed, Color.LightGray},
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
            { EOSConstants.CircleProgressShown, true},
            { EOSConstants.FabShadow, null}
        };
    }
}