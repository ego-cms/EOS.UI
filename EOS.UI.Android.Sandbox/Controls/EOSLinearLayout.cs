using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using EOS.UI.Android.Sandbox.Styles;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Sandbox.Controls
{
    public class EOSLinearLayout : LinearLayout, IEOSThemeControl
    {
        #region constructors

        public EOSLinearLayout(Context context) : base(context)
        {
        }

        public EOSLinearLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public EOSLinearLayout(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public EOSLinearLayout(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected EOSLinearLayout(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
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
            if(!IsEOSCustomizationIgnored)
            {
                (Background as ColorDrawable).Color = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6);
            }
        }

        public void ResetCustomization()
        {
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public IEOSStyle GetCurrentEOSStyle()
        {
            return EOSSendboxStyleProvider.Instance.Style;
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {
            switch(style)
            {
                case EOSStyleEnumeration.SendboxLight:
                    EOSSendboxStyleProvider.Instance.Style = new EOSSendboxLightStyle();
                    break;
                case EOSStyleEnumeration.SendboxDark:
                    EOSSendboxStyleProvider.Instance.Style = new EOSSendboxDarkStyle();
                    break;
            }
        }

        #endregion
    }
}