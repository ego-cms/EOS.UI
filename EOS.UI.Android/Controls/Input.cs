using System;
using Android.Content;
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
using A = Android;

namespace EOS.UI.Android.Controls
{
    public class Input: EditText, IEOSThemeControl, View.IOnFocusChangeListener
    {
        #region constructors

        public Input(Context context) : base(context)
        {
            Initialize();
        }

        public Input(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public Input(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Initialize(attrs);
        }

        public Input(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
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

        private Color _underlineColorFocused;
        public Color UnderlineColorFocused
        {
            get => _underlineColorFocused;
            set
            {
                IsEOSCustomizationIgnored = true;
                _underlineColorFocused = value;
                if(Enabled && FindFocus() == this)
                    Background.SetColorFilter(value, PorterDuff.Mode.SrcIn);
            }
        }

        private Color _underlineColorUnfocused;
        public Color UnderlineColorUnfocused
        {
            get => _underlineColorUnfocused;
            set
            {
                IsEOSCustomizationIgnored = true;
                _underlineColorUnfocused = value;
                if(Enabled && FindFocus() != this)
                    Background.SetColorFilter(value, PorterDuff.Mode.SrcIn);
            }
        }

        private Color _underlineColorDisabled;
        public Color UnderlineColorDisabled
        {
            get => _underlineColorDisabled;
            set
            {
                IsEOSCustomizationIgnored = true;
                _underlineColorDisabled = value;
                if(!Enabled)
                    Background.SetColorFilter(value, PorterDuff.Mode.SrcIn);
            }
        }

        private Drawable _leftImageUnfocused;
        public Drawable LeftImageUnfocused
        {
            get => _leftImageUnfocused;
            set
            {
                IsEOSCustomizationIgnored = true;
                _leftImageUnfocused = value;
                if(Enabled && FindFocus() != this)
                    base.SetCompoundDrawablesWithIntrinsicBounds(_leftImageUnfocused, null, null, null);
            }
        }

        private Drawable _leftImageFocused;
        public Drawable LeftImageFocused
        {
            get => _leftImageFocused;
            set
            {
                IsEOSCustomizationIgnored = true;
                _leftImageFocused = value;
                if(Enabled && FindFocus() == this)
                    base.SetCompoundDrawablesWithIntrinsicBounds(_leftImageFocused, null, null, null);
            }
        }

        private Drawable _leftImageDisabled;
        public Drawable LeftImageDisabled
        {
            get => _leftImageDisabled;
            set
            {
                IsEOSCustomizationIgnored = true;
                _leftImageDisabled = value;
                if(!Enabled)
                    base.SetCompoundDrawablesWithIntrinsicBounds(_leftImageDisabled, null, null, null);
            }
        }

        public override void SetCompoundDrawables(Drawable left, Drawable top, Drawable right, Drawable bottom)
        {
            if(left != null)
                IsEOSCustomizationIgnored = true;

            base.SetCompoundDrawables(left, top, right, bottom);
        }

        private Color _hintTextColor;
        public Color HintTextColor
        {
            get => _hintTextColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _hintTextColor = value;
                if(Enabled)
                    base.SetHintTextColor(value);
            }
        }

        private Color _hintTextColorDisabled;
        public Color HintTextColorDisabled
        {
            get => _hintTextColorDisabled;
            set
            {
                IsEOSCustomizationIgnored = true;
                _hintTextColorDisabled = value;
                if(!Enabled)
                    base.SetHintTextColor(value);
            }
        }

        public new void SetHintTextColor(Color color)
        {
            if(Enabled)
                HintTextColor = color;
            else
                HintTextColorDisabled = color;
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

        public override void SetTypeface(Typeface tf, [GeneratedEnum] TypefaceStyle style)
        {
            IsEOSCustomizationIgnored = true;
            base.SetTypeface(tf, style);
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

        private Color _textColor;
        public Color TextColor
        {
            get => _textColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _textColor = value;
                if(Enabled)
                    base.SetTextColor(value);
            }
        }

        private Color _textColorDisabled;
        public Color TextColorDisabled
        {
            get => _textColorDisabled;
            set
            {
                IsEOSCustomizationIgnored = true;
                _textColorDisabled = value;
                if(!Enabled)
                    base.SetTextColor(value);
            }
        }


        public override void SetTextColor(Color color)
        {
            TextColor = color;
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

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            SetHorizontallyScrolling(true);
            SetLines(1);
            Ellipsize = A.Text.TextUtils.TruncateAt.End;

            var denisty = Resources.DisplayMetrics.Density;
            CompoundDrawablePadding = (int)(10 * denisty);
            SetPaddingRelative(0, (int)(14 * denisty), 0, (int)(14 * denisty));

            OnFocusChangeListener = this;
            if(attrs != null)
                InitializeAttributes(attrs);
            UpdateAppearance();
        }

        private void InitializeAttributes(IAttributeSet attrs)
        {
            //TODO: Implement set attrs logic
        }


        private void UpdateEnabledState(bool enabled)
        {
            base.SetTextColor(enabled ? TextColor : TextColorDisabled);
            base.SetHintTextColor(enabled ? HintTextColor : HintTextColorDisabled);
            if(!enabled)
            {
                base.SetCompoundDrawablesWithIntrinsicBounds(LeftImageDisabled, null, null, null);
                Background.SetColorFilter(UnderlineColorDisabled, PorterDuff.Mode.SrcIn);
            }
            else if(FindFocus() == this)
            {
                base.SetCompoundDrawablesWithIntrinsicBounds(LeftImageFocused, null, null, null);
                Background.SetColorFilter(UnderlineColorFocused, PorterDuff.Mode.SrcIn);
            }
            else
            {
                base.SetCompoundDrawablesWithIntrinsicBounds(LeftImageUnfocused, null, null, null);
                Background.SetColorFilter(UnderlineColorUnfocused, PorterDuff.Mode.SrcIn);
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
                Typeface = Typeface.CreateFromAsset(Context.Assets, GetThemeProvider().GetEOSProperty<string>(this, EOSConstants.Font));
                LetterSpacing = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.LetterSpacing);
                TextSize = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.TextSize);
                TextColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor1);
                TextColorDisabled = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                HintTextColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor2);
                HintTextColorDisabled = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                LeftImageFocused = Context.Resources.GetDrawable(GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.LeftImageFocused));
                LeftImageUnfocused = Context.Resources.GetDrawable(GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.LeftImageUnfocused));
                LeftImageDisabled = Context.Resources.GetDrawable(GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.LeftImageDisabled));
                UnderlineColorFocused = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                UnderlineColorUnfocused = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                UnderlineColorDisabled = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                IsEOSCustomizationIgnored = false;
                UpdateEnabledState(Enabled);
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

        #region IOnFocusChangeListener implementation

        public void OnFocusChange(View v, bool hasFocus)
        {
            if(hasFocus)
                SetSelection(string.IsNullOrEmpty(Text) ? 0 : Text.Length - 1);
            Background.SetColorFilter(hasFocus ? UnderlineColorFocused : UnderlineColorUnfocused, PorterDuff.Mode.SrcIn);
            base.SetCompoundDrawablesWithIntrinsicBounds(hasFocus ? LeftImageFocused : LeftImageUnfocused, null, null, null);
        }

        #endregion
    }
}