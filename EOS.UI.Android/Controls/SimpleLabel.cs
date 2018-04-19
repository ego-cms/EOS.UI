using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Controls
{
    public class SimpleLabel : TextView, IEOSThemeControl
    {
        #region constructors

        public SimpleLabel(Context context) : base(context)
        {
            Initialize();
        }

        public SimpleLabel(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public SimpleLabel(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Initialize(attrs);
        }

        public SimpleLabel(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        #endregion

        #region public Api

        public void SetCustomFont(Typeface fontTypeFace)
        {
            IsEOSCustomizationIgnored = false;
            SetTypeface(fontTypeFace, TypefaceStyle.Normal);
        }

        public void SetCustomLetterSpacing(float spacing)
        {
            IsEOSCustomizationIgnored = false;
            LetterSpacing = spacing;
        }

        public void SetCustomTextColor(Color color)
        {
            IsEOSCustomizationIgnored = false;
            SetTextColor(color);
        }

        public void SetCustomTextSize(float size)
        {
            IsEOSCustomizationIgnored = false;
            TextSize = size;
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            if(attrs != null)
                InitializeAttributes(attrs);
        }

        private void InitializeAttributes(IAttributeSet attrs)
        {
            //TODO: Implement set attrs logic
        }

        #endregion

        #region IEOSThemeControl implementation

        public bool IsEOSCustomizationIgnored { get; private set; } = true;

        public IEOSThemeProvider GetThemeProvider()
        {
            return EOSThemeProvider.Instance;
        }

        public void UpdateAppearance()
        {
            if(IsEOSCustomizationIgnored)
            {
                SetTypeface(Typeface.CreateFromAsset(Context.Assets, GetThemeProvider().GetEOSProperty<string>(this, EOSConstants.Font)), TypefaceStyle.Normal);
                LetterSpacing = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.LetterSpacing);
                SetTextColor(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.TextColor));
                TextSize = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.TextSize);
            }
        }

        public void ResetCustomization()
        {
            IsEOSCustomizationIgnored = true;
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