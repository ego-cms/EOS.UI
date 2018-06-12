using System;
using System.Collections.Generic;
using CoreAnimation;
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
    [Register("SimpleButton")]
    public class SimpleButton : UIButton, IEOSThemeControl
    {
        private CABasicAnimation _rotationAnimation;
        private const string _rotationAnimationKey = "rotationAnimation";
        private const string _rippleAnimationKey = "rippleAnimation";
        private const double _360degrees = 6.28319;//value in radians
        private Dictionary<UIControlState, NSAttributedString> _attributedTitles = new Dictionary<UIControlState, NSAttributedString>();
        private const double _verticalPaddingRatio = 0.25;
        private CAAnimationGroup _rippleAnimations;
        private CALayer _rippleLayer;
        
        #region constructor

        public SimpleButton()
        {
            Initialization();
        }

        #endregion

        #region customization

        private UIFont _font;
        public override UIFont Font
        {
            get => _font ?? base.Font;
            set
            {
                _font = value.WithSize(TextSize);
                this.SetFont(_font);
                base.Font = _font;
                IsEOSCustomizationIgnored = true;
            }
        }

        private int _letterSpacing;
        public int LetterSpacing
        {
            get => _letterSpacing;
            set
            {
                _letterSpacing = value;
                this.SetLetterSpacing(_letterSpacing);
                IsEOSCustomizationIgnored = true;
            }
        }

        private int _textSize;
        public int TextSize
        {
            get => _textSize == 0 ? (int)base.Font.PointSize : _textSize;
            set
            {
                _textSize = value;
                this.SetTextSize(_textSize);
                IsEOSCustomizationIgnored = true;
            }
        }

        private UIColor _textColor;
        public UIColor TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                SetTitleColor(_textColor, UIControlState.Normal);
                ImageView.TintColor = _textColor;
                IsEOSCustomizationIgnored = true;
            }
        }

        private UIColor _disabledTextColor;
        public UIColor DisabledTextColor
        {
            get => _disabledTextColor;
            set
            {
                _disabledTextColor = value;
                SetTitleColor(_disabledTextColor, UIControlState.Disabled);
                IsEOSCustomizationIgnored = true;
            }
        }

        private UIColor _pressedTextColor;
        public UIColor PressedTextColor
        {
            get => _pressedTextColor;
            set
            {
                _pressedTextColor = value;
                SetTitleColor(_pressedTextColor, UIControlState.Highlighted);
                IsEOSCustomizationIgnored = true;
            }
        }

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

        private UIColor _disabledBackgroundColor;
        public UIColor DisabledBackgroundColor
        {
            get => _disabledBackgroundColor;
            set
            {
                _disabledBackgroundColor = value;
                if(!Enabled)
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
        
        private UIColor _rippleColor;
        public UIColor RippleColor
        {
            get => _rippleColor;
            set
            {
                _rippleColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                if(Enabled != value)
                    ToggleState(value);
                base.Enabled = value;
            }
        }

        public int CornerRadius
        {
            get => (int)Layer.CornerRadius;
            set
            {
                Layer.CornerRadius = value;
                IsEOSCustomizationIgnored = true;
            }
        }
        
        public StateEnum ButtonState { get; set; }

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

        #endregion

        #region utility methods

        private void Initialization()
        {
            Layer.MasksToBounds = true;
            Layer.CornerRadius = 5;
            TitleLabel.Lines = 1;
            TitleLabel.LineBreakMode = UILineBreakMode.TailTruncation;
            ContentEdgeInsets = new UIEdgeInsets(ContentEdgeInsets.Top, 10, ContentEdgeInsets.Bottom, 10);
            base.SetAttributedTitle(new NSAttributedString(string.Empty), UIControlState.Normal);
            _rotationAnimation = new CABasicAnimation();
            _rotationAnimation.KeyPath = "transform.rotation.z";
            _rotationAnimation.From = new NSNumber(0);
            _rotationAnimation.To = new NSNumber(_360degrees);
            _rotationAnimation.Duration = 1;
            _rotationAnimation.Cumulative = true;
            _rotationAnimation.RepeatCount = Int32.MaxValue;
            ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            UpdateAppearance();
        }

        public override bool Highlighted
        {
            get => base.Highlighted;
            set
            {
                base.BackgroundColor = value ? PressedBackgroundColor : BackgroundColor;
                base.Highlighted = value;
            }
        }

        private void ToggleState(bool enabled)
        {
            var state = enabled ? UIControlState.Normal : UIControlState.Disabled;
            SetTitle(Title(state), state);
            base.BackgroundColor = enabled ? BackgroundColor : DisabledBackgroundColor;
        }

        public override void SetTitleColor(UIColor color, UIControlState forState)
        {
            var attrString = new NSMutableAttributedString(GetAttributedTitle(UIControlState.Normal));
            attrString.AddAttribute(UIStringAttributeKey.ForegroundColor, color, new NSRange(0, attrString.Length));
            SetAttributedTitle(attrString, forState);
        }
        public override void SetTitle(string title, UIControlState forState)
        {
            NSMutableAttributedString attrString;
            if (title != null)
            {
                attrString = new NSMutableAttributedString(title);
            }
            else
            {
                var defaultSourceString = GetAttributedTitle(UIControlState.Normal);
                var sourceString = GetAttributedTitle(forState);
                attrString = new NSMutableAttributedString(sourceString?.Length > 0 ? sourceString : defaultSourceString);
            }

            var range = new NSRange(0, attrString.Length);
            attrString.AddAttribute(UIStringAttributeKey.KerningAdjustment, new NSNumber(LetterSpacing), range);
            attrString.AddAttribute(UIStringAttributeKey.Font, Font.WithSize(TextSize), range);

            NSMutableAttributedString resultString = null;
            switch (forState)
            {
                case UIControlState.Normal:
                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, TextColor, range);
                    SetAttributedTitle(resultString, UIControlState.Normal);

                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, DisabledTextColor, range);
                    SetAttributedTitle(resultString, UIControlState.Disabled);

                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, PressedTextColor, range);
                    SetAttributedTitle(resultString, UIControlState.Highlighted);
                    break;
                case UIControlState.Disabled:
                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, DisabledTextColor, range);
                    SetAttributedTitle(resultString, forState);
                    break;
                case UIControlState.Highlighted:
                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, PressedTextColor, range);
                    SetAttributedTitle(resultString, forState);
                    break;
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            var tapLocation = (touches.AnyObject as UITouch).LocationInView(this);
            _rippleAnimations = this.CreateRippleAnimations(tapLocation);
            _rippleLayer = this.CrateRippleAnimationLayer(tapLocation, RippleColor);
            _rippleAnimations.SetValueForKey(_rippleLayer, new NSString("animationLayer"));
            _rippleLayer.AddAnimation(_rippleAnimations, _rippleAnimationKey);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            _rippleLayer.RemoveAnimation(_rippleAnimationKey);
        }

        #endregion

        #region IEOSThemeControl implementation

        public bool IsEOSCustomizationIgnored { get; protected set; }


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

        public virtual void UpdateAppearance()
        {
            if(!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                Font = provider.GetEOSProperty<UIFont>(this, EOSConstants.Font);
                TextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6);
                DisabledTextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3);
                PressedTextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6);
                TextSize = provider.GetEOSProperty<int>(this, EOSConstants.TextSize);
                LetterSpacing = provider.GetEOSProperty<int>(this, EOSConstants.LetterSpacing);
                BackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                DisabledBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor4);
                PressedBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColorVariant1);
                CornerRadius = provider.GetEOSProperty<int>(this, EOSConstants.CornerRadius);
                RippleColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.RippleColor);
                Enabled = base.Enabled;
                PreloaderImage = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.FabProgressPreloaderImage));
                IsEOSCustomizationIgnored = false;
            }
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
            var padding = (nfloat)(_verticalPaddingRatio * Frame.Height);
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

        #endregion
    }
}