using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Sandbox.Enums;
using EOS.UI.Shared.Helpers;
using EOS.UI.Shared.Themes.Enums;
using EOS.UI.Shared.Themes.Helpers;
using UIKit;

namespace EOS.UI.iOS.Sandbox.Helpers
{
    public static class Constants
    {
        public static UIColor BackgroundColor = UIColor.FromRGB(224, 224, 224);

        public static List<int> CornerRadiusValues;

        public static List<int> FontSizeValues;

        public static List<int> LetterSpacingValues;

        public static List<int> FabProgressSizes;


        public static Dictionary<string, EOSThemeEnumeration> ThemeCollection = new Dictionary<string, EOSThemeEnumeration>()
        {
            { "Light", EOSThemeEnumeration.Light },
            { "Dark", EOSThemeEnumeration.Dark },
        };

        public static Dictionary<string, SimpleButtonTypeEnum> ButtonTypes = new Dictionary<string, SimpleButtonTypeEnum>()
        {
            { "Simple button", SimpleButtonTypeEnum.Simple },
            { "Full-bleed button", SimpleButtonTypeEnum.FullBleed },
        };

        public static Dictionary<string, string> Icons = new Dictionary<string, string>()
        {
            { "Calendar", "icCalendar" },
            { "Account circle", "icAccountCircle" },
            { "Account key", "icAccountKey" },
            { "Account off", "icAccountOff" },
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

        public static Dictionary<String, Predicate<string>> Validations = new Dictionary<string, Predicate<string>>()
        {
            {"without validation", null },
            {"e-mail validation", (s) => s.Contains("@") && !String.IsNullOrEmpty(s)},
            {"empty validation", (s) => !String.IsNullOrEmpty(s) },
        };

        public static List<int> WidthValues;
        public static List<int> PaddingValues;
        public static List<int> ShadowOffsetValues;
        public static List<int> ShadowRadiusValues;
        public static List<double> ShadowOpacityValues;

        static Constants()
        {
            FontSizeValues = Enumerable.Range(10, 31).Where(i => i % 2 == 0).ToList();
            CornerRadiusValues = Enumerable.Range(1, 10).Where(i => (i - 10) % 4 == 0).ToList();
            LetterSpacingValues = Enumerable.Range(1, 10).ToList();
            FabProgressSizes = Enumerable.Range(40, 50).Where(i => i % 10 == 0).ToList();
            WidthValues = Enumerable.Range(1, 10).ToList();
            PaddingValues = Enumerable.Range(1, 10).ToList();
            ShadowOffsetValues = Enumerable.Range(-14, 29).ToList();
            ShadowRadiusValues = Enumerable.Range(1, 15).ToList();
            ShadowOpacityValues = new List<double>() { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0 };
        }
    }
}
