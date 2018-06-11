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

namespace EOS.UI.Android.Components
{
    internal class EOSDayLabel : TextView, IEOSThemeControl, ISelectable
    {
        #region constructors

        public EOSDayLabel(Context context) : base(context)
        {
            Initialize();
        }

        public EOSDayLabel(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public EOSDayLabel(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(attrs);
        }

        public EOSDayLabel(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(attrs);
        }

        protected EOSDayLabel(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        #endregion

        #region customization

        private Typeface _titleFont;
        public Typeface TitleFont
        {
            get => _titleFont;
            set
            {
                _titleFont = value;
                Typeface = value;
                IsEOSCustomizationIgnored = false;
            }
        }

        private int _titleTextSize;
        public int TitleTextSize
        {
            get => _titleTextSize;
            set
            {
                _titleTextSize = value;
                TextSize = value;
                IsEOSCustomizationIgnored = false;
            }
        }

        private Color _titleColor;
        public Color TitleColor
        {
            get => _titleColor;
            set
            {
                _titleColor = value;
                IsEOSCustomizationIgnored = false;
                if(!IsSelected)
                    SetTextColor(value);
            }
        }

        private Color _currentDayTextColor;
        public Color CurrentDayTextColor
        {
            get => _currentDayTextColor;
            set
            {
                _currentDayTextColor = value;
                IsEOSCustomizationIgnored = false;
                if(IsSelected)
                    SetTextColor(value);
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
                SetTextColor(IsSelected ? CurrentDayTextColor : TitleColor);
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
                TitleFont = Typeface.CreateFromAsset(Context.Assets, GetThemeProvider().GetEOSProperty<string>(this, EOSConstants.TitleFont));
                TitleTextSize = GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.TitleTextSize);
                TitleColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6);
                CurrentDayTextColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor1);
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
