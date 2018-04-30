using Foundation;
using System;
using System.Linq;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;
using CoreGraphics;
using CoreAnimation;
using static EOS.UI.iOS.Helpers.Constants;

namespace EOS.UI.iOS.Controls
{
    [Register("Input")]
    public class Input : UITextField, IEOSThemeControl
    {
        #region fields

        private UIImageView _leftImageView;
        private UIView _leftImageContainer;
        private CALayer _underlineLayer;

        #endregion

        #region constructors

        public Input()
        {
            Initialize();
        }

        public Input(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        #endregion

        #region customization

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                if(Enabled != value)
                    UpdateEnabledState(value);
                base.Enabled = value;
            }
        }

        private int _letterSpacing;
        public int LetterSpacing
        {
            get => _letterSpacing;
            set
            {
                SetLetterSpacing(value);
                _letterSpacing = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public int TextSize
        {
            get => (int)Font.PointSize;
            set
            {
                SetTextSize(value);
                IsEOSCustomizationIgnored = true;
            }
        }

        public override UIFont Font
        {
            get => base.Font;
            set
            {
                base.Font = value.WithSize(TextSize);
                IsEOSCustomizationIgnored = true;
            }
        }

        public override UIColor BackgroundColor
        {
            get => base.BackgroundColor;
            set
            {
                base.BackgroundColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private UIColor _textColor;
        public override UIColor TextColor
        {
            get => _textColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _textColor = value;
                if(Enabled)
                    base.TextColor = value;
            }
        }

        private UIColor _textColorDisabled;
        public UIColor TextColorDisabled
        {
            get => _textColorDisabled;
            set
            {
                IsEOSCustomizationIgnored = true;
                _textColorDisabled = value;
                if(!Enabled)
                    base.TextColor = value;
            }
        }

        private UIColor _placeholderColor;
        public UIColor PlaceholderColor
        {
            get => _placeholderColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _placeholderColor = value;
                if(Enabled && Placeholder != null)
                    AttributedPlaceholder = new NSAttributedString(Placeholder, null, value);
            }
        }

        private UIColor _placeholderColorDisabled;
        public UIColor PlaceholderColorDisabled
        {
            get => _placeholderColorDisabled;
            set
            {
                IsEOSCustomizationIgnored = true;
                _placeholderColorDisabled = value;
                if(!Enabled && Placeholder != null)
                    AttributedPlaceholder = new NSAttributedString(Placeholder, null, value);
            }
        }

        private UIColor _underlineColorFocused;
        public UIColor UnderlineColorFocused
        {
            get => _underlineColorFocused;
            set
            {
                IsEOSCustomizationIgnored = true;
                _underlineColorFocused = value;
                if(Enabled && Focused && _underlineLayer != null)
                {
                    _underlineLayer.BorderWidth = InputConstants.UnderlineHeight;
                    _underlineLayer.BorderColor = value.CGColor;
                }
            }
        }

        private UIColor _underlineColorUnfocused;
        public UIColor UnderlineColorUnfocused
        {
            get => _underlineColorUnfocused;
            set
            {
                IsEOSCustomizationIgnored = true;
                _underlineColorUnfocused = value;
                if(Enabled && !Focused && _underlineLayer != null)
                {
                    _underlineLayer.BorderWidth = InputConstants.UnderlineHeight;
                    _underlineLayer.BorderColor = value.CGColor;
                }
            }
        }

        private UIColor _underlineColorDisabled;
        public UIColor UnderlineColorDisabled
        {
            get => _underlineColorDisabled;
            set
            {
                IsEOSCustomizationIgnored = true;
                _underlineColorDisabled = value;
                if(!Enabled && _underlineLayer != null)
                {
                    _underlineLayer.BorderWidth = InputConstants.UnderlineHeight;
                    _underlineLayer.BorderColor = value.CGColor;
                }
            }
        }

        private UIImage _leftImageFocused;
        public UIImage LeftImageFocused
        {
            get => _leftImageFocused;
            set
            {
                IsEOSCustomizationIgnored = true;
                _leftImageFocused = value;
                if(Enabled && Focused)
                    _leftImageView.Image = value;
            }
        }

        private UIImage _leftImageUnfocused;
        public UIImage LeftImageUnfocused
        {
            get => _leftImageUnfocused;
            set
            {
                IsEOSCustomizationIgnored = true;
                _leftImageUnfocused = value;
                if(Enabled && !Focused)
                    _leftImageView.Image = value;
            }
        }

        private UIImage _leftImageDisabled;
        public UIImage LeftImageDisabled
        {
            get => _leftImageDisabled;
            set
            {
                IsEOSCustomizationIgnored = true;
                _leftImageDisabled = value;
                if(!Enabled)
                    _leftImageView.Image = value;
            }
        }

        private string _text;
        public override string Text
        {
            get => _text;
            set
            {
                if(_text == value)
                    return;

                _text = value;
                NSMutableAttributedString attributedString = AttributedText != null ?
                       new NSMutableAttributedString(AttributedText) : new NSMutableAttributedString(_text);
                attributedString.MutableString.SetString(new NSString(_text));
                AttributedText = attributedString;
            }
        }

        #endregion

        #region utility methods

        private void Initialize()
        {
            _leftImageView = new UIImageView(new CGRect(0, 0, InputConstants.IconSize, InputConstants.IconSize));
            _leftImageContainer = new UIView(new CGRect(0, 0, InputConstants.IconSize + InputConstants.IconPadding, InputConstants.IconSize));
            _leftImageContainer.AddSubview(_leftImageView);

            LeftView = _leftImageContainer;
            LeftViewMode = UITextFieldViewMode.Always;
            Started += Input_Started;
            Ended += Input_Ended;

            Layer.MasksToBounds = true;
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        private void Input_Ended(object sender, EventArgs e)
        {
            _leftImageView.Image = LeftImageUnfocused;
            _underlineLayer.BorderWidth = InputConstants.UnderlineHeight;
            _underlineLayer.BorderColor = UnderlineColorUnfocused.CGColor;
        }

        private void Input_Started(object sender, EventArgs e)
        {
            _leftImageView.Image = LeftImageFocused;
            _underlineLayer.BorderWidth = InputConstants.UnderlineHeight;
            _underlineLayer.BorderColor = UnderlineColorFocused.CGColor;
        }

        private void UpdateEnabledState(bool enabled)
        {
            base.TextColor = (enabled ? TextColor : TextColorDisabled);
            if(Placeholder != null)
                base.AttributedPlaceholder = new NSAttributedString(Placeholder, null, enabled ? PlaceholderColor : PlaceholderColorDisabled);

            if(!enabled)
            {
                _leftImageView.Image = LeftImageDisabled;
                _underlineLayer.BorderWidth = InputConstants.UnderlineHeight;
                _underlineLayer.BorderColor = UnderlineColorDisabled.CGColor;
            }
            else if(Focused)
            {
                _leftImageView.Image = LeftImageFocused;
                _underlineLayer.BorderWidth = InputConstants.UnderlineHeight;
                _underlineLayer.BorderColor = UnderlineColorFocused.CGColor;
            }
            else
            {
                _leftImageView.Image = LeftImageUnfocused;
                _underlineLayer.BorderWidth = InputConstants.UnderlineHeight;
                _underlineLayer.BorderColor = UnderlineColorUnfocused.CGColor;
            }
        }

        private void SetLetterSpacing(int spacing)
        {
            if(AttributedText != null)
            {
                var attributedString = new NSMutableAttributedString(AttributedText);
                attributedString.AddAttribute(UIStringAttributeKey.KerningAdjustment, new NSNumber(spacing), new NSRange(0, AttributedText.Length));
                AttributedText = attributedString;
                SizeToFit();
            }
        }

        private void SetTextSize(int size)
        {
            if(AttributedText != null)
            {
                var attributedString = new NSMutableAttributedString(AttributedText);
                attributedString.AddAttribute(UIStringAttributeKey.Font, Font.WithSize(size), new NSRange(0, AttributedText.Length));
                AttributedText = attributedString;
                SizeToFit();
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if(_underlineLayer == null)
            {
                _underlineLayer = new CALayer
                {
                    BorderColor = UIColor.Red.CGColor,
                    BorderWidth = InputConstants.UnderlineHeight,
                    Frame = new CGRect(
                        0,
                        Frame.Size.Height - InputConstants.UnderlineHeight,
                        Frame.Size.Width,
                        Frame.Size.Height
                    ),
                    Name = InputConstants.UnderlineName
                };
                Layer.AddSublayer(_underlineLayer);
                Layer.MasksToBounds = true;
            }

            UpdateUnderline();
        }

        private void UpdateUnderline()
        {
            var underlineLayer = Layer.Sublayers.FirstOrDefault(item => item.Name == InputConstants.UnderlineName);
            if(underlineLayer != null)
            {
                var frame = underlineLayer.Frame;
                frame.Width = Bounds.Width;
                underlineLayer.Frame = frame;
            }
        }

        #endregion

        #region IEOSThemeControl implementation

        public bool IsEOSCustomizationIgnored { get; private set; }

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
            if(!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                Font = provider.GetEOSProperty<UIFont>(this, EOSConstants.Font);
                LetterSpacing = provider.GetEOSProperty<int>(this, EOSConstants.LetterSpacing);
                TextSize = provider.GetEOSProperty<int>(this, EOSConstants.TextSize);
                TextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.TextColor);
                TextColorDisabled = provider.GetEOSProperty<UIColor>(this, EOSConstants.TextColorDisabled);
                PlaceholderColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.HintTextColor);
                PlaceholderColorDisabled = provider.GetEOSProperty<UIColor>(this, EOSConstants.HintTextColorDisabled);
                LeftImageFocused = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.LeftImageFocused));
                LeftImageUnfocused = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.LeftImageUnfocused));
                LeftImageDisabled = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.LeftImageDisabled));
                UnderlineColorFocused = provider.GetEOSProperty<UIColor>(this, EOSConstants.UnderlineColorFocused);
                UnderlineColorUnfocused = provider.GetEOSProperty<UIColor>(this, EOSConstants.UnderlineColorUnfocused);
                UnderlineColorDisabled = provider.GetEOSProperty<UIColor>(this, EOSConstants.UnderlineColorDisabled);
                IsEOSCustomizationIgnored = false;
                SizeToFit();
            }
        }

        #endregion
    }
}