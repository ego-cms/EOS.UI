using EOS.UI.Shared.Themes.Helpers;

namespace UIFrameworks.Shared.Themes.Interfaces
{
    public interface IEOSThemeProvider
    {
        IEOSTheme GetCurrentTheme();
        void SetCurrentTheme(EOSThemeEnumeration themeEnumeration);
        T GetEOSProperty<T>(IEOSThemeControl control, string propertyName);
        T GetEOSProperty<T>(string propertyName);
    }
}
