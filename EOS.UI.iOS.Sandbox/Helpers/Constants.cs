using System;
using System.Collections.Generic;
using EOS.UI.Shared.Themes.Helpers;
using UIKit;

namespace EOS.UI.iOS.Sandbox.Helpers
{
    public static class Constants
    {
        public static Dictionary<string, UIColor> Colors = new Dictionary<string, UIColor>()
        {
            {"White", UIColor.White},
            {"Red", UIColor.Red},
            {"Green", UIColor.Green},
            {"Blue", UIColor.Blue},
            {"Gray", UIColor.Gray},
            {"Yellow", UIColor.Yellow},
            {"Orange", UIColor.Orange},
            {"Black", UIColor.Black}
        };
        public static List<int> CornerRadiusValues = new List<int>() {0, 1, 2, 3, 4, 5, 7, 8, 9, 10 };

        public static List<int> FontSizeValues = new List<int>() { 17, 19, 24, 32, 40 };

        public static List<int> LetterSpacingValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };

        public static Dictionary<string, EOSThemeEnumeration> Themes = new Dictionary<string, EOSThemeEnumeration>()
            {
                { "Light", EOSThemeEnumeration.Light },
                { "Dark", EOSThemeEnumeration.Dark },
            };

        public static List<UIFont> Fonts;

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
        }
    }
}
