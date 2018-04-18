using System;
using EOS.UI.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace UIFrameworks.Android.Themes
{
    public class EOSThemeProvider: IEOSThemeProvider
    {
        private IEOSTheme _theme;

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
            if (currentStyle == null)
            {
                return (T)currentStyle.ThemeValues[propertyName];
            }
            else
            {
                return (T)_theme.ThemeValues[propertyName];
            }
        }
    }
}