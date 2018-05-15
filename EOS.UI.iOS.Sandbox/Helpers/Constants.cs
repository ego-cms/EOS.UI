using System;
using System.Collections.Generic;
using EOS.UI.Shared.Themes.Helpers;
using UIKit;
using System.Linq;
using EOS.UI.iOS.Helpers;
using CoreGraphics;

namespace EOS.UI.iOS.Sandbox.Helpers
{
    public static class Constants
    {
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
            { "account-circle"},
            { "account-key"},
            { "account-off"},
            { "airbaloon"},
            { "android"},
            { "apple"}
        };

        public static Dictionary<string, bool> States = new Dictionary<string, bool>()
            {
                { "Enabled", true },
                { "Disabled", false },
            };

        public static Dictionary<string, ShadowConfig> ShadowConfigs = new Dictionary<string, ShadowConfig>()
            {
                {"Shadow 1", new ShadowConfig(){
                        Color = UIColor.LightGray.CGColor,
                        Offset = new CGSize(0,0),
                        Radius = 5,
                        Opacity = 0.7f
                    }},
                {"Shadow 2", new ShadowConfig(){
                        Color = UIColor.Black.CGColor,
                        Offset = new CGSize(0,0),
                        Radius = 3,
                        Opacity = 0.2f
                    }}
            };

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
        }
    }
}
