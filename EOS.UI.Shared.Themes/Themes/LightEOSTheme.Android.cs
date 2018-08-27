#if __ANDROID__
using System;
using Android.Graphics;
using Android.App;
using System.Collections.Generic;
using EOS.UI.Droid;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Helpers;

namespace EOS.UI.Shared.Themes.Themes
{
    public partial class LightEOSTheme
    {
        private Typeface _robotoBold;
        private Typeface RobotoBold
        {
            get
            {
                if (_robotoBold == null)
                    _robotoBold = Typeface.CreateFromAsset(Application.Context.Assets, "Fonts/Roboto-Bold.ttf");
                return _robotoBold;
            }
        }

        private Typeface _robotoMedium;
        private Typeface RobotoMedium
        {
            get
            {
                if (_robotoMedium == null)
                    _robotoMedium = Typeface.CreateFromAsset(Application.Context.Assets, "Fonts/Roboto-Medium.ttf");
                return _robotoMedium;
            }
        }

        private Typeface _robotoRegular;
        private Typeface RobotoRegular
        {
            get
            {
                if (_robotoRegular == null)
                    _robotoRegular = Typeface.CreateFromAsset(Application.Context.Assets, "Fonts/Roboto-Regular.ttf");
                return _robotoRegular;
            }
        }

        public Dictionary<string, object> ThemeValues => new Dictionary<string, object>()
        {
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
            { EOSConstants.NeutralColor1S, Color.ParseColor(neutralColor1S)},
            { EOSConstants.NeutralColor2S, Color.ParseColor(neutralColor2S)},
            { EOSConstants.NeutralColor3S, Color.ParseColor(neutralColor3S)},
            { EOSConstants.NeutralColor4S, Color.ParseColor(neutralColor4S)},
            { EOSConstants.NeutralColor5S, Color.ParseColor(neutralColor5S)},
            { EOSConstants.NeutralColor6S, Color.ParseColor(neutralColor6S)},
            { EOSConstants.Blackout, Color.Argb(164, Color.ParseColor(neutralColor5).R, Color.ParseColor(neutralColor5).G, Color.ParseColor(neutralColor5).B) },
            { EOSConstants.RippleColor, Color.ParseColor(rippleColor) },
            { EOSConstants.DisabledInputColor, Color.ParseColor(neutralColor3)},
            { EOSConstants.LabelCornerRadius, 4f },
            { EOSConstants.ButtonCornerRadius, 24f },
            { EOSConstants.LeftImage, Resource.Drawable.icCalendar },
            { EOSConstants.CalendarImage, Resource.Drawable.icCalendar },
            //should always be white
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
                    Offset = new Point(0, 12),
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
            { EOSConstants.R2C1S,
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
            { EOSConstants.R2C4S,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor3S),
                    Typeface = RobotoBold,
                    Size = 13f,
                    LetterSpacing = -0.005f,
                    LineHeight = 15f
                }
            },
            { EOSConstants.R2C5S,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor6S),
                    Typeface = RobotoBold,
                    Size = 13f,
                    LetterSpacing = -0.005f,
                    LineHeight = 15f
                }
            },
            { EOSConstants.R3C1S,
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
            { EOSConstants.R3C4S,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor3S),
                    Typeface = RobotoMedium,
                    Size = 16f,
                    LetterSpacing = -0.01f,
                    LineHeight = 19f
                }
            },
            { EOSConstants.R3C5S,
                new FontStyleItem()
                {
                    Color = Color.ParseColor(neutralColor6S),
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
            }
        };
    }
}
#endif
