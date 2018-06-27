using System;
using System.Collections.Generic;
using EOS.UI.Shared.Helpers;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.DataModels;

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
        private const string brandPrimaryColor = "#3C6DF0";
        private const string brandPrimaryColorV1 = "#3151B7";
        private const string semanticSuccessColor = "#00AA5E";
        private const string semanticErrorColor = "#FF5C49";
        private const string semanticWarningColor = "#FED500";
        private const string neutralColor1 = "#343334";
        private const string neutralColor2 = "#777677";
        private const string neutralColor3 = "#C0BFC0";
        private const string neutralColor4 = "#EAEAEA";
        private const string neutralColor5 = "#F6F6F6";
        private const string neutralColor6 = "#FFFFFF";
        private const string rippleColor = "#1AFFFFFF";

        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
#if __IOS__
            { EOSConstants.BrandPrimaryColor, ColorExtension.FromHex(brandPrimaryColor) },
            { EOSConstants.BrandPrimaryColorVariant1, ColorExtension.FromHex(brandPrimaryColorV1)  },
            { EOSConstants.SemanticSuccessColor, ColorExtension.FromHex(semanticSuccessColor) },
            { EOSConstants.SemanticErrorColor, ColorExtension.FromHex(semanticErrorColor) },
            { EOSConstants.SemanticWarningColor, ColorExtension.FromHex(semanticWarningColor) },
            { EOSConstants.NeutralColor1, ColorExtension.FromHex(neutralColor1) },
            { EOSConstants.NeutralColor2, ColorExtension.FromHex(neutralColor2) },
            { EOSConstants.NeutralColor3, ColorExtension.FromHex(neutralColor3) },
            { EOSConstants.NeutralColor4, ColorExtension.FromHex(neutralColor4) },
            { EOSConstants.NeutralColor5, ColorExtension.FromHex(neutralColor5) },
            { EOSConstants.NeutralColor6, ColorExtension.FromHex(neutralColor6) },
            { EOSConstants.RippleColor, ColorExtension.FromHex(rippleColor) },
            { EOSConstants.TextSize, 17 },
            { EOSConstants.SecondaryTextSize, 17 },
            { EOSConstants.Font, UIFont.SystemFontOfSize(17)},
            { EOSConstants.SecondaryFont, UIFont.SystemFontOfSize(17) },
            { EOSConstants.SimpleButtonCornerRadius, 25 },
            { EOSConstants.LetterSpacing, 1 },
            { EOSConstants.SecondaryLetterSpacing, 1 },
            { EOSConstants.LeftImageFocused, "icAccountCircle" },
            { EOSConstants.LeftImageUnfocused, "icAccountKey" },
            { EOSConstants.LeftImageDisabled, "icAccountOff" },
            { EOSConstants.CalendarImage, "icCalendar"},
            { EOSConstants.FabProgressPreloaderImage, "icPreloader"},
            { EOSConstants.FabProgressSize, 50},
            { EOSConstants.CircleProgressShown, true},
            { EOSConstants.FabShadow, new ShadowConfig(){
                        Color = UIColor.Black.CGColor,
                        Offset = new CGSize(0, 0),
                        Radius = 2,
                        Opacity = 0.4f
            }},
            { EOSConstants.SimpleButtonShadow, new ShadowConfig(){
                        Color = UIColor.Black.CGColor,
                        Offset = new CGSize(0, 12),
                        Radius = 5,
                        Opacity = 0.2f
            }},
            { EOSConstants.BorderWidth, 2 },
            { EOSConstants.SectionTitle, "Light section" },
            { EOSConstants.SectionActionTitle, "View All" },
            { EOSConstants.LeftPadding, 5 },
            { EOSConstants.TopPadding, 7 },
            { EOSConstants.RightPadding, 7 },
            { EOSConstants.BottomPadding, 5 },
            { EOSConstants.HasSectionBorder, true },
            { EOSConstants.HasSectionAction, true },
            { EOSConstants.WorkTimeTitleSize, 13 },
            { EOSConstants.WorkTimeTitleFont, UIFont.BoldSystemFontOfSize(13) },
            { EOSConstants.WorkTimeDayTextSize, 11 },
            { EOSConstants.WorkTimeDayTextFont, UIFont.BoldSystemFontOfSize(13) },
            //simblebutton enabled
            { EOSConstants.R3C1, new FontStyleItem() {
                    Color = ColorExtension.FromHex(neutralColor6),
                    Font = UIFont.SystemFontOfSize(16f, UIFontWeight.Medium),
                    Size = 16f,
                    LetterSpacing = -0.2f,
                    LineHeight = 19f
            }},
            //simplebutton disabled
            { EOSConstants.R3C4, new FontStyleItem() {
                    Color = ColorExtension.FromHex(neutralColor3),
                    Font = UIFont.SystemFontOfSize(16f, UIFontWeight.Medium),
                    Size = 16f,
                    LetterSpacing = -0.2f,
                    LineHeight = 19f
            }},
            //ghostbutton enabled
            { EOSConstants.R2C1, new FontStyleItem() {
                    Color = ColorExtension.FromHex(brandPrimaryColor),
                    Font = UIFont.SystemFontOfSize(13f, UIFontWeight.Semibold),
                    Size = 13f,
                    LetterSpacing = -0.6f,
                    LineHeight = 15f
            }},
            //ghostbutton disabled
            { EOSConstants.R2C4, new FontStyleItem() {
                    Color = ColorExtension.FromHex(neutralColor3),
                    Font = UIFont.SystemFontOfSize(13f, UIFontWeight.Semibold),
                    Size = 13f,
                    LetterSpacing = -0.6f,
                    LineHeight = 15f
            }},
#endif

#if __ANDROID__
            { EOSConstants.BrandPrimaryColor, Color.ParseColor(brandPrimaryColor) },
            { EOSConstants.BrandPrimaryColorVariant1, Color.ParseColor(brandPrimaryColorV1) },
            { EOSConstants.SemanticSuccessColor,Color.ParseColor(semanticSuccessColor)},
            { EOSConstants.SemanticErrorColor, Color.ParseColor(semanticErrorColor)},
            { EOSConstants.SemanticWarningColor, Color.ParseColor(semanticWarningColor)},
            { EOSConstants.NeutralColor1, Color.ParseColor(neutralColor1)},
            { EOSConstants.NeutralColor2, Color.ParseColor(neutralColor2)},
            { EOSConstants.NeutralColor3, Color.ParseColor(neutralColor3)},
            { EOSConstants.NeutralColor4, Color.ParseColor(neutralColor4)},
            { EOSConstants.NeutralColor5, Color.ParseColor(neutralColor5)},
            { EOSConstants.NeutralColor6, Color.ParseColor(neutralColor6)},
            { EOSConstants.RippleColor, Color.ParseColor(rippleColor) },
            { EOSConstants.TextSize, 17f },
            { EOSConstants.LabelTextSize, 13f },
            { EOSConstants.SecondaryTextSize, 17f },
            { EOSConstants.Font, "Fonts/Roboto.ttf" },
            { EOSConstants.SecondaryFont, "Fonts/Roboto.ttf" },
            { EOSConstants.SimpleButtonCornerRadius, 25f },
            { EOSConstants.LabelCornerRadius, 5f },
            { EOSConstants.LetterSpacing, 0f },
            { EOSConstants.SecondaryLetterSpacing, 0.1f },
            { EOSConstants.LeftImageFocused, Resource.Drawable.AccountCircle },
            { EOSConstants.LeftImageUnfocused, Resource.Drawable.AccountKey },
            { EOSConstants.LeftImageDisabled, Resource.Drawable.AccountOff },
            { EOSConstants.CalendarImage, Resource.Drawable.icCalendar },
            { EOSConstants.FabProgressPreloaderImage, Resource.Drawable.icPreloader },
            { EOSConstants.CircleProgressShown, true},
            { EOSConstants.BorderWidth, 2 },
            { EOSConstants.SectionTitle, "Light section" },
            { EOSConstants.SectionActionTitle, "View All" },
            { EOSConstants.LeftPadding, 5 },
            { EOSConstants.TopPadding, 7 },
            { EOSConstants.RightPadding, 7 },
            { EOSConstants.BottomPadding, 5 },
            { EOSConstants.HasSectionBorder, true },
            { EOSConstants.HasSectionAction, true },
            { EOSConstants.WorkTimeTitleFont, "Fonts/SFUITextSemibold.ttf" },
            { EOSConstants.WorkTimeDayTextFont, "Fonts/SFUITextBold.ttf"},
            { EOSConstants.WorkTimeTitleSize,  13 },
            { EOSConstants.WorkTimeDayTextSize, 11 },
#endif
        };
    }
}
