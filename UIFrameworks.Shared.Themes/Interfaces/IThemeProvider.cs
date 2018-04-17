using UIFrameworks.Shared.Themes.Helpers;

namespace UIFrameworks.Shared.Themes.Interfaces
{
    public interface IThemeProvider
    {
        ITheme GetCurrentTheme();
        void SetCurrentTheme(ThemeEnumeration themeEnumeration);
    }
}
