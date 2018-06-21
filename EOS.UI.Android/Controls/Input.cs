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
    public class Input: EditText, IEOSThemeControl, View.IOnFocusChangeListener, View.IOnTouchListener
    {
        #region fields

        private const int IconWidth = 24;
        private Drawable _notValidImage;
        private Drawable _closeImage;
        private Color _notValidColor;
        private Color _clearColor;

        #endregion

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
                {
                    base.Enabled = value;
                    UpdateEnabledState(value);
                }
            }
        }

        private Color _focusedColor;
        public Color FocusedColor
        {
            get => _focusedColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _focusedColor = value;
                if(Enabled && FindFocus() == this && IsValid)
                {
                    LeftImage?.Mutate().SetColorFilter(value, PorterDuff.Mode.SrcIn);
                    Background.SetColorFilter(value, PorterDuff.Mode.SrcIn);
                }
            }
        }

        private Color _normalIconColor;
        public Color NormalIconColor
        {
            get => _normalIconColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _normalIconColor = value;
                if(Enabled && FindFocus() != this && !Populated && IsValid)
                    LeftImage?.Mutate().SetColorFilter(value, PorterDuff.Mode.SrcIn);
            }
        }

        private Color _normalUnderlineColor;
        public Color NormalUnderlineColor
        {
            get => _normalUnderlineColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _normalUnderlineColor = value;
                if(Enabled && FindFocus() != this && !Populated && IsValid)
                    Background.SetColorFilter(value, PorterDuff.Mode.SrcIn);
            }
        }

        private Color _populatedUnderlineColor;
        public Color PopulatedUnderlineColor
        {
            get => _populatedUnderlineColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _populatedUnderlineColor = value;
                if(Enabled && FindFocus() != this && Populated && IsValid)
                    Background.SetColorFilter(value, PorterDuff.Mode.SrcIn);
            }
        }

        private Color _populatedIconColor;
        public Color PopulatedIconColor
        {
            get => _populatedIconColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _populatedIconColor = value;
                if(Enabled && FindFocus() != this && Populated && IsValid)
                    LeftImage?.Mutate().SetColorFilter(value, PorterDuff.Mode.SrcIn);
            }
        }

        private Color _disabledColor;
        public Color DisabledColor
        {
            get => _disabledColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _disabledColor = value;
                if(!Enabled)
                {
                    LeftImage?.Mutate().SetColorFilter(value, PorterDuff.Mode.SrcIn);
                    Background.SetColorFilter(value, PorterDuff.Mode.SrcIn);
                }
            }
        }

        private Drawable _leftImage;
        public Drawable LeftImage
        {
            get => _leftImage;
            set
            {
                IsEOSCustomizationIgnored = true;
                _leftImage = value;

                var color = default(Color);
                if(!Enabled)
                    color = DisabledColor;
                else if(FindFocus() == this)
                    color = FocusedColor;
                else
                    color = Populated ? PopulatedIconColor : NormalIconColor;

                _leftImage.SetColorFilter(color, PorterDuff.Mode.SrcIn);
                base.SetCompoundDrawablesWithIntrinsicBounds(_leftImage, null, null, null);
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

        private bool _isValid = true;
        public bool IsValid
        {
            get => _isValid;
            set
            {
                _isValid = value;
                if(value)
                {
                    if(Enabled)
                    {
                        var color = FindFocus() == this ? FocusedColor : Populated ? PopulatedIconColor : NormalIconColor;
                        LeftImage.Mutate().SetColorFilter(color, PorterDuff.Mode.SrcIn);
                        Background.SetColorFilter(color, PorterDuff.Mode.SrcIn);
                        base.SetCompoundDrawablesWithIntrinsicBounds(LeftImage, null, FindFocus() == this && Populated ?_closeImage : null, null);
                    }
                    else
                    {
                        LeftImage.Mutate().SetColorFilter(DisabledColor, PorterDuff.Mode.SrcIn);
                        Background.SetColorFilter(DisabledColor, PorterDuff.Mode.SrcIn);
                        base.SetCompoundDrawablesWithIntrinsicBounds(LeftImage, null, null, null);
                    }
                }
                else
                {
                    LeftImage.Mutate().SetColorFilter(_notValidColor, PorterDuff.Mode.SrcIn);
                    Background.SetColorFilter(_notValidColor, PorterDuff.Mode.SrcIn);
                    base.SetCompoundDrawablesWithIntrinsicBounds(LeftImage, null, FindFocus() == this && Populated ? _closeImage : _notValidImage, null);
                }
            }
        }

        private bool Populated
        {
            get
            {
                try
                {
                    if(Text == null)
                        return false;
                    else
                        return !string.IsNullOrWhiteSpace(Text);
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            SetOnTouchListener(this);

            _notValidImage = Context.Resources.GetDrawable(Resource.Drawable.WarningIcon);
            _closeImage = Context.Resources.GetDrawable(Resource.Drawable.CloseIcon);

            AfterTextChanged += (s, e) =>
            {
                if(!string.IsNullOrEmpty(Text))
                    base.SetCompoundDrawablesWithIntrinsicBounds(LeftImage, null, _closeImage, null);
                else
                    base.SetCompoundDrawablesWithIntrinsicBounds(LeftImage, null, null, null);
            };

            SetHorizontallyScrolling(true);
            SetLines(1);
            Ellipsize = A.Text.TextUtils.TruncateAt.End;

            var denisty = Resources.DisplayMetrics.Density;
            CompoundDrawablePadding = (int)(10 * denisty);
            SetPaddingRelative(0, (int)(14 * denisty), 0, (int)(14 * denisty));

            OnFocusChangeListener = this;
            if(attrs != null)
                InitializeAttributes(attrs);
        }

        private void InitializeAttributes(IAttributeSet attrs)
        {
            var styledAttributes = Context.ObtainStyledAttributes(attrs, Resource.Styleable.Input, 0, 0);

            var colorFocused = styledAttributes.GetColor(Resource.Styleable.Input_eos_color_focused, Color.Transparent);
            if(colorFocused != Color.Transparent)
                FocusedColor = colorFocused;

            var colorDisabled = styledAttributes.GetColor(Resource.Styleable.Input_eos_color_disabled, Color.Transparent);
            if(colorDisabled != Color.Transparent)
                DisabledColor = colorDisabled;

            var underLineColorNormal = styledAttributes.GetColor(Resource.Styleable.Input_eos_underlinecolor_normal, Color.Transparent);
            if(underLineColorNormal != Color.Transparent)
                NormalUnderlineColor = underLineColorNormal;

            var underLineColorPopulated = styledAttributes.GetColor(Resource.Styleable.Input_eos_underlinecolor_populated, Color.Transparent);
            if(underLineColorPopulated != Color.Transparent)
                PopulatedUnderlineColor = underLineColorPopulated;

            var iconColorNormal = styledAttributes.GetColor(Resource.Styleable.Input_eos_iconcolor_normal, Color.Transparent);
            if(iconColorNormal != Color.Transparent)
                NormalIconColor = iconColorNormal;

            var iconColorPopulated = styledAttributes.GetColor(Resource.Styleable.Input_eos_iconcolor_populated, Color.Transparent);
            if(iconColorNormal != Color.Transparent)
                PopulatedIconColor = iconColorPopulated;

            var textColor = styledAttributes.GetColor(Resource.Styleable.Input_eos_textcolor, Color.Transparent);
            if(textColor != Color.Transparent)
                TextColor = textColor;

            var disabledTextColor = styledAttributes.GetColor(Resource.Styleable.Input_eos_textcolor_disabled, Color.Transparent);
            if(disabledTextColor != Color.Transparent)
                TextColorDisabled = disabledTextColor;

            var hintTextColor = styledAttributes.GetColor(Resource.Styleable.Input_eos_hintcolor, Color.Transparent);
            if(hintTextColor != Color.Transparent)
                HintTextColor = hintTextColor;

            var disabledHintTextColor = styledAttributes.GetColor(Resource.Styleable.Input_eos_hintcolor_disabled, Color.Transparent);
            if(disabledHintTextColor != Color.Transparent)
                HintTextColorDisabled = disabledHintTextColor;

            var imageFocused = styledAttributes.GetDrawable(Resource.Styleable.Input_eos_leftimage);
            if(imageFocused != null)
                LeftImage = imageFocused;

            var font = styledAttributes.GetString(Resource.Styleable.Input_eos_font);
            if(!string.IsNullOrEmpty(font))
                Typeface = Typeface.CreateFromAsset(Context.Assets, font);

            var letterSpacing = styledAttributes.GetFloat(Resource.Styleable.Input_eos_letterspacing, -1);
            if(letterSpacing > 0)
                LetterSpacing = letterSpacing;

            var textSize = styledAttributes.GetFloat(Resource.Styleable.Input_eos_textsize, -1);
            if(textSize > 0)
                TextSize = textSize;

            var enabled = styledAttributes.GetBoolean(Resource.Styleable.Input_eos_enabled, true);
            if(!enabled)
                Enabled = enabled;
        }

        private void UpdateEnabledState(bool enabled)
        {
            base.SetTextColor(enabled ? TextColor : TextColorDisabled);
            base.SetHintTextColor(enabled ? HintTextColor : HintTextColorDisabled);

            if(enabled)
            {
                IsValid = IsValid;
            }
            else
            {
                Background.SetColorFilter(DisabledColor, PorterDuff.Mode.SrcIn);
                LeftImage.SetColorFilter(DisabledColor, PorterDuff.Mode.SrcIn);
                base.SetCompoundDrawablesWithIntrinsicBounds(LeftImage, null, null, null);
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
                _notValidColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.SemanticErrorColor);
                _notValidImage.Mutate().SetColorFilter(_notValidColor, PorterDuff.Mode.SrcIn);
                _clearColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                _closeImage.Mutate().SetColorFilter(_clearColor, PorterDuff.Mode.SrcIn);

                Typeface = Typeface.CreateFromAsset(Context.Assets, GetThemeProvider().GetEOSProperty<string>(this, EOSConstants.Font));
                LetterSpacing = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.LetterSpacing);
                TextSize = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.TextSize);
                TextColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor1);
                TextColorDisabled = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                HintTextColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor2);
                HintTextColorDisabled = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                FocusedColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                DisabledColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                NormalIconColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor2);
                NormalUnderlineColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                PopulatedIconColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                PopulatedUnderlineColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                LeftImage = Context.Resources.GetDrawable(GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.LeftImage));
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

        #region listeners implementation

        public void OnFocusChange(View v, bool hasFocus)
        {
            if(hasFocus)
                SetSelection(string.IsNullOrEmpty(Text) ? 0 : Text.Length - 1);

            var iconColor = hasFocus ? FocusedColor : Populated ? PopulatedIconColor : NormalIconColor;
            LeftImage.Mutate().SetColorFilter(IsValid ?  iconColor : _notValidColor, PorterDuff.Mode.SrcIn);

            var underlineColor = hasFocus ? FocusedColor : Populated ? PopulatedUnderlineColor : NormalIconColor;
            Background.SetColorFilter(IsValid ? underlineColor : _notValidColor, PorterDuff.Mode.SrcIn);

            if(hasFocus)
                base.SetCompoundDrawablesWithIntrinsicBounds(LeftImage, null, !string.IsNullOrEmpty(Text)? _closeImage : null, null);
            else
                base.SetCompoundDrawablesWithIntrinsicBounds(LeftImage, null, !IsValid ? _notValidImage : null, null);
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            if(FindFocus() == this && !string.IsNullOrEmpty(Text))
            {
                if(Width - e.GetX() <= IconWidth)
                    Text = string.Empty;
            }

            return false;
        }

        #endregion
    }
}
