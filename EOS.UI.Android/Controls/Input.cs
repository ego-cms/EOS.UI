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
using A = Android;

namespace EOS.UI.Android.Controls
{
    public class Input: EditText, IEOSThemeControl
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

        private Color _backgroundColor;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _backgroundColor = value;
                (Background as GradientDrawable).SetColor(_backgroundColor);
            }
        }

        public override void SetBackgroundColor(Color color)
        {
            BackgroundColor = color;
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

        public override float TextSize
        {
            get => base.TextSize;
            set
            {
                IsEOSCustomizationIgnored = true;
                base.TextSize = value; 
            }
        }

        public float CornerRadius
        {
            get => (Background as GradientDrawable).CornerRadius;
            set
            {
                IsEOSCustomizationIgnored = true;
                (Background as GradientDrawable).SetCornerRadius(value);
            }
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            Background = CreateDefaultDrawable();
            SetPadding(15, 0, 15, 0);
            SetMaxLines(1);
            Ellipsize = A.Text.TextUtils.TruncateAt.End;
            if(attrs != null)
                InitializeAttributes(attrs);
            UpdateAppearance();
        }

        private GradientDrawable CreateDefaultDrawable()
        {
            var drawable = new GradientDrawable();
            drawable.SetShape(ShapeType.Rectangle);
            return drawable;
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
                (Background as GradientDrawable).SetColor(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BackgroundColor));
                base.SetTypeface(Typeface.CreateFromAsset(Context.Assets, GetThemeProvider().GetEOSProperty<string>(this, EOSConstants.Font)), TypefaceStyle.Normal);
                base.LetterSpacing = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.LetterSpacing);
                base.SetTextColor(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.TextColor));
                base.TextSize = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.TextSize);
                (Background as GradientDrawable).SetCornerRadius(GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.CornerRadius));
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