using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.iOS.Themes
{
    public class EOSThemeProvider : IEOSThemeProvider
    {
        private IEOSTheme _theme;

        internal EOSThemeProvider()
        {
        }

        public IEOSTheme GetCurrentTheme()
        {
            if(_theme == null)
                _theme = new LightEOSTheme();

            return _theme;
        }

        public void SetCurrentTheme(EOSThemeEnumeration themeEnumeration)
        {
            switch(themeEnumeration)
            {
                case EOSThemeEnumeration.Light:
                    _theme = new LightEOSTheme();
                    break;
                case EOSThemeEnumeration.Dark:
                    _theme = new DarkEOSTheme();
                    break;
            }
        }
    }
}
