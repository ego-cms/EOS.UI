using System;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Sandbox.Controls
{
    [Register("EOSSandboxNavigationBar")]
    public class EOSSandboxNavigationBar: UINavigationBar, IEOSThemeControl
    {
        #region constructors

        public EOSSandboxNavigationBar(IntPtr handle) : base(handle)
        {
            UpdateAppearance();
        }

        #endregion

        #region IEOSThemeControl implementation

        public bool IsEOSCustomizationIgnored { get; set; }

        public IEOSStyle GetCurrentEOSStyle()
        {
            return null;
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
                BarTintColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6);
                TintColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                TitleTextAttributes = new UIStringAttributes()
                {
                    ForegroundColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor1)
                };
            }
        }

        #endregion
    }
}