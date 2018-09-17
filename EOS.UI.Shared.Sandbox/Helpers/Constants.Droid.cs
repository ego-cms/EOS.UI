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
            public static Dictionary<string, Color> MainColorsCollection { get; } = new Dictionary<string, Color>()
            {
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

            public static Dictionary<string, Color> FontColorsCollection { get; } = new Dictionary<string, Color>()
            {
                {ColorNameBlack, Color.ParseColor(ColorBlack)},
                {ColorNameDarkGray, Color.ParseColor(ColorDarkGray)},
                {ColorNameGray, Color.ParseColor(ColorGray)},
                {ColorNameLightGray, Color.ParseColor(ColorLightGray)},
                {ColorNameWhite, Color.ParseColor(ColorWhite)},
                {ColorNameRed, Color.ParseColor(ColorRed)},
                {ColorNameUltramarine, Color.ParseColor(ColorUltramarine)}
            };

            public static Dictionary<string, Color> GetGhostButtonFontColors()
            {
                return MainColorsCollection.Union(FontColorsCollection).ToDictionary(a => a.Key, b => b.Value);
            }
        }

        public static class Fonts
        {
            private const string ArimoRegular = "ArimoRegular";
            private const string ArimoBold = "ArimoBold";
            private const string LatoRegular = "LatoRegular";
            private const string LatoBold = "LatoBold";
            private const string LatoBlack = "LatoBlack";
            private const string MontserratRegular = "MontserratRegular";
            private const string MontserratMedium = "MontserratMedium";
            private const string MontserratSemiBold = "MontserratSemiBold";
            private const string MontserratBold = "MontserratBold";
            private const string MontserratExtraBold = "MontserratExtraBold";
            private const string OpenSansRegular = "OpenSansRegular";
            private const string OpenSansSemiBold = "OpenSansSemiBold";
            private const string OpenSansBold = "OpenSansBold";
            private const string OpenSansExtraBold = "OpenSansExtraBold";
            private const string UbuntuRegular = "UbuntuRegular";
            private const string UbuntuMedium = "UbuntuMedium";
            private const string UbuntuBold = "UbuntuBold";
            private const string RobotoRegular = "RobotoRegular";
            private const string RobotoMedium = "RobotoMedium";
            private const string RobotoBold = "RobotoBold";

            public static Dictionary<string, string> GetButtonBadgeFonts()
            {
                return new Dictionary<string, string>
                {
                    { ArimoBold, "Fonts/Arimo-Bold.ttf" },
                    { LatoBlack, "Fonts/Lato-Black.ttf" },
                    { LatoBold, "Fonts/Lato-Bold.ttf" },
                    { MontserratSemiBold, "Fonts/Montserrat-SemiBold.ttf" },
                    { MontserratBold, "Fonts/Montserrat-Bold.ttf" },
                    { OpenSansSemiBold, "Fonts/OpenSans-SemiBold.ttf" },
                    { OpenSansBold, "Fonts/OpenSans-Bold.ttf" },
                    { UbuntuBold, "Fonts/Ubuntu-Bold.ttf" },
                    { RobotoRegular, "Fonts/Roboto-Regular.ttf" },
                    { RobotoMedium, "Fonts/Roboto-Medium.ttf" },
                    { RobotoBold, "Fonts/Roboto-Bold.ttf" }
                };
            }

            public static Dictionary<string, string> GetWorktimeTitleFonts()
            {
                return new Dictionary<string, string>
                {
                    { ArimoBold, "Fonts/Arimo-Bold.ttf" },
                    { LatoBlack, "Fonts/Lato-Black.ttf" },
                    { LatoBold, "Fonts/Lato-Bold.ttf" },
                    { MontserratSemiBold, "Fonts/Montserrat-SemiBold.ttf" },
                    { MontserratBold, "Fonts/Montserrat-Bold.ttf" },
                    { MontserratExtraBold, "Fonts/Montserrat-ExtraBold.ttf" },
                    { OpenSansBold, "Fonts/OpenSans-Bold.ttf" },
                    { OpenSansExtraBold, "Fonts/OpenSans-ExtraBold.ttf" },
                    { UbuntuMedium, "Fonts/Ubuntu-Medium.ttf" },
                    { UbuntuBold, "Fonts/Ubuntu-Bold.ttf" },
                    { RobotoRegular, "Fonts/Roboto-Regular.ttf" },
                    { RobotoMedium, "Fonts/Roboto-Medium.ttf" },
                    { RobotoBold, "Fonts/Roboto-Bold.ttf" }
                };
            }

            public static Dictionary<string, string> GetWorktimeDayFonts()
            {
                return new Dictionary<string, string>
                {
                    { ArimoBold, "Fonts/Arimo-Bold.ttf" },
                    { LatoRegular, "Fonts/Lato-Regular.ttf" },
                    { LatoBold, "Fonts/Lato-Bold.ttf" },
                    { MontserratMedium, "Fonts/Montserrat-Medium.ttf" },
                    { MontserratSemiBold, "Fonts/Montserrat-SemiBold.ttf" },
                    { MontserratBold, "Fonts/Montserrat-Bold.ttf" },
                    { OpenSansSemiBold, "Fonts/OpenSans-SemiBold.ttf" },
                    { OpenSansBold, "Fonts/OpenSans-Bold.ttf" },
                    { UbuntuMedium, "Fonts/Ubuntu-Medium.ttf" },
                    { UbuntuBold, "Fonts/Ubuntu-Bold.ttf" },
                    { RobotoRegular, "Fonts/Roboto-Regular.ttf" },
                    { RobotoMedium, "Fonts/Roboto-Medium.ttf" },
                    { RobotoBold, "Fonts/Roboto-Bold.ttf" }
                };
            }

            public static Dictionary<string, string> GetGhostButtonSimpleLabelFonts()
            {
                return new Dictionary<string, string>
                {
                    { ArimoBold, "Fonts/Arimo-Bold.ttf" },
                    { LatoBlack, "Fonts/Lato-Black.ttf" },
                    { LatoBold, "Fonts/Lato-Bold.ttf" },
                    { MontserratMedium, "Fonts/Montserrat-Medium.ttf" },
                    { MontserratSemiBold, "Fonts/Montserrat-SemiBold.ttf" },
                    { MontserratBold, "Fonts/Montserrat-Bold.ttf" },
                    { OpenSansSemiBold, "Fonts/OpenSans-SemiBold.ttf" },
                    { OpenSansBold, "Fonts/OpenSans-Bold.ttf" },
                    { UbuntuMedium, "Fonts/Ubuntu-Medium.ttf" },
                    { UbuntuBold, "Fonts/Ubuntu-Bold.ttf" },
                    { RobotoRegular, "Fonts/Roboto-Regular.ttf" },
                    { RobotoMedium, "Fonts/Roboto-Medium.ttf" },
                    { RobotoBold, "Fonts/Roboto-Bold.ttf" }
                };
            }

            public static Dictionary<string, string> GetCircleProgressFonts()
            {
                return new Dictionary<string, string>
                {
                    { ArimoBold, "Fonts/Arimo-Bold.ttf" },
                    { LatoBlack, "Fonts/Lato-Black.ttf" },
                    { LatoBold, "Fonts/Lato-Bold.ttf" },
                    { MontserratSemiBold, "Fonts/Montserrat-SemiBold.ttf" },
                    { MontserratBold, "Fonts/Montserrat-Bold.ttf" },
                    { OpenSansBold, "Fonts/OpenSans-Bold.ttf" },
                    { UbuntuBold, "Fonts/Ubuntu-Bold.ttf" },
                    { RobotoRegular, "Fonts/Roboto-Regular.ttf" },
                    { RobotoMedium, "Fonts/Roboto-Medium.ttf" },
                    { RobotoBold, "Fonts/Roboto-Bold.ttf" }
                };
            }

            public static Dictionary<string, string> GetInputFonts()
            {
                return new Dictionary<string, string>
                {
                    { ArimoRegular, "Fonts/Arimo-Regular.ttf" },
                    { LatoRegular, "Fonts/Lato-Regular.ttf" },
                    { MontserratRegular, "Fonts/Montserrat-Regular.ttf" },
                    { MontserratMedium, "Fonts/Montserrat-Medium.ttf" },
                    { OpenSansRegular, "Fonts/OpenSans-Regular.ttf" },
                    { UbuntuRegular, "Fonts/Ubuntu-Regular.ttf" },
                    { RobotoRegular, "Fonts/Roboto-Regular.ttf" },
                    { RobotoMedium, "Fonts/Roboto-Medium.ttf" },
                    { RobotoBold, "Fonts/Roboto-Bold.ttf" }
                };
            }

            public static Dictionary<string, string> GetSectionFonts()
            {
                return new Dictionary<string, string>
                {
                    { RobotoRegular, "Fonts/Roboto-Regular.ttf" },
                    { RobotoMedium, "Fonts/Roboto-Medium.ttf" },
                    { RobotoBold, "Fonts/Roboto-Bold.ttf" }
                };
            }
        }

        public static class Validation
        {
            public static Dictionary<string, int> ValidationCollection { get; } = new Dictionary<string, int>()
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
            public const string Lock = "Lock";
            public const string AccountOff = "Account off";

            public static Dictionary<string, int> DrawableCollection { get; } = new Dictionary<string, int>()
            {
                { string.Empty, 0 },
                { Calendar, Resource.Drawable.icCalendar },
                { AccountCircle, Resource.Drawable.AccountCircle },
                { Lock, Resource.Drawable.Lock },
                { AccountOff, Resource.Drawable.AccountOff },
            };
        }
    }
}
#endif
