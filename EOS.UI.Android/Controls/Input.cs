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

        private Drawable _drawableLeftUnfocuced;
        public Drawable DrawableLeftUnfocuced
        {
            get => _drawableLeftUnfocuced;
            set
            {
                _drawableLeftUnfocuced = value;
                if(FindFocus() != this)
                    SetCompoundDrawablesWithIntrinsicBounds(_drawableLeftUnfocuced, null, null, null);
            }
        }

        private Drawable _drawableLeftFocuced;
        public Drawable DrawableLeftFocuced
        {
            get => _drawableLeftFocuced;
            set
            {
                _drawableLeftFocuced = value;
                if(FindFocus() == this)
                    SetCompoundDrawablesWithIntrinsicBounds(_drawableLeftUnfocuced, null, null, null);
            }
        }

        public override void SetCompoundDrawablesWithIntrinsicBounds(Drawable left, Drawable top, Drawable right, Drawable bottom)
        {
            if(left != null)
                IsEOSCustomizationIgnored = true;

            base.SetCompoundDrawablesWithIntrinsicBounds(left, top, right, bottom);
        }

        private Color _hintTextColor;
        public Color HintTextColor
        {
            get => _hintTextColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _hintTextColor = value;
                base.SetHintTextColor(value);
            }
        }
        
        public new void SetHintTextColor(Color color)
        {
            HintTextColor = color;
        }

        private float _hintTextSize;
        public float HintTextSize
        {
            get => _hintTextSize;
            set
            {
                IsEOSCustomizationIgnored = true;
                _hintTextSize = value;
                if(FindFocus() != this)
                    TextSize = _hintTextSize;
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
                base.SetTextColor(value);
            }
        }

        public override void SetTextColor(Color color)
        {
            TextColor = color;
        }

        private float _textSize;
        public override float TextSize
        {
            get => _textSize;
            set
            {
                IsEOSCustomizationIgnored = true;
                _textSize = value;
                if(FindFocus() != this)
                    base.TextSize = value;
            }
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            SetMaxLines(1);
            Ellipsize = A.Text.TextUtils.TruncateAt.End;
            OnFocusChangeListener = this;
            if(attrs != null)
                InitializeAttributes(attrs);
            UpdateAppearance();
        }

        private void InitializeAttributes(IAttributeSet attrs)
        {
            //TODO: Implement set attrs logic
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
                base.SetTextColor(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.TextColor));
                TextSize = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.TextSize);
                HintTextSize = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.HintTextSize);
                base.SetHintTextColor(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.HintTextColor));
                DrawableLeftFocuced = Context.Resources.GetDrawable(GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.LeftImageFocused));
                DrawableLeftUnfocuced = Context.Resources.GetDrawable(GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.LeftImageUnfocused));
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
            SetCompoundDrawablesWithIntrinsicBounds(hasFocus ? DrawableLeftFocuced : DrawableLeftUnfocuced, null, null, null);
        }

        #endregion
    }
}