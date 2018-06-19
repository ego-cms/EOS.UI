using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Java.Util;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using R = Android.Resource;
using TextUtils = Android.Text.TextUtils;


namespace EOS.UI.Android.Controls
{
    public class GhostButton : Button, IEOSThemeControl
    {
        public bool IsEOSCustomizationIgnored { get; private set; }

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
                SetTextColorSet(PressedStateTextColor, _enabledTextColor, DisabledTextColor);
                IsEOSCustomizationIgnored = true;
            }
        }

        private void SetTextColorSet(Color pressedColor, Color enabledColor, Color disabledColor)
        {
            var colorSet = new ColorStateList(
            new int[][]
            {
                new int[] { R.Attribute.StatePressed },
                new int[] { R.Attribute.StateEnabled },
                new int[] { -R.Attribute.StateEnabled},
            },
            new int[]
            {
                pressedColor,
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
                _disabledTextColor = value;
                SetTextColorSet(PressedStateTextColor, EnabledTextColor, _disabledTextColor);
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
                SetTextColorSet(_pressedStateTextColor, EnabledTextColor, DisabledTextColor);
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
            if(attributeSet != null)
                InitializeAttributes(attributeSet);

            SetAllCaps(false);
            UpdateAppearance();
            Background = CreateRippleDrawable();
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
            Arrays.Fill(outerRadii, 10);
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
            drawable.SetCornerRadius(10);
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
                base.SetTypeface(Typeface.CreateFromAsset(Context.Assets, provider.GetEOSProperty<string>(this, EOSConstants.Font)), TypefaceStyle.Normal);
                base.LetterSpacing = provider.GetEOSProperty<float>(this, EOSConstants.LetterSpacing);
                EnabledTextColor = provider.GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                DisabledTextColor = provider.GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                PressedStateTextColor = provider.GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                RippleColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColorVariant1);
                base.TextSize = provider.GetEOSProperty<float>(this, EOSConstants.TextSize);
                IsEOSCustomizationIgnored = false;
            }
        }
    }
}
