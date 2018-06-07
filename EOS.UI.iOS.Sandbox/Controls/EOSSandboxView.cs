using System;
using EOS.UI.iOS.Sandbox.Styles;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Sandbox.Controls
{
    [Register("EOSSandboxView")]
    public class EOSSandboxView: UIView, IEOSThemeControl
    {
        #region constructors

        public EOSSandboxView(IntPtr handle) : base(handle)
        {
        }

        #endregion

        #region IEOSThemeControl implementation

        public bool IsEOSCustomizationIgnored { get; set; }

        public IEOSStyle GetCurrentEOSStyle()
        {
            return EOSSandboxStyleProvider.Instance.Style;
        }

        public IEOSThemeProvider GetThemeProvider()
        {
            return EOSThemeProvider.Instance;
        }

        public void ResetCustomization()
        {
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {
        }

        public void UpdateAppearance()
        {
            if(!IsEOSCustomizationIgnored)
            {
                BackgroundColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6);
            }
        }

        #endregion
    }
}