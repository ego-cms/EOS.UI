using System;
using System.Threading.Tasks;
using CoreGraphics;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Helpers;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Controls
{
    [Register("FabProgress")]
    public class FabProgress : UIButton, IEOSThemeControl
    {
        private bool _isOpen;

        public bool IsEOSCustomizationIgnored { get; private set; }

        private UIColor _backgroundColor;
        public override UIColor BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                if(Enabled)
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
            }
        }

        public override bool Highlighted
        {
            get => base.Highlighted;
            set
            {
                base.Highlighted = value;
                base.BackgroundColor = value ? PressedBackgroundColor : BackgroundColor;
            }
        }

        private UIImage _image;
        public UIImage Image
        {
            get => _image;
            set
            {
                _image = value;
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
                _preloaderImage = value;
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
                UpdateSize();
                UpdateImageInsets();
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
				SetShadowConfig(_shadowConfig);
			}
		}

        public FabProgress()
        {
            TouchUpInside += (sender, e) =>
            {
                if (!_isOpen)
                    OpenAnimate();
                else
                    CloseAnimate();
            };
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
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {
        }

        public void UpdateAppearance()
        {
            if (!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                BackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.FabProgressPrimaryColor);
                PressedBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.FabProgressPressedColor);
                DisabledBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.FabProgressDisabledColor);
                Image = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.CalendarImage));
                PreloaderImage = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.FabProgressPreloaderImage));
                ButtonSize = provider.GetEOSProperty<int>(this, EOSConstants.FabProgressSize);
                ShadowConfig = provider.GetEOSProperty<ShadowConfig>(this, EOSConstants.FabShadow);
                IsEOSCustomizationIgnored = false;
            }
        }

        private void UpdateSize()
        {
            Layer.MasksToBounds = false;
            Frame = Frame.ResizeRect(height: ButtonSize, width: ButtonSize);
            Layer.CornerRadius = ButtonSize / 2;
        }

        private async void OpenAnimate()
        {
            BeginAnimations("a1");
            SetAnimationDuration(0.1);
            SetImage(PreloaderImage);
            Transform = CGAffineTransform.MakeScale(0.9f, 0.9f);
            CommitAnimations();
            await Task.Delay(100);
            BeginAnimations("a2");
            SetAnimationDuration(0.2);
            Transform = CGAffineTransform.MakeScale(2f, 2f);
            Transform = CGAffineTransform.MakeScale(1, 1);
            Transform = CGAffineTransform.MakeRotation(3.14f);
            CommitAnimations();
            _isOpen = true;
        }

        private async void CloseAnimate()
        {
            BeginAnimations("a3");
            SetAnimationDuration(0.1);
            Transform = CGAffineTransform.MakeScale(0.9f, 0.9f);
            CommitAnimations();
            await Task.Delay(100);
            BeginAnimations("a4");
            SetAnimationDuration(0.2);
            Transform = CGAffineTransform.MakeScale(2f, 2f);
            Transform = CGAffineTransform.MakeScale(1, 1);
            Transform = CGAffineTransform.MakeRotation(0f);
            CommitAnimations();
            await Task.Delay(150);
            SetImage(Image);
            _isOpen = false;
        }

        private void UpdateImageInsets()
        {
            var padding =(nfloat)(ButtonSize * 0.15);
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

		private void SetShadowConfig(ShadowConfig config)
        {
            Layer.ShadowColor = config.Color;
            Layer.ShadowOffset = config.Offset;
            Layer.ShadowRadius = config.Radius;
            Layer.ShadowOpacity = config.Opacity;
            IsEOSCustomizationIgnored = true;
        }
    }
}
