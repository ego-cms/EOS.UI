namespace UIFrameworks.Shared.Themes.Interfaces
{
    public interface IThemeControl
    {
        IThemeProvider GetThemeProvider();
        void UpdateAppearance();
        bool IsThemeIgnored { get; }
        void ResetCustomization();
    }
}
