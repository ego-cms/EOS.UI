using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Components
{
    internal class EOSWorkTimeContainer : LinearLayout, IEOSThemeControl, ISelectable
    {
        #region constructors

        public EOSWorkTimeContainer(Context context) : base(context)
        {
            Initialize();
        }

        public EOSWorkTimeContainer(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public EOSWorkTimeContainer(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(attrs);
        }

        public EOSWorkTimeContainer(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(attrs);
        }

        protected EOSWorkTimeContainer(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        #endregion

        #region customization

        public bool IsEven { get; set; }

        private Color _currentDayBackgroundColor;
        public Color CurrentDayBackgroundColor
        {
            get
            {
                if(_currentDayBackgroundColor == default(Color))
                {
                    _currentDayBackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                    if(IsSelected)
                        Background = CreateGradientDrawable(_currentDayBackgroundColor);
                }
                return _currentDayBackgroundColor;
            }
            set
            {
                _currentDayBackgroundColor = value;
                IsEOSCustomizationIgnored = true;
                if(IsSelected)
                    Background = CreateGradientDrawable(value);
            }
        }

        private Color _dayEvenBackgroundColor;
        public Color DayEvenBackgroundColor
        {
            get
            {
                if(_dayEvenBackgroundColor == default(Color))
                {
                    _dayEvenBackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor4);
                    if(!IsSelected)
                        Background = CreateGradientDrawable(!IsEven ? _dayEvenBackgroundColor : Color.Transparent);
                }
                return _dayEvenBackgroundColor;
            }
            set
            {
                _dayEvenBackgroundColor = value;
                IsEOSCustomizationIgnored = true;
                if(!IsSelected)
                    Background = CreateGradientDrawable(!IsEven ? value : Color.Transparent);
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

        private GradientDrawable CreateGradientDrawable(Color color)
        {
            var drawable = new GradientDrawable();
            drawable.SetShape(ShapeType.Rectangle);
            drawable.SetColor(color);
            drawable.SetCornerRadius(7f);
            return drawable;
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
                if(IsSelected)
                    Background = CreateGradientDrawable(CurrentDayBackgroundColor);
                else
                    Background = CreateGradientDrawable(!IsEven ? DayEvenBackgroundColor : Color.Transparent);
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
                CurrentDayBackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                DayEvenBackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor4);
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
