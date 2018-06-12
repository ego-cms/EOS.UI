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

        private nfloat _initX;
        private nfloat _initY;
        private nfloat _initWidth;
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

        private int _textSize;
        public int TextSize
        {
            get => _textSize;
            set
            {
                _textSize = value;
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
                    _leftImageView.TintColor = value;
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
                    _leftImageView.TintColor = value;
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
                    _leftImageView.TintColor = value;
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
                if (Enabled && Focused)
                {
                    _leftImageView.Image = value;
                    _leftImageView.TintColor = UnderlineColorFocused;
                }
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
                if (Enabled && !Focused)
                {
                    _leftImageView.Image = value;
                    _leftImageView.TintColor = UnderlineColorUnfocused;
                }
                
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
                if (!Enabled)
                {
                    _leftImageView.Image = value;
                    _leftImageView.TintColor = UnderlineColorDisabled;
                }
            }
        }

        private string _text;
        public override string Text
        {
            get => _text;
            set
            {
                _text = value;
                var attributedString = AttributedText != null ?
                    new NSMutableAttributedString(AttributedText) :
                    new NSMutableAttributedString(_text);

                attributedString.MutableString.SetString(new NSString(_text));
                AttributedText = attributedString;
            }
        }

        private string _plaseHolder;
        public override string Placeholder
        {
            get => _plaseHolder;
            set
            {
                _plaseHolder = value;
                var attributedString = AttributedPlaceholder != null ?
                    new NSMutableAttributedString(AttributedPlaceholder) : 
                    new NSMutableAttributedString(_plaseHolder);

                attributedString.MutableString.SetString(new NSString(_plaseHolder));
                AttributedPlaceholder = attributedString;
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
            EditingChanged += (sender, e) =>
            {
                if(AttributedText.Length == 1)
                {
                    SetLetterSpacing(LetterSpacing);
                    SetTextSize(TextSize);
                }
            };

            Layer.MasksToBounds = true;
            IsEOSCustomizationIgnored = false;

            Text = string.Empty;
            Placeholder = string.Empty;
            UpdateAppearance();
        }

        private void Input_Ended(object sender, EventArgs e)
        {
            _leftImageView.Image = LeftImageUnfocused;
            _leftImageView.TintColor = UnderlineColorUnfocused;
            _underlineLayer.BorderWidth = InputConstants.UnderlineHeight;
            _underlineLayer.BorderColor = UnderlineColorUnfocused.CGColor;
        }

        private void Input_Started(object sender, EventArgs e)
        {
            _leftImageView.Image = LeftImageFocused;
            _leftImageView.TintColor = UnderlineColorFocused;
            _underlineLayer.BorderWidth = InputConstants.UnderlineHeight;
            _underlineLayer.BorderColor = UnderlineColorFocused.CGColor;
        }

        private void UpdateEnabledState(bool enabled)
        {
            base.TextColor = (enabled ? TextColor : TextColorDisabled);
            if(Placeholder != null)
                base.AttributedPlaceholder = new NSAttributedString(Placeholder, null, enabled ? PlaceholderColor : PlaceholderColorDisabled);

            if (!enabled)
            {
                _leftImageView.Image = LeftImageDisabled;
                _leftImageView.TintColor = UnderlineColorDisabled;
                _underlineLayer.BorderWidth = InputConstants.UnderlineHeight;
                _underlineLayer.BorderColor = UnderlineColorDisabled.CGColor;
            }
            else
            {
                if (Focused)
                {
                    _leftImageView.Image = LeftImageFocused;
                    _leftImageView.TintColor = UnderlineColorFocused;
                    _underlineLayer.BorderWidth = InputConstants.UnderlineHeight;
                    _underlineLayer.BorderColor = UnderlineColorFocused.CGColor;
                }
                else
                {
                    _leftImageView.Image = LeftImageUnfocused;
                    _leftImageView.TintColor = UnderlineColorUnfocused;
                    _underlineLayer.BorderWidth = InputConstants.UnderlineHeight;
                    _underlineLayer.BorderColor = UnderlineColorUnfocused.CGColor;
                }
            }
        }

        private void SetLetterSpacing(int spacing)
        {
            if(AttributedText == null)
                Text = string.Empty;

            if(AttributedPlaceholder == null)
                Placeholder = " ";

            var attributedText = new NSMutableAttributedString(AttributedText);
            attributedText.AddAttribute(UIStringAttributeKey.KerningAdjustment, new NSNumber(spacing), new NSRange(0, AttributedText.Length));
            AttributedText = attributedText;

            var attributedPlaceholder = new NSMutableAttributedString(AttributedPlaceholder);
            attributedPlaceholder.AddAttribute(UIStringAttributeKey.KerningAdjustment, new NSNumber(spacing), new NSRange(0, AttributedPlaceholder.Length));
            AttributedPlaceholder = attributedPlaceholder;
        }

        private void SetTextSize(int size)
        {
            if(AttributedText == null)
                Text = string.Empty;

            if(AttributedPlaceholder == null)
                Placeholder = " ";

            var attributedText = new NSMutableAttributedString(AttributedText);
            attributedText.AddAttribute(UIStringAttributeKey.Font, Font.WithSize(size), new NSRange(0, AttributedText.Length));
            AttributedText = attributedText;

            var attributedPlaceholder = new NSMutableAttributedString(AttributedPlaceholder);
            attributedPlaceholder.AddAttribute(UIStringAttributeKey.Font, Font.WithSize(size), new NSRange(0, AttributedPlaceholder.Length));
            AttributedPlaceholder = attributedPlaceholder;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if(_initX == 0 && _initY == 0)
            {
                _initX = Frame.X;
                _initY = Frame.Y;
                _initWidth = Frame.Width;
            }

            if(Frame.Height < 35)
                Frame = new CGRect(_initX, _initY, _initWidth, 35);

            if(_underlineLayer == null)
            {
                _underlineLayer = new CALayer
                {
                    BorderColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3).CGColor,
                    BorderWidth = InputConstants.UnderlineHeight,
                    Frame = new CGRect(
                        0,
                        Bounds.Height - InputConstants.UnderlineHeight,
                        Bounds.Size.Width,
                        InputConstants.UnderlineHeight
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
                var height = LeftView.Bounds.Height;
                underlineLayer.Frame = new CGRect(
                        0,
                        Bounds.Height - InputConstants.UnderlineHeight,
                        Bounds.Size.Width,
                        InputConstants.UnderlineHeight
                    );
            }
        }

        #endregion

        #region IEOSThemeControl implementation

        private bool _isEOSCustomizationIgnored;
        public bool IsEOSCustomizationIgnored
        {
            get => _isEOSCustomizationIgnored;
            private set
            {
                _isEOSCustomizationIgnored = value;
                UpdateUnderline();
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
            if(!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                LetterSpacing = provider.GetEOSProperty<int>(this, EOSConstants.LetterSpacing);
                TextSize = provider.GetEOSProperty<int>(this, EOSConstants.TextSize);
                Font = provider.GetEOSProperty<UIFont>(this, EOSConstants.Font);
                TextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor1);
                TextColorDisabled = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3);
                PlaceholderColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor2);
                PlaceholderColorDisabled = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3);
                LeftImageFocused = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.LeftImageFocused));
                LeftImageUnfocused = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.LeftImageUnfocused));
                LeftImageDisabled = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.LeftImageDisabled));
                UnderlineColorFocused = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                UnderlineColorUnfocused = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3);
                UnderlineColorDisabled = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3);
                IsEOSCustomizationIgnored = false;
            }
        }

        #endregion
    }
}