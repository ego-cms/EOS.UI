using System;
using System.Collections.Generic;
using System.Text;

namespace Theme
{
    public interface IThemeControl
    {
        IThemeProvider GetThemeProvider();
        void UpdateAppearance();
        bool IsThemeIgnored { get; }
        void ResetCustomization();
    }
}
