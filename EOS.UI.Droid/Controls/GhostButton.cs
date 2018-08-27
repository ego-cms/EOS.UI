using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Util;
using EOS.UI.Droid.Themes;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Java.Util;


namespace EOS.UI.Droid.Controls
{
    public class GhostButton : AppCompatButton, IEOSThemeControl
    {
        public bool IsEOSCustomizationIgnored { get; private set; }

        public override Typeface Typeface
        {
            get => base.Typeface;
            set
            {
                base.Typeface = value;
                FontStyle.Typeface = value;
                SetFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public override float LetterSpacing
        {
            get => base.LetterSpacing;
            set
            {
                base.LetterSpacing = value;
                FontStyle.LetterSpacing = value;
                SetFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public override float TextSize
        {
            get => base.TextSize;
            set
            {
                base.TextSize = value;
                FontStyle.Size = value;
                SetFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public Color EnabledTextColor
        {
            get => FontStyle.Color;
            set
            {
                FontStyle.Color = value;
                SetFontStyle();
                SetTextColorSet(value, DisabledTextColor);
                IsEOSCustomizationIgnored = true;
            }
        }

        private void SetTextColorSet(Color enabledColor, Color disabledColor)
        {
            var colorSet = new ColorStateList(
            new int[][]
            {
                new int[] { Android.Resource.Attribute.StatePressed },
                new int[] { Android.Resource.Attribute.StateEnabled },
                new int[] { -Android.Resource.Attribute.StateEnabled },
            },
            new int[]
            {
                enabledColor,
                enabledColor,
                disabledColor
            });
            base.SetTextColor(colorSet);
        }


        private Color _disabledTextColor;
        public Color DisabledTextColor
        {
            get => _disabledTextColor;
            set
            {
                _disabledTextColor= value;
                SetTextColorSet(EnabledTextColor, _disabledTextColor);
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _rippleColor;
        public Color RippleColor
        {
            get => _rippleColor;
            set
            {
                _rippleColor = value.A == 255 ? Color.Argb(26, value.R, value.G, value.B) : value;
                IsEOSCustomizationIgnored = true;
                Background = CreateRippleDrawable();
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
            SetTextColorSet(FontStyle.Color, _disabledTextColor);
            base.LetterSpacing = FontStyle.LetterSpacing;
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

        protected GhostButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        private void Initialize(IAttributeSet attributeSet = null)
        {
            SetAllCaps(false);
            SetMinimumHeight(0);
            UpdateAppearance();
            Background = CreateRippleDrawable();
            SetLines(1);
            Ellipsize = TextUtils.TruncateAt.End;
            if(attributeSet != null)
                InitializeAttributes(attributeSet);

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

            var textSize = styledAttributes.GetFloat(Resource.Styleable.GhostButton_eos_textsize, -1);
            if(textSize > 0)
                TextSize = textSize;
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
                    RippleColor
                }),
                CreateGradientDrawable(),
                CreateRoundedMaskDrawable());
        }

        private ShapeDrawable CreateRoundedMaskDrawable()
        {
            var outerRadii = new float[8];
            Arrays.Fill(outerRadii, 15);
            var shapeDrawable = new ShapeDrawable(new RoundRectShape(outerRadii, null, null));
            shapeDrawable.Paint.Color = Color.White;
            shapeDrawable.Paint.StrokeWidth = 0;
            shapeDrawable.SetState(new int[] { });
            return shapeDrawable;
        }

        private GradientDrawable CreateGradientDrawable()
        {
            var drawable = new GradientDrawable();
            drawable.SetShape(ShapeType.Rectangle);
            drawable.SetColor(Color.Transparent);
            drawable.SetCornerRadius(15);
            return drawable;
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
            if(!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                FontStyle = GetThemeProvider().GetEOSProperty<FontStyleItem>(this, EOSConstants.R2C1S);
                DisabledTextColor = GetThemeProvider().GetEOSProperty<FontStyleItem>(this, EOSConstants.R2C4S).Color;
                RippleColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.RippleColor);
                IsEOSCustomizationIgnored = false;
            }
        }
    }
}
