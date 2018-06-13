using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Controls
{
    public class FabProgress : ImageButton, IEOSThemeControl
    {
        private const int _animationDuration = 100;
        private const float _startScale = 0.85f;
        private const float _endScale = 1.0f;
        private const int _startPadding = 30;
        private const double _paddingRation = 0.24;
        private Animation _rotationAnimation;

        public bool IsEOSCustomizationIgnored { get; private set; }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                (Background as GradientDrawable).SetColor(value ? BackgroundColor : DisabledBackgroundColor);
                Image.SetColorFilter(value ? 
                    GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6) :
                    GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3), 
                    PorterDuff.Mode.SrcIn);
            }
        }

        private Color _backgroundColor;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                if(Enabled)
                {
                    (Background as GradientDrawable).SetColor(value);
                    Image.SetColorFilter(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6), PorterDuff.Mode.SrcIn);
                }
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _disabledBackgroundColor;
        public Color DisabledBackgroundColor
        {
            get => _disabledBackgroundColor;
            set
            {
                _disabledBackgroundColor = value;
                if(!Enabled)
                {
                    (Background as GradientDrawable).SetColor(DisabledBackgroundColor);
                    Image.SetColorFilter(GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3), PorterDuff.Mode.SrcIn);
                }
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _pressedBackgroundColor;
        public Color PressedBackgroundColor
        {
            get => _pressedBackgroundColor;
            set
            {
                _pressedBackgroundColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private int _buttonSize;
        public int ButtonSize
        {
            get => _buttonSize;
            set
            {
                _buttonSize = value;
                LayoutParameters.Width = value;
                LayoutParameters.Height = value;
                SetScaleType(ScaleType.FitCenter);
                RequestLayout();
                var padding = (int)System.Math.Round(_paddingRation * _buttonSize);
                SetPadding(padding, padding, padding, padding);
                IsEOSCustomizationIgnored = true;
            }
        }

        private Drawable _image;
        public Drawable Image
        {
            get => _image;
            set
            {
                _image = value;
                SetImageDrawable(value);
                IsEOSCustomizationIgnored = true;
            }
        }

        private Drawable _preloaderImage;
        public Drawable PreloaderImage
        {
            get => _preloaderImage;
            set
            {
                _preloaderImage = value;
                IsEOSCustomizationIgnored = true;
            }
        }
        
        public bool InProgress { get; private set; }

        public FabProgress(Context context) : base(context)
        {
            Initialize();
        }

        public FabProgress(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize();
        }

        public FabProgress(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize();
        }

        public FabProgress(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize();
        }

        protected FabProgress(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        private void Initialize(IAttributeSet attrs = null)
        {
            _rotationAnimation = AnimationUtils.LoadAnimation(Application.Context, Resource.Animation.FabRotationAnimation);

            if(attrs != null)
                InitializeAttributes(attrs);

            UpdateAppearance();
            Elevation = 10f;
        }

        private void InitializeAttributes(IAttributeSet attrs)
        {
            var styledAttributes = Context.ObtainStyledAttributes(attrs, Resource.Styleable.FabProgress, 0, 0);

            var backgroundColor = styledAttributes.GetColor(Resource.Styleable.FabProgress_eos_backgroundcolor, Color.Transparent);
            if(backgroundColor != Color.Transparent)
                BackgroundColor = backgroundColor;

            var disabledBackgroundColor = styledAttributes.GetColor(Resource.Styleable.FabProgress_eos_backgroundcolor_disabled, Color.Transparent);
            if(disabledBackgroundColor != Color.Transparent)
                DisabledBackgroundColor = disabledBackgroundColor;

            var pressedBackgroundColor = styledAttributes.GetColor(Resource.Styleable.FabProgress_eos_backgroundcolor_pressed, Color.Transparent);
            if(pressedBackgroundColor != Color.Transparent)
                PressedBackgroundColor = pressedBackgroundColor;

            var buttonSize = styledAttributes.GetInt(Resource.Styleable.FabProgress_eos_buttonsize, -1);
            if(buttonSize > 0)
                ButtonSize = buttonSize;

            var image = styledAttributes.GetDrawable(Resource.Styleable.FabProgress_eos_image);
            if(image != null)
                Image = image;

            var preloaderImage = styledAttributes.GetDrawable(Resource.Styleable.FabProgress_eos_preloaderimage);
            if(preloaderImage != null)
                PreloaderImage = preloaderImage;

            var enabled = styledAttributes.GetBoolean(Resource.Styleable.FabProgress_eos_enabled, true);
            if(!enabled)
                Enabled = enabled;
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
            LayoutParameters.Width = ViewGroup.LayoutParams.WrapContent;
            LayoutParameters.Height = ViewGroup.LayoutParams.WrapContent;
            RequestLayout();
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {
        }

        public void UpdateAppearance()
        {
            if (!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                Image = Resources.GetDrawable(provider.GetEOSProperty<int>(this, EOSConstants.CalendarImage));
                PreloaderImage = Resources.GetDrawable(provider.GetEOSProperty<int>(this, EOSConstants.FabProgressPreloaderImage));
                var roundedDrawable = (GradientDrawable)Resources.GetDrawable(Resource.Drawable.FabButton);
                SetBackgroundDrawable(roundedDrawable);
                BackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                DisabledBackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.NeutralColor4);
                PressedBackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColorVariant1);
                SetPadding(_startPadding, _startPadding, _startPadding, _startPadding);
                IsEOSCustomizationIgnored = false;
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (Enabled)
            {
                if (e.Action == MotionEventActions.Down)
                {
                    (Background as GradientDrawable).SetColor(PressedBackgroundColor);
                    Animate().ScaleX(_startScale).ScaleY(_startScale).SetDuration(_animationDuration).Start();
                }
                if (e.Action == MotionEventActions.Up || e.Action == MotionEventActions.Cancel)
                {
                    (Background as GradientDrawable).SetColor(BackgroundColor);
                    Animate().ScaleX(_endScale).ScaleY(_endScale).SetDuration(_animationDuration).Start();
                    PerformClick();
                }
            }
            return true;
        }

        public void StartProgressAnimation()
        {
            if (InProgress)
                return;
            SetImageDrawable(PreloaderImage);
            StartAnimation(_rotationAnimation);
            InProgress = true;
        }
        
        public void StopProgressAnimation()
        {
            if (!InProgress)
                return;
            SetImageDrawable(Image);
            ClearAnimation();
            _rotationAnimation.Cancel();
            InProgress = false;
        }
    }
}
