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
            public static string AvenirBlack = "Avenir-Black";
            public static string AvenirBook= "Avenir-Book";
            public static string AvenirHeavy = "Avenir-Heavy";
            public static string AvenirMedium = "Avenir-Medium";
            public static string AvenirRoman = "Avenir-Roman";
            public static string AvenirNextDemiBold = "AvenirNext-DemiBold";
            public static string AvenirNextBold = "AvenirNext-Bold";
            public static string AvenirNextMedium = "AvenirNext-Medium";
            public static string AvenirNextRegular = "AvenirNext-Regular";
            public static string FuturaBold = "Futura-Bold";
            public static string FuturaMedium = "Futura-Medium";
            public static string GeezaProRegular = "GeezaPro";
            public static string GeezaProBold = "GeezaPro-Bold";
            public static string HelveticaBold = "Helvetica-Bold";
            public static string HelveticaRoman = "Helvetica";
            public static string HelveticaNeueBold = "HelveticaNeue-Bold";
            public static string HelveticaNeueLight = "HelveticaNeue-Light";
            public static string HelveticaNeueMedium = "HelveticaNeue-Medium";
            public static string HelveticaNeueRoman = "HelveticaNeue";
            public static string HiraginoSansW3 = "HiraginoSans-W3";

            static Fonts()
            {
                FontsCollection = new List<UIFont>();
                var s =UIFont.SystemFontOfSize(10);
                var s1 = UIFont.BoldSystemFontOfSize(10);
                var s2 = UIFont.ItalicSystemFontOfSize(10);
                var ff = UIFont.FamilyNames.OrderBy(a => a).ToList();
                var f0 = UIFont.FontNamesForFamilyName("Avenir");
                var f1 = UIFont.FontNamesForFamilyName("Avenir Next");
                var f2 = UIFont.FontNamesForFamilyName("Futura");
                var f3 = UIFont.FontNamesForFamilyName("Helvetica");
                var f4 = UIFont.FontNamesForFamilyName("Helvetica Neue");
                var f5 = UIFont.FontNamesForFamilyName("Geeza Pro");
                var f6 = UIFont.FontNamesForFamilyName("Hiragino Sans");
                FontsCollection.Add(UIFont.FromName(AvenirBlack, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(AvenirBook, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(AvenirHeavy, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(AvenirMedium, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(AvenirRoman, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(AvenirNextDemiBold, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(AvenirNextBold, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(AvenirNextMedium, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(AvenirNextRegular, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(FuturaBold, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(FuturaMedium, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(GeezaProBold, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(GeezaProRegular, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(HelveticaBold, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(HelveticaRoman, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(HelveticaNeueBold, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(HelveticaNeueLight, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(HelveticaNeueRoman, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(HelveticaNeueMedium, UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.FromName(HiraginoSansW3, UIFont.SystemFontSize));
                //FontsCollection.Add(UIFont.FromName("SFProDisplay-Bold", UIFont.SystemFontSize));
                //FontsCollection.Add(UIFont.FromName("SFProText-Bold", UIFont.SystemFontSize));
                //FontsCollection.Add(UIFont.FromName("SFProText-Medium", UIFont.SystemFontSize));
                //FontsCollection.Add(UIFont.FromName("SFProText-Regular", UIFont.SystemFontSize));
                //FontsCollection.Add(UIFont.FromName("SFProText-Semibold", UIFont.SystemFontSize));
                FontsCollection.Add(UIFont.SystemFontOfSize(UIFont.SystemFontSize, UIFontWeight.Bold));
                FontsCollection.Add(UIFont.SystemFontOfSize(UIFont.SystemFontSize, UIFontWeight.Medium));
                FontsCollection.Add(UIFont.SystemFontOfSize(UIFont.SystemFontSize, UIFontWeight.Regular));
                FontsCollection.Add(UIFont.SystemFontOfSize(UIFont.SystemFontSize, UIFontWeight.Semibold));

                //FontsCollection.Add(UIFont.BoldSystemFontOfSize(UIFont.SystemFontSize));
                //FontsCollection.Add(UIFont.SystemFontOfSize(UIFont.SystemFontSize));
                //foreach (var familyName in UIFont.FamilyNames)
                //{
                //    foreach (var fontName in UIFont.FontNamesForFamilyName(familyName))
                //    {
                //        var font = UIFont.FromName(fontName, UIFont.SystemFontSize);
                //        FontsCollection.Add(font);
                //    }
                //}

                FontsCollection = FontsCollection.OrderBy(f => f.Name).ToList();
            }

            public static IEnumerable<UIFont> GetButtonLabelFonts()
            {
                return FontsCollection.Except(new[]
                {
                    UIFont.FromName(AvenirBook, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirMedium, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirRoman, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirNextMedium, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirNextRegular, UIFont.SystemFontSize),
                    UIFont.FromName(GeezaProBold, UIFont.SystemFontSize),
                    UIFont.FromName(GeezaProRegular, UIFont.SystemFontSize),
                    UIFont.FromName(HiraginoSansW3, UIFont.SystemFontSize),
                    UIFont.FromName(HelveticaNeueLight, UIFont.SystemFontSize)
                });
            }

            public static IEnumerable<UIFont> GetWorkTimeTitleFonts()
            {
                return FontsCollection.Except(new[]
                {
                    UIFont.FromName(AvenirBook, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirMedium, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirRoman, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirNextMedium, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirNextRegular, UIFont.SystemFontSize),
                    UIFont.FromName(GeezaProBold, UIFont.SystemFontSize),
                    UIFont.FromName(GeezaProRegular, UIFont.SystemFontSize),
                    UIFont.FromName(HiraginoSansW3, UIFont.SystemFontSize),
                    UIFont.FromName(FuturaBold, UIFont.SystemFontSize),
                    UIFont.FromName(HelveticaRoman, UIFont.SystemFontSize),
                    UIFont.FromName(HelveticaNeueLight, UIFont.SystemFontSize),
                    UIFont.FromName(HelveticaNeueRoman, UIFont.SystemFontSize)
                });
            }

            public static IEnumerable<UIFont> GetCircleProgressFonts()
            {
                return FontsCollection.Except(new[]
                {
                    UIFont.FromName(AvenirBlack, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirBook, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirMedium, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirRoman, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirNextBold, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirNextMedium, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirNextRegular, UIFont.SystemFontSize),
                    UIFont.FromName(FuturaBold, UIFont.SystemFontSize),
                    UIFont.FromName(FuturaMedium, UIFont.SystemFontSize),
                    UIFont.FromName(GeezaProRegular, UIFont.SystemFontSize),
                    UIFont.FromName(HelveticaRoman, UIFont.SystemFontSize),
                    UIFont.FromName(HelveticaNeueMedium, UIFont.SystemFontSize),
                    UIFont.FromName(HelveticaNeueLight, UIFont.SystemFontSize),
                    UIFont.FromName(HiraginoSansW3, UIFont.SystemFontSize)
                });
            }

            public static IEnumerable<UIFont> GetGhostButtonSimpleLabelFonts()
            {
                return FontsCollection.Except(new[]
                {
                    UIFont.FromName(AvenirBlack, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirBook, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirMedium, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirRoman, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirNextBold, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirNextMedium, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirNextRegular, UIFont.SystemFontSize),
                    UIFont.FromName(GeezaProRegular, UIFont.SystemFontSize),
                    UIFont.FromName(HelveticaNeueLight, UIFont.SystemFontSize),
                    UIFont.FromName(HelveticaRoman, UIFont.SystemFontSize),
                    UIFont.FromName(HiraginoSansW3, UIFont.SystemFontSize)
                });
            }

            public static IEnumerable<UIFont> GetWorkTimeDayFonts()
            {
                return FontsCollection.Except(new[]
                {
                    UIFont.FromName(AvenirBook, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirMedium, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirRoman, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirNextMedium, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirNextRegular, UIFont.SystemFontSize),
                    UIFont.FromName(HiraginoSansW3, UIFont.SystemFontSize),
                    UIFont.FromName(GeezaProBold, UIFont.SystemFontSize),
                    UIFont.FromName(GeezaProRegular, UIFont.SystemFontSize),
                    UIFont.FromName(HelveticaBold, UIFont.SystemFontSize),
                    UIFont.FromName(HelveticaRoman, UIFont.SystemFontSize),
                    UIFont.FromName(HelveticaNeueBold, UIFont.SystemFontSize),
                    UIFont.FromName(HelveticaNeueLight, UIFont.SystemFontSize),
                    UIFont.FromName(FuturaBold, UIFont.SystemFontSize)
                });
            }

            public static IEnumerable<UIFont> GetInputFonts()
            {
                return FontsCollection.Except(new[]
                {
                    UIFont.FromName(AvenirBlack, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirHeavy, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirNextBold, UIFont.SystemFontSize),
                    UIFont.FromName(AvenirNextDemiBold, UIFont.SystemFontSize),
                    UIFont.FromName(GeezaProBold, UIFont.SystemFontSize),
                    UIFont.FromName(HelveticaBold, UIFont.SystemFontSize),
                    UIFont.FromName(HelveticaNeueBold, UIFont.SystemFontSize),
                    UIFont.FromName(HelveticaNeueMedium, UIFont.SystemFontSize),
                    UIFont.FromName(FuturaBold, UIFont.SystemFontSize),
                    UIFont.FromName(FuturaMedium, UIFont.SystemFontSize)
                });
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
