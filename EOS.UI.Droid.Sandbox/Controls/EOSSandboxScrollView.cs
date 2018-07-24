using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using EOS.UI.Droid.Sandbox.Styles;
using EOS.UI.Droid.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;

namespace EOS.UI.Droid.Sandbox.Controls
{
    public class EOSSandboxScrollView : ScrollView, IEOSThemeControl
    {
        #region constructors

        public EOSSandboxScrollView(Context context) : base(context)
        {
        }

        public EOSSandboxScrollView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public EOSSandboxScrollView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public EOSSandboxScrollView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected EOSSandboxScrollView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
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
                if(Background == null)
                    Background = new ColorDrawable(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6));
                else
                    (Background as ColorDrawable).Color =  GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6);
            }
        }

        public void ResetCustomization()
        {
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public IEOSStyle GetCurrentEOSStyle()
        {
            return EOSSandboxStyleProvider.Instance.Style;
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {
        }

        #endregion
    }
}