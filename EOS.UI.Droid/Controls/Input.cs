using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Util;
using Android.Views;
using EOS.UI.Droid.Themes;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;

namespace EOS.UI.Droid.Controls
{
    public class Input : AppCompatEditText, IEOSThemeControl, View.IOnFocusChangeListener, View.IOnTouchListener
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
                base.Enabled = value;
                UpdateEnabledState();
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
                UpdateIconColor();
                UpdateUnderlineColor();
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
                UpdateIconColor();
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
                UpdateUnderlineColor();
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
                UpdateUnderlineColor();
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
                UpdateIconColor();
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
                UpdateIconColor();
                UpdateUnderlineColor();
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
                UpdateIconColor();
                UpdateRightImage();
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
                SetHintTextColor();
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
                SetDisabledHintTextColor();
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
                base.Typeface = value;
                //Should check FontStyle
                //Set method works with base(context) constructor, which works ahead of FontStyle set
                if (FontStyle != null)
                {
                    IsEOSCustomizationIgnored = true;
                    FontStyle.Typeface = value;
                    SetFontStyle();
                }
            }
        }

        public override void SetTypeface(Typeface tf, [GeneratedEnum] TypefaceStyle style)
        {
            //Should check FontStyle
            //Set method works with base(context) constructor, which works ahead of FontStyle set
            if (FontStyle != null)
            {
                IsEOSCustomizationIgnored = true;
            }
            base.SetTypeface(tf, style);
        }

        public override float LetterSpacing
        {
            get => base.LetterSpacing;
            set
            {
                base.LetterSpacing = value;
                //Should check FontStyle
                //Set method works with base(context) constructor, which works ahead of FontStyle set
                if (FontStyle != null)
                {
                    IsEOSCustomizationIgnored = true;
                    FontStyle.LetterSpacing = value;
                    SetFontStyle();
                }
            }
        }

        public Color TextColor
        {
            get => FontStyle.Color;
            set
            {
                IsEOSCustomizationIgnored = true;
                FontStyle.Color = value;
                SetFontStyle();
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
                SetDisabledTextColor();
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
                //Should check FontStyle
                //Set method works with base(context) constructor, which works ahead of FontStyle set
                if (FontStyle != null)
                {
                    IsEOSCustomizationIgnored = true;
                    FontStyle.Size = value;
                    SetFontStyle();
                }
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
                UpdateIconColor();
                UpdateUnderlineColor();
                UpdateRightImage();
            }
        }

        private FontStyleItem _fontStyle;
        public FontStyleItem FontStyle
        {
            get => _fontStyle;
            set
            {
                _fontStyle = value;
                SetFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        private void SetFontStyle()
        {
            base.Typeface = FontStyle.Typeface;
            base.TextSize = FontStyle.Size;
            if(Enabled)
                base.SetTextColor(FontStyle.Color);
            base.LetterSpacing = FontStyle.LetterSpacing;
        }

        private void SetDisabledTextColor()
        {
            if(!Enabled)
                base.SetTextColor(TextColorDisabled);
        }

        private void SetHintTextColor()
        {
            if(Enabled)
                base.SetHintTextColor(HintTextColor);
        }

        private void SetDisabledHintTextColor()
        {
            if(!Enabled)
                base.SetHintTextColor(HintTextColorDisabled);
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
                base.SetCompoundDrawablesWithIntrinsicBounds(LeftImage, null, !string.IsNullOrEmpty(Text) ? _closeImage : null, null);
            };

            SetHorizontallyScrolling(true);
            SetLines(1);
            SetSingleLine(true);
            Ellipsize = TextUtils.TruncateAt.End;

            var denisty = Resources.DisplayMetrics.Density;
            CompoundDrawablePadding = (int)(10 * denisty);
            SetPaddingRelative(0, (int)(14 * denisty), (int)(6 * denisty), (int)(14 * denisty));

            OnFocusChangeListener = this;
            if(attrs != null)
                InitializeAttributes(attrs);

            Text = string.Empty;
            UpdateAppearance();
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
        }

        private void UpdateEnabledState()
        {
            base.SetTextColor(Enabled ? TextColor : TextColorDisabled);
            base.SetHintTextColor(Enabled ? HintTextColor : HintTextColorDisabled);
            UpdateIconColor();
            UpdateUnderlineColor();
            UpdateRightImage();
        }

        private void UpdateIconColor()
        {
            Color imageColor;
            if(Enabled)
                if(IsValid)
                    if(FindFocus() == this)
                        imageColor = FocusedColor;
                    else
                        imageColor = !string.IsNullOrEmpty(Text) ? PopulatedIconColor : NormalIconColor;
                else
                    imageColor = _notValidColor;
            else
                imageColor = DisabledColor;

            LeftImage?.Mutate().SetColorFilter(imageColor, PorterDuff.Mode.SrcIn);
        }

        private void UpdateUnderlineColor()
        {
            Color underlineColor;
            if(Enabled)
                if(IsValid)
                    if(FindFocus() == this)
                        underlineColor = FocusedColor;
                    else
                        underlineColor = !string.IsNullOrEmpty(Text) ? PopulatedUnderlineColor : NormalUnderlineColor;
                else
                    underlineColor = _notValidColor;
            else
                underlineColor = DisabledColor;

            Background.Mutate().SetColorFilter(underlineColor, PorterDuff.Mode.SrcIn);
        }

        private void UpdateRightImage()
        {
            if(Enabled)
                if(FindFocus() == this)
                    base.SetCompoundDrawablesWithIntrinsicBounds(LeftImage, null, !string.IsNullOrEmpty(Text) ? _closeImage : null, null);
                else
                    base.SetCompoundDrawablesWithIntrinsicBounds(LeftImage, null, !IsValid ? _notValidImage : null, null);
            else
                base.SetCompoundDrawablesWithIntrinsicBounds(LeftImage, null, null, null);
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

                //There are different fontstyles, but the difference only in colors, 
                //so from font style items we only get the right color
                FontStyle = GetThemeProvider().GetEOSProperty<FontStyleItem>(this, EOSConstants.R4C2);
                HintTextColor = GetThemeProvider().GetEOSProperty<FontStyleItem>(this, EOSConstants.R4C3).Color;
                HintTextColorDisabled = GetThemeProvider().GetEOSProperty<FontStyleItem>(this, EOSConstants.R4C4).Color;
                FocusedColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                DisabledColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.DisabledInputColor);
                TextColorDisabled = GetThemeProvider().GetEOSProperty<FontStyleItem>(this, EOSConstants.R4C4).Color;
                NormalIconColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor2);
                NormalUnderlineColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                PopulatedIconColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                PopulatedUnderlineColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                LeftImage = Context.Resources.GetDrawable(GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.LeftImage));
                IsEOSCustomizationIgnored = false;
                UpdateEnabledState();
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
            UpdateIconColor();
            UpdateUnderlineColor();
            UpdateRightImage();
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            if(FindFocus() == this && !string.IsNullOrEmpty(Text))
            {
                if(Width - e.GetX() <= IconWidth * Resources.DisplayMetrics.Density)
                    Text = string.Empty;
            }

            return false;
        }

        #endregion
    }
}
