#if __ANDROID__
using System;
using System.Collections.Generic;
using System.Linq;
using Android.Graphics;
using Android.Widget;
using EOS.UI.Droid.Sandbox;

namespace EOS.UI.Shared.Sandbox.Helpers
{
    public static partial class Constants
    {
        public static class Colors
        {
            public static readonly Dictionary<string, Color> MainColorsCollection = new Dictionary<string, Color>()
            {
                { string.Empty, Color.Transparent },
                {ColorNameBlue, Color.ParseColor(ColorBlue)},
                {ColorNameUltramarine, Color.ParseColor(ColorUltramarine)},
                {ColorNameCerulean, Color.ParseColor(ColorCerulean)},
                {ColorNameTeal, Color.ParseColor(ColorTeal)},
                {ColorNameGreen, Color.ParseColor(ColorGreen)},
                {ColorNameLime, Color.ParseColor(ColorLime)},
                {ColorNameYellow, Color.ParseColor(ColorYellow)},
                {ColorNameGold, Color.ParseColor(ColorGold)},
                {ColorNameOrange, Color.ParseColor(ColorOrange)},
                {ColorNamePeach, Color.ParseColor(ColorPeach)},
                {ColorNameRed, Color.ParseColor(ColorRed)},
                {ColorNameMagenta, Color.ParseColor(ColorMagenta)},
                {ColorNamePurple, Color.ParseColor(ColorPurple)},
                {ColorNameViolet, Color.ParseColor(ColorViolet)},
                {ColorNameIndigo, Color.ParseColor(ColorIndigo)},
            };

            public static Dictionary<string, Color> FontColorsCollection = new Dictionary<string, Color>()
            {
                { string.Empty, Color.Transparent },
                {ColorNameBlack, Color.ParseColor(ColorBlack)},
                {ColorNameDarkGray, Color.ParseColor(ColorDarkGray)},
                {ColorNameGray, Color.ParseColor(ColorGray)},
                {ColorNameLightGray, Color.ParseColor(ColorLightGray)},
                {ColorNameWhite, Color.ParseColor(ColorWhite)},
                {ColorNameRed, Color.ParseColor(ColorRed)},
                {ColorNameUltramarine, Color.ParseColor(ColorUltramarine)}
            };

            public static Dictionary<string, Color> GetGhostButtonFonts()
            {
                return MainColorsCollection.Union(FontColorsCollection).ToDictionary(a => a.Key, b => b.Value);
            }
        }

        public static class Fonts
        {
            public const string Roboto = "Roboto";
            public const string Berkshireswash = "Berkshireswash";
            public const string Amita = "Amita";
            public const string AcademyEngraved = "AcademyEngraved";

            public static readonly Dictionary<string, string> FontsCollection = new Dictionary<string, string>()
            {
                { string.Empty, string.Empty },
                { Roboto, "Fonts/Roboto.ttf" },
                { Berkshireswash, "Fonts/Berkshireswash.ttf" },
                { Amita, "Fonts/Amita.ttf" },
                { AcademyEngraved, "Fonts/academyEngraved.ttf" },
            };
        }

        public static class Validation
        {
            public static readonly Dictionary<string, int> ValidationCollection = new Dictionary<string, int>()
            {
                { string.Empty, 0 },
                { "without validation", 1 },
                { "e-mail validation", 2 },
                { "empty validation", 3 }
            };
        }

        public static class Icons
        {
            public const string Calendar = "Calendar";
            public const string AccountCircle = "Account circle";
            public const string AccountKey = "Account key";
            public const string AccountOff = "Account off";

            public static Dictionary<string, int> DrawableCollection = new Dictionary<string, int>()
            {
                { string.Empty, 0 },
                { Calendar, Resource.Drawable.icCalendar },
                { AccountCircle, Resource.Drawable.AccountCircle },
                { AccountKey, Resource.Drawable.AccountKey },
                { AccountOff, Resource.Drawable.AccountOff },
            };
        }
    }
}
#endif
