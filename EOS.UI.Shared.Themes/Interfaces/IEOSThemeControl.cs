using EOS.UI.Shared.Themes.Helpers;

namespace EOS.UI.Shared.Themes.Interfaces
{
    public interface IEOSThemeControl
    {
        IEOSThemeProvider GetThemeProvider();
        void UpdateAppearance();
        bool IsEOSCustomizationIgnored { get; }
        void ResetCustomization();
        IEOSStyle GetCurrentEOSStyle();
        void SetEOSStyle(EOSStyleEnumeration style);
    }
}
