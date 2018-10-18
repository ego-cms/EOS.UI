using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Util;
using EOS.UI.Droid.Themes;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;

namespace EOS.UI.Droid.Controls
{
    public class BadgeLabel: AppCompatTextView, IEOSThemeControl
    {
        #region fields

        private const int LeftPadding = 20;
        private const int RightPadding = 20;
        private const int TopPadding = 1;
        private const int BottomPadding = 3;

        #endregion

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
            get => FontStyle?.Typeface;
            set
            {
                //Should check FontStyle
                //Set method works with base(context) constructor, which works ahead of FontStyle set
                if (FontStyle != null)
                {
                    IsEOSCustomizationIgnored = true;
                    FontStyle.Typeface = value;
                    SetFontStyle();
                }
                base.Typeface = value;
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
                //Should check FontStyle
                //Set method works with base(context) constructor, which works ahead of FontStyle set
                if (FontStyle != null)
                {
                    IsEOSCustomizationIgnored = true;
                    FontStyle.LetterSpacing = value;
                    SetFontStyle();
                }
                base.LetterSpacing = value;
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

        float _cornerRadius;
        public float CornerRadius
        {
            get => _cornerRadius;
            set
            {
                _cornerRadius = value;
                IsEOSCustomizationIgnored = true;
                (Background as GradientDrawable).SetCornerRadius(value);
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
            base.SetTextColor(FontStyle.Color);
            base.LetterSpacing = FontStyle.LetterSpacing;
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            Background = CreateDefaultDrawable();
            var denisty = Resources.DisplayMetrics.Density;
            SetMaxLines(1);
            Ellipsize = TextUtils.TruncateAt.End;
            Gravity = Android.Views.GravityFlags.Center;
            SetPadding((int)(denisty * LeftPadding), (int)(denisty * TopPadding), (int)(denisty * RightPadding), (int)(denisty * BottomPadding));

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

            var letterSpacing = styledAttributes.GetFloat(Resource.Styleable.BadgeLabel_eos_letterspacing, -1);
            if(letterSpacing > 0)
                LetterSpacing = letterSpacing;

            var textColor = styledAttributes.GetColor(Resource.Styleable.BadgeLabel_eos_textcolor, Color.Transparent);
            if(textColor != Color.Transparent)
                TextColor = textColor;

            var textSize = styledAttributes.GetFloat(Resource.Styleable.BadgeLabel_eos_textsize, -1);
            if(textSize > 0)
                TextSize = textSize;

            var cornerRadius = styledAttributes.GetFloat(Resource.Styleable.BadgeLabel_eos_cornerradius, -1);
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
                FontStyle = GetThemeProvider().GetEOSProperty<FontStyleItem>(this, EOSConstants.R2C5S); 
                BackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                CornerRadius = GetCornerRadius();
                IsEOSCustomizationIgnored = false;
            }
        }

        private float GetCornerRadius()
        {
            return GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.LabelCornerRadius) * Resources.DisplayMetrics.Density;
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
