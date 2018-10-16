using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Com.Airbnb.Lottie;
using EOS.UI.Droid.Helpers;
using EOS.UI.Droid.Themes;
using EOS.UI.Shared.Helpers;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;

namespace EOS.UI.Droid.Controls
{
    public class FabProgress : AppCompatImageButton, IEOSThemeControl
    {
        private const int _animationDuration = 100;
        private const float _startScale = 1f;
        private const float _endScale = 0.9f;
        private const int _startPadding = 30;
        private const int _cornerRadius = 1000;
        private const int _shadowLayerIndex = 0;
        private const int _backgroundLayerIndex = 1;
        private const int _imageLayerIndex = 2;
        private const float _pivot = 0.5f;
        private const string AnimationKey = "Animations/preloader-snake.json";

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

        public LottieDrawable LottieAnimation { get; set; }

        private string _lottieAnimationKey;
        public string LottieAnimationKey
        {
            get => _lottieAnimationKey;
            set
            {
                _lottieAnimationKey = value;
                LottieAnimation = CreateLottieAnimationDrawable(_lottieAnimationKey);
            }
        }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                var wasEnabled = base.Enabled;
                base.Enabled = value;

                if(wasEnabled != value)
                {
                    ToggleShadow(value);
                    SetBackgroundColor(value ? BackgroundColor : DisabledBackgroundColor);
                    ChangeImageColor(value ? EnabledImageColor : DisabledImageColor);
                }
            }
        }

        private void ToggleShadow(bool value)
        {
            var layer = Background as LayerDrawable;
            if (HasShadowDrawable(layer))
            {
                var drawable = layer.GetDrawable(_shadowLayerIndex);
                drawable.Alpha = value ? 255 : 0;
                if (value)
                {
                    SetShadow(ShadowConfig);
                }
                layer.Mutate();
                layer.InvalidateSelf();
            }
        }

        private void ChangeImageColor(Color color)
        {
            var layer = Background as LayerDrawable;
            if (HasShadowDrawable(layer))
            {
                var drawable = layer.GetDrawable(_imageLayerIndex);
                drawable.SetColorFilter(color, PorterDuff.Mode.SrcIn);
                layer.Mutate();
                layer.InvalidateSelf();
            }
            else
            {
                Image.SetColorFilter(color, PorterDuff.Mode.SrcIn);
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
                if(!Enabled)
                    SetBackgroundColor(value);

                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _enabledImageColor;
        public Color EnabledImageColor
        {
            get => _enabledImageColor;
            set
            {
                _enabledImageColor = value;
                if(Enabled)
                    ChangeImageColor(_enabledImageColor);
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _disabledImageColor;
        public Color DisabledImageColor
        {
            get => _disabledImageColor;
            set
            {
                _disabledImageColor = value;
                if(!Enabled)
                    ChangeImageColor(_disabledImageColor);
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
                ChangeImageColor(Enabled ? EnabledImageColor : DisabledImageColor);
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
                if (Enabled)
                {
                    SetShadowOrBackground(value != null, value);
                }
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
            if (_shadowConfig != null && _initialWidth > 0)
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

        private void Initialize(IAttributeSet attrs = null)
        {
            if(Id == -1)
                Id = Guid.NewGuid().GetHashCode();

            LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            if(attrs != null)
                InitializeAttributes(attrs);

            UpdateAppearance();

            try
            {
                LottieAnimation = CreateLottieAnimationDrawable(AnimationKey);
            }
            catch(Exception ex)
            {
                var m = ex.Message;
            }
            LottieAnimation.Loop(true);
            LottieAnimation.Scale = (Image.IntrinsicWidth * Resources.DisplayMetrics.Density) / LottieAnimation.IntrinsicWidth;
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
                ShadowConfig = provider.GetEOSProperty<ShadowConfig>(this, EOSConstants.FabShadow);

                //Should initialize after ShadowConfig
                //ShadowConfig method checks and background drawable which should be used for color.
                BackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColor);
                DisabledBackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.NeutralColor4S);
                PressedBackgroundColor = provider.GetEOSProperty<Color>(this, EOSConstants.BrandPrimaryColorVariant1);

                EnabledImageColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor6S);
                DisabledImageColor = GetThemeProvider().GetEOSProperty<Color>(this, EOSConstants.NeutralColor3S);

                IsEOSCustomizationIgnored = false;
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if(Enabled && IsNotShadowClick(e))
            {
                if(e.Action == MotionEventActions.Down)
                {
                    _touchIsDown = true;
                    var animation = StartTouchAnimation(_startScale, _endScale);
                    animation.AnimationEnd += delegate
                    {
                        SetBackgroundColor(PressedBackgroundColor);
                    };
                    StartAnimation(StartTouchAnimation(_startScale, _endScale));
                }
                if(e.Action == MotionEventActions.Up || e.Action == MotionEventActions.Cancel)
                {
                    var animation = StartTouchAnimation(_endScale, _startScale);
                    animation.AnimationEnd += delegate
                    {
                        SetActionUpUIStyle();
                        PerformClick();
                    };
                    StartAnimation(animation);
                }
            }
            //return in previous state if button is pressed outside its content
            if(_touchIsDown && e.Action == MotionEventActions.Up || e.Action == MotionEventActions.Cancel)
            {
                SetActionUpUIStyle();
            }

            return true;
        }

        private ScaleAnimation StartTouchAnimation(float startScale, float endScale)
        {
            var scaleInAnimation = new ScaleAnimation(startScale, endScale, startScale, endScale, Dimension.RelativeToSelf, _pivot, Dimension.RelativeToSelf, _pivot)
            {
                Duration = _animationDuration,
                FillAfter = true
            };
            return scaleInAnimation;
        }

        private void SetActionUpUIStyle()
        {
            _touchIsDown = false;
            SetBackgroundColor(BackgroundColor);
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

        public void StartLottieAnimation()
        {
            if (InProgress)
                return;

            StartLottie();
            InProgress = true;
        }

        public void StopLottieAnimation()
        {
            if (!InProgress)
                return;

            StopLottie();
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

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            //if shadow is exist image should be resetted 
            base.OnSizeChanged(w, h, oldw, oldh);
            SetImageDrawable(Image);
            ChangeImageColor(Enabled ? EnabledImageColor : DisabledImageColor);
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

            Drawable[] layers = new Drawable[3];
            layers[_shadowLayerIndex] = new UpdatedCircleShadowDrawable(config);
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
                var magic = blur > Math.Abs(offsetX) ? blur - Math.Abs(offsetX) : 0;
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
            var color = Enabled ? BackgroundColor.ToArgb() : DisabledBackgroundColor.ToArgb();
            var colors = new[] { color, color };
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
            if(drawable == null)
            {
                base.SetImageDrawable(null);
                return;
            }

            var layer = Background as LayerDrawable;
            if(HasShadowDrawable(layer))
            {
                //Should use this method instead of GetDrawable for compatibility with API <23
                layer.SetDrawableByLayerId(_imageLayerIndex, drawable);

                var densityOffsetX = (int)Helpers.Helpers.DpToPx(ShadowConfig.Offset.X);
                //Wrong implementation for y offset should be inverted
                //TODO need to fix
                var densityOffsetY = (int)Helpers.Helpers.DpToPx(ShadowConfig.Offset.Y) * -1;
                var densityBlur = (int)Helpers.Helpers.DpToPx(ShadowConfig.Blur);
                SetInsetForImageLayer(layer, drawable, _initialWidth / 2, densityOffsetX, densityOffsetY, densityBlur);
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

        #region Lottie animation

        private LottieDrawable CreateLottieAnimationDrawable(string animationKey)
        {
            var animationDrawable = new LottieDrawable();
            var result = (LottieComposition)LottieCompositionFactory.FromAssetSync(Context, animationKey).Value;
            animationDrawable.SetComposition(result);
            return animationDrawable;
        }

        private void StartLottie()
        {
            //clearing drawable if shadow is exist
            SetImageDrawable(new ColorDrawable(Color.Transparent));

            base.SetImageDrawable(LottieAnimation);

            //updating paddings according configuration shadow            
            base.SetPadding(0, 0, 2 * (int)(ShadowConfig.Offset.X * Resources.DisplayMetrics.Density), 2 * (int)(ShadowConfig.Offset.Y * Resources.DisplayMetrics.Density));

            LottieAnimation.PlayAnimation();
        }

        private void StopLottie()
        {
            LottieAnimation.Stop();
            //clear lottie animation
            base.SetImageDrawable(null);
            SetImageDrawable(Image);
            ChangeImageColor(Enabled ? EnabledImageColor : DisabledImageColor);
        }

        #endregion
    }
}
