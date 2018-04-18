using EOS.UI.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Helpers;

namespace UIFrameworks.Shared.Themes.Interfaces
{
    public interface IEOSThemeControl
    {
        IEOSThemeProvider GetThemeProvider();
        void UpdateAppearance();
        bool IsEOSCustomizationIgnored { get; }
        void ResetCustomization();
        IEOSStyle GetCurrentEOSStyle();
        IEOSStyle SetEOSStyle(EOSStyleEnumeration style);
    }
}
