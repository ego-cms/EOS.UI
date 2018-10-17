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
                TintColor = value ? EnabledImageColor : DisabledImageColor;
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
        public bool InProgress { get; private set; }

        private string _lottieAnimationKey;
        public string LottieAnimationKey
        {
            get => _lottieAnimationKey;
            set
            {
                _lottieAnimationKey = value;
                if (LottieAnimation != null)
                {
                    LottieAnimation.RemoveFromSuperview();
                }
                LottieAnimation = LOTAnimationView.AnimationNamed(_lottieAnimationKey);
                LottieAnimation.LoopAnimation = true;
                _animationView.AddSubview(LottieAnimation);
            }
        }

        public LOTAnimationView LottieAnimation { get; private set; }

        public override CGRect Frame
        {
            get => base.Frame;
            set
            {
                base.Frame = value;
                if (value != CGRect.Empty)
                {
                    UpdateAnimationFrame();
                    UpdateImageInsets();
                }
            }
        }

        private UIColor _enabledImageColor;
        public UIColor EnabledImageColor
        {
            get => _enabledImageColor;
            set
            {
                _enabledImageColor = value;
                IsEOSCustomizationIgnored = true;
                if (Enabled)
                {
                    TintColor = _enabledImageColor;
                }
            }
        }

        private UIColor _disabledImageColor;
        public UIColor DisabledImageColor
        {
            get => _disabledImageColor;
            set
            {
                _disabledImageColor = value;
                IsEOSCustomizationIgnored = true;
                if (!Enabled)
                {
                    TintColor = _disabledImageColor;
                }
            }
        }

        public override CGSize IntrinsicContentSize
        {
            get
            {
                if (Image != null)
                {
                    var imageSize = Math.Max(Image.Size.Width, Image.Size.Height);
                    var side = imageSize * (1 - 2 * _paddingRatio) / _paddingRatio;
                    return new CGSize(side, side);
                }
                else
                {
                    return base.IntrinsicContentSize;
                }
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
            UpdateImageInsets();
        }

        private void SetShadowConfig(ShadowConfig config)
        {
            if (config != null)
            {
                Layer.ShadowColor = config.Color.CGColor;
                Layer.ShadowOffset = new CGSize(config.Offset);
                Layer.ShadowRadius = config.Blur / 2;
                Layer.ShadowOpacity = 1.0f;
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
                ShadowConfig = provider.GetEOSProperty<ShadowConfig>(this, EOSConstants.FabShadow);
                LottieAnimationKey = provider.GetEOSProperty<string>(this, EOSConstants.LottiePreloaderKey);
                EnabledImageColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6S);
                DisabledImageColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3S);
                IsEOSCustomizationIgnored = false;
            }
        }

        public void StartLottieAnimation()
        {
            if (InProgress)
                return;
            InProgress = true;
            UpdateAnimationFrame();
            ImageView.Hidden = true;
            _animationView.Hidden = false;
            LottieAnimation.Play();
        }

        public void StopLottieAnimation()
        {
            if (!InProgress)
                return;
            InProgress = false;
            ImageView.Hidden = false;
            _animationView.Hidden = true;
            LottieAnimation.Stop();
        }

        public override void MovedToSuperview()
        {
            base.MovedToSuperview();
            UpdateFrames();
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            //Should call these methods from AwakeFromNib
            //Otherwise iOS designers ignores any customization
            SetEmptyTitle();
            UpdateFrames();
            SetImage(_image);
        }

        public override void SetTitle(string title, UIControlState forState)
        {
            //you cant set any text for this button
        }

        public override void SetAttributedTitle(NSAttributedString title, UIControlState state)
        {
            //you cant set any text for this button
        }

        private void UpdateFrames()
        {
            UpdateImageInsets();
            UpdateAnimationFrame();
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
            TintColor = Enabled ? EnabledImageColor : DisabledImageColor;
            UpdateImageInsets();
        }

        private void Initialize()
        {
            TouchDown += (sender, e) => ScaleButton(_startScale, _scaleAnimationDuration);
            TouchUpInside += (sender, e) => ScaleButton(_endScale, _scaleAnimationDuration);
            TouchDragExit += (sender, e) => ScaleButton(_endScale, _scaleAnimationDuration);
            _animationView = new UIView()
            {
                Frame = CGRect.Empty,
                BackgroundColor = UIColor.Clear,
                Hidden = true
            };
            AddSubview(_animationView);
            UpdateAppearance();
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
            LottieAnimation.Frame = _animationView.Bounds;
        }

        private void SetEmptyTitle()
        {
            base.SetTitle(String.Empty, UIControlState.Normal);
            base.SetTitle(String.Empty, UIControlState.Highlighted);
            base.SetTitle(String.Empty, UIControlState.Disabled);
            base.SetTitle(String.Empty, UIControlState.Focused);
            base.SetTitle(String.Empty, UIControlState.Application);
            base.SetTitle(String.Empty, UIControlState.Reserved);
            base.SetTitle(String.Empty, UIControlState.Selected);
        }
    }
}
