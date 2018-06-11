using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Components
{
    internal class EOSTimeLabel : TextView, IEOSThemeControl, ISelectable
    {
        #region constructors

        public EOSTimeLabel(Context context) : base(context)
        {
            Initialize();
        }

        public EOSTimeLabel(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public EOSTimeLabel(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(attrs);
        }

        public EOSTimeLabel(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(attrs);
        }

        protected EOSTimeLabel(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        #endregion

        #region customization

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set
            {
                _isEmpty = value;
                if(value)
                    Text = "00:00";
            }
        }

        private Typeface _dayTextFont;
        public Typeface DayTextFont
        {
            get => _dayTextFont;
            set
            {
                _dayTextFont = value;
                Typeface = value;
                IsEOSCustomizationIgnored = false;
            }
        }

        private int _dayTextSize;
        public int DayTextSize
        {
            get => _dayTextSize;
            set
            {
                _dayTextSize = value;
                TextSize = value;
                IsEOSCustomizationIgnored = false;
            }
        }

        private Color _dayTextColor;
        public Color DayTextColor
        {
            get => _dayTextColor;
            set
            {
                _dayTextColor = value;
                IsEOSCustomizationIgnored = false;

                if(IsEmpty)
                    SetTextColor(Color.Transparent);
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

                if(IsEmpty)
                    SetTextColor(Color.Transparent);
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
                SetTextColor(IsSelected ? CurrentDayTextColor : DayTextColor);
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
