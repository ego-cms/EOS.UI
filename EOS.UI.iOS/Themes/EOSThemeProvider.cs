using System;
using EOS.UI.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.iOS.Themes
{
    public class EOSThemeProvider : IEOSThemeProvider
    {
        private IEOSTheme _theme;

        private EOSThemeProvider()
        {
        }

        static Lazy<EOSThemeProvider> _instance = new Lazy<EOSThemeProvider>(() => new EOSThemeProvider());

        public static EOSThemeProvider Instance{
            get
            {
                return _instance.Value;
            }
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

        public T GetEOSProperty<T>(IEOSThemeControl control, string propertyName)
        {
            var currentStyle = control.GetCurrentEOSStyle();
            Object value = null;
            if (currentStyle != null)
            {
                currentStyle.ThemeValues.TryGetValue(propertyName, out value);
            }
            else
            {
                _theme.ThemeValues.TryGetValue(propertyName, out value);
            }
            return (T)value;
        }
    }
}
