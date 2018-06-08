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
using TextUtils = Android.Text.TextUtils;

namespace EOS.UI.Android.Controls
{
    public class GhostButton : Button, IEOSThemeControl, View.IOnTouchListener
    {
        public bool IsEOSCustomizationIgnored { get; private set; }

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

        public override Typeface Typeface
        {
            get => base.Typeface;
            set
            {
                base.Typeface = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public override float LetterSpacing
        {
            get => base.LetterSpacing;
            set
            {
                base.LetterSpacing = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public override float TextSize
        {
            get => base.TextSize;
            set
            {
                base.TextSize = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _enabledTextColor;
        public Color EnabledTextColor
        {
            get => _enabledTextColor;
            set
            {
                _enabledTextColor = value;
                if(Enabled)
                    base.SetTextColor(value);
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _disabledTextColor;
        public Color DisabledTextColor
        {
            get => _disabledTextColor;
            set
            {
                _disabledTextColor = value;
                if(!Enabled)
                    base.SetTextColor(value);
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _pressedStateTextColor;
        public Color PressedStateTextColor
        {
            get => _pressedStateTextColor;
            set
            {
                _pressedStateTextColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public GhostButton(Context context) : base(context)
        {
            Initialize();
        }

        public GhostButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public GhostButton(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(attrs);
        }

        public GhostButton(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(attrs);
        }

        protected GhostButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        private void Initialize(IAttributeSet attributeSet = null)
        {
            SetOnTouchListener(this);
            Background = CreateRippleDrawable();

            if(attributeSet != null)
                InitializeAttributes(attributeSet);

            UpdateAppearance();
            SetLines(1);
            Ellipsize = TextUtils.TruncateAt.End;
        }

        private void InitializeAttributes(IAttributeSet attrs)
        {
            var styledAttributes = Context.ObtainStyledAttributes(attrs, Resource.Styleable.GhostButton, 0, 0);

            var font = styledAttributes.GetString(Resource.Styleable.GhostButton_eos_font);
            if(!string.IsNullOrEmpty(font))
                Typeface = Typeface.CreateFromAsset(Context.Assets, font);

            var letterSpacing = styledAttributes.GetFloat(Resource.Styleable.GhostButton_eos_letterspacing, -1);
            if(letterSpacing > 0)
                LetterSpacing = letterSpacing;

            var textColor = styledAttributes.GetColor(Resource.Styleable.GhostButton_eos_textcolor, Color.Transparent);
            if(textColor != Color.Transparent)
                EnabledTextColor = textColor;

            var disabledTextColor = styledAttributes.GetColor(Resource.Styleable.GhostButton_eos_textcolor_disabled, Color.Transparent);
            if(disabledTextColor != Color.Transparent)
                DisabledTextColor = disabledTextColor;

            var pressedTextColor = styledAttributes.GetColor(Resource.Styleable.GhostButton_eos_textcolor_pressed, Color.Transparent);
            if(pressedTextColor != Color.Transparent)
                PressedStateTextColor = pressedTextColor;

            var textSize = styledAttributes.GetFloat(Resource.Styleable.GhostButton_eos_textsize, -1);
            if(textSize > 0)
                TextSize = textSize;

            var enabled = styledAttributes.GetBoolean(Resource.Styleable.GhostButton_eos_enabled, true);
            if(!enabled)
                Enabled = enabled;
        }

        private void UpdateEnabledState(bool enabled)
        {
            base.SetTextColor(enabled ? EnabledTextColor : DisabledTextColor);
        }

        private Drawable CreateRippleDrawable()
        {
            return new RippleDrawable(new ColorStateList(
                new int[][]
                {
                    new int[] { },
                },
                new int[]
                {
                    _pressedStateTextColor
                }), 
                new ColorDrawable(Color.Transparent), new ColorDrawable(Color.White));
        }

        public override void SetTypeface(Typeface tf, [GeneratedEnum] TypefaceStyle style)
        {
            base.SetTypeface(tf, style);
            IsEOSCustomizationIgnored = true;
        }

        public IEOSStyle GetCurrentEOSStyle()
        {
            return null;
        }

        public IEOSThemeProvider GetThemeProvider()
        {
            return EOSThemeProvider.Instance;
        }

        public void ResetCustomization()
        {
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {

        }

        public void UpdateAppearance()
        {
            if (!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                base.SetTypeface(Typeface.CreateFromAsset(Context.Assets, provider.GetEOSProperty<string>(this, EOSConstants.Font)), TypefaceStyle.Normal);
                base.LetterSpacing = provider.GetEOSProperty<float>(this, EOSConstants.LetterSpacing);
                EnabledTextColor = provider.GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                DisabledTextColor = provider.GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                PressedStateTextColor = provider.GetEOSProperty<Color>(this, EOSConstants.NeutralColor6);
                base.TextSize = provider.GetEOSProperty<float>(this, EOSConstants.TextSize);
                IsEOSCustomizationIgnored = false;
            }
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            if(Enabled)
            {
                if(e.Action == MotionEventActions.Down)
                    base.SetTextColor(PressedStateTextColor);
                if(e.Action == MotionEventActions.Up || e.Action == MotionEventActions.Cancel)
                    base.SetTextColor(EnabledTextColor);
            }
            return false;
        }
    }
}
