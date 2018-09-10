using System.Collections.Generic;
using System.Linq;
using EOS.UI.Shared.Themes.Enums;
using EOS.UI.Shared.Themes.Helpers;

//On Android 0 element is selected 'default' value for control, that doesn't show in spinner's dropdown
namespace EOS.UI.Shared.Sandbox.Helpers
{
    public static partial class Constants
    {
        private static string ColorNameBlue = "Blue";
        private static string ColorNameUltramarine = "Ultramarine";
        private static string ColorNameCerulean = "Cerulean";
        private static string ColorNameTeal = "Teal";
        private static string ColorNameGreen = "Green";
        private static string ColorNameLime = "Lime";
        private static string ColorNameYellow = "Yellow";
        private static string ColorNameGold = "Gold";
        private static string ColorNameOrange = "Orange";
        private static string ColorNamePeach = "Peach";
        private static string ColorNameRed = "Red";
        private static string ColorNameMagenta = "Magenta";
        private static string ColorNamePurple = "Purple";
        private static string ColorNameViolet = "Violet";
        private static string ColorNameIndigo = "Indigo";
        private static string ColorNameBlack = "Black";
        private static string ColorNameDarkGray = "Dark Gray";
        private static string ColorNameGray = "Gray";
        private static string ColorNameLightGray = "Light Gray";
        private static string ColorNameWhite = "White";

        private static string ColorBlue = "#2d74da";
        private static string ColorUltramarine = "#3c6df0";
        private static string ColorCerulean = "#047cc0";
        private static string ColorTeal = "#00baa1";
        private static string ColorGreen = "#34bc6e";
        private static string ColorLime = "#81b532";
        private static string ColorYellow = "#fed500";
        private static string ColorGold = "#ffb000";
        private static string ColorOrange = "#fe8500";
        private static string ColorPeach = "#fc835c";
        private static string ColorRed = "#ff5c49";
        private static string ColorMagenta = "#dc267f";
        private static string ColorPurple = "#c22dd5";
        private static string ColorViolet = "#9753e1";
        private static string ColorIndigo = "#785ef0";
        private static string ColorBlack = "#343334";
        private static string ColorDarkGray = "#777677";
        private static string ColorGray = "#C0BFC0";
        private static string ColorLightGray = "#EAEAEA";
        private static string ColorWhite = "#FFFFFF";

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
            public const string DisabledBackground = "Disabled background";
            public const string PressedBackground = "Pressed background";
            public const string HintTextColor = "Hint text color";
            public const string HintTextColorDisabled = "Hint text color disabled";
            public const string Icon = "Icon";
            public const string FocusedColor = "Focused color";
            public const string PopulatedUnderlineColor = "Populated underline color";
            public const string ValidationRules = "Validation rules";
            public const string NormalIconColor = "Normal icon color";
            public const string NormalUnderlineColor = "Normal underline color";
            public const string PopulatedIconColor = "Populated icon color";
            public const string DisabledColor = "Disabled color";
            public const string PressedColor = "Pressed color";
            public const string Size = "Size";
            public const string Color = "Color";
            public const string AlternativeColor = "Alternative color";
            public const string FillColor = "Fill color";
            public const string Shadow = "Shadow";
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
            public const string RippleColor = "Ripple color";
            public const string TitleFont = "Title font";
            public const string TitleColor = "Title color";
            public const string DayTextFont = "Day text font";
            public const string TitleTextSize = "Title text size";
            public const string DayTextSize = "Day text size";
            public const string DayTextColor = "Day text color";
            public const string CurrentDayTextColor = "Current day text color";
            public const string CurrentDayBackgroundColor = "Current day background color";
            public const string ColorDividers = "Divider color";
            public const string CurrentColorDividers = "Current day divider color";
            public const string DayEvenBackgroundColor = "Even day background color";
            public const string WeekStartDay = "Week start day";
            public const string Default = "Default";
            public const string ButtonType = "Button type";
            public const string ShadowColor = "Shadow color";
            public const string ShadowOffsetX = "Shadow offset X";
            public const string ShadowOffsetY = "Shadow offset Y";
            public const string ShadowRadius = "Shadow Blur";
            public const string ShadowOpacity = "Shadow opacity";
            public const string UnfocusedBackgroundColor = "Unfocused background color";
            public const string FocusedBackgroundColor = "Focused background color";
            public const string FocusedIconColor = "Focused icon color";
            public const string UnfocusedIconColor = "Unfocused icon color";
            public const string CircleMenuItems = "Circle menu items";
        };

        public static partial class Sizes
        {
            public static readonly Dictionary<string, float> TextSizeCollection = new Dictionary<string, float>();
            public static readonly Dictionary<string, int> CornerRadiusCollection = new Dictionary<string, int>();
            public static readonly Dictionary<string, int> BorderWidthCollection = new Dictionary<string, int>();
            public static readonly Dictionary<string, int> PaddingsCollection = new Dictionary<string, int>();
            public static readonly Dictionary<string, float> LetterSpacingCollection = new Dictionary<string, float>();

            static Sizes()
            {
                foreach (var val in Enumerable.Range(1, 10))
                {
#if __IOS__
                    var v = val;
#else
                    var v = (float) val / 10;
#endif
                    LetterSpacingCollection.Add(v.ToString(), v);
                }

                foreach (var val in Enumerable.Range(10, 11))
                {
                    TextSizeCollection.Add(val.ToString(), val);
                }

                foreach (var val in Enumerable.Range(0, 11))
                {
                    CornerRadiusCollection.Add(val.ToString(), val);
                }
                foreach (var val in Enumerable.Range(1, 10))
                {
                    BorderWidthCollection.Add(val.ToString(), val);
                }
                foreach (var val in Enumerable.Range(1, 20))
                {
                    PaddingsCollection.Add(val.ToString(), val);
                }
            }
        }

        public static class ThemeTypes
        {
            public static Dictionary<string, EOSThemeEnumeration> ThemeCollection = new Dictionary<string, EOSThemeEnumeration>()
            {
#if __ANDROID__
                { string.Empty, EOSThemeEnumeration.Light },
#endif
                { "Light", EOSThemeEnumeration.Light },
                { "Dark", EOSThemeEnumeration.Dark },
            };
        }

        public static class Buttons
        {
            public const string Simple = "Simple button";
            public const string CTA = "CTA button";
            public const string FullBleed = "Full bleed button";

            public static Dictionary<string, SimpleButtonTypeEnum> SimpleButtonTypeCollection = new Dictionary<string, SimpleButtonTypeEnum>()
            {
#if __ANDROID__
                { string.Empty, SimpleButtonTypeEnum.Simple },
#endif
                { Simple, SimpleButtonTypeEnum.Simple },
                { FullBleed, SimpleButtonTypeEnum.FullBleed },
            };

            public static Dictionary<string, SimpleButtonTypeEnum> CTAButtonTypeCollection = new Dictionary<string, SimpleButtonTypeEnum>()
            {
#if __ANDROID__
                { string.Empty, SimpleButtonTypeEnum.Simple },
#endif
                { CTA, SimpleButtonTypeEnum.Simple  },
                { FullBleed, SimpleButtonTypeEnum.FullBleed  },
            };
        }

        public static class Titles
        {
            public static Dictionary<string, string> TitleCollection = new Dictionary<string, string>()
            {
#if __ANDROID__
                { string.Empty, string.Empty },
#endif
                { "First", "First" },
                { "Second" , "Second" },
                { "Third", "Third" },
            };
        }

        public static class Days
        {
            public static Dictionary<string, WeekStartEnum> DaysCollection = new Dictionary<string, WeekStartEnum>()
            {
#if __ANDROID__
                { string.Empty, WeekStartEnum.Sunday },
#endif
                { "Sunday", WeekStartEnum.Sunday },
                { "Monday", WeekStartEnum.Monday },
            };
        }

        public static class Shadow
        {
            public static readonly Dictionary<string, int> OffsetCollection = new Dictionary<string, int>();
            public static readonly Dictionary<string, int> RadiusCollection = new Dictionary<string, int>();
            public static readonly Dictionary<string, double> OpacityCollection = new Dictionary<string, double>();

            static Shadow()
            {
                foreach (var val in Enumerable.Range(0, 10))
                {
                    var v = val * 0.1 + 0.1;
                    OpacityCollection.Add(v.ToString(), v);
                }

                foreach (var val in Enumerable.Range(-14, 29))
                    OffsetCollection.Add(val.ToString(), val);

                foreach (var val in Enumerable.Range(1, 15))
                    RadiusCollection.Add(val.ToString(), val);
            }
        }
    
        public static class CircleMenuSource
        {
            public static Dictionary<string, int> SourceCollection = new Dictionary<string, int>()
            {
#if __ANDROID__
                { string.Empty, 9 },
#endif
                { "3", 3 },
                { "4", 4 },
                { "9", 9 }
            };
        }
    }


    public static class ControlNames
    {
        public const string MainTitle = "EOS Sandbox";
        public const string BadgeLabel = "Badge label";
        public const string GhostButton = "Ghost button";
        public const string SimpleButton = "Simple button";
        public const string SimpleLabel = "Simple label";
        public const string Input = "Input";
        public const string FabProgress = "Fab progress";
        public const string CircleProgress = "Circle progress";
        public const string Section = "Section";
        public const string CTAButton = "CTA Button";
        public const string WorkTimeCalendar = "Work time calendar";
        public const string CircleMenu = "Circle menu";
        public const string CircleMenuItem = "Circle menu item";
    }

    public enum SimpleButtonTypeEnum
    {
        Simple,
        FullBleed
    }
}
