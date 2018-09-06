using System;
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
        private const string _rotationAnimationKey = "rotationAnimation";
        private const double _360degrees = 6.28319;//value in radians
        private const float _startScale = 0.85f;
        private const float _endScale = 1.0f;
        private const double _animationDuration = 0.1;

        private CABasicAnimation _rotationAnimation;

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

        private UIImage _preloaderImage;
        public UIImage PreloaderImage
        {
            get => _preloaderImage;
            set
            {
                _preloaderImage = value?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
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
                Image = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.CalendarImage));
                PreloaderImage = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.FabProgressPreloaderImage));
                ShadowConfig = provider.GetEOSProperty<ShadowConfig>(this, EOSConstants.FabShadow);
                Enabled = Enabled;
                IsEOSCustomizationIgnored = false;
            }
        }

        public void StartProgressAnimation()
        {
            if (InProgress)
                return;
            SetImage(PreloaderImage);
            ImageView.Layer.AddAnimation(_rotationAnimation, _rotationAnimationKey);
            InProgress = true;
        }

        public void StopProgressAnimation()
        {
            if (!InProgress)
                return;
            ImageView.Layer.RemoveAnimation(_rotationAnimationKey);
            SetImage(Image);
            InProgress = false;
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
            TouchDown += (sender, e) => ScaleButton(_startScale, _animationDuration);
            TouchUpInside += (sender, e) => ScaleButton(_endScale, _animationDuration);
            TouchDragExit += (sender, e) => ScaleButton(_endScale, _animationDuration);

            _rotationAnimation = new CABasicAnimation();
            _rotationAnimation.KeyPath = "transform.rotation.z";
            _rotationAnimation.From = new NSNumber(0);
            _rotationAnimation.To = new NSNumber(_360degrees);
            _rotationAnimation.Duration = 1;
            _rotationAnimation.Cumulative = true;
            _rotationAnimation.RepeatCount = Int32.MaxValue;
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
    }
}
