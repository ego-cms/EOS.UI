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
            public static Dictionary<string, UIColor> MainColorsCollection { get; } = new Dictionary<string, UIColor>()
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

            public static Dictionary<string, UIColor> FontColorsCollection { get; } = new Dictionary<string, UIColor>()
            {
                {ColorNameBlack, ColorExtension.FromHex(ColorBlack)},
                {ColorNameDarkGray, ColorExtension.FromHex(ColorDarkGray)},
                {ColorNameGray, ColorExtension.FromHex(ColorGray)},
                {ColorNameLightGray, ColorExtension.FromHex(ColorLightGray)},
                {ColorNameWhite, ColorExtension.FromHex(ColorWhite)},
                {ColorNameRed, ColorExtension.FromHex(ColorRed)},
                {ColorNameUltramarine, ColorExtension.FromHex(ColorUltramarine)}
            };

            public static Dictionary<string, UIColor> GetGhostButtonFontColors()
            {
                return MainColorsCollection.Union(FontColorsCollection).ToDictionary(a => a.Key, b => b.Value);
            }
        }

        public static class Fonts
        {
            public static Dictionary<string, UIFont> FontsCollection;
            private const string AvenirBlack = "Avenir-Black";
            private const string AvenirBook = "Avenir-Book";
            private const string AvenirHeavy = "Avenir-Heavy";
            private const string AvenirMedium = "Avenir-Medium";
            private const string AvenirRoman = "Avenir-Roman";
            private const string AvenirNextDemiBold = "AvenirNext-DemiBold";
            private const string AvenirNextBold = "AvenirNext-Bold";
            private const string AvenirNextMedium = "AvenirNext-Medium";
            private const string AvenirNextRegular = "AvenirNext-Regular";
            private const string FuturaBold = "Futura-Bold";
            private const string FuturaMedium = "Futura-Medium";
            private const string HelveticaBold = "Helvetica-Bold";
            private const string HelveticaRoman = "Helvetica";
            private const string HelveticaNeueBold = "HelveticaNeue-Bold";
            private const string HelveticaNeueLight = "HelveticaNeue-Light";
            private const string HelveticaNeueMedium = "HelveticaNeue-Medium";
            private const string HelveticaNeueRoman = "HelveticaNeue";
            private const string HiraginoSansW3 = "HiraginoSans-W3";
            private const string SystemBold = "SystemBold";
            private const string SystemMedium = "SystemMedium";
            private const string SystemRegular = "SystemRegular";
            private const string SystemSemibold = "SystemSemibold";

            static Fonts()
            {
                FontsCollection = new Dictionary<string, UIFont>
                {
                    { SystemBold, UIFont.SystemFontOfSize(UIFont.SystemFontSize, UIFontWeight.Bold) },
                    { SystemMedium, UIFont.SystemFontOfSize(UIFont.SystemFontSize, UIFontWeight.Medium) },
                    { SystemRegular, UIFont.SystemFontOfSize(UIFont.SystemFontSize, UIFontWeight.Regular) },
                    { SystemSemibold, UIFont.SystemFontOfSize(UIFont.SystemFontSize, UIFontWeight.Semibold) },

                    { AvenirRoman, UIFont.FromName(AvenirRoman, UIFont.SystemFontSize) },
                    { AvenirBook, UIFont.FromName(AvenirBook, UIFont.SystemFontSize) },
                    { AvenirMedium, UIFont.FromName(AvenirMedium, UIFont.SystemFontSize) },
                    { AvenirBlack, UIFont.FromName(AvenirBlack, UIFont.SystemFontSize) },
                    { AvenirHeavy, UIFont.FromName(AvenirHeavy, UIFont.SystemFontSize) },
                    { AvenirNextRegular, UIFont.FromName(AvenirNextRegular, UIFont.SystemFontSize) },
                    { AvenirNextMedium, UIFont.FromName(AvenirNextMedium, UIFont.SystemFontSize) },
                    { AvenirNextDemiBold, UIFont.FromName(AvenirNextDemiBold, UIFont.SystemFontSize) },
                    { FuturaMedium, UIFont.FromName(FuturaMedium, UIFont.SystemFontSize) },
                    { FuturaBold, UIFont.FromName(FuturaBold, UIFont.SystemFontSize) },
                    { HelveticaRoman, UIFont.FromName(HelveticaRoman, UIFont.SystemFontSize) },
                    { HelveticaBold, UIFont.FromName(HelveticaBold, UIFont.SystemFontSize) },
                    { HelveticaNeueLight, UIFont.FromName(HelveticaNeueLight, UIFont.SystemFontSize) },
                    { HelveticaNeueRoman, UIFont.FromName(HelveticaNeueRoman, UIFont.SystemFontSize) },
                    { HelveticaNeueMedium, UIFont.FromName(HelveticaNeueMedium, UIFont.SystemFontSize) },
                    { HelveticaNeueBold, UIFont.FromName(HelveticaNeueBold, UIFont.SystemFontSize) },
                    { HiraginoSansW3, UIFont.FromName(HiraginoSansW3, UIFont.SystemFontSize) }
                };
            }

            public static Dictionary<string, UIFont> GetButtonLabelFonts()
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
                }).Select(k => FontsCollection.Single(s => s.Key == k)).ToDictionary(e => e.Key, e => e.Value);
            }

            public static Dictionary<string, UIFont> GetWorkTimeTitleFonts()
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
                }).Select(k => FontsCollection.Single(s => s.Key == k)).ToDictionary(e => e.Key, e => e.Value);
            }

            public static Dictionary<string, UIFont> GetCircleProgressFonts()
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
                }).Select(k => FontsCollection.Single(s => s.Key == k)).ToDictionary(e => e.Key, e => e.Value);
            }

            public static Dictionary<string, UIFont> GetGhostButtonSimpleLabelFonts()
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
                }).Select(k => FontsCollection.Single(s => s.Key == k)).ToDictionary(e => e.Key, e => e.Value);
            }

            public static Dictionary<string, UIFont> GetWorkTimeDayFonts()
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
                }).Select(k => FontsCollection.Single(s => s.Key == k)).ToDictionary(e => e.Key, e => e.Value);
            }

            public static Dictionary<string, UIFont> GetInputFonts()
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
                    }).Select(k => FontsCollection.Single(s => s.Key == k)).ToDictionary(e => e.Key, e => e.Value);
            }
        }

        public static class Validation
        {
            public static Dictionary<string, Predicate<string>> ValidationCollection { get; } = new Dictionary<string, Predicate<string>>()
            {
                {"without validation", null },
                {"e-mail validation", (s) => s.Contains("@") && !String.IsNullOrEmpty(s)},
                {"empty validation", (s) => !String.IsNullOrEmpty(s) },
            };
        }

        public static class Icons
        {
            public static Dictionary<string, string> IconsCollection { get; } = new Dictionary<string, string>()
            {
                { "Calendar", "icCalendar" },
                { "Account circle", "icAccountCircle" },
                { "Lock", "icLock" },
                { "Account off", "icAccountOff" },
            };
        }
    }
}
#endif
