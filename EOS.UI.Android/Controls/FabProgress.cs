using System;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
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
    public class FabProgress : ImageButton, IEOSThemeControl
    {
        private const int _animationDuration = 100;
        private const float _startScale = 0.85f;
        private const float _endScale = 1.0f;
        private const int _startPadding = 30;
        private const double _paddingRatio = 0.24;
        private Animation _rotationAnimation;
        private const int _cornerRadius = 200;
        private const int _shadowLayerIndex = 0;
        private const int _backgroundLayerIndex = 1;
        private const int _imageLayerIndex = 2;
        private const int _rotationAnimationDuration = 1000;
        private const float _pivot = 0.5f;
        private int _initialWidth = -1;

        public bool IsEOSCustomizationIgnored { get; private set; }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                SetBackgroundColor(value ? BackgroundColor : DisabledBackgroundColor);
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
                    SetBackgroundColor(value);
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
                    SetBackgroundColor(DisabledBackgroundColor);
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

        //private int _buttonSize;
        //public int ButtonSize
        //{
        //    get => _buttonSize;
        //    set
        //    {
        //        _buttonSize = value;
        //        LayoutParameters.Width = value;
        //        LayoutParameters.Height = value;
        //        SetScaleType(ScaleType.FitCenter);
        //        RequestLayout();
        //        var padding = (int)System.Math.Round(_paddingRatio * _buttonSize);
        //        SetPadding(padding, padding, padding, padding);
        //        IsEOSCustomizationIgnored = true;
        //    }
        //}

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
                if (value != null)
                {
                    SetShadow(value);
                }
                else
                {
                    Background = CreateBackgroundDrawable();
                    SetImageDrawable(Image);
                    SetPadding(_startPadding, _startPadding, _startPadding, _startPadding);
                }
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
            //LayoutParameters.Width = ViewGroup.LayoutParams.WrapContent;
            //LayoutParameters.Height = ViewGroup.LayoutParams.WrapContent;
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
                Image = Resources.GetDrawable(provider.GetEOSProperty<int>(this, EOSConstants.CalendarImage), null);
                PreloaderImage = Resources.GetDrawable(provider.GetEOSProperty<int>(this, EOSConstants.FabProgressPreloaderImage), null);
                //var roundedDrawable = (GradientDrawable)Resources.GetDrawable(Resource.Drawable.FabButton);
                //SetBackgroundDrawable(roundedDrawable);
                DisabledBackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.NeutralColor4);
                PressedBackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColorVariant1);
                ShadowConfig = provider.GetEOSProperty<ShadowConfig>(this, EOSConstants.FabShadow);
                //SetPadding(_startPadding, _startPadding, _startPadding, _startPadding);

                //Should initialize after ShadowConfig
                //ShadowConfig method checks and background drawable which should be used for color.
                BackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                IsEOSCustomizationIgnored = false;
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (Enabled)
            {
                if (e.Action == MotionEventActions.Down)
                {
                    SetBackgroundColor(PressedBackgroundColor);
                    Animate().ScaleX(_startScale).ScaleY(_startScale).SetDuration(_animationDuration).Start();
                }
                if (e.Action == MotionEventActions.Up || e.Action == MotionEventActions.Cancel)
                {
                    SetBackgroundColor(BackgroundColor);
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

            var layer = Background as LayerDrawable;
            if (HasShadowDrawable(layer))
            {
                CreateAndAnimateRotationDrawable();
            }
            else
            {
                SetImageDrawable(PreloaderImage);
                StartAnimation(_rotationAnimation);
            }
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

        public override void Layout(int l, int t, int r, int b)
        {
            base.Layout(l, t, r, b);
            if(_initialWidth<=0 || _initialWidth != Width)
                _initialWidth = Width;
        }

        //Will fail if config = null.
        private void SetShadow(ShadowConfig config)
        {
            if (LayoutParameters == null)
                return;

            SetImageDrawable(null);

            var paddings = _initialWidth / 2;
            SetPadding(paddings, paddings, paddings, paddings);

            Drawable[] layers = new Drawable[3];
            layers[_shadowLayerIndex] = new CircleShadowDrawable(config);
            layers[_backgroundLayerIndex] = CreateBackgroundDrawable();
            layers[_imageLayerIndex] = Image;

            var densityOffsetX = (int)Helpers.Helpers.DpToPx(config.Offset.X);
            var densityOffsetY = (int)Helpers.Helpers.DpToPx(config.Offset.Y);
            var densityOffsetBlur = config.Blur;//(int)Helpers.Helpers.DpToPx(config.Blur);

            var layerList = CreateLayerList(layers);
            layerList.SetLayerInset(_shadowLayerIndex, 0, 0, 0, 0);
            //layerList.SetLayerInset(_backgroundLayerIndex, 0 - config.Offset.X + config.Blur, config.Offset.Y + config.Blur, config.Offset.X + config.Blur, 0 - config.Offset.Y + config.Blur);
            layerList.SetLayerInset(_backgroundLayerIndex, 0 - densityOffsetX + densityOffsetBlur, densityOffsetY + densityOffsetBlur, densityOffsetX + densityOffsetBlur, 0 - densityOffsetY + densityOffsetBlur);
            SetInsetForImageLayer(layerList, Image, paddings, config.Offset);

            Background = layerList;
        }

        private LayerDrawable CreateLayerList(Drawable[] layers)
        {
            //Should add ids for compatibility with API <21
            var ls = new LayerDrawable(layers);
            ls.SetId(_shadowLayerIndex, _shadowLayerIndex);
            ls.SetId(_backgroundLayerIndex, _backgroundLayerIndex);
            ls.SetId(_imageLayerIndex, _imageLayerIndex);
            return ls;
        }

        private GradientDrawable CreateBackgroundDrawable()
        {
            var colors = new[] { BackgroundColor.ToArgb(), BackgroundColor.ToArgb() };
            var backColor = new GradientDrawable(GradientDrawable.Orientation.TopBottom, colors);
            backColor.SetCornerRadius(_cornerRadius);
            return backColor;
        }

        private void SetInsetForImageLayer(LayerDrawable layerList, Drawable drawable, int halfWidth, Offset offset)
        {
            var xOffset = halfWidth - drawable.IntrinsicWidth / 2 - (int)Helpers.Helpers.DpToPx(offset.X);
            var yOffset = halfWidth - drawable.IntrinsicWidth / 2 + (int)Helpers.Helpers.DpToPx(offset.Y);
            var rightOffset = halfWidth - drawable.IntrinsicWidth / 2 + (int)Helpers.Helpers.DpToPx(offset.X);
            var bottomOffset = halfWidth - drawable.IntrinsicWidth / 2 - (int)Helpers.Helpers.DpToPx(offset.Y);
            layerList.SetLayerInset(_imageLayerIndex, xOffset, yOffset, rightOffset, bottomOffset);
        }

        public override void SetBackgroundColor(Color color)
        {
            var layer = Background as LayerDrawable;
            //if layer drawable gets background color layer.
            //Otherwise just set GradientDrawable color.
            if (HasShadowDrawable(layer))
            {
                var drawable = layer.GetDrawable(_backgroundLayerIndex);
                (drawable as GradientDrawable).SetColor(color);
                layer.Mutate();
                layer.InvalidateSelf();
            }
            else
            {
                (Background as GradientDrawable).SetColor(color);
            }
        }

        //if layer drawable gets background color layer.
        //Otherwise just set GradientDrawable color.
        public override void SetImageDrawable(Drawable drawable)
        {
            if (drawable == null)
            {
                base.SetImageDrawable(null);
                return;
            }

            var layer = Background as LayerDrawable;
            if (HasShadowDrawable(layer))
            {
                //Should use this method instead of GetDrawable for compatibility with API <23
                layer.SetDrawableByLayerId(_imageLayerIndex, drawable);
                SetInsetForImageLayer(layer, drawable, Width / 2, ShadowConfig.Offset);
                layer.Mutate();
                layer.InvalidateSelf();
            }
            else
            {
                base.SetImageDrawable(drawable);
            }
        }

        private bool HasShadowDrawable(LayerDrawable layer)
        {
            //By default android has ripple drawable which extended from LayerDrawable.
            //Should check number of layers also.
            return layer != null && layer.NumberOfLayers == 3;
        }


        private void CreateAndAnimateRotationDrawable()
        {
            var d = CreateRotateDrawable(PreloaderImage);
            SetImageDrawable(d);
            var anim = ObjectAnimator.OfInt(d, "level", 0, 10000);
            anim.SetDuration(_rotationAnimationDuration);
            anim.SetInterpolator(new LinearInterpolator());
            anim.RepeatCount = ValueAnimator.Infinite;
            anim.Start();
        }

        private Drawable CreateRotateDrawable(Drawable childDrawable)
        {
            var drawable = new RotateDrawable();
            drawable.Drawable = childDrawable;
            drawable.PivotXRelative = true;
            drawable.PivotX = _pivot;
            drawable.PivotYRelative = true;
            drawable.PivotY = _pivot;
            return drawable;
        }
    }
}
