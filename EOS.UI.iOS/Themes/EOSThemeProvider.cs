using System;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Themes;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.iOS.Themes
{
    public class EOSThemeProvider : IEOSThemeProvider
    {
        private IEOSTheme _theme = new LightEOSTheme();

        private EOSThemeProvider()
        {
        }

        static Lazy<EOSThemeProvider> _instance = new Lazy<EOSThemeProvider>(() => new EOSThemeProvider());

        public static EOSThemeProvider Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public IEOSTheme GetCurrentTheme()
        {
            return _theme;
        }

        public void SetCurrentTheme(EOSThemeEnumeration themeEnumeration)
        {
            switch (themeEnumeration)
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
            if(control == null)
                throw new ArgumentNullException(nameof(control));
            
            var currentStyle = control.GetCurrentEOSStyle();
            Object val = null;
            currentStyle?.ThemeValues?.TryGetValue(propertyName, out val);
            return (T)(val ?? _theme.ThemeValues[propertyName]);
        }

        public T GetEOSProperty<T>(string propertyName)
        {
            return (T)_theme.ThemeValues[propertyName];
        }
    }
}
