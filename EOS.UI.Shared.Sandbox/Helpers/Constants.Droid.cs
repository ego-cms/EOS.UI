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
            //public const string Roboto = "Roboto";
            //public const string Berkshireswash = "Berkshireswash";
            //public const string Amita = "Amita";
            //public const string AcademyEngraved = "AcademyEngraved";

            //public static readonly Dictionary<string, string> FontsCollection = new Dictionary<string, string>()
            //{
            //    { string.Empty, string.Empty },
            //    { Roboto, "Fonts/Roboto.ttf" },
            //    { Berkshireswash, "Fonts/Berkshireswash.ttf" },
            //    { Amita, "Fonts/Amita.ttf" },
            //    { AcademyEngraved, "Fonts/academyEngraved.ttf" },
            //};

            public const string ArimoRegular = "ArimoRegular";
            public const string ArimoBold = "ArimoBold";
            public const string LatoRegular = "LatoRegular";
            public const string LatoBold = "LatoBold";
            public const string LatoBlack = "LatoBlack";
            public const string MontserratRegular = "MontserratRegular";
            public const string MontserratMedium = "MontserratMedium";
            public const string MontserratSemiBold = "MontserratSemiBold";
            public const string MontserratBold = "MontserratBold";
            public const string MontserratExtraBold = "MontserratExtraBold";
            public const string OpenSansRegular = "OpenSansRegular";
            public const string OpenSansSemiBold = "OpenSansSemiBold";
            public const string OpenSansBold = "OpenSansBold";
            public const string OpenSansExtraBold = "OpenSansExtraBold";
            public const string UbuntuRegular = "UbuntuRegular";
            public const string UbuntuMedium = "UbuntuMedium";
            public const string UbuntuBold = "UbuntuBold";
            public const string RobotoRegular = "RobotoRegular";
            public const string RobotoMedium = "RobotoMedium";
            public const string RobotoBold = "RobotoBold";

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
            public const string Lock = "Lock";
            public const string AccountOff = "Account off";

            public static Dictionary<string, int> DrawableCollection = new Dictionary<string, int>()
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
