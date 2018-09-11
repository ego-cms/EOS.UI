using System;
using Airbnb.Lottie;
using CoreAnimation;
using CoreGraphics;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Helpers;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Helpers;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using UIKit;

namespace EOS.UI.iOS.Controls
{
    [Register("FabProgress")]
    public class FabProgress : UIButton, IEOSThemeControl
    {
        //image padding percent
        private const double _paddingRatio = 0.27;
        private const float _startScale = 0.85f;
        private const float _endScale = 1.0f;
        private const double _scaleAnimationDuration = 0.1;
        private const string _snakeAnimationKey = "Animations/preloader-snake";
        private LOTAnimationView _snakeAnimation;
        private UIView _animationView;

        public bool IsEOSCustomizationIgnored { get; private set; }

        private UIColor _backgroundColor;
        public override UIColor BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                if (Enabled)
                    base.BackgroundColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private UIColor _pressedBackgroundColor;
        public UIColor PressedBackgroundColor
        {
            get => _pressedBackgroundColor;
            set
            {
                _pressedBackgroundColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private UIColor _disabledBackgroundColor;
        public UIColor DisabledBackgroundColor
        {
            get => _disabledBackgroundColor;
            set
            {
                _disabledBackgroundColor = value;
                if (!Enabled)
                    base.BackgroundColor = _disabledBackgroundColor;
                IsEOSCustomizationIgnored = true;
            }
        }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                base.BackgroundColor = value ? BackgroundColor : DisabledBackgroundColor;
                base.ImageView.TintColor = value ?
                    GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6S) :
                    GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3S);

                SetShadowConfig(Enabled ? _shadowConfig : null);
            }
        }

        public override bool Highlighted
        {
            get => base.Highlighted;
            set
            {
                base.Highlighted = false;
                base.BackgroundColor = value ? PressedBackgroundColor : BackgroundColor;
            }
        }

        private UIImage _image;
        public UIImage Image
        {
            get => _image;
            set
            {
                _image = value?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                SetImage(_image);
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
                IsEOSCustomizationIgnored = true;
                SetShadowConfig(Enabled ? _shadowConfig : null);
            }
        }

        private bool _inProgress;
        public bool InProgress
        {
            get => _inProgress;
            private set
            {
                _inProgress = value;
                UserInteractionEnabled = !value;
            }
        }

        #region .ctors

        public FabProgress()
        {
            Initialize();
        }

        public FabProgress(UIButtonType type) : base(type)
        {
            Initialize();
        }

        public FabProgress(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public FabProgress(NSObjectFlag t) : base(t)
        {
            Initialize();
        }

        public FabProgress(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public FabProgress(CGRect frame) : base(frame)
        {
            Initialize();
        }

        #endregion

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            Layer.CornerRadius = Frame.Width / 2;
        }

        private void SetShadowConfig(ShadowConfig config)
        {
            if (config != null)
            {
                Layer.ShadowColor = config.Color.CGColor;
                Layer.ShadowOffset = new CGSize(config.Offset);
                Layer.ShadowRadius = config.Blur / 2;
                Layer.ShadowOpacity = 1.0f;//(float)config.Color.CGColor.Alpha;
            }
            else
            {
                Layer.ShadowOpacity = 0.0f;
            }
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
            if (!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                BackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                PressedBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColorVariant1);
                DisabledBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor4S);
                PreloaderColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6S);
                Image = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.CalendarImage));
                ShadowConfig = provider.GetEOSProperty<ShadowConfig>(this, EOSConstants.FabShadow);
                Enabled = Enabled;
                IsEOSCustomizationIgnored = false;
            }
        }

        public void StartProgressAnimation()
        {
            if (InProgress)
                return;
            InProgress = true;
            UpdateAnimationFrame();
            ImageView.Hidden = true;
            _animationView.Hidden = false;
            _snakeAnimation.Play();
        }

        public void StopProgressAnimation()
        {
            if (!InProgress)
                return;
            InProgress = false;
            ImageView.Hidden = false;
            _animationView.Hidden = true;
            _snakeAnimation.Stop();
        }

        private void UpdateImageInsets()
        {
            var padding = (nfloat)(Frame.Width * _paddingRatio);
            var insets = new UIEdgeInsets(padding, padding, padding, padding);
            ImageEdgeInsets = insets;
        }

        private void SetImage(UIImage image)
        {
            base.SetImage(image, UIControlState.Normal);
            VerticalAlignment = UIControlContentVerticalAlignment.Fill;
            HorizontalAlignment = UIControlContentHorizontalAlignment.Fill;
            ContentMode = UIViewContentMode.ScaleToFill;
            UpdateImageInsets();
        }

        private void Initialize()
        {
            TouchDown += (sender, e) => ScaleButton(_startScale, _scaleAnimationDuration);
            TouchUpInside += (sender, e) => ScaleButton(_endScale, _scaleAnimationDuration);
            TouchDragExit += (sender, e) => ScaleButton(_endScale, _scaleAnimationDuration);

            _snakeAnimation = LOTAnimationView.AnimationNamed(_snakeAnimationKey);
            _snakeAnimation.LoopAnimation = true;
            _animationView = new UIView()
            {
                Frame = new CGRect(0, 0, 0, 0),
                BackgroundColor = UIColor.Clear,
                Hidden = true
            };
            _animationView.AddSubview(_snakeAnimation);
            AddSubview(_animationView);

            UpdateAppearance();
            ImageView.TintColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6S);
            AdjustsImageWhenDisabled = false;
        }

        private void ScaleButton(float scale, double duration)
        {
            UIView.Animate(duration, () =>
            {
                Transform = CGAffineTransform.MakeScale(scale, scale);
            });
        }

        private void UpdateAnimationFrame()
        {
            var padding = (nfloat)(_paddingRatio * Frame.Width);
            var heightWidth = Frame.Height - padding * 2;
            var x = (Frame.Width / 2) - heightWidth / 2;
            var y = padding;
            var newFrame = new CGRect(x, y, heightWidth, heightWidth);
            _animationView.Frame = newFrame;
            _snakeAnimation.Frame = _animationView.Bounds;
        }
    }
}
