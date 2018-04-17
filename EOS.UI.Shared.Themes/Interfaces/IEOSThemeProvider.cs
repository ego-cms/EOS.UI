using UIFrameworks.Shared.Themes.Helpers;

namespace UIFrameworks.Shared.Themes.Interfaces
{
    public interface IEOSThemeProvider
    {
        IEOSTheme GetCurrentTheme();
        void SetCurrentTheme(EOSThemeEnumeration themeEnumeration);
    }
}
