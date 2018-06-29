using System;
using System.Collections.Generic;
using EOS.UI.Shared.Themes.Helpers;
using UIKit;
using System.Linq;
using EOS.UI.iOS.Helpers;
using CoreGraphics;
using EOS.UI.Shared.Helpers;
using EOS.UI.Shared.Themes.Enums;

namespace EOS.UI.iOS.Sandbox.Helpers
{
    public static class Constants
    {
        public static class Fields
        {
            public const string Theme = "Theme";
            public const string Background = "Background";
            public const string Font = "Font";
            public const string TextColor = "Text color";
            public const string LetterSpacing = "Letter spacing";
            public const string TextSize = "Text size";
            public const string ConerRadius = "Corner radius";
            public const string EnabledTextColor = "Enabled text color";
            public const string DisabledTextColor = "Disabled text color";
            public const string PressedTextColor = "Pressed text color";
            public const string EnabledBackground = "Enabled background";
            public const string DisabledBackground = "Disabled background";
            public const string PressedBackground = "Pressed background";
            public const string HintTextColor = "Hint text color";
            public const string HintTextColorDisabled = "Hint text color disabled";
            public const string IconFocused = "Icon focused";
            public const string IconUnfocused = "Icon unfocused";
            public const string IconDisabled = "Icon disabled";
            public const string UnderlineColorFocused = "Underline color focused";
            public const string UnderlineColorUnfocused = "Underline color unfocused";
            public const string UnderlineColorDisabled = "Underline color disabled";
            public const string DisabledColor = "Disabled color";
            public const string PressedColor = "Pressed color";
            public const string Size = "Size";
            public const string Color = "Color";
            public const string AlternativeColor = "Alternative color";
            public const string FillColor = "Fill color";
            public const string Shadow = "Shadow";
            public const string SectionName = "Section name";
            public const string ButtonText = "Button text";
            public const string SectionNameLetterSpacing = "Section name letter spacing";
            public const string ButtonTextLetterSpacing = "Button text letter spacing";
            public const string SectionNameFont = "Section name font";
            public const string ButtonTextFont = "Button text font";
            public const string SectionTextSize = "Section text size";
            public const string ButtonTextSize = "Button text size";
            public const string SectionTextColor = "Section text color";
            public const string ButtonTextColor = "Button text color";
            public const string BackgroundColor = "Backgroud color";
            public const string BorderColor = "Border color";
            public const string BorderWidth = "Border width";
            public const string PaddingTop = "Padding top";
            public const string PaddingBottom = "Padding bottom";
            public const string PaddingLeft = "Padding left";
            public const string PaddingRight = "Padding right";
            public const string RippleColor = "Ripple color";
            public const string TitleFont = "Title font";
            public const string TitleColor = "Title color";
            public const string DayTextFont = "Day text font";
            public const string TitleTextSize = "Title text size";
            public const string DayTextSize = "Day text size";
            public const string DayTextColor = "Day text color";
            public const string CurrentDayTextColor = "Current day text color";
            public const string CurrentDayBackgroundColor = "Current day background color";
            public const string ColorDividers = "Divider color";
            public const string CurrentColorDividers = "Current day divider color";
            public const string DayEvenBackgroundColor = "Even day background color";
            public const string WeekStartDay = "Week start day";
            public const string Default = "Default";
        };

        public static UIColor BackgroundColor = UIColor.FromRGB(224, 224, 224);

        public static Dictionary<string, UIColor> Colors = new Dictionary<string, UIColor>()
        {
            {"Black", UIColor.Black},
            {"White", UIColor.White},
            {"Gray", UIColor.Gray},
            {"Green", UIColor.Green},
            {"Blue", UIColor.Blue},
            {"Red", UIColor.Red},
            {"Yellow", UIColor.Yellow},
            {"Brown", UIColor.Brown},
        };
        public static List<int> CornerRadiusValues;

        public static List<int> FontSizeValues;

        public static List<int> LetterSpacingValues;

        public static List<int> FabProgressSizes;

        public static List<UIFont> Fonts;

        public static Dictionary<string, EOSThemeEnumeration> Themes = new Dictionary<string, EOSThemeEnumeration>()
        {
            { "Light", EOSThemeEnumeration.Light },
            { "Dark", EOSThemeEnumeration.Dark },
        };

        public static List<string> Icons = new List<string>()
        {
            { "icAccountCircle"},
            { "icAccountKey"},
            { "icAccountOff"},
        };

        public static Dictionary<string, bool> States = new Dictionary<string, bool>()
        {
            { "Enabled", true },
            { "Disabled", false },
        };

        public static List<WeekStartEnum> WeekStartDays = new List<WeekStartEnum>()
        {
            WeekStartEnum.Monday,
            WeekStartEnum.Sunday
        };


        public const string Shadow1 = "ShadowOffsetXPositiveLargerBlur";
        public const string Shadow2 = "ShadowOffsetXNegativeLargerBlur";
        public const string Shadow3 = "ShadowOffsetYPositiveLargerBlur";
        public const string Shadow4 = "ShadowOffsetYNegativeLargerBlur";
        public const string Shadow5 = "ShadowBlurLargerOffsetXPositive";
        public const string Shadow6 = "ShadowBlurLargerOffsetXNegative";
        public const string Shadow7 = "ShadowBlurLargerOffsetYPositive";
        public const string Shadow8 = "ShadowBlurLargerOffsetYNegative";
        public const string Shadow9 = "ShadowOffsetXYPositiveEqualBlur";
        public const string Shadow10 = "ShadowOffsetXPositiveEqualBlur";
        public const string Shadow11 = "ShadowOffsetXNegativeYPositiveEqualBlur";
        public const string Shadow12 = "ShadowOffsetXPositiveYNegativeEqualBlur";
        public const string Shadow13 = "ShadowOffsetXNegativeYNegativeEqualBlur";
        public const string Shadow14 = "ShadowBlurLargerOffsetXPositiveYNegative";
        public const string Shadow15 = "ShadowOffsetXPositiveYNegativeTinyBlur";
        public const string Shadow16 = "ShadowWithAlpha09";
        public const string Shadow17 = "ShadowWithAlpha07";
        public const string Shadow18 = "ShadowWithAlpha05";
        public const string Shadow19 = "ShadowWithAlpha03";
        public const string Shadow20 = "Medium shadow";
        public const string Shadow21 = "Normal shadow";
        public const string Shadow22 = "Big shadow";
        public const string Shadow23 = "No shadow";

        public static Dictionary<string, ShadowConfig> ShadowConfigs = new Dictionary<string, ShadowConfig>()
        {
                { string.Empty, null },
                { Shadow1, new ShadowConfig{ Color = UIColor.Red, Offset = new CGPoint(15,0), Blur = 5, Spread = 200} },
                { Shadow2, new ShadowConfig{ Color = UIColor.Red, Offset = new CGPoint(-15,0), Blur = 5, Spread = 200} },
                { Shadow3, new ShadowConfig{ Color = UIColor.Red, Offset = new CGPoint(0,15), Blur = 5, Spread = 200} },
                { Shadow4, new ShadowConfig{ Color = UIColor.Red, Offset = new CGPoint(0,-15), Blur = 5, Spread = 200} },
                { Shadow5, new ShadowConfig{ Color = UIColor.Red, Offset = new CGPoint(5,0), Blur = 15, Spread = 200} },
                { Shadow6, new ShadowConfig{ Color = UIColor.Red, Offset = new CGPoint(-5,0), Blur = 15, Spread = 200} },
                { Shadow7, new ShadowConfig{ Color = UIColor.Red, Offset = new CGPoint(0,5), Blur = 25, Spread = 200} },
                { Shadow8, new ShadowConfig{ Color = UIColor.Red, Offset = new CGPoint(0,-5), Blur = 25, Spread = 200} },
                { Shadow9, new ShadowConfig{ Color = UIColor.Green, Offset = new CGPoint(5,5), Blur = 5, Spread = 200} },
                { Shadow10, new ShadowConfig{ Color = UIColor.Green, Offset = new CGPoint(5,0), Blur = 5, Spread = 200} },
                { Shadow11, new ShadowConfig{ Color = UIColor.Blue, Offset = new CGPoint(-5,5), Blur = 5, Spread = 200} },
                { Shadow12, new ShadowConfig{ Color = UIColor.Yellow, Offset = new CGPoint(5,-5), Blur = 5, Spread = 200} },
                { Shadow13, new ShadowConfig{ Color = UIColor.Purple, Offset = new CGPoint(-5,-5), Blur = 5, Spread = 200} },
                { Shadow14, new ShadowConfig{ Color = UIColor.Purple, Offset = new CGPoint(25,-25), Blur = 15, Spread = 200} },
                { Shadow15, new ShadowConfig{ Color = UIColor.Purple, Offset = new CGPoint(6,-6), Blur = 1, Spread = 200} },
                { Shadow16, new ShadowConfig{ Color = UIColor.FromRGBA(255,0,0,230), Offset = new CGPoint(15,0), Blur = 5, Spread = 200} },
                { Shadow17, new ShadowConfig{ Color = UIColor.FromRGBA(255,0,0,178), Offset = new CGPoint(15,0), Blur = 5, Spread = 200} },
                { Shadow18, new ShadowConfig{ Color = UIColor.FromRGBA(255,0,0, 127), Offset = new CGPoint(15,0), Blur = 5, Spread = 200} },
                { Shadow19, new ShadowConfig{ Color = UIColor.FromRGBA(255,0,0,77), Offset = new CGPoint(15,0), Blur = 5, Spread = 200} },
                { Shadow20, new ShadowConfig{ Color = UIColor.Black, Offset = new CGPoint(0,0), Blur = 15, Spread = 200}  },
                { Shadow21, new ShadowConfig{ Color = UIColor.Black, Offset = new CGPoint(0,0), Blur = 5, Spread = 200}  },
                { Shadow22, new ShadowConfig{ Color = UIColor.Black, Offset = new CGPoint(0,0), Blur = 25, Spread = 200}  },
                { Shadow23, null },
            };

        public static List<string> Titles = new List<string>()
        {
            "First",
            "Second",
            "Third",
        };

        public static List<int> WidthValues;
        public static List<int> PaddingValues;

        static Constants()
        {
            Fonts = new List<UIFont>();

            foreach (var familyName in UIFont.FamilyNames)
            {
                foreach (var fontName in UIFont.FontNamesForFamilyName(familyName))
                {
                    var font = UIFont.FromName(fontName, UIFont.SystemFontSize);
                    Fonts.Add(font);
                }
            }

            Fonts = Fonts.OrderBy(f => f.Name).ToList();
            FontSizeValues = Enumerable.Range(10, 31).Where(i => i % 2 == 0).ToList();
            CornerRadiusValues = Enumerable.Range(1, 10).Where(i => (i - 10) % 4 == 0).ToList();
            LetterSpacingValues = Enumerable.Range(1, 10).ToList();
            FabProgressSizes = Enumerable.Range(40, 50).Where(i => i % 10 == 0).ToList();
            WidthValues = Enumerable.Range(1, 10).ToList();
            PaddingValues = Enumerable.Range(1, 10).ToList();
        }
    }
}
