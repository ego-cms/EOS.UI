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
            public static string Theme = "Theme";
            public static string Background = "Background";
            public static string Font = "Font";
            public static string TextColor = "Text color";
            public static string LetterSpacing = "Letter spacing";
            public static string TextSize = "Text size";
            public static string ConerRadius = "Corner radius";
            public static string EnabledTextColor = "Enabled text color";
            public static string DisabledTextColor = "Disabled text color";
            public static string PressedTextColor = "Pressed text color";
            public static string EnabledBackground = "Enabled background";
            public static string DisabledBackground = "Disabled background";
            public static string PressedBackground = "Pressed background";
            public static string HintTextColor = "Hint text color";
            public static string HintTextColorDisabled = "Hint text color disabled";
            public static string IconFocused = "Icon focused";
            public static string IconUnfocused = "Icon unfocused";
            public static string IconDisabled = "Icon disabled";
            public static string UnderlineColorFocused = "Underline color focused";
            public static string UnderlineColorUnfocused = "Underline color unfocused";
            public static string UnderlineColorDisabled = "Underline color disabled";
            public static string DisabledColor = "Disabled color";
            public static string PressedColor = "Pressed color";
            public static string Size = "Size";
            public static string Color = "Color";
            public static string AlternativeColor = "Alternative color";
            public static string FillColor = "Fill color";
            public static string Shadow = "Shadow";
            public static string SectionName = "Section name";
            public static string ButtonText = "Button text";
            public static string SectionNameLetterSpacing = "Section name letter spacing";
            public static string ButtonTextLetterSpacing = "Button text letter spacing";
            public static string SectionNameFont = "Section name font";
            public static string ButtonTextFont = "Button text font";
            public static string SectionTextSize = "Section text size";
            public static string ButtonTextSize = "Button text size";
            public static string SectionTextColor = "Section text color";
            public static string ButtonTextColor = "Button text color";
            public static string BackgroundColor = "Backgroud color";
            public static string BorderColor = "Border color";
            public static string BorderWidth = "Border width";
            public static string PaddingTop = "Padding top";
            public static string PaddingBottom = "Padding bottom";
            public static string PaddingLeft = "Padding left";
            public static string PaddingRight = "Padding right";
            public static string RippleColor = "Ripple color";
            public static string TitleFont = "Title font";
            public static string TitleColor = "Title color";
            public static string DayTextFont = "Day text font";
            public static string TitleTextSize = "Title text size";
            public static string DayTextSize = "Day text size";
            public static string DayTextColor = "Day text color";
            public static string CurrentDayTextColor = "Current day text color";
            public static string CurrentDayBackgroundColor = "Current day background color";
            public static string ColorDividers = "Divider color";
            public static string CurrentColorDividers = "Current day divider color";
            public static string DayEvenBackgroundColor = "Even day background color";
            public static string WeekStartDay = "Week start day";
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

        public static Dictionary<string, ShadowConfig> ShadowConfigs = new Dictionary<string, ShadowConfig>()
            {
                {"Shadow 1", new ShadowConfig(){
                        Color = UIColor.Black.CGColor,
                        Offset = new CGPoint(0,0),
                        Radius = 4,
                        Opacity = 0.7f
                    }},
                {"Shadow 2", new ShadowConfig(){
                        Color = UIColor.Black.CGColor,
                        Offset = new CGPoint(0,0),
                        Radius = 8,
                        Opacity = 0.9f
                    }},
                { "Shadow 3", new ShadowConfig{ Color = UIColor.Blue.CGColor, Offset = new CGPoint(-5,5), Radius = 5, Opacity = 1.0f} },
                { "Shadow 4", new ShadowConfig{ Color = UIColor.Yellow.CGColor, Offset = new CGPoint(5,-5), Radius = 5, Opacity = 1.0f} },
                { "Shadow 5", new ShadowConfig{ Color = UIColor.Purple.CGColor, Offset = new CGPoint(-5,-5), Radius = 5, Opacity = 1.0f} },
                { "Shadow 6", new ShadowConfig{ Color = UIColor.Purple.CGColor, Offset = new CGPoint(-25,-25), Radius = 15, Opacity = 1.0f} },
                { "Shadow 7", null },
                { "Shadow 8", new ShadowConfig{ Color = UIColor.Black.CGColor, Offset = new CGPoint(0,0), Radius = 5, Opacity = 1.0f}  },
                { "Shadow 9", new ShadowConfig{ Color = UIColor.Black.CGColor, Offset = new CGPoint(0,0), Radius = 25, Opacity = 1.0f}  },
                { "Shadow 10", new ShadowConfig{ Color = UIColor.Black.CGColor, Offset = new CGPoint(0,0), Radius = 15, Opacity = 1.0f}  },
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
