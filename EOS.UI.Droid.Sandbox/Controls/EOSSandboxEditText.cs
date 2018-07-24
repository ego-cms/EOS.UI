using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using EOS.UI.Droid.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;

namespace EOS.UI.Droid.Sandbox.Controls
{
    public class EOSSandboxEditText : EditText, IEOSThemeControl
    {
        #region constructors

        public EOSSandboxEditText(Context context) : base(context)
        {
        }

        public EOSSandboxEditText(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public EOSSandboxEditText(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public EOSSandboxEditText(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected EOSSandboxEditText(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        #endregion

        #region IEOSThemeControl implementation

        public bool IsEOSCustomizationIgnored { get; private set; }

        public IEOSThemeProvider GetThemeProvider()
        {
            return EOSThemeProvider.Instance;
        }

        public void UpdateAppearance()
        {
            if (!IsEOSCustomizationIgnored)
            {
                SetTextColor(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor1));
            }
        }

        public void ResetCustomization()
        {
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public IEOSStyle GetCurrentEOSStyle()
        {
            return null;
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {

        }

        #endregion
    }
}
