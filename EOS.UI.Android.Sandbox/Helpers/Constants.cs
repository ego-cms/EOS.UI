using System.Collections.Generic;
using System.Linq;
using Android.Graphics;
using EOS.UI.Shared.Themes.Enums;
using EOS.UI.Shared.Themes.Helpers;

namespace EOS.UI.Android.Sandbox.Helpers
{
    public class Constants
    {
        public static class Fields
        {
            public const string Theme = "Theme";
            public const string Background = "Background";
            public const string Font = "Font";
            public const string TextColor = "Text color";
            public const string LetterSpacing = "Letter spacing";
            public const string TextSize = "Text size";
            public const string ConerRadius = "Corner radius";
            public const string EnabledTextColor = "Enabled text color";
            public const string DisabledTextColor = "Disabled text color";
            public const string PressedTextColor = "Pressed text color";
            public const string EnabledBackground = "Enabled background";
            public const string DisabledBackground  = "Disabled background";
            public const string PressedBackground = "Pressed background";
            public const string HintTextColor = "Hint text color";
            public const string HintTextColorDisabled = "Hint text color disabled";
            public const string Icon = "Icon";
            public const string IconUnocused = "Icon unfocused";
            public const string IconDisabled = "Icon disabled";
            public const string FocusedColor = "Focused color";
            public const string NormalUnderlineColor = "Normal underline color";
            public const string NormalIconColor = "Normal icon color";
            public const string PopulatedUnderlineColor = "Populated underline color";
            public const string ValidationRules = "Validation rules";
            public const string PopulatedIconColor = "Populated icon color";
            public const string DisabledColor = "Disabled color";
            public const string PressedColor = "Pressed color";
            public const string Size = "Size";
            public const string Color = "Color";
            public const string AlternativeColor = "Alternative color";
            public const string FillColor = "Fill color";
            public const string SectionName = "Section name";
            public const string ButtonText = "Button text";
            public const string SectionNameLetterSpacing = "Section name letter spacing";
            public const string ButtonTextLetterSpacing = "Button text letter spacing";
            public const string SectionNameFont = "Section name font";
            public const string ButtonTextFont = "Button text font";
            public const string SectionTextSize = "Section text size";
            public const string ButtonTextSize = "Button text size";
            public const string SectionTextColor = "Section text color";
            public const string ButtonTextColor = "Button text color";
            public const string BackgroundColor = "Backgroud color";
            public const string BorderColor = "Border color";
            public const string BorderWidth = "Border width";
            public const string PaddingTop = "Padding top";
            public const string PaddingBottom = "Padding bottom";
            public const string PaddingLeft = "Padding left";
            public const string PaddingRight = "Padding right";
            public const string TitleFont = "Title font";
            public const string DayTextFont = "Day text font";
            public const string TitleTextSize = "Title text size";
            public const string DayTextSize = "Day text size";
            public const string TitleColor = "Title color";
            public const string DayTextColor = "Day text color";
            public const string CurrentDayBackgroundColor = "Current day background color";
            public const string CurrentDayTextColor = "Current day text color";
            public const string DayEvenBackgroundColor = "Even day background color";
            public const string DividerColor = "Divider color";
            public const string CurrentDividerColor = "Current day divider color";
            public const string RippleColor = "Ripple color";
            public const string FirstDayOfWeek = "Week start day";
            public const string ButtonType = "Button type";
            public const string ShadowOffsetZ = "Shadow offset Z";
            public const string ShadowRadius = "Shadow radius";
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

        public static class Validation
        {
            public const string Without = "without validation";
            public const string Email = "e-mail validation";
            public const string Empty = "empty validation";

            public static readonly Dictionary<string, int> ValidationCollection = new Dictionary<string, int>()
            {
                { string.Empty, 0 },
                { Without, 1 },
                { Email, 2 },
                { Empty, 3 }
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
            public const string CTA = "CTA button";
            public const string FullBleed = "Full bleed button";

            public static Dictionary<string, int> SimpleButtonTypeCollection = new Dictionary<string, int>()
            {
                { string.Empty, 0 },
                { Simple, 1 },
                { FullBleed, 2 },
            };

            public static Dictionary<string, int> CTAButtonTypeCollection = new Dictionary<string, int>()
            {
                { string.Empty, 0 },
                { CTA, 1 },
                { FullBleed, 2 },
            };
        }

        public static class Shadow
        {
            public static readonly Dictionary<string, float> OffsetCollection = new Dictionary<string, float>();
            public static readonly Dictionary<string, float> RadiusCollection = new Dictionary<string, float>();

            static Shadow()
            {
                OffsetCollection.Add("default", 0f);
                var offsetValues = Enumerable.Range(0, 30);
                foreach(var val in offsetValues)
                    OffsetCollection.Add(val.ToString(), (float)val);

                RadiusCollection.Add("default", 0f);
                var radiusValues = Enumerable.Range(0, 16);
                foreach(var val in radiusValues)
                    RadiusCollection.Add(val.ToString(), (float)val);
            }
        }
    }
}
