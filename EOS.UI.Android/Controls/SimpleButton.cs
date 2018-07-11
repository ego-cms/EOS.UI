using System;
using Android.Animation;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using EOS.UI.Shared.Helpers;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Java.Util;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using static EOS.UI.Android.Helpers.Constants;
using A = Android;

namespace EOS.UI.Android.Controls
{
    public class SimpleButton : Button, IEOSThemeControl
    {
        #region fields

        private float _pivot = 0.5f;
        private ObjectAnimator _animator;
        private RotateDrawable _rotateDrawable;

        public bool InProgress { get; private set; }

        #endregion

        #region constructors

        public SimpleButton(Context context) : base(context)
        {
            Initialize();
        }

        public SimpleButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public SimpleButton(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Initialize(attrs);
        }

        public SimpleButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
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

        private Color _backgroundColor;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _backgroundColor = value;
                if(Enabled)
                    Background = CreateRippleDrawable(BackgroundColor);
            }
        }

        private Color _disabledBackgroundColor;
        public Color DisabledBackgroundColor
        {
            get => _disabledBackgroundColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _disabledBackgroundColor = value;
                if(!Enabled)
                    Background = CreateGradientDrawable(_disabledBackgroundColor);
            }
        }

        private Color _pressedBackgroundColor;
        public Color PressedBackgroundColor
        {
            get => _pressedBackgroundColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _pressedBackgroundColor = value;
                if(Enabled)
                    Background = CreateRippleDrawable(BackgroundColor);
            }
        }

        public override Typeface Typeface
        {
            get => base.Typeface;
            set
            {
                IsEOSCustomizationIgnored = true;
                FontStyle.Typeface = value;
                SetFontStyle();
                base.Typeface = value;
            }
        }

        public override float LetterSpacing
        {
            get => base.LetterSpacing;
            set
            {
                IsEOSCustomizationIgnored = true;
                FontStyle.LetterSpacing = value;
                SetFontStyle();
                base.LetterSpacing = value;
            }
        }

        public override float TextSize
        {
            get => base.TextSize;
            set
            {
                IsEOSCustomizationIgnored = true;
                FontStyle.Size = value;
                SetFontStyle();
                base.TextSize = value;
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
                _rotateDrawable.Drawable?.SetColorFilter(value, PorterDuff.Mode.SrcIn);
                if(Enabled)
                    base.SetTextColor(value);
            }
        }

        public override void SetTextColor(Color color)
        {
            base.SetTextColor(Enabled ? TextColor : DisabledTextColor);
        }

        public Color DisabledTextColor
        {
            get => DisabledFontStyle.Color;
            set
            {
                IsEOSCustomizationIgnored = true;
                DisabledFontStyle.Color = value;
                SetDisabledFontStyle();
                if(!Enabled)
                    base.SetTextColor(value);
            }
        }

        private Color _rippleColor;
        public Color RippleColor
        {
            get => _rippleColor;
            set
            {
                _rippleColor = value.A == 255 ? Color.Argb(26, value.R, value.G, value.B) : value;
                Background = CreateRippleDrawable(BackgroundColor);
                IsEOSCustomizationIgnored = true;
            }
        }

        private float _cornerRadius;
        public float CornerRadius
        {
            get => _cornerRadius;
            set
            {
                _cornerRadius = value;
                Background = Enabled ? CreateRippleDrawable(BackgroundColor) : CreateGradientDrawable(DisabledBackgroundColor);
                IsEOSCustomizationIgnored = true;
            }
        }

        private ShadowConfig _shadowConfig;
        public ShadowConfig ShadowConfig
        {
            get => _shadowConfig;
            set
            {
                _shadowConfig = value;

                if(Build.VERSION.SdkInt > BuildVersionCodes.Lollipop)
                {
                    UpdateStateListAnimator();
                }
                else
                {
                    StateListAnimator = null;
                    Elevation = 2 * _shadowConfig.Radius;
                }

                IsEOSCustomizationIgnored = true;
            }
        }

        private void UpdateStateListAnimator()
        {
            var stateList = new StateListAnimator();

            var elevationHolderToPressed = PropertyValuesHolder.OfFloat("Elevation", _shadowConfig.Radius, 0);
            var translationZHolderToPressed = PropertyValuesHolder.OfFloat("TranslationZ", _shadowConfig.Radius, 0);
            var pressedAnimation = ObjectAnimator.OfPropertyValuesHolder(this, elevationHolderToPressed, translationZHolderToPressed);
            pressedAnimation.SetDuration(100);

            var disabledAnimation = ObjectAnimator.OfPropertyValuesHolder(this, elevationHolderToPressed, translationZHolderToPressed);
            disabledAnimation.SetDuration(0);

            var elevationHolderToNormal = PropertyValuesHolder.OfFloat("Elevation", 0, _shadowConfig.Radius);
            var translationZHolderToNormal = PropertyValuesHolder.OfFloat("TranslationZ", 0, _shadowConfig.Radius);
            var normalAnimation = ObjectAnimator.OfPropertyValuesHolder(this, elevationHolderToNormal, translationZHolderToNormal);
            normalAnimation.SetDuration(100);

            stateList.AddState(new int[1] { A.Resource.Attribute.StatePressed }, pressedAnimation);
            stateList.AddState(new int[1] { A.Resource.Attribute.StateEnabled }, normalAnimation);
            stateList.AddState(new int[1] { -A.Resource.Attribute.StateEnabled }, disabledAnimation);

            StateListAnimator = stateList;
        }

        private Drawable _preloaderImage;
        public Drawable PreloaderImage
        {
            get => _preloaderImage;
            set
            {
                _preloaderImage = value;
                _rotateDrawable.Drawable = _preloaderImage;
                _rotateDrawable.Drawable.SetColorFilter(TextColor, PorterDuff.Mode.SrcIn);
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

        private FontStyleItem _disabledFontStyle;
        public FontStyleItem DisabledFontStyle
        {
            get => _disabledFontStyle;
            set
            {
                _disabledFontStyle = value;
                SetDisabledFontStyle();
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

        private void SetDisabledFontStyle()
        {
            base.Typeface = DisabledFontStyle.Typeface;
            base.TextSize = DisabledFontStyle.Size;
            if(!Enabled)
                base.SetTextColor(DisabledFontStyle.Color);
            base.LetterSpacing = DisabledFontStyle.LetterSpacing;
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            _rotateDrawable = CreateRotateDrawable();
            SetAllCaps(false);
            SetLines(1);
            Ellipsize = TextUtils.TruncateAt.End;
            ClipToOutline = false;

            if(attrs != null)
                InitializeAttributes(attrs);

            UpdateAppearance();
            Background = CreateRippleDrawable(BackgroundColor);
        }

        private void InitializeAttributes(IAttributeSet attrs)
        {
            var styledAttributes = Context.ObtainStyledAttributes(attrs, Resource.Styleable.SimpleButton, 0, 0);

            var backgroundColor = styledAttributes.GetColor(Resource.Styleable.SimpleButton_eos_backgroundcolor, Color.Transparent);
            if(backgroundColor != Color.Transparent)
                BackgroundColor = backgroundColor;

            var disabledBackgroundColor = styledAttributes.GetColor(Resource.Styleable.SimpleButton_eos_backgroundcolor_disabled, Color.Transparent);
            if(disabledBackgroundColor != Color.Transparent)
                DisabledBackgroundColor = disabledBackgroundColor;

            var pressedBackgroundColor = styledAttributes.GetColor(Resource.Styleable.SimpleButton_eos_backgroundcolor_pressed, Color.Transparent);
            if(pressedBackgroundColor != Color.Transparent)
                PressedBackgroundColor = pressedBackgroundColor;

            var font = styledAttributes.GetString(Resource.Styleable.SimpleButton_eos_font);
            if(!string.IsNullOrEmpty(font))
                Typeface = Typeface.CreateFromAsset(Context.Assets, font);

            var letterSpacing = styledAttributes.GetFloat(Resource.Styleable.SimpleButton_eos_letterspacing, -1);
            if(letterSpacing > 0)
                LetterSpacing = letterSpacing;

            var textColor = styledAttributes.GetColor(Resource.Styleable.SimpleButton_eos_textcolor, Color.Transparent);
            if(textColor != Color.Transparent)
                TextColor = textColor;

            var disabledTextColor = styledAttributes.GetColor(Resource.Styleable.SimpleButton_eos_textcolor_disabled, Color.Transparent);
            if(disabledTextColor != Color.Transparent)
                DisabledTextColor = disabledTextColor;

            var textSize = styledAttributes.GetFloat(Resource.Styleable.SimpleButton_eos_textsize, -1);
            if(textSize > 0)
                TextSize = textSize;

            var cornerRadius = styledAttributes.GetFloat(Resource.Styleable.SimpleButton_eos_cornerradius, -1);
            if(cornerRadius > 0)
                CornerRadius = cornerRadius;

            var preloaderImage = styledAttributes.GetDrawable(Resource.Styleable.SimpleButton_eos_preloaderimage);
            if(preloaderImage != null)
                PreloaderImage = preloaderImage;

            var enabled = styledAttributes.GetBoolean(Resource.Styleable.SimpleButton_eos_enabled, true);
            if(!enabled)
                Enabled = enabled;
        }

        private Drawable CreateRippleDrawable(Color contentColor)
        {
            return new RippleDrawable(
                CreateRippleColorStateList(),
                CreateGradientDrawable(contentColor),
                CreateRoundedMaskDrawable());
        }

        private ColorStateList CreateRippleColorStateList()
        {
            return new ColorStateList(
               new int[][] { new int[] { } },
               new int[]
               {
                   RippleColor,
               });
        }

        private GradientDrawable CreateGradientDrawable(Color color)
        {
            var drawable = new GradientDrawable();
            drawable.SetShape(ShapeType.Rectangle);
            drawable.SetColor(color);
            drawable.SetCornerRadius(CornerRadius);
            return drawable;
        }

        private ShapeDrawable CreateRoundedMaskDrawable()
        {
            var outerRadii = new float[8];
            Arrays.Fill(outerRadii, CornerRadius);
            var shapeDrawable = new ShapeDrawable(new RoundRectShape(outerRadii, null, null));
            shapeDrawable.Paint.Color = PressedBackgroundColor;
            return shapeDrawable;
        }

        private void UpdateEnabledState(bool enabled)
        {
            base.SetTextColor(enabled ? TextColor : DisabledTextColor);
            Background = enabled ? CreateRippleDrawable(BackgroundColor) : CreateGradientDrawable(DisabledBackgroundColor);
        }

        public void StartProgressAnimation()
        {
            if(Enabled && !InProgress)
            {
                Drawable[] layers = { CreateGradientDrawable(BackgroundColor), _rotateDrawable };
                var layerDrawable = new LayerDrawable(layers);

                var preloaderSize = Height / 2;
                var insetVertical = Height / 4;
                var insetHorizontal = (Width - preloaderSize) / 2;
                layerDrawable.SetLayerInset(1, insetHorizontal, insetVertical, insetHorizontal, insetVertical);

                Background = layerDrawable;
                base.SetTextColor(Color.Transparent);
                _animator = ObjectAnimator.OfInt(_rotateDrawable, "Level", 0, AnimationConstants.LevelMaxCount);
                _animator.SetInterpolator(new LinearInterpolator());
                _animator.SetDuration(AnimationConstants.TurnoverTime);
                _animator.RepeatCount = ValueAnimator.Infinite;
                _animator.RepeatMode = ValueAnimatorRepeatMode.Restart;
                _animator.Start();
                InProgress = true;
            }
        }

        public void StopProgressAnimation()
        {
            _animator.Cancel();
            InProgress = false;
            BackgroundColor = _backgroundColor;
            base.SetTextColor(TextColor);
        }

        private RotateDrawable CreateRotateDrawable()
        {
            if(Build.VERSION.SdkInt >= BuildVersionCodes.M)
                return CreateRotateDrawableAPI23();
            else
                return CreateRotateDrawableAPI21();
        }

        private RotateDrawable CreateRotateDrawableAPI23()
        {
            var drawable = new RotateDrawable();
            drawable.PivotXRelative = true;
            drawable.PivotX = _pivot;
            drawable.PivotYRelative = true;
            drawable.PivotY = _pivot;
            return drawable;
        }

        private RotateDrawable CreateRotateDrawableAPI21()
        {
            //It's impossible adequate creation from code due
            //https://github.com/aosp-mirror/platform_frameworks_base/blob/lollipop-dev/graphics/java/android/graphics/drawable/RotateDrawable.java#L218
            //use creation from xml hack
            var drawable = (RotateDrawable)Drawable.CreateFromXml(Resources, Resources.GetXml(Resource.Drawable.RotateDrawable));
            return drawable;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if(Build.VERSION.SdkInt <= BuildVersionCodes.Lollipop)
            {
                if(e.Action == MotionEventActions.Down)
                    Elevation = 0; 
                if(e.Action == MotionEventActions.Up || e.Action == MotionEventActions.Cancel)
                    Elevation = 2 * _shadowConfig.Radius;
            }
            return base.OnTouchEvent(e);
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
                FontStyle = GetThemeProvider().GetEOSProperty<FontStyleItem>(this, EOSConstants.R3C5);
                DisabledFontStyle = GetThemeProvider().GetEOSProperty<FontStyleItem>(this, EOSConstants.R3C4);
                BackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                DisabledBackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor4);
                PressedBackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColorVariant1);
                RippleColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColorVariant1);
                CornerRadius = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.ButtonCornerRadius);
                PreloaderImage = Resources.GetDrawable(GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.FabProgressPreloaderImage), null);
                ShadowConfig = GetThemeProvider().GetEOSProperty<ShadowConfig>(this, EOSConstants.SimpleButtonShadow);

                IsEOSCustomizationIgnored = false;
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
