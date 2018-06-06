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
    public class BadgeLabel: TextView, IEOSThemeControl
    {
        #region constructors

        public BadgeLabel(Context context) : base(context)
        {
            Initialize();
        }

        public BadgeLabel(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public BadgeLabel(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Initialize(attrs);
        }

        public BadgeLabel(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        #endregion

        #region customization

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
            var styledAttributes = Context.ObtainStyledAttributes(attrs, Resource.Styleable.BadgeLabel, 0, 0);

            var backgroundColor = styledAttributes.GetColor(Resource.Styleable.BadgeLabel_eos_background, Color.Transparent);
            if(backgroundColor != Color.Transparent)
                BackgroundColor = backgroundColor;

            var font = styledAttributes.GetString(Resource.Styleable.BadgeLabel_eos_font);
            if(!string.IsNullOrEmpty(font))
                Typeface = Typeface.CreateFromAsset(Context.Assets, font);

            var letterSpacing = styledAttributes.GetFloat(Resource.Styleable.BadgeLabel_eos_letter_spacing, -1);
            if(letterSpacing > 0)
                LetterSpacing = letterSpacing;

            var textColor = styledAttributes.GetColor(Resource.Styleable.BadgeLabel_eos_text_color, Color.Transparent);
            if(textColor != Color.Transparent)
                TextColor = textColor;

            var textSize = styledAttributes.GetFloat(Resource.Styleable.BadgeLabel_eos_text_size, -1);
            if(textSize > 0)
                TextSize = textSize;

            var cornerRadius = styledAttributes.GetFloat(Resource.Styleable.BadgeLabel_eos_corner_radius, -1);
            if(cornerRadius > 0)
                CornerRadius = cornerRadius;
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
                (Background as GradientDrawable).SetColor(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor));
                base.SetTypeface(Typeface.CreateFromAsset(Context.Assets, GetThemeProvider().GetEOSProperty<string>(this, EOSConstants.Font)), TypefaceStyle.Normal);
                base.LetterSpacing = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.LetterSpacing);
                base.SetTextColor(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6));
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