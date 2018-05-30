using System;
using Android.Animation;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Java.Util;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using static EOS.UI.Android.Helpers.Constants;
using R = Android.Resource;

namespace EOS.UI.Android.Controls
{
    public class SimpleButton: Button, IEOSThemeControl, View.IOnTouchListener
    {
        #region fields

        private bool _isAnimated;
        private ObjectAnimator _animator;

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
                base.Typeface = value;
            }
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

        public override float TextSize
        {
            get => base.TextSize;
            set
            {
                IsEOSCustomizationIgnored = true;
                base.TextSize = value;
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

        public override void SetTextColor(Color color)
        {
            base.SetTextColor(Enabled ? TextColor : DisabledTextColor);
        }

        private Color _disabledTextColor;
        public Color DisabledTextColor
        {
            get => _disabledTextColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _disabledTextColor = value;
                if(!Enabled)
                    base.SetTextColor(value);
            }
        }

        private Color _pressedTextColor;
        public Color PressedTextColor
        {
            get => _pressedTextColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _pressedTextColor = value;
            }
        }

        private float _cornerRadius;
        public float CornerRadius
        {
            get => _cornerRadius;
            set
            {
                _cornerRadius = value;
                Background = Enabled? CreateRippleDrawable(BackgroundColor) : CreateGradientDrawable(DisabledBackgroundColor);
                IsEOSCustomizationIgnored = true;
            }
        }

        #endregion

        #region utility methods

        private void Initialize(IAttributeSet attrs = null)
        {
            SetOnTouchListener(this);
            Background = CreateRippleDrawable(BackgroundColor);
            SetLines(1);
            Ellipsize = TextUtils.TruncateAt.End;
            UpdateAppearance();
            if(attrs != null)
                InitializeAttributes(attrs);
        }

        private void InitializeAttributes(IAttributeSet attrs)
        {
            //TODO: Implement set attrs logic
        }

        private Drawable CreateRippleDrawable(Color contentColor)
        {
            return new RippleDrawable(
                CreateBackgroundColorStateList(),
                CreateGradientDrawable(contentColor),
                CreateRoundedMaskDrawable());
        }

        private ColorStateList CreateBackgroundColorStateList()
        {
            return new ColorStateList(
               new int[][] { new int[] { } },
               new int[]
               {
                   PressedBackgroundColor,
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

        public void StartAnimation()
        {
            if(Enabled && !_isAnimated)
            {
                var rotateDrawable = new RotateDrawable();
                rotateDrawable.Drawable = ContextCompat.GetDrawable(Context, Resource.Drawable.icPreloader);

                Drawable[] layers = { CreateGradientDrawable(BackgroundColor), rotateDrawable };
                var layerDrawable = new LayerDrawable(layers);
                layerDrawable.SetLayerGravity(1, GravityFlags.Center);
                Background = layerDrawable;
                base.SetTextColor(Color.Transparent);

                _animator = ObjectAnimator.OfInt(rotateDrawable, "Level", 0, AnimationConstants.LevelMaxCount);
                _animator.SetInterpolator(new LinearInterpolator());
                _animator.SetDuration(AnimationConstants.TurnoverTime);
                _animator.RepeatCount = ValueAnimator.Infinite;
                _animator.RepeatMode = ValueAnimatorRepeatMode.Restart;
                _animator.Start();
                _isAnimated = true;
            }
        }

        public void StopAnimation()
        {
            _animator.Cancel();
            _isAnimated = false;
            BackgroundColor = _backgroundColor;
            base.SetTextColor(_textColor);
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
                base.TextSize = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.TextSize);
                TextColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6);
                DisabledTextColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3);
                PressedTextColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6);
                BackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                DisabledBackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor4);
                PressedBackgroundColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColorVariant1);
                CornerRadius = GetThemeProvider().GetEOSProperty<float>(this, EOSConstants.CornerRadius);
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

        #region IOnTouchListener implementation

        public bool OnTouch(View v, MotionEvent e)
        {
            if(Enabled && !_isAnimated)
            {
                if(e.Action == MotionEventActions.Down)
                    base.SetTextColor(PressedTextColor);
                if(e.Action == MotionEventActions.Up || e.Action == MotionEventActions.Cancel)
                    base.SetTextColor(TextColor);
            }
            return false;
        }

        #endregion
    }
}