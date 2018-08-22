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
            public static Dictionary<string, UIColor> MainColorsCollection = new Dictionary<string, UIColor>()
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


            public static Dictionary<string, UIColor> FontColorsCollection = new Dictionary<string, UIColor>()
            {
                {ColorNameBlack, ColorExtension.FromHex(ColorBlack)},
                {ColorNameDarkGray, ColorExtension.FromHex(ColorDarkGray)},
                {ColorNameGray, ColorExtension.FromHex(ColorGray)},
                {ColorNameLightGray, ColorExtension.FromHex(ColorLightGray)},
                {ColorNameWhite, ColorExtension.FromHex(ColorWhite)},
                {ColorNameRed, ColorExtension.FromHex(ColorRed)},
                {ColorNameUltramarine, ColorExtension.FromHex(ColorUltramarine)}
            };

            public static Dictionary<string, UIColor> GetGhostButtonFonts()
            {
                return MainColorsCollection.Union(FontColorsCollection).ToDictionary(a => a.Key, b => b.Value);
            }
        }

        public static class Fonts
        {
            public static Dictionary<string, UIFont> FontsCollection;
            public static string AvenirBlack = "Avenir-Black";
            public static string AvenirBook = "Avenir-Book";
            public static string AvenirHeavy = "Avenir-Heavy";
            public static string AvenirMedium = "Avenir-Medium";
            public static string AvenirRoman = "Avenir-Roman";
            public static string AvenirNextDemiBold = "AvenirNext-DemiBold";
            public static string AvenirNextBold = "AvenirNext-Bold";
            public static string AvenirNextMedium = "AvenirNext-Medium";
            public static string AvenirNextRegular = "AvenirNext-Regular";
            public static string FuturaBold = "Futura-Bold";
            public static string FuturaMedium = "Futura-Medium";
            public static string HelveticaBold = "Helvetica-Bold";
            public static string HelveticaRoman = "Helvetica";
            public static string HelveticaNeueBold = "HelveticaNeue-Bold";
            public static string HelveticaNeueLight = "HelveticaNeue-Light";
            public static string HelveticaNeueMedium = "HelveticaNeue-Medium";
            public static string HelveticaNeueRoman = "HelveticaNeue";
            public static string HiraginoSansW3 = "HiraginoSans-W3";
            public static string SystemBold = "SystemBold";
            public static string SystemMedium = "SystemMedium";
            public static string SystemRegular = "SystemRegular";
            public static string SystemSemibold = "SystemSemibold";

            static Fonts()
            {
                FontsCollection = new Dictionary<string, UIFont>();

                FontsCollection.Add(SystemBold, UIFont.SystemFontOfSize(UIFont.SystemFontSize, UIFontWeight.Bold));
                FontsCollection.Add(SystemMedium, UIFont.SystemFontOfSize(UIFont.SystemFontSize, UIFontWeight.Medium));
                FontsCollection.Add(SystemRegular, UIFont.SystemFontOfSize(UIFont.SystemFontSize, UIFontWeight.Regular));
                FontsCollection.Add(SystemSemibold, UIFont.SystemFontOfSize(UIFont.SystemFontSize, UIFontWeight.Semibold));

                FontsCollection.Add(AvenirRoman, UIFont.FromName(AvenirRoman, UIFont.SystemFontSize));
                FontsCollection.Add(AvenirBook, UIFont.FromName(AvenirBook, UIFont.SystemFontSize));
                FontsCollection.Add(AvenirMedium, UIFont.FromName(AvenirMedium, UIFont.SystemFontSize));
                FontsCollection.Add(AvenirBlack, UIFont.FromName(AvenirBlack, UIFont.SystemFontSize));
                FontsCollection.Add(AvenirHeavy, UIFont.FromName(AvenirHeavy, UIFont.SystemFontSize));
                FontsCollection.Add(AvenirNextRegular, UIFont.FromName(AvenirNextRegular, UIFont.SystemFontSize));
                FontsCollection.Add(AvenirNextMedium, UIFont.FromName(AvenirNextMedium, UIFont.SystemFontSize));
                FontsCollection.Add(AvenirNextDemiBold, UIFont.FromName(AvenirNextDemiBold, UIFont.SystemFontSize));
                FontsCollection.Add(FuturaMedium, UIFont.FromName(FuturaMedium, UIFont.SystemFontSize));
                FontsCollection.Add(FuturaBold, UIFont.FromName(FuturaBold, UIFont.SystemFontSize));
                FontsCollection.Add(HelveticaRoman, UIFont.FromName(HelveticaRoman, UIFont.SystemFontSize));
                FontsCollection.Add(HelveticaBold, UIFont.FromName(HelveticaBold, UIFont.SystemFontSize));
                FontsCollection.Add(HelveticaNeueLight, UIFont.FromName(HelveticaNeueLight, UIFont.SystemFontSize));
                FontsCollection.Add(HelveticaNeueRoman, UIFont.FromName(HelveticaNeueRoman, UIFont.SystemFontSize));
                FontsCollection.Add(HelveticaNeueMedium, UIFont.FromName(HelveticaNeueMedium, UIFont.SystemFontSize));
                FontsCollection.Add(HelveticaNeueBold, UIFont.FromName(HelveticaNeueBold, UIFont.SystemFontSize));
                FontsCollection.Add(HiraginoSansW3, UIFont.FromName(HiraginoSansW3, UIFont.SystemFontSize));
            }

            public static IEnumerable<UIFont> GetButtonLabelFonts()
            {
                return FontsCollection.Keys.Except(new[] {
                    AvenirBook,
                    AvenirMedium,
                    AvenirNextBold,
                    AvenirRoman,
                    AvenirNextMedium,
                    AvenirNextRegular,
                    HiraginoSansW3,
                    HelveticaNeueLight
                }).Select(k => FontsCollection[k]);
            }

            public static IEnumerable<UIFont> GetWorkTimeTitleFonts()
            {
                return FontsCollection.Keys.Except(new[] {
                   AvenirBook,
                    AvenirMedium,
                    AvenirRoman,
                    AvenirNextBold,
                    AvenirNextMedium,
                    AvenirNextRegular,
                    HiraginoSansW3,
                    FuturaBold,
                    HelveticaNeueLight,
                    HelveticaNeueRoman
                }).Select(k => FontsCollection[k]);
            }

            public static IEnumerable<UIFont> GetCircleProgressFonts()
            {
                return FontsCollection.Keys.Except(new[] {
                    AvenirBook,
                    AvenirMedium,
                    AvenirRoman,
                    AvenirNextBold,
                    AvenirNextMedium,
                    AvenirNextRegular,
                    FuturaBold,
                    FuturaMedium,
                    HelveticaRoman,
                    HelveticaNeueRoman,
                    HelveticaNeueMedium,
                    HelveticaNeueLight,
                    HiraginoSansW3,
                }).Select(k => FontsCollection[k]);
            }

            public static IEnumerable<UIFont> GetGhostButtonSimpleLabelFonts()
            {
                return FontsCollection.Keys.Except(new[] {
                        AvenirBook,
                    AvenirMedium,
                    AvenirRoman,
                    AvenirNextBold,
                    AvenirNextMedium,
                    AvenirNextRegular,
                    HelveticaRoman,
                    HelveticaNeueRoman,
                    HelveticaNeueLight,
                    HiraginoSansW3
                }).Select(k => FontsCollection[k]);
            }

            public static IEnumerable<UIFont> GetWorkTimeDayFonts()
            {
                return FontsCollection.Keys.Except(new[] {
                    AvenirBook,
                    AvenirMedium,
                    AvenirRoman,
                    AvenirNextBold,
                    AvenirNextMedium,
                    AvenirNextRegular,
                    HelveticaBold,
                    HelveticaNeueBold,
                    HelveticaNeueLight,
                    FuturaBold
                }).Select(k => FontsCollection[k]);
            }

            public static IEnumerable<UIFont> GetInputFonts()
            {
                return FontsCollection.Keys.Except(new[] {
                    AvenirBlack,
                    AvenirHeavy,
                    AvenirNextBold,
                    AvenirNextDemiBold,
                    HelveticaBold,
                    HelveticaNeueBold,
                    HelveticaNeueMedium,
                    FuturaBold,
                    FuturaMedium,
                    }).Select(k => FontsCollection[k]);
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
