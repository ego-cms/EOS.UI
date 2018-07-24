﻿using System.Collections.Generic;
using EOS.UI.Shared.Helpers;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.DataModels;

#if __IOS__
using CoreGraphics;
using EOS.UI.iOS.Helpers;
using UIKit;
#endif

#if __ANDROID__
using Android.Graphics;
using EOS.UI.Droid;
using Android.App;
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
        private const string shadowColor = "#A3EAEAEA";
        private const string fabShadowColor = "#3D000000";

#if __ANDROID__

        private Typeface _robotoBold;
        private Typeface RobotoBold 
        {
            get
            {
                if(_robotoBold == null)
                    _robotoBold = Typeface.CreateFromAsset(Application.Context.Assets, "Fonts/RobotoBold.ttf");
                return _robotoBold;
            }
        }

        private Typeface _robotoMedium;
        private Typeface RobotoMedium
        {
            get
            {
                if(_robotoMedium == null)
                    _robotoMedium = Typeface.CreateFromAsset(Application.Context.Assets, "Fonts/RobotoMedium.ttf");
                return _robotoMedium;
            }
        }

        private Typeface _robotoRegular;
        private Typeface RobotoRegular
        {
            get
            {
                if(_robotoRegular == null)
                    _robotoRegular = Typeface.CreateFromAsset(Application.Context.Assets, "Fonts/RobotoRegular.ttf");
                return _robotoRegular;
            }
        }

#endif

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
            { EOSConstants.ButtonCornerRadius, 25 },
            { EOSConstants.LabelCornerRadius, 5 },
            { EOSConstants.LetterSpacing, 1 },
            { EOSConstants.SecondaryLetterSpacing, 1 },
            { EOSConstants.LeftImage, "icCalendar" },
            { EOSConstants.CalendarImage, "icCalendar"},
            { EOSConstants.FabProgressPreloaderImage, "icPreloader"},
            //should always be white
            { EOSConstants.FabIconColor, ColorExtension.FromHex(neutralColor6)},
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
                Color = ColorExtension.FromHex("#000", 0.2f),
                Offset = new CGPoint(0, 12),
                Blur = 5,
                Spread = 2
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
            { EOSConstants.WorkTimeTitleSize, 13 },
            { EOSConstants.WorkTimeTitleFont, UIFont.BoldSystemFontOfSize(13) },
            { EOSConstants.WorkTimeDayTextSize, 11 },
            { EOSConstants.WorkTimeDayTextFont, UIFont.BoldSystemFontOfSize(13) },
            //simblebutton enabled
            { EOSConstants.R3C1, new FontStyleItem() {
                    Color = ColorExtension.FromHex(neutralColor6), //must be white in all themes
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
            //ghostbutton enabled, simple label, section button
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
            //badge label
            { EOSConstants.R2C5, new FontStyleItem() {
                    Color = ColorExtension.FromHex(neutralColor6), //must be white in all themes
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
            { EOSConstants.CornerRadius, 5f },
            { EOSConstants.LabelCornerRadius, 4f },
            { EOSConstants.ButtonCornerRadius, 100f },
            { EOSConstants.LeftImage, Resource.Drawable.icCalendar },
            { EOSConstants.CalendarImage, Resource.Drawable.icCalendar },
            //should always be white
            { EOSConstants.FabIconColor, Color.ParseColor(neutralColor6)},
            { EOSConstants.FabProgressPreloaderImage, Resource.Drawable.icPreloader },
            { EOSConstants.CircleProgressShown, true},
            { EOSConstants.BorderWidth, 2 },
            { EOSConstants.SectionTitle, "Light section" },
            { EOSConstants.SectionActionTitle, "View All" },
            { EOSConstants.LeftPadding, 16 },
            { EOSConstants.TopPadding, 10 },
            { EOSConstants.RightPadding, 16 },
            { EOSConstants.BottomPadding, 10 },
            { EOSConstants.HasSectionBorder, true },
            { EOSConstants.HasSectionAction, true },
            { EOSConstants.FabShadow,  new ShadowConfig(){
                Color = Color.ParseColor(fabShadowColor),
                Offset = new Point(0, 6),
                Blur = 12,
                Spread = 200
            }},
            { EOSConstants.SimpleButtonShadow,
                new ShadowConfig()
                {
                    Color = Color.ParseColor(shadowColor),
                    Offset = new Point(0, 6),
                    Blur = 12,
                    Spread = 200
                }
            },
            { EOSConstants.R1C1,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(brandPrimaryColor),
                    Typeface = RobotoBold,
                    Size = 11f,
                    LetterSpacing = 0.005f,
                    LineHeight = 13f
                }
            },
            { EOSConstants.R1C2,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor1),
                    Typeface = RobotoBold,
                    Size = 11f,
                    LetterSpacing = 0.005f,
                    LineHeight = 13f
                }
            },
            { EOSConstants.R1C3,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor2),
                    Typeface = RobotoBold,
                    Size = 11f,
                    LetterSpacing = 0.005f,
                    LineHeight = 13f
                }
            },
            { EOSConstants.R1C4,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor3),
                    Typeface = RobotoBold,
                    Size = 11f,
                    LetterSpacing = 0.005f,
                    LineHeight = 13f
                }
            },
            { EOSConstants.R1C5,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor6),
                    Typeface = RobotoBold,
                    Size = 11f,
                    LetterSpacing = 0.005f,
                    LineHeight = 13f
                }
            },
            { EOSConstants.R2C1,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(brandPrimaryColor),
                    Typeface = RobotoBold,
                    Size = 13f,
                    LetterSpacing = -0.005f,
                    LineHeight = 15f
                }
            },
            { EOSConstants.R2C2,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor1),
                    Typeface = RobotoBold,
                    Size = 13f,
                    LetterSpacing = -0.005f,
                    LineHeight = 15f
                }
            },
            { EOSConstants.R2C3,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor2),
                    Typeface = RobotoBold,
                    Size = 13f,
                    LetterSpacing = -0.005f,
                    LineHeight = 15f
                }
            },
            { EOSConstants.R2C4,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor3),
                    Typeface = RobotoBold,
                    Size = 13f,
                    LetterSpacing = -0.005f,
                    LineHeight = 15f
                }
            },
            { EOSConstants.R2C5,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor6),
                    Typeface = RobotoBold,
                    Size = 13f,
                    LetterSpacing = -0.005f,
                    LineHeight = 15f
                }
            },
            { EOSConstants.R3C1,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(brandPrimaryColor),
                    Typeface = RobotoMedium,
                    Size = 16f,
                    LetterSpacing = -0.01f,
                    LineHeight = 19f
                }
            },
            { EOSConstants.R3C2,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor1),
                    Typeface = RobotoMedium,
                    Size = 16f,
                    LetterSpacing = -0.01f,
                    LineHeight = 19f
                }
            },
            { EOSConstants.R3C3,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor2),
                    Typeface = RobotoMedium,
                    Size = 16f,
                    LetterSpacing = -0.01f,
                    LineHeight = 19f
                }
            },
            { EOSConstants.R3C4,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor3),
                    Typeface = RobotoMedium,
                    Size = 16f,
                    LetterSpacing = -0.01f,
                    LineHeight = 19f
                }
            },
            { EOSConstants.R3C5,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor6),
                    Typeface = RobotoMedium,
                    Size = 16f,
                    LetterSpacing = -0.01f,
                    LineHeight = 19f
                }
            },
            { EOSConstants.R4C1,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(brandPrimaryColor),
                    Typeface = RobotoRegular,
                    Size = 17f,
                    LetterSpacing = -0.02f,
                    LineHeight = 20f
                }
            },
            { EOSConstants.R4C2,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor1),
                    Typeface = RobotoRegular,
                    Size = 17f,
                    LetterSpacing = -0.02f,
                    LineHeight = 20f
                }
            },
            { EOSConstants.R4C3,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor2),
                    Typeface = RobotoRegular,
                    Size = 17f,
                    LetterSpacing = -0.02f,
                    LineHeight = 20f
                }
            },
            { EOSConstants.R4C4,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor3),
                    Typeface = RobotoRegular,
                    Size = 17f,
                    LetterSpacing = -0.02f,
                    LineHeight = 20f
                }
            },
            { EOSConstants.R4C5,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor6),
                    Typeface = RobotoRegular,
                    Size = 17f,
                    LetterSpacing = -0.02f,
                    LineHeight = 20f
                }
            },
#endif
        };
    }
}
