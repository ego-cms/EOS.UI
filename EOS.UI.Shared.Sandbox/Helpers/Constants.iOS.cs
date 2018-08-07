#if __IOS__
using System;
using System.Collections.Generic;
using System.Linq;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.Helpers;
using UIKit;

namespace EOS.UI.Shared.Sandbox.Helpers
{

    public static partial class Constants
    {
        public static class Colors
        {
            public static Dictionary<string, UIColor> ColorsCollection = new Dictionary<string, UIColor>()
            {
                {ColorNameBlue, ColorExtension.FromHex(ColorBlue)},
                {ColorNameUltramarine, ColorExtension.FromHex(ColorUltramarine)},
                {ColorNameCerulean, ColorExtension.FromHex(ColorCerulean)},
                {ColorNameTeal, ColorExtension.FromHex(ColorTeal)},
                {ColorNameGreen, ColorExtension.FromHex(ColorGreen)},
                {ColorNameLime, ColorExtension.FromHex(ColorLime)},
                {ColorNameYellow, ColorExtension.FromHex(ColorYellow)},
                {ColorNameGold, ColorExtension.FromHex(ColorGold)},
                {ColorNameOrange, ColorExtension.FromHex(ColorOrange)},
                {ColorNamePeach, ColorExtension.FromHex(ColorPeach)},
                {ColorNameRed, ColorExtension.FromHex(ColorRed)},
                {ColorNameMagenta, ColorExtension.FromHex(ColorMagenta)},
                {ColorNamePurple, ColorExtension.FromHex(ColorPurple)},
                {ColorNameViolet, ColorExtension.FromHex(ColorViolet)},
                {ColorNameIndigo, ColorExtension.FromHex(ColorIndigo)},
            };
        }

        public static class Fonts
        {

            public static List<UIFont> FontsCollection;

            static Fonts()
            {
                FontsCollection = new List<UIFont>();

                foreach (var familyName in UIFont.FamilyNames)
                {
                    foreach (var fontName in UIFont.FontNamesForFamilyName(familyName))
                    {
                        var font = UIFont.FromName(fontName, UIFont.SystemFontSize);
                        FontsCollection.Add(font);
                    }
                }

                FontsCollection = FontsCollection.OrderBy(f => f.Name).ToList();
            }
        }

        public static class Validation
        {
            public static Dictionary<String, Predicate<string>> ValidationCollection = new Dictionary<string, Predicate<string>>()
            {
                {"without validation", null },
                {"e-mail validation", (s) => s.Contains("@") && !String.IsNullOrEmpty(s)},
                {"empty validation", (s) => !String.IsNullOrEmpty(s) },
            };
        }

        public static class Icons
        {
            public static Dictionary<string, string> IconsCollection = new Dictionary<string, string>()
            {
                { "Calendar", "icCalendar" },
                { "Account circle", "icAccountCircle" },
                { "Account key", "icAccountKey" },
                { "Account off", "icAccountOff" },
            };
        }
    }
}
#endif
