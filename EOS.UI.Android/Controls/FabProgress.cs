using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using EOS.UI.Android.Helpers;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Controls
{
    public class FabProgress : ImageButton, IEOSThemeControl, View.IOnTouchListener
    {
        private const int _animationDuration = 100;
        private const float _startScale = 0.85f;
        private const float _endScale = 1.0f;
        private const int _startPadding = 30;
        private const double _paddingRatio = 0.24;
        private Animation _rotationAnimation;
        private const int _cornerRadius = 200;
        

        public bool IsEOSCustomizationIgnored { get; private set; }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                (Background as GradientDrawable).SetColor(value ? BackgroundColor : DisabledBackgroundColor);
            }
        }

        private Color _backgroundColor;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                if (Enabled)
                    (Background as GradientDrawable).SetColor(value);
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
                if (!Enabled)
                    (Background as GradientDrawable).SetColor(DisabledBackgroundColor);
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
                var padding = (int)System.Math.Round(_paddingRatio * _buttonSize);
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
        
        private ShadowConfig _shadowConfig;
        public ShadowConfig ShadowConfig
        {
            get => _shadowConfig;
            set
            {
                _shadowConfig = value;
                IsEOSCustomizationIgnored = true;
                //this.SetElevation(_shadowConfig.Radius, _shadowConfig.Color, _shadowConfig.Offset.X, _shadowConfig.Offset.Y);
            }
        }

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

        private void Initialize()
        {
            _rotationAnimation = AnimationUtils.LoadAnimation(Application.Context, Resource.Animation.FabRotationAnimation);
            SetOnTouchListener(this);
            UpdateAppearance();
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
                BackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.FabProgressPrimaryColor);
                DisabledBackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.FabProgressDisabledColor);
                PressedBackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.FabProgressPressedColor);
                SetPadding(_startPadding, _startPadding, _startPadding, _startPadding);
                IsEOSCustomizationIgnored = false;

                //var config = new ShadowConfig()
                //{
                //    Color = Color.Black,
                //    Offset = new Offset(0,0),
                //    Radius = 2,
                //    Opacity = 50
                //};
                //ShadowConfig = config;
            }
        }

        public bool OnTouch(View v, MotionEvent e)
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
                }
            }
            return false;
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
        
        private void SetShadow(ShadowConfig config)
        {
            GradientDrawable shadow = null;
            
            var colors1 = new[] { config.Color.ToArgb(), config.Color.ToArgb() };
            shadow = new GradientDrawable(GradientDrawable.Orientation.TopBottom, colors1);
            shadow.Alpha = config.Opacity;
            shadow.SetCornerRadius(_cornerRadius);
            
            var colors = new[] { BackgroundColor.ToArgb(), BackgroundColor.ToArgb()};
            var backColor = new GradientDrawable(GradientDrawable.Orientation.TopBottom, colors);
            backColor.SetCornerRadius(_cornerRadius);

            Drawable[] layers = new Drawable[2];
            layers[0] = shadow;
            layers[1] = backColor;

            LayerDrawable layerList = new LayerDrawable(layers);
            layerList.SetLayerInset(0, 0, 0, 0, 0);
            layerList.SetLayerInset(1, 0 - config.Offset.X + config.Radius, config.Offset.Y + config.Radius, config.Offset.X+config.Radius, 0 - config.Offset.Y+config.Radius);
            
            
            LayoutParameters.Width = 110 + config.Radius;
            LayoutParameters.Height = 110 + config.Radius;
            SetScaleType(ScaleType.FitCenter);
            
            var paddings = (int)(_paddingRatio * 110) + config.Radius;
            SetPadding(paddings, paddings, paddings, paddings);
            SetBackgroundDrawable(layerList);
        }
        
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            //LayoutParameters.Width = 170+20;
            //LayoutParameters.Height = 170+20;
            //SetScaleType(ScaleType.FitCenter);
            //var padding = (int)System.Math.Round((_paddingRatio * 100)+20);
            //SetPadding(padding, padding, padding, padding);
        }
    }
}
