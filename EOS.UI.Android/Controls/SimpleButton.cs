using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using R = Android.Resource;

namespace EOS.UI.Android.Controls
{
    public class SimpleButton: Button, IEOSThemeControl, View.IOnTouchListener
    {
        #region constructors

        public SimpleButton(Context context) : base(context)
        {
            Initialize();
        }

        public SimpleButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public SimpleButton(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Initialize(attrs);
        }

        public SimpleButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        #endregion

        #region customization

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                if(Enabled != value)
                    UpdateEnabledState(value);
                base.Enabled = value;
            }
        }

        private Color _enabledBackgroundColor;
        public Color EnabledBackgroundColor
        {
            get => _enabledBackgroundColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _enabledBackgroundColor = value;
                if(Enabled)
                    Background = CreateRippleDrawable(EnabledBackgroundColor);
            }
        }

        private Color _disabledBackgroundColor;
        public Color DisabledBackgroundColor
        {
            get => _disabledBackgroundColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _disabledBackgroundColor = value;
                if(!Enabled)
                    Background = new ColorDrawable(_disabledBackgroundColor);
            }
        }

        private Color _pressedBackgroundColor;
        public Color PressedBackgroundColor
        {
            get => _pressedBackgroundColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _pressedBackgroundColor = value;
                if(Enabled)
                    Background = CreateRippleDrawable(EnabledBackgroundColor);
            }
        }

        public override Typeface Typeface
        {
            get => base.Typeface;
            set
            {
                IsEOSCustomizationIgnored = true;
                base.Typeface = value;
            }
        }

        public override float LetterSpacing
        {
            get => base.LetterSpacing;
            set
            {
                IsEOSCustomizationIgnored = true;
                base.LetterSpacing = value;
            }
        }

        public override float TextSize
        {
            get => base.TextSize;
            set
            {
                IsEOSCustomizationIgnored = true;
                base.TextSize = value;
            }
        }

        private Color _enabledTextColor;
        public Color EnabledTextColor
        {
            get => _enabledTextColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _enabledTextColor = value;
                if(Enabled)
                    base.SetTextColor(value);
            }
        }

        private Color _disabledTextColor;
        public Color DisabledTextColor
        {
            get => _disabledTextColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _disabledTextColor = value;
                if(!Enabled)
                    base.SetTextColor(value);
            }
        }

        private Color _pressedTextColor;
        public Color PressedTextColor
        {
            get => _pressedTextColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _pressedTextColor = value;
            }
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            SetOnTouchListener(this);
            Background = CreateRippleDrawable(EnabledBackgroundColor);
            UpdateAppearance();
            if(attrs != null)
                InitializeAttributes(attrs);
        }

        private void InitializeAttributes(IAttributeSet attrs)
        {
            //TODO: Implement set attrs logic
        }

        private Drawable CreateRippleDrawable(Color contentColor)
        {
            return new RippleDrawable(
                CreateBackgroundColorStateList(),
                new ColorDrawable(contentColor),
                new ColorDrawable(Color.Gray));
        }

        private ColorStateList CreateBackgroundColorStateList()
        {
            return new ColorStateList(
               new int[][] { new int[] { } },
               new int[]
               {
                   PressedBackgroundColor,
               });
        }

        private void UpdateEnabledState(bool enabled)
        {
            base.SetTextColor(enabled ? EnabledTextColor : DisabledTextColor);
            Background = enabled ? CreateRippleDrawable(EnabledBackgroundColor) : new ColorDrawable(DisabledBackgroundColor);
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
                base.SetTypeface(Typeface.CreateFromAsset(Context.Assets, GetThemeProvider().GetEOSProperty<string>(this, EOSConstants.Font)), TypefaceStyle.Normal);
                base.LetterSpacing = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.LetterSpacing);
                base.TextSize = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.TextSize);
                EnabledTextColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.SecondaryColor);
                DisabledTextColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.SecondaryColorDisabled);
                PressedTextColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.SecondaryColorPressed);
                EnabledBackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.PrimaryColor);
                DisabledBackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.PrimaryColorDisabled);
                PressedBackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.PrimaryColorPressed);
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

        #region IOnTouchListener implementation

        public bool OnTouch(View v, MotionEvent e)
        {
            if(Enabled)
            {
                if(e.Action == MotionEventActions.Down)
                    base.SetTextColor(PressedTextColor);
                if(e.Action == MotionEventActions.Up || e.Action == MotionEventActions.Cancel)
                    base.SetTextColor(EnabledTextColor);
            }
            return false;
        }

        #endregion
    }
}