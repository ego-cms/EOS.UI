using UIKit;
using Foundation;
using CoreGraphics;
using CoreAnimation;
using System;
using System.Linq;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Extensions;

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
        private UIImageView _rightImageView;
        private UIView _leftImageContainer;
        private UIView _rightImageContainer;
        private CALayer _underlineLayer;
        private readonly UIColor _warningColor = ColorExtension.FromHex("#FF5C49");

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
                if (!value)
                    ResignFirstResponder();
                if (Enabled != value)
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
                if (Enabled)
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
                if (!Enabled)
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
                if (Enabled && Placeholder != null)
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
                if (!Enabled && Placeholder != null)
                    AttributedPlaceholder = new NSAttributedString(Placeholder, null, value);
            }
        }

        private UIColor _focusedColor;
        public UIColor FocusedColor
        {
            get => _focusedColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _focusedColor = value;
                if (Enabled && IsFirstResponder && _underlineLayer != null)
                {
                    _leftImageView.TintColor = value;
                    _underlineLayer.BorderColor = value.CGColor;
                }
            }
        }

        private UIColor _normalIconColor;
        public UIColor NormalIconColor
        {
            get => _normalIconColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _normalIconColor = value;
                if (String.IsNullOrEmpty(Text) && Enabled && !IsFirstResponder)
                    _leftImageView.TintColor = _normalIconColor;
            }
        }

        private UIColor _normalUnderlineColor;
        public UIColor NormalUnderlineColor
        {
            get => _normalUnderlineColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _normalUnderlineColor = value;
                if (String.IsNullOrEmpty(Text) && Enabled && !IsFirstResponder && _underlineLayer != null)
                    _underlineLayer.BorderColor = _normalUnderlineColor.CGColor;
            }
        }

        private UIColor _populatedUnderlineColor;
        public UIColor PopulatedUnderlineColor
        {
            get => _populatedUnderlineColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _populatedUnderlineColor = value;
                if (Enabled && !IsFirstResponder && _underlineLayer != null && !String.IsNullOrEmpty(Text))
                    _underlineLayer.BorderColor = value.CGColor;
            }
        }

        private UIColor _populatedIconColor;
        public UIColor PopulatedIconColor
        {
            get => _populatedIconColor;
            set
            {
                IsEOSCustomizationIgnored = true;
                _populatedIconColor = value;
                if (Enabled && !IsFirstResponder && !String.IsNullOrEmpty(Text))
                    _leftImageView.TintColor = _populatedIconColor;
            }
        }

        private UIColor _colorDisabled;
        public UIColor DisabledColor
        {
            get => _colorDisabled;
            set
            {
                IsEOSCustomizationIgnored = true;
                _colorDisabled = value;
                if (!Enabled && _underlineLayer != null)
                {
                    _leftImageView.TintColor = value;
                    _underlineLayer.BorderColor = value.CGColor;
                }
            }
        }

        private UIImage _leftImage;
        public UIImage LeftImage
        {
            get => _leftImage;
            set
            {
                IsEOSCustomizationIgnored = true;
                _leftImage = value;
                _leftImageView.Image = value;
                if (Enabled)
                {
                    _leftImageView.TintColor =
                        IsFirstResponder ? FocusedColor : (String.IsNullOrEmpty(Text) ? NormalIconColor : PopulatedUnderlineColor);
                }
                else
                {
                    _leftImageView.TintColor = DisabledColor;
                }
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

        private bool _isValid = true;
        public bool IsValid
        {
            get => _isValid;
            set
            {
                _isValid = value;
                if (!Enabled)
                    return;

                if (!_isValid)
                {
                    if (!IsFirstResponder)
                        RightViewMode = UITextFieldViewMode.Always;
                    _leftImageView.TintColor = _warningColor;
                    _underlineLayer.BorderColor = _warningColor.CGColor;
                }
                else
                {
                    RightViewMode = UITextFieldViewMode.Never;
                    _leftImageView.TintColor =
                                      IsFirstResponder ? FocusedColor : (String.IsNullOrEmpty(Text) ? NormalIconColor : PopulatedIconColor);
                    _underlineLayer.BorderColor =
                        IsFirstResponder ? FocusedColor.CGColor : (String.IsNullOrEmpty(Text) ? NormalIconColor.CGColor : PopulatedUnderlineColor.CGColor);
                }
            }
        }

        #endregion

        #region utility methods

        private void Initialize()
        {
            _leftImageView = new UIImageView(new CGRect(0, 0, InputConstants.IconSize, InputConstants.IconSize));
            _leftImageContainer = new UIView(new CGRect(0, 0, InputConstants.IconSize + InputConstants.IconPadding, InputConstants.IconSize));
            _leftImageContainer.AddSubview(_leftImageView);
            _rightImageView = new UIImageView(new CGRect(0, 0, InputConstants.IconSize, InputConstants.IconSize));
            _rightImageContainer = new UIView(new CGRect(0, 0, InputConstants.IconSize + InputConstants.IconPadding, InputConstants.IconSize));
            _rightImageContainer.AddSubview(_rightImageView);

            LeftView = _leftImageContainer;
            LeftViewMode = UITextFieldViewMode.Always;
            RightView = _rightImageContainer;
            RightViewMode = UITextFieldViewMode.Never;
            Started += Input_Started;
            Ended += Input_Ended;
            EditingChanged += (sender, e) =>
            {
                if (AttributedText.Length == 1)
                {
                    SetLetterSpacing(LetterSpacing);
                    SetTextSize(TextSize);
                }
            };

            Layer.MasksToBounds = true;
            IsEOSCustomizationIgnored = false;

            Text = string.Empty;
            Placeholder = string.Empty;
            ClearButtonMode = UITextFieldViewMode.WhileEditing;
            UpdateAppearance();
        }

        private void Input_Ended(object sender, EventArgs e)
        {
            _leftImageView.TintColor = IsValid ? (String.IsNullOrEmpty(Text) ? NormalIconColor : PopulatedIconColor) : _warningColor;
            _underlineLayer.BorderColor = IsValid ?
                (String.IsNullOrEmpty(Text) ? NormalUnderlineColor.CGColor : PopulatedUnderlineColor.CGColor) : _warningColor.CGColor;
            if (!IsValid)
                RightViewMode = UITextFieldViewMode.Always;
            //IsFirstResponder = false;
        }

        private void Input_Started(object sender, EventArgs e)
        {
            //IsFirstResponder = true;
            _leftImageView.TintColor = IsValid ? FocusedColor : _warningColor;
            _underlineLayer.BorderColor = IsValid ? FocusedColor.CGColor : _warningColor.CGColor;
            RightViewMode = UITextFieldViewMode.Never;
        }

        private void UpdateEnabledState(bool enabled)
        {
            base.TextColor = (enabled ? TextColor : TextColorDisabled);
            if (Placeholder != null)
                base.AttributedPlaceholder = new NSAttributedString(Placeholder, null, enabled ? PlaceholderColor : PlaceholderColorDisabled);

            if (!enabled)
            {
                RightViewMode = UITextFieldViewMode.Never;
                _leftImageView.TintColor = DisabledColor;
                _underlineLayer.BorderColor = DisabledColor.CGColor;
            }
            else
            {
                RightViewMode = IsValid ? UITextFieldViewMode.Never : UITextFieldViewMode.Always;
                if (IsFirstResponder)
                {
                    _leftImageView.TintColor = IsValid ? FocusedColor : _warningColor;
                    _underlineLayer.BorderColor = IsValid ? FocusedColor.CGColor : _warningColor.CGColor;
                }
                else
                {
                    _leftImageView.TintColor = IsValid ? (String.IsNullOrEmpty(Text) ? NormalIconColor : PopulatedIconColor) : _warningColor;
                    _underlineLayer.BorderColor = IsValid ? (String.IsNullOrEmpty(Text) ? NormalUnderlineColor.CGColor : PopulatedUnderlineColor.CGColor) : _warningColor.CGColor;
                }
            }
        }

        private void SetLetterSpacing(int spacing)
        {
            if (AttributedText == null)
                Text = string.Empty;

            if (AttributedPlaceholder == null)
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
            if (AttributedText == null)
                Text = string.Empty;

            if (AttributedPlaceholder == null)
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

            if (_initX == 0 && _initY == 0)
            {
                _initX = Frame.X;
                _initY = Frame.Y;
                _initWidth = Frame.Width;
            }

            if (Frame.Height < 35)
                Frame = new CGRect(_initX, _initY, _initWidth, 35);

            if (_underlineLayer == null)
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

                if (Enabled)
                {
                    if (IsValid)
                    {
                        if (IsFirstResponder)
                        {
                            _underlineLayer.BorderColor = FocusedColor.CGColor;
                        }
                        else
                        {
                            _underlineLayer.BorderColor = String.IsNullOrEmpty(Text) ? NormalUnderlineColor.CGColor : PopulatedUnderlineColor.CGColor;
                        }
                    }
                    else
                    {
                        _underlineLayer.BorderColor = _warningColor.CGColor;
                    }
                }
                else
                {
                    _underlineLayer.BorderColor = DisabledColor.CGColor;
                }
            }
            UpdateUnderline();
        }

        private void UpdateUnderline()
        {
            var underlineLayer = Layer.Sublayers.FirstOrDefault(item => item.Name == InputConstants.UnderlineName);
            if (underlineLayer != null)
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
            if (!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                LetterSpacing = provider.GetEOSProperty<int>(this, EOSConstants.LetterSpacing);
                TextSize = provider.GetEOSProperty<int>(this, EOSConstants.TextSize);
                Font = provider.GetEOSProperty<UIFont>(this, EOSConstants.Font);
                TextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor1);
                TextColorDisabled = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3);
                PlaceholderColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor2);
                PlaceholderColorDisabled = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3);
                LeftImage = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.LeftImage));
                NormalIconColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor2);
                NormalUnderlineColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3);
                PopulatedIconColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                PopulatedUnderlineColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3);
                FocusedColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                DisabledColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3);
                _rightImageView.Image = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.WarningInputImage));
                _rightImageView.TintColor = _warningColor;
                IsEOSCustomizationIgnored = false;
            }
        }

        #endregion
    }
}
