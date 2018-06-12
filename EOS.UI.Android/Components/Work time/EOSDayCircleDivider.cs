using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Components
{
    internal class EOSDayCircleDivider : View, IEOSThemeControl, ISelectable
    {
        #region constructors

        public EOSDayCircleDivider(Context context) : base(context)
        {
            Initialize();
        }

        public EOSDayCircleDivider(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public EOSDayCircleDivider(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(attrs);
        }

        public EOSDayCircleDivider(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(attrs);
        }

        protected EOSDayCircleDivider(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        #endregion

        #region customization

        private Color _dividerColor;
        public Color DividerColor
        {
            get => _dividerColor;
            set
            {
                _dividerColor = value;
                IsEOSCustomizationIgnored = true;
                if(!IsSelected)
                    (Background as VectorDrawable).SetColorFilter(value, PorterDuff.Mode.SrcIn);
            }
        }

        private Color _currentDividerColor;
        public Color CurrentDividerColor
        {
            get => _currentDividerColor;
            set
            {
                _currentDividerColor = value;
                IsEOSCustomizationIgnored = true;
                if(IsSelected)
                    (Background as VectorDrawable).SetColorFilter(value, PorterDuff.Mode.SrcIn);
            }
        }

        #endregion

        #region utility method

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

        #region ISelectable implementation

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                (Background as VectorDrawable).SetColorFilter(_isSelected ?  DividerColor : CurrentDividerColor, PorterDuff.Mode.SrcIn);
            }
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
                DividerColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                CurrentDividerColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6);
                IsEOSCustomizationIgnored = false;
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
