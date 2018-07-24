using UIKit;
using Foundation;
using CoreGraphics;
using CoreAnimation;
using System;
using System.Linq;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Extensions;

using static EOS.UI.iOS.Helpers.Constants;
using EOS.UI.Shared.Themes.DataModels;

namespace EOS.UI.iOS.Controls
{
    [Register("Input")]
    public class Input : UITextField, IEOSThemeControl
    {
        #region fields

        private UIImageView _leftImageView;
        private UIImageView _rightImageView;
        private UIView _leftImageContainer;
        private UIView _rightImageContainer;
        private CALayer _underlineLayer;
        private UIImage _clearImage;
        private UIColor _clearImageColor;
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

        private FontStyleItem _fontStyle;
        public FontStyleItem FontStyle
        {
            get => _fontStyle;
            set
            {
                _fontStyle = value;
                SetFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        private FontStyleItem _disabledFontStyle;
        public FontStyleItem DisabledFontStyle
        {
            get => _disabledFontStyle;
            set
            {
                _disabledFontStyle = value;
                SetDisabledFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        private FontStyleItem _placeholderFontStyle;
        public FontStyleItem PlaceholderFontStyle
        {
            get => _placeholderFontStyle;
            set
            {
                _placeholderFontStyle = value;
                SetPlaceholderFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }


        private FontStyleItem _placeholderDisabledFontStyle;
        public FontStyleItem PlaceholderDisabledFontStyle
        {
            get => _placeholderDisabledFontStyle;
            set
            {
                _placeholderDisabledFontStyle = value;
                SetPlaceholderDisabledFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                UpdateEnabledState();
                if (!value)
                    ResignFirstResponder();
            }
        }

        public float LetterSpacing
        {
            get => FontStyle.LetterSpacing;
            set
            {
                FontStyle.LetterSpacing = value;
                SetFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public float TextSize
        {
            get => FontStyle.Size;
            set
            {
                FontStyle.Size = value;
                SetFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public override UIFont Font
        {
            get => FontStyle?.Font ?? base.Font;
            set
            {
                FontStyle.Font = value.WithSize(FontStyle.Size);
                SetFontStyle();
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

        public override UIColor TextColor
        {
            get => FontStyle?.Color ?? base.TextColor;
            set
            {
                FontStyle.Color = value;
                SetFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public UIColor TextColorDisabled
        {
            get => DisabledFontStyle.Color;
            set
            {
                DisabledFontStyle.Color = value;
                SetDisabledFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public UIColor PlaceholderColor
        {
            get => PlaceholderFontStyle.Color;
            set
            {
                PlaceholderFontStyle.Color = value;
                SetPlaceholderFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public UIColor PlaceholderColorDisabled
        {
            get => PlaceholderDisabledFontStyle.Color;
            set
            {
                PlaceholderDisabledFontStyle.Color = value;
                SetPlaceholderDisabledFontStyle();
                IsEOSCustomizationIgnored = true;
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
                UpdateUnderlineColor();
                UpdateIconColor();
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
                UpdateIconColor();
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
                UpdateUnderlineColor();
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
                UpdateUnderlineColor();
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
                UpdateIconColor();
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
                UpdateIconColor();
                UpdateUnderlineColor();
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
                UpdateIconColor();
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
                }
                else
                {
                    RightViewMode = UITextFieldViewMode.Never;
                }
                UpdateIconColor();
                UpdateUnderlineColor();
            }
        }

        #endregion

        #region utility methods

        private void Initialize()
        {
            _leftImageView = new UIImageView(new CGRect(0, 0, InputConstants.IconSize, InputConstants.IconSize));
            _leftImageContainer = new UIView(new CGRect(0, 0, InputConstants.IconSize + InputConstants.IconPadding, InputConstants.IconSize));
            _leftImageContainer.AddSubview(_leftImageView);
            _rightImageView = new UIImageView(new CGRect(8, 0, InputConstants.IconSize, InputConstants.IconSize));
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
            if (!IsValid)
                RightViewMode = UITextFieldViewMode.Always;
            UpdateIconColor();
            UpdateUnderlineColor();
        }

        private void Input_Started(object sender, EventArgs e)
        {
            RightViewMode = UITextFieldViewMode.Never;
            UpdateIconColor();
            UpdateUnderlineColor();
        }

        private void UpdateEnabledState()
        {
            base.TextColor = (Enabled ? TextColor : TextColorDisabled);
            if (Placeholder != null)
                base.AttributedPlaceholder = new NSAttributedString(Placeholder, null, Enabled ? PlaceholderColor : PlaceholderColorDisabled);

            if (!Enabled)
            {
                RightViewMode = UITextFieldViewMode.Never;
            }
            else
            {
                RightViewMode = IsValid ? UITextFieldViewMode.Never : UITextFieldViewMode.Always;
            }
            UpdateIconColor();
            UpdateUnderlineColor();
        }

        private void SetLetterSpacing(float spacing)
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

        private void SetTextSize(float size)
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

            if (Frame.Height < 35)
                Frame = new CGRect(Frame.X, Frame.Y, Frame.Width, 35);

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
                UpdateUnderlineColor();
            }
            UpdateUnderline();
            UpdateClearButtonColor();
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
                FontStyle = provider.GetEOSProperty<FontStyleItem>(this, EOSConstants.R4C2);
                PlaceholderFontStyle = provider.GetEOSProperty<FontStyleItem>(this, EOSConstants.R4C3);
                PlaceholderDisabledFontStyle = provider.GetEOSProperty<FontStyleItem>(this, EOSConstants.R4C4);
                DisabledFontStyle = provider.GetEOSProperty<FontStyleItem>(this, EOSConstants.R4C4);
                LeftImage = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.LeftImage));
                NormalIconColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor2);
                NormalUnderlineColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3);
                PopulatedIconColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                PopulatedUnderlineColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3);
                FocusedColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                DisabledColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3);
                _clearImage = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.ClearInputImage));
                _rightImageView.Image = UIImage.FromBundle(provider.GetEOSProperty<string>(this, EOSConstants.WarningInputImage)); ;
                _rightImageView.TintColor = _warningColor;
                _clearImageColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3);
                IsEOSCustomizationIgnored = false;
            }
        }

        private void UpdateClearButtonColor()
        {
            var button = (UIButton)Subviews.SingleOrDefault(s => s is UIButton);
            if (button == null || button.ImageView.Image == _clearImage)
                return;
            button.SetImage(_clearImage, UIControlState.Normal);
            button.SetImage(_clearImage, UIControlState.Highlighted);
            button.ImageEdgeInsets = new UIEdgeInsets(-1, -1, -1, -1);
            button.ImageView.TintColor = _clearImageColor;
        }

        private void UpdateIconColor()
        {
            if (Enabled)
            {
                if (IsValid)
                {
                    if (IsFirstResponder)
                    {
                        _leftImageView.TintColor = FocusedColor;
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Text) && !String.IsNullOrWhiteSpace(Text))
                        {
                            _leftImageView.TintColor = PopulatedIconColor;
                        }
                        else
                        {
                            _leftImageView.TintColor = NormalIconColor;
                        }
                    }
                }
                else
                {
                    _leftImageView.TintColor = _warningColor;
                }
            }
            else
            {
                _leftImageView.TintColor = DisabledColor;
            }
        }

        private void UpdateUnderlineColor()
        {
            if (_underlineLayer == null)
                return;
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
                        if (!String.IsNullOrEmpty(Text) && !String.IsNullOrWhiteSpace(Text))
                        {
                            _underlineLayer.BorderColor = PopulatedUnderlineColor.CGColor;
                        }
                        else
                        {
                            _underlineLayer.BorderColor = NormalUnderlineColor.CGColor;
                        }
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

        private void SetFontStyle()
        {
            base.Font = Font.WithSize(TextSize);
            this.SetTextSize(TextSize);
            if (Enabled)
                base.TextColor = FontStyle.Color;
            this.SetLetterSpacing(FontStyle.LetterSpacing);
        }

        private void SetPlaceholderFontStyle()
        {
            if (Enabled && Placeholder != null)
                AttributedPlaceholder = new NSAttributedString(Placeholder, null, PlaceholderColor);
        }

        private void SetPlaceholderDisabledFontStyle()
        {
            if (!Enabled && Placeholder != null)
                AttributedPlaceholder = new NSAttributedString(Placeholder, null, PlaceholderColorDisabled);
        }

        private void SetDisabledFontStyle()
        {
            if (!Enabled)
                base.TextColor = TextColorDisabled;
        }
        #endregion
    }
}
