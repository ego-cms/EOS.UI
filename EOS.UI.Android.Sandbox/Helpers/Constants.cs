using System.Collections.Generic;
using System.Linq;
using Android.Graphics;
using EOS.UI.Shared.Themes.Enums;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Android.Helpers;
using EOS.UI.Shared.Helpers;

namespace EOS.UI.Android.Sandbox.Helpers
{
    public class Constants
    {
        public static class Fields
        {
            public static string Theme = "Theme";
            public static string Background = "Background";
            public static string Font = "Font";
            public static string TextColor = "Text color";
            public static string LetterSpacing = "Letter spacing";
            public static string TextSize = "Text size";
            public static string ConerRadius = "Corner radius";
            public static string EnabledTextColor = "Enabled text color";
            public static string DisabledTextColor = "Disabled text color";
            public static string PressedTextColor = "Pressed text color";
            public static string EnabledBackground = "Enabled background";
            public static string DisabledBackground  = "Disabled background";
            public static string PressedBackground = "Pressed background";
            public static string HintTextColor = "Hint text color";
            public static string HintTextColorDisabled = "Hint text color disabled";
            public static string IconFocused = "Icon focused";
            public static string IconUnocused = "Icon unfocused";
            public static string IconDisabled = "Icon disabled";
            public static string UnderlineColorFocused = "Underline color focused";
            public static string UnderlineColorUnocused = "Underline color unfocused";
            public static string UnderlineColorDisabled = "Underline color disabled";
            public static string DisabledColor = "Disabled color";
            public static string PressedColor = "Pressed color";
            public static string Size = "Size";
            public static string Shadow = "Shadow";
            public static string ShadowColor = "ShadowColor";
            public static string Color = "Color";
            public static string AlternativeColor = "Alternative color";
            public static string FillColor = "Fill color";
            public static string SectionName = "Section name";
            public static string ButtonText = "Button text";
            public static string SectionNameLetterSpacing = "Section name letter spacing";
            public static string ButtonTextLetterSpacing = "Button text letter spacing";
            public static string SectionNameFont = "Section name font";
            public static string ButtonTextFont = "Button text font";
            public static string SectionTextSize = "Section text size";
            public static string ButtonTextSize = "Button text size";
            public static string SectionTextColor = "Section text color";
            public static string ButtonTextColor = "Button text color";
            public static string BackgroundColor = "Backgroud color";
            public static string BorderColor = "Border color";
            public static string BorderWidth = "Border width";
            public static string PaddingTop = "Padding top";
            public static string PaddingBottom = "Padding bottom";
            public static string PaddingLeft = "Padding left";
            public static string PaddingRight = "Padding right";
            public static string TitleFont = "Title font";
            public static string DayTextFont = "Day text font";
            public static string TitleTextSize = "Title text size";
            public static string DayTextSize = "Day text size";
            public static string TitleColor = "Title color";
            public static string DayTextColor = "Day text color";
            public static string CurrentDayBackgroundColor = "Current day background color";
            public static string CurrentDayTextColor = "Current day text color";
            public static string DayEvenBackgroundColor = "Even day background color";
            public static string DividerColor = "Divider color";
            public static string CurrentDividerColor = "Current day divider color";
            public static string RippleColor = "Ripple color";
            public static string FirstDayOfWeek = "Week start day";
            public static string ButtonType = "Button type";
        };

        public static class Colors
        {
            public const string Black = "black";
            public const string White = "white";
            public const string Gray = "gray";
            public const string Green = "green";
            public const string Blue = "blue";
            public const string Red = "red";
            public const string Yellow = "yellow";
            public const string Brown = "brown";

            public static readonly Dictionary<string, Color> ColorsCollection = new Dictionary<string, Color>()
            {
                { string.Empty, Color.Transparent },
                { Black, Color.Black },
                { White, Color.White },
                { Gray, Color.Gray },
                { Green, Color.Green },
                { Blue, Color.Blue },
                { Red, Color.Red },
                { Yellow, Color.Yellow },
                { Brown, Color.Brown }
            };
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

        public static readonly Dictionary<string, bool> ControlState = new Dictionary<string, bool>()
        {
            {string.Empty, true},
            {"enabled", true},
            {"disabled", false},
        };

        public static class Sizes
        {
            public static readonly Dictionary<string, float> LetterSpacingCollection = new Dictionary<string, float>()
            {
                { string.Empty, 0f },
                { "0.1", 0.1f },
                { "0.2", 0.2f },
                { "0.3", 0.3f },
                { "0.4", 0.4f },
                { "0.5", 0.5f },
                { "0.6", 0.6f },
                { "0.7", 0.7f },
                { "0.8", 0.8f },
                { "0.9", 0.9f },
                { "1", 1f },
            };

            public static readonly Dictionary<string, float> TextSizeCollection = new Dictionary<string, float>()
            {
                { string.Empty, 0f },
                { "10", 10f },
                { "12", 12f },
                { "14", 14f },
                { "16", 16f },
                { "18", 18f },
                { "20", 20f },
                { "22", 22f },
                { "24", 24f },
                { "26", 26f },
                { "28", 28f },
                { "30", 30f },
                { "32", 32f },
                { "34", 34f },
                { "36", 36f },
                { "38", 38f },
                { "40", 40f },
            };

            public static readonly Dictionary<string, float> CornerRadusCollection = new Dictionary<string, float>()
            {
                { string.Empty, 0f },
                { "10", 10f },
                { "14", 14f },
                { "18", 18f },
                { "22", 22f },
                { "26", 26f },
                { "30", 30f },
                { "34", 34f },
                { "38", 38f },
                { "42", 42f },
                { "46", 46f },
                { "50", 50f },
                { "54", 54f },
                { "58", 58f },
                { "62", 62f },
                { "66", 66f },
                { "70", 70f },
            };

            public static readonly Dictionary<string, int> BorderWidthCollection = new Dictionary<string, int>()
            {
                { string.Empty, 0 },
                { "1", 1 },
                { "2", 2 },
                { "3", 3 },
                { "4", 4 },
                { "5", 5 },
                { "6", 6 },
                { "7", 7 },
                { "8", 8 },
                { "9", 9 },
                { "10", 10 },
            };

            public static readonly Dictionary<string, int> PaddingsCollection = new Dictionary<string, int>()
            {
                { string.Empty, 0 },
                { "1", 1 },
                { "2", 2 },
                { "3", 3 },
                { "4", 4 },
                { "5", 5 },
                { "6", 6 },
                { "7", 7 },
                { "8", 8 },
                { "9", 9 },
                { "10", 10 },
                { "11", 11 },
                { "12", 12 },
                { "13", 13 },
                { "14", 14 },
                { "15", 15 },
                { "16", 16 },
                { "17", 17 },
                { "18", 18 },
                { "19", 19 },
                { "20", 20 },
            };
           
            public static readonly Dictionary<string, int> FabProgressSizes = new Dictionary<string, int>()
            {
                { string.Empty, 100 },
                { "110", 110 },
                { "120", 120 },
                { "130", 130 },
                { "140", 140 },
                { "150", 150 },
                { "160", 160 },
                { "170", 170 },
                { "180", 180 },
                { "220", 220 },
                { "400", 400 },
            };
        }

        public static class ThemeTypes
        {
            public const string Light = "light";
            public const string Dark = "dark";

            public static Dictionary<string, EOSThemeEnumeration> ThemeCollection = new Dictionary<string, EOSThemeEnumeration>()
            {
                { string.Empty, EOSThemeEnumeration.Light },
                { Light, EOSThemeEnumeration.Light },
                { Dark, EOSThemeEnumeration.Dark },
            };
        }

        public static class Shadows
        {
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

            public static Dictionary<string, ShadowConfig> ShadowsCollection = new Dictionary<string, ShadowConfig>()
            {
                { string.Empty, null },
                { Shadow1, new ShadowConfig{ Color = Color.Red, Offset = new Point(15,0), Blur = 5, Spread = 200} },
                { Shadow2, new ShadowConfig{ Color = Color.Red, Offset = new Point(-15,0), Blur = 5, Spread = 200} },
                { Shadow3, new ShadowConfig{ Color = Color.Red, Offset = new Point(0,15), Blur = 5, Spread = 200} },
                { Shadow4, new ShadowConfig{ Color = Color.Red, Offset = new Point(0,-15), Blur = 5, Spread = 200} },
                { Shadow5, new ShadowConfig{ Color = Color.Red, Offset = new Point(5,0), Blur = 15, Spread = 200} },
                { Shadow6, new ShadowConfig{ Color = Color.Red, Offset = new Point(-5,0), Blur = 15, Spread = 200} },
                { Shadow7, new ShadowConfig{ Color = Color.Red, Offset = new Point(0,5), Blur = 25, Spread = 200} },
                { Shadow8, new ShadowConfig{ Color = Color.Red, Offset = new Point(0,-5), Blur = 25, Spread = 200} },
                { Shadow9, new ShadowConfig{ Color = Color.Green, Offset = new Point(5,5), Blur = 5, Spread = 200} },
                { Shadow10, new ShadowConfig{ Color = Color.Green, Offset = new Point(5,0), Blur = 5, Spread = 200} },
                { Shadow11, new ShadowConfig{ Color = Color.Blue, Offset = new Point(-5,5), Blur = 5, Spread = 200} },
                { Shadow12, new ShadowConfig{ Color = Color.Yellow, Offset = new Point(5,-5), Blur = 5, Spread = 200} },
                { Shadow13, new ShadowConfig{ Color = Color.Purple, Offset = new Point(-5,-5), Blur = 5, Spread = 200} },
                { Shadow14, new ShadowConfig{ Color = Color.Purple, Offset = new Point(25,-25), Blur = 15, Spread = 200} },
                { Shadow15, new ShadowConfig{ Color = Color.Purple, Offset = new Point(6,6), Blur = 1, Spread = 200} },
                { Shadow16, new ShadowConfig{ Color = Color.Argb(230,255,0,0), Offset = new Point(15,0), Blur = 5, Spread = 200} },
                { Shadow17, new ShadowConfig{ Color = Color.Argb(178,255,0,0), Offset = new Point(15,0), Blur = 5, Spread = 200} },
                { Shadow18, new ShadowConfig{ Color = Color.Argb(127,255,0,0), Offset = new Point(15,0), Blur = 5, Spread = 200} },
                { Shadow19, new ShadowConfig{ Color = Color.Argb(77,255,0,0), Offset = new Point(15,0), Blur = 5, Spread = 200} },
                { Shadow20, new ShadowConfig{ Color = Color.Black, Offset = new Point(0,0), Blur = 15, Spread = 200}  },
                { Shadow21, new ShadowConfig{ Color = Color.Black, Offset = new Point(0,0), Blur = 5, Spread = 200}  },
                { Shadow22, new ShadowConfig{ Color = Color.Black, Offset = new Point(0,0), Blur = 25, Spread = 200}  },
                { Shadow23, null },
            };
        }

        public static class Icons
        {
            public const string AccountCircle = "account circle";
            public const string AccountKey = "account key";
            public const string AccountOff = "account off";
            public const string AndroidIcon = "android";
            public const string Airballoon = "air balloon";
            public const string Apple = "apple";

            public static Dictionary<string, int> DrawableCollection = new Dictionary<string, int>()
            {
                { string.Empty, 0 },
                { AccountCircle, Resource.Drawable.AccountCircle },
                { AccountKey, Resource.Drawable.AccountKey },
                { AccountOff, Resource.Drawable.AccountOff },
                { Airballoon, Resource.Drawable.Airballoon },
                { AndroidIcon, Resource.Drawable.Android },
                { Apple, Resource.Drawable.Apple },
            };
        }

        public static class Titles
        {
            public const string FirstTitle = "first title";
            public const string SecondTitle = "second title";
            public const string ThirdTitle = "third title";

            public static Dictionary<string, string> TitleCollection = new Dictionary<string, string>()
            {
                { string.Empty, string.Empty },
                { FirstTitle, "First" },
                { SecondTitle, "Second" },
                { ThirdTitle, "Third" },
            };
        }

        public static class Days
        {
            public const string Sunday = "Sunday";
            public const string Monday = "Monday";

            public static Dictionary<string, WeekStartEnum> DaysCollection = new Dictionary<string, WeekStartEnum>()
            {
                { string.Empty, WeekStartEnum.Sunday },
                { Sunday, WeekStartEnum.Sunday },
                { Monday, WeekStartEnum.Monday },
            };
        }

        public static class Buttons
        {
            public const string Simple = "Simple button";
            public const string FullBleed = "Full bleed button";

            public static Dictionary<string, int> ButtonTypeollection = new Dictionary<string, int>()
            {
                { string.Empty, 0 },
                { Simple, 1 },
                { FullBleed, 2 },
            };
        }

    }
}
