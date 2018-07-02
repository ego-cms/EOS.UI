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
        //flag set when width recalculated to including shadows. 
        //When flag is set button will ignore setting new width and will biuld it's shadow based on an old one.
        private bool _shadowRecalculatedWidth;
        //Initial x-y position of control. Altered by shadow property
        private float? _initialXPosition;
        private float? _initialYPosition;
        bool _shouldShadowDrawingWaitForLayout = false;
        bool _touchIsDown;

        public bool IsEOSCustomizationIgnored { get; private set; }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                if (base.Enabled != value)
                {
                    SetShadowOrBackground(value, ShadowConfig);
                    SetBackgroundColor(value ? BackgroundColor : DisabledBackgroundColor);
                    Image.SetColorFilter(value ?
                        GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6) :
                        GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3),
                        PorterDuff.Mode.SrcIn);
                }

                base.Enabled = value;
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
                    SetBackgroundColor(value);
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
                    SetBackgroundColor(value);
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
                IsEOSCustomizationIgnored = true;
                SetShadowOrBackground(value != null, value);
                _shadowConfig = value;
            }
        }

        private void SetShadowOrBackground(bool condition, ShadowConfig config)
        {
            if (condition)
            {
                SetShadow(config);
            }
            else
            {
                SetBackground();
            }
        }

        private void SetBackground()
        {
            if (_shadowConfig != null && _initialWidth>0)
            {
                ResetShadowParameters();
            }
            Background = CreateBackgroundDrawable();
            SetImageDrawable(Image);
            SetPadding(_startPadding, _startPadding, _startPadding, _startPadding);
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

                //Should initialize after ShadowConfig
                //ShadowConfig method checks and background drawable which should be used for color.
                BackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                IsEOSCustomizationIgnored = false;
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (Enabled && IsNotShadowClick(e))
            {
                if (e.Action == MotionEventActions.Down)
                {
                    _touchIsDown = true;
                    SetBackgroundColor(PressedBackgroundColor);
                    Animate().ScaleX(_startScale).ScaleY(_startScale).SetDuration(_animationDuration).Start();
                }
                if (e.Action == MotionEventActions.Up || e.Action == MotionEventActions.Cancel)
                {
                    SetActionUpUIStyle();
                    PerformClick();
                }
            }
            //return in previous state if button is pressed outside its content
            if (_touchIsDown && e.Action == MotionEventActions.Up || e.Action == MotionEventActions.Cancel)
            {
                SetActionUpUIStyle();
            }

            return true;
        }

        private void SetActionUpUIStyle()
        {
            _touchIsDown = false;
            SetBackgroundColor(BackgroundColor);
            Animate().ScaleX(_endScale).ScaleY(_endScale).SetDuration(_animationDuration).Start();
        }

        private bool IsNotShadowClick(MotionEvent e)
        {
            var layer = Background as LayerDrawable;
            if (HasShadowDrawable(layer))
            {
                //Should use this method instead of GetDrawable for compatibility with API <23
                var back = layer.GetDrawable(_backgroundLayerIndex);
                return back.Bounds.Contains((int)e.GetX(), (int)e.GetY());
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

            if (_shadowRecalculatedWidth)
            {
                return;
            }

            if(_initialWidth<=0 || _initialWidth != Width)
                _initialWidth = Width;

            if (_shouldShadowDrawingWaitForLayout && !CheckIfShadowDrawingIsAllowed(_shadowConfig))
            {
                SetShadow(_shadowConfig);
            }
        }

        #region Shadow methods

        //Will fail if config = null.
        private void SetShadow(ShadowConfig config)
        {
            if (CheckIfShadowDrawingIsAllowed(config))
            {
                SetBackground();
                return;
            }

            SetImageDrawable(null);

            var densityOffsetX = (int)Helpers.Helpers.DpToPx(config.Offset.X);
            //Wrong implementation for y offset should be inverted
            //TODO need to fix
            var densityOffsetY = (int)Helpers.Helpers.DpToPx(config.Offset.Y) * -1;
            var densityBlur = (int)Helpers.Helpers.DpToPx(config.Blur);

            var newWidth = RecalculateWidth(densityOffsetX, densityOffsetY, densityBlur);

            var paddings = newWidth / 2;
            SetPadding(paddings, paddings, paddings, paddings);

            Drawable[] layers = new Drawable[4];
            layers[0] = new GradientDrawable(GradientDrawable.Orientation.BlTr, new int[] { Color.Black.ToArgb(), Color.Black.ToArgb() });
            layers[_shadowLayerIndex] = new CircleShadowDrawable(config);
            layers[_backgroundLayerIndex] = CreateBackgroundDrawable();
            layers[_imageLayerIndex] = Image;

            var layerList = CreateLayerList(layers);
            layerList.SetLayerInset(0, 0, 0, 0, 0);
            layerList.SetLayerInset(_shadowLayerIndex, 0, 0, 0, 0);
            SetInsetForBackgroundLayer(densityOffsetX, densityOffsetY, densityBlur, layerList);
            SetInsetForImageLayer(layerList, Image, _initialWidth / 2, densityOffsetX, densityOffsetY, densityBlur);

            Background = layerList;
        }

        private bool CheckIfShadowDrawingIsAllowed(ShadowConfig config)
        {
            var result = config==null || LayoutParameters == null || Width == 0 || config.Blur <= 0;
            _shouldShadowDrawingWaitForLayout = result;
            return result;
        }

        private void SetInsetForBackgroundLayer(int densityOffsetX, int densityOffsetY, int densityBlur, LayerDrawable layerList)
        {
            int insetL = GetInsetLeft(densityOffsetX, densityBlur);
            var insetT = GetInsetTop(densityOffsetY, densityBlur);
            var insetR = GetInsetRight(densityOffsetX, densityBlur);
            var insetB = GetInsetBottom(densityOffsetY, densityBlur);

            layerList.SetLayerInset(_backgroundLayerIndex, insetL, insetT, insetR, insetB);
        }

        private int GetInsetLeft(int densityOffsetX, int densityBlur)
        {
            if (densityOffsetX == 0)
                return densityBlur;

            if (densityOffsetX < 0)
                return densityBlur + Math.Abs(densityOffsetX);
            else
                return densityBlur > densityOffsetX ? densityBlur - densityOffsetX : 0;
        }

        private int GetInsetRight(int densityOffsetX, int densityBlur)
        {
            if (densityOffsetX == 0)
                return densityBlur;
            
            if (densityOffsetX > 0)
                return densityBlur + Math.Abs(densityOffsetX);
            else
                return densityBlur > Math.Abs(densityOffsetX) ? densityBlur - Math.Abs(densityOffsetX) : 0;
        }

        private int GetInsetBottom(int densityOffsetY, int densityBlur)
        {
            if (densityOffsetY == 0)
                return densityBlur;
            
            if (densityOffsetY < 0)
                return densityBlur + Math.Abs(densityOffsetY);
            else
                return densityBlur > densityOffsetY ? densityBlur - Math.Abs(densityOffsetY) : 0;
        }

        private int GetInsetTop(int densityOffsetY, int densityBlur)
        {
            if (densityOffsetY == 0)
                return densityBlur;
            
            if (densityOffsetY > 0)
                return densityBlur + Math.Abs(densityOffsetY);
            else
                return densityBlur > Math.Abs(densityOffsetY) ? densityBlur - Math.Abs(densityOffsetY) : 0;
        }

        //Recalculates width of the view to include shadow
        //Set translation XY for visual feeling that image hasn't change its place
        private int RecalculateWidth(int offsetX, int offsetY, int blur)
        {
            if (blur == 0)
                return _initialWidth;
            
            _shadowRecalculatedWidth = true;

            var newWidth = ShadowHelpers.GetNewWidth(_initialWidth, offsetX, blur);
            var newHeight = ShadowHelpers.GetNewWidth(_initialWidth, offsetY, blur);
            this.SetLayoutParameters(newWidth, newHeight);

            if (offsetX > 0)
            {
                //doesn't understand why it offsets on that value, but still.
                var magic = blur > Math.Abs(offsetX)? blur - Math.Abs(offsetX) : 0;
                TranslationX = newWidth / 2 - _initialWidth/2 - magic;
            }
            if (offsetX < 0)
            {
                TranslationX = newWidth / 2 - _initialWidth / 2 + offsetX - blur;
            }
            if (offsetX == 0)
            {
                //won't do anything when shadow applies at the first time
                SetX(GetInitialX());
            }
            if (offsetY > 0)
            {
                SetY(GetInitialY() + (offsetY + blur) * -1);
            }
            if (offsetY < 0)
            {
                if (blur > Math.Abs(offsetY))
                {
                    SetY(GetInitialY() + (offsetY + blur) * -1);
                }
                else
                {
                    SetY(GetInitialY());
                }
            }
            if (offsetY == 0)
            {
                SetY(GetInitialY() - blur);
            }

            return newWidth;
        }

        private float GetInitialX()
        {
            _initialXPosition = _initialXPosition ?? GetX();
            return _initialXPosition.Value;
        }

        private float GetInitialY()
        {
            _initialYPosition = _initialYPosition ?? GetY();
            return _initialYPosition.Value;
        }

        //return view to it's initial state
        private void ResetShadowParameters()
        {
            this.SetLayoutParameters(_initialWidth, _initialWidth);

            TranslationX = 0;
            TranslationY = 0;
            _initialXPosition = null;
            _initialYPosition = null;
            _shadowRecalculatedWidth = false;
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
            var imagePosition = halfWidth - drawable.IntrinsicWidth / 2;

            var insetL = GetInsetLeft(offsetX, blur) + imagePosition;
            var insetT = GetInsetTop(offsetY, blur) + imagePosition;
            var insetR = GetInsetRight(offsetX, blur) + imagePosition;
            var insetB = GetInsetBottom(offsetY, blur) + imagePosition;
            layerList.SetLayerInset(_imageLayerIndex, insetL, insetT, insetR, insetB);
        }

        #endregion

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
                //Wrong implementation for y offset should be inverted
                //TODO need to fix
                var densityOffsetY = (int)Helpers.Helpers.DpToPx(ShadowConfig.Offset.Y) *-1;
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


        #region Rotation animation

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

        #endregion
    }
}
