using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace UIFrameworks.Android.Themes
{
    public class ThemeProvider: IThemeProvider
    {
        private ITheme _theme;

        internal ThemeProvider()
        {

        }

        public ITheme GetCurrentTheme()
        {
            if(_theme == null)
                _theme = new LightTheme();

            return _theme;
        }

        public void SetCurrentTheme(ThemeEnumeration themeEnumeration)
        {
            switch(themeEnumeration)
            {
                case ThemeEnumeration.Light:
                    _theme = new LightTheme();
                    break;
                case ThemeEnumeration.Dark:
                    _theme = new DarkTheme();
                    break;
            }
        }
    }
}