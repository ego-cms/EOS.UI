using System;
using System.Collections.Generic;
using CoreAnimation;
using CoreGraphics;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Controls
{
    [Register("CTAButton")]
    public class CTAButton : SimpleButton, IEOSThemeControl
    {

        private CABasicAnimation _rotationAnimation;
        private const string _rotationAnimationKey = "rotationAnimation";
        private const double _360degrees = 6.28319;//value in radians
        private Dictionary<UIControlState, NSAttributedString> _attributedTitles = new Dictionary<UIControlState, NSAttributedString>();
        private const double _verticalPaddingRatio = 0.25;
        
        
        public StateEnum ButtonState { get; set; }

        private bool _inProgress;
        public bool InProgress
        {
            get => _inProgress;
            set
            {
                _inProgress = value;
                UserInteractionEnabled = !value;
            }
        }

        public bool Success { get; set; }

        public bool Failed { get; set; }

        public bool DisableDefaultAfterProgress { get; set; }

        public UIColor SuccessColor { get; set; }

        public UIColor FailedColor { get; set; }

        public string SuccessText { get; set; }

        public string FailedText { get; set; }

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
        
        public CTAButton()
        {
            _rotationAnimation = new CABasicAnimation();
            _rotationAnimation.KeyPath = "transform.rotation.z";
            _rotationAnimation.From = new NSNumber(0);
            _rotationAnimation.To = new NSNumber(_360degrees);
            _rotationAnimation.Duration = 1;
            _rotationAnimation.Cumulative = true;
            _rotationAnimation.RepeatCount = Int32.MaxValue;
            ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
        }


        public override void UpdateAppearance()
        {
            base.UpdateAppearance();
            var provider = GetThemeProvider();
            PreloaderImage = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.FabProgressPreloaderImage));
            IsEOSCustomizationIgnored = false;
        }

        public void StartProgressAnimation()
        {
            InProgress = true;
            SaveTitles();
            SetTitle(string.Empty, UIControlState.Normal);
            SetImage(PreloaderImage);
            ImageView.Layer.AddAnimation(_rotationAnimation, _rotationAnimationKey);
        }
        
        public void StopProgressAnimation()
        {
            ImageView.Layer.RemoveAnimation(_rotationAnimationKey);
            ClearImage();
            RestoreTitles();
            InProgress = false;
        }
        
        private void SetImage(UIImage image)
        {
            base.SetImage(image, UIControlState.Normal);
            VerticalAlignment = UIControlContentVerticalAlignment.Fill;
            HorizontalAlignment = UIControlContentHorizontalAlignment.Fill;
            var padding =(nfloat)(_verticalPaddingRatio * Frame.Height);
            ImageEdgeInsets = new UIEdgeInsets(padding, 0, padding, 0);
        }
        
        private void ClearImage()
        {
            base.SetImage(null, UIControlState.Normal);
            VerticalAlignment = UIControlContentVerticalAlignment.Center;
            HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
        }
        
        private void SaveTitles()
        {
            _attributedTitles.Clear();
            _attributedTitles.Add(UIControlState.Normal, GetAttributedTitle(UIControlState.Normal));
            _attributedTitles.Add(UIControlState.Disabled, GetAttributedTitle(UIControlState.Disabled));
            _attributedTitles.Add(UIControlState.Highlighted, GetAttributedTitle(UIControlState.Highlighted));
        }
        
        private void RestoreTitles()
        {
            SetAttributedTitle(_attributedTitles[UIControlState.Normal], UIControlState.Normal);
            SetAttributedTitle(_attributedTitles[UIControlState.Disabled], UIControlState.Disabled);
            SetAttributedTitle(_attributedTitles[UIControlState.Highlighted], UIControlState.Highlighted);
        }
    }
}
