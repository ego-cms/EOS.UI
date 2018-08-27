#if __IOS__
using System;
using System.Collections.Generic;
using CoreGraphics;
using EOS.UI.Shared.Helpers;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.Helpers;
using UIKit;

namespace EOS.UI.Shared.Themes.Themes
{
    public partial class LightEOSTheme
    {
        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
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
            { EOSConstants.NeutralColor1S, ColorExtension.FromHex(neutralColor1S) },
            { EOSConstants.NeutralColor2S, ColorExtension.FromHex(neutralColor2S) },
            { EOSConstants.NeutralColor3S, ColorExtension.FromHex(neutralColor3S) },
            { EOSConstants.NeutralColor4S, ColorExtension.FromHex(neutralColor4S) },
            { EOSConstants.NeutralColor5S, ColorExtension.FromHex(neutralColor5S) },
            { EOSConstants.NeutralColor6S, ColorExtension.FromHex(neutralColor6S) },
            { EOSConstants.RippleColor, ColorExtension.FromHex(rippleColor) },
            { EOSConstants.DisabledInputColor, ColorExtension.FromHex(neutralColor3)},
            { EOSConstants.ButtonCornerRadius, 24 },
            { EOSConstants.LabelCornerRadius, 4 },
            { EOSConstants.LeftImage, "icCalendar" },
            { EOSConstants.CalendarImage, "icCalendar"},
            { EOSConstants.FabProgressPreloaderImage, "icPreloader"},
            //should always be white
            { EOSConstants.WarningInputImage, "icWarning"},
            { EOSConstants.ClearInputImage, "icClear"},
            { EOSConstants.CircleProgressShown, true},
            { EOSConstants.FabShadow, new ShadowConfig(){
                Color = ColorExtension.FromHex(fabShadowColor),
                Offset = new CGPoint(0, 6),
                Blur = 12,
                Spread = 1
            }},
            { EOSConstants.SimpleButtonShadow, new ShadowConfig(){
                Color = ColorExtension.FromHex(shadowColor),
                Offset = new CGPoint(0, 12),
                Blur = 12,
                Spread = 200
            }},
            { EOSConstants.BorderWidth, 2 },
            { EOSConstants.SectionTitle, "Light section" },
            { EOSConstants.SectionActionTitle, "View All" },
            { EOSConstants.LeftPadding, 16 },
            { EOSConstants.TopPadding, 10 },
            { EOSConstants.RightPadding, 16 },
            { EOSConstants.BottomPadding, 10 },
            { EOSConstants.HasSectionBorder, true },
            { EOSConstants.HasSectionAction, true },
            //simblebutton enabled
            { EOSConstants.R3C5S, new FontStyleItem() {
                    Color = ColorExtension.FromHex(neutralColor6S),
                    Font = UIFont.SystemFontOfSize(16f, UIFontWeight.Medium),
                    Size = 16f,
                    LetterSpacing = -0.2f,
                    LineHeight = 19f
            }},
            //simplebutton, ghostbutton disabled
            { EOSConstants.R3C4S, new FontStyleItem() {
                    Color = ColorExtension.FromHex(neutralColor3S),
                    Font = UIFont.SystemFontOfSize(16f, UIFontWeight.Medium),
                    Size = 16f,
                    LetterSpacing = -0.2f,
                    LineHeight = 19f
            }},
            //ghostbutton enabled, simple label, section button
            { EOSConstants.R2C1S, new FontStyleItem() {
                    Color = ColorExtension.FromHex(brandPrimaryColor),
                    Font = UIFont.SystemFontOfSize(13f, UIFontWeight.Semibold),
                    Size = 13f,
                    LetterSpacing = -0.6f,
                    LineHeight = 15f
            }},
            //ghostbutton disabled
            { EOSConstants.R2C4S, new FontStyleItem() {
                    Color = ColorExtension.FromHex(neutralColor3S),
                    Font = UIFont.SystemFontOfSize(13f, UIFontWeight.Semibold),
                    Size = 13f,
                    LetterSpacing = -0.6f,
                    LineHeight = 15f
            }},
            //badge label
            { EOSConstants.R2C5S, new FontStyleItem() {
                    Color = ColorExtension.FromHex(neutralColor6S),
                    Font = UIFont.SystemFontOfSize(13f, UIFontWeight.Semibold),
                    Size = 13f,
                    LetterSpacing = -0.6f,
                    LineHeight = 15f
            }},
            //section fontstyle
            { EOSConstants.R2C3, new FontStyleItem() {
                    Color = ColorExtension.FromHex(neutralColor2),
                    Font = UIFont.SystemFontOfSize(13f, UIFontWeight.Semibold),
                    Size = 13f,
                    LetterSpacing = -0.6f,
                    LineHeight = 15f
            }},
            //circle progress
            { EOSConstants.R1C1, new FontStyleItem() {
                    Color = ColorExtension.FromHex(brandPrimaryColor),
                    Font = UIFont.SystemFontOfSize(11f, UIFontWeight.Bold),
                    Size = 11f,
                    LetterSpacing = 0.06f,
                    LineHeight = 13f
            }},
            //input normal
            { EOSConstants.R4C2, new FontStyleItem() {
                    Color = ColorExtension.FromHex(neutralColor1),
                    Font = UIFont.SystemFontOfSize(17f, UIFontWeight.Regular),
                    Size = 17f,
                    LetterSpacing = -0.24f,
                    LineHeight = 20f
            }},
            //input placeholder
            { EOSConstants.R4C3, new FontStyleItem() {
                    Color = ColorExtension.FromHex(neutralColor2),
                    Font = UIFont.SystemFontOfSize(17f, UIFontWeight.Regular),
                    Size = 17f,
                    LetterSpacing = -0.24f,
                    LineHeight = 20f
            }},
            //input placeholder disabled, input disabled
            { EOSConstants.R4C4, new FontStyleItem() {
                    Color = ColorExtension.FromHex(neutralColor3),
                    Font = UIFont.SystemFontOfSize(17f, UIFontWeight.Regular),
                    Size = 17f,
                    LetterSpacing = -0.24f,
                    LineHeight = 20f
            }},
            //worktimecalendar title day fontstyle
            { EOSConstants.R2C2, new FontStyleItem() {
                    Color = ColorExtension.FromHex(neutralColor1),
                    Font = UIFont.SystemFontOfSize(13f, UIFontWeight.Bold),
                    Size = 13f,
                    LetterSpacing = -0.06f,
                    LineHeight = 15f
            }},
            //worktimecalendar day fontstyle
            { EOSConstants.R1C3, new FontStyleItem() {
                    Color = ColorExtension.FromHex(neutralColor2),
                    Font = UIFont.SystemFontOfSize(11f, UIFontWeight.Bold),
                    Size = 11f,
                    LetterSpacing = 0.06f,
                    LineHeight = 13f
            }},
            //worktimecalendar currentday fontstyle
            { EOSConstants.R1C6, new FontStyleItem() {
                    Color = ColorExtension.FromHex(neutralColor6),
                    Font = UIFont.SystemFontOfSize(11f, UIFontWeight.Bold),
                    Size = 11f,
                    LetterSpacing = 0.06f,
                    LineHeight = 13f
            }},
        };
    }
}
#endif