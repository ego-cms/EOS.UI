using System;
using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using EOS.UI.Android.Helpers;
using EOS.UI.Shared.Helpers;
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
        private const int _cornerRadius = 200;
        private const int _shadowLayerIndex = 1;
        private const int _backgroundLayerIndex = 2;
        private const int _imageLayerIndex = 3;
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

        private void Initialize(IAttributeSet attrs = null)
        {
            if(attrs != null)
                InitializeAttributes(attrs);

            UpdateAppearance();
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
                CreateAndAnimateRotationDrawable();
            }
            InProgress = true;
        }

        public void StopProgressAnimation()
        {
            if (!InProgress)
                return;
            
            SetImageDrawable(Image);
            ClearAnimation();
            InProgress = false;
        }

        public override void Layout(int l, int t, int r, int b)
        {
            base.Layout(l, t, r, b);
            if(_initialWidth<=0)// || _initialWidth != Width)
                _initialWidth = Width;
        }

        //Will fail if config = null.
        private void SetShadow(ShadowConfig config)
        {
            if (LayoutParameters == null)
                return;

            SetImageDrawable(null);

            var densityOffsetX = (int)Helpers.Helpers.DpToPx(config.Offset.X);
            var densityOffsetY = (int)Helpers.Helpers.DpToPx(config.Offset.Y);
            var densityBlur = (int)Helpers.Helpers.DpToPx(config.Blur);

            var newWidth = RecalculateWidth(densityOffsetX, densityOffsetY, densityBlur);

            var paddings = newWidth / 2;
            SetPadding(paddings, paddings, paddings, paddings);

            Drawable[] layers = new Drawable[4];
            layers[0] = new GradientDrawable(GradientDrawable.Orientation.BlTr, new int[] { Color.Black.ToArgb(), Color.Black.ToArgb() });
            layers[_shadowLayerIndex] = new CircleShadowDrawable(config);
            layers[_backgroundLayerIndex] = CreateBackgroundDrawable();
            layers[_imageLayerIndex] = Image;

            //var densityOffsetX = (int)Helpers.Helpers.DpToPx(config.Offset.X);
            //var densityOffsetY = (int)Helpers.Helpers.DpToPx(config.Offset.Y);
            //var densityOffsetBlur = (int)Helpers.Helpers.DpToPx(config.Blur);

            var layerList = CreateLayerList(layers);
            layerList.SetLayerInset(0, 0, 0, 0, 0);
            layerList.SetLayerInset(_shadowLayerIndex, 0, 0, 0, 0);
            //layerList.SetLayerInset(_backgroundLayerIndex, 0 - config.Offset.X + config.Blur, config.Offset.Y + config.Blur, config.Offset.X + config.Blur, 0 - config.Offset.Y + config.Blur);
            //layerList.SetLayerInset(_backgroundLayerIndex, 0 - densityOffsetX + densityOffsetBlur, densityOffsetY + densityOffsetBlur, densityOffsetX + densityOffsetBlur, 0 - densityOffsetY + densityOffsetBlur);
            var insetL = densityOffsetX > 0? 0 : Math.Abs(densityOffsetX) + densityBlur;
            var insetB = densityOffsetY > 0 ? 0 : Math.Abs(densityOffsetY) + densityBlur;
            var insetR = densityOffsetX > 0 ? Math.Abs(densityOffsetX) + densityBlur : 0;
            var insetT = densityOffsetY > 0 ? Math.Abs(densityOffsetY) + densityBlur : 0;


            Console.WriteLine($"l - {insetL}\nt - {insetT}\nr - {insetR}\nb - {insetB}\n");

            layerList.SetLayerInset(_backgroundLayerIndex, insetL, insetT, insetR, insetB);
            SetInsetForImageLayer(layerList, Image, _initialWidth/2, densityOffsetX, densityOffsetY, densityBlur);

            Background = layerList;
        }

        private int RecalculateWidth(int offsetX, int offsetY, int blur)
        {
            if (blur == 0)
                return _initialWidth;

            Console.WriteLine($"Initial:\nWidth - {_initialWidth}\nHeight - {_initialWidth}");

            var lp = LayoutParameters;
            lp.Width = _initialWidth + Math.Abs(offsetX) + blur;
            lp.Height = _initialWidth + Math.Abs(offsetY) + blur;
            LayoutParameters = lp;
            if (offsetX > 0)
            {
                SetX(GetX() + (offsetX + blur) / 2);
            }
            else
            {
                SetX(GetX() + (offsetX + blur * -1) / 2);
            }
            if (offsetY > 0)
            {
                SetY(GetY() - (offsetY + blur));
            }
            else
            {
                //SetY(GetY() - (offsetY + blur * -1) / 2);
            }
            Console.WriteLine($"After:\nWidth - {lp.Width}\nHeight - {lp.Height}");

            return lp.Width;
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

        private void SetInsetForImageLayer(LayerDrawable layerList, Drawable drawable, int halfWidth, int offsetX, int offsetY, int blur)
        {
            //var xOffset = halfWidth - drawable.IntrinsicWidth / 2 + (int)Helpers.Helpers.DpToPx(offset.X);
            //var yOffset = halfWidth - drawable.IntrinsicWidth / 2 + (int)Helpers.Helpers.DpToPx(offset.Y);
            //var rightOffset = halfWidth - drawable.IntrinsicWidth / 2 - (int)Helpers.Helpers.DpToPx(offset.X);
            //var bottomOffset = halfWidth - drawable.IntrinsicWidth / 2 - (int)Helpers.Helpers.DpToPx(offset.Y);
            //layerList.SetLayerInset(_imageLayerIndex, xOffset, yOffset, rightOffset, bottomOffset);
            var imagePosition = halfWidth - drawable.IntrinsicWidth / 2;

            var insetL = offsetX > 0 ? imagePosition : Math.Abs(offsetX) + blur + imagePosition;
            var insetB = offsetY > 0 ? imagePosition : Math.Abs(offsetY) + blur + imagePosition;
            var insetR = offsetX > 0 ? Math.Abs(offsetX) + blur + imagePosition : imagePosition;
            var insetT = offsetY > 0 ? Math.Abs(offsetY) + blur + imagePosition : imagePosition;
            layerList.SetLayerInset(_imageLayerIndex, insetL, insetT, insetR, insetB);
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

                var densityOffsetX = (int)Helpers.Helpers.DpToPx(ShadowConfig.Offset.X);
                var densityOffsetY = (int)Helpers.Helpers.DpToPx(ShadowConfig.Offset.Y);
                var densityBlur = (int)Helpers.Helpers.DpToPx(ShadowConfig.Blur);
                SetInsetForImageLayer(layer, drawable, _initialWidth/2, densityOffsetX, densityOffsetY, densityBlur);
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
            return layer != null && layer.NumberOfLayers == 4;
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
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
                return CreateRotateDrawableAPI23(childDrawable);
            else
                return CreateRotateDrawableAPI21(childDrawable);
        }

        private Drawable CreateRotateDrawableAPI23(Drawable childDrawable)
        {
            var drawable = new RotateDrawable();
            drawable.Drawable = childDrawable;
            drawable.PivotXRelative = true;
            drawable.PivotX = _pivot;
            drawable.PivotYRelative = true;
            drawable.PivotY = _pivot;
            return drawable;
        }

        private Drawable CreateRotateDrawableAPI21(Drawable childDrawable)
        {
            //It's impossible adequate creation from code due
            //https://github.com/aosp-mirror/platform_frameworks_base/blob/lollipop-dev/graphics/java/android/graphics/drawable/RotateDrawable.java#L218
            //use creation from xml hack
            var drawable = (RotateDrawable)Drawable.CreateFromXml(Resources, Resources.GetXml(Resource.Drawable.RotateDrawable));
            drawable.Drawable = childDrawable;
            return drawable;
        }
    }
}
