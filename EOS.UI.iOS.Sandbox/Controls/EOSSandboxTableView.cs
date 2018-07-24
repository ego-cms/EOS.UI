using System;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Sandbox.Controls
{
    [Register("EOSSandboxTableView")]
    public class EOSSandboxTableView: UITableView, IEOSThemeControl
    {
        #region constructors

        public EOSSandboxTableView(IntPtr handle) : base(handle)
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
                BackgroundColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6);
                (Source as IEOSThemeControl)?.UpdateAppearance();
                ReloadData();
            }
        }

        #endregion
    }
}