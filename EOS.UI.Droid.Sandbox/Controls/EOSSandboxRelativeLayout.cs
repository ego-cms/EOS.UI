﻿using System;
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
    public class EOSSandboxRelativeLayout : RelativeLayout, IEOSThemeControl
    {
        #region constructor

        public EOSSandboxRelativeLayout(Context context) : base(context)
        {
        }

        public EOSSandboxRelativeLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public EOSSandboxRelativeLayout(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public EOSSandboxRelativeLayout(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected EOSSandboxRelativeLayout(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
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
                Background = new ColorDrawable(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6));
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
