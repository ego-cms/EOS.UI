using System;
using System.Threading.Tasks;
using CoreGraphics;
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
        private UIColor _normalBackgroundColor;
        private UIImage _normalImage;

        public bool IsEOSCustomizationIgnored { get; private set; }

        public override UIColor BackgroundColor
        {
            get => base.BackgroundColor;
            set
            {
                base.BackgroundColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private UIColor _pressedColor;
        public UIColor PressedColor
        {
            get => _pressedColor;
            set
            {
                _pressedColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private UIColor _disabledColor;
        public UIColor DisabledColor
        {
            get => _disabledColor;
            set
            {
                _disabledColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                if (DisabledColor != null)
                {
                    BackgroundColor = DisabledColor;
                }
            }
        }

        public override bool Highlighted
        {
            get => base.Highlighted;
            set
            {
                base.Highlighted = value;
                if (value)
                    _normalBackgroundColor = BackgroundColor;
                BackgroundColor = value ? PressedColor : _normalBackgroundColor;
            }
        }

        private UIImage _image;
        public UIImage Image
        {
            get => _image;
            set
            {
                _normalImage = _image;
                _image = value;
                SetImage(_image, UIControlState.Normal);
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
                IsEOSCustomizationIgnored = true;
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
                PressedColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.FabProgressPressedColor);
                DisabledColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.FabProgressDisabledColor);
                Image = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.CalendarImage));
                PreloaderImage = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.FabProgressPreloaderImage));
                ButtonSize = provider.GetEOSProperty<int>(this, EOSConstants.FabProgressSize);
                IsEOSCustomizationIgnored = false;
            }
        }

        private void UpdateSize()
        {
            Layer.MasksToBounds = false;
            Layer.CornerRadius = ButtonSize / 2;
            Layer.ShadowColor = UIColor.Black.CGColor;
            Layer.ShadowOffset = new CGSize(0, 0);
            Layer.ShadowRadius = 5;
            Layer.ShadowOpacity = 0.2f;
        }

        private async void OpenAnimate()
        {
            BeginAnimations("a1");
            SetAnimationDuration(0.1);
            Image = PreloaderImage;
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
            Image = _normalImage;
            _isOpen = false;
        }
    }
}
