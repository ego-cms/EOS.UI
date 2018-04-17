namespace UIFrameworks.Shared.Themes.Interfaces
{
    public interface IEOSThemeControl
    {
        IEOSThemeProvider GetThemeProvider();
        void UpdateAppearance();
        bool IsThemeIgnored { get; }
        void ResetCustomization();
    }
}
