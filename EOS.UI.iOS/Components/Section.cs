using System;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIKit;
using static EOS.UI.iOS.Helpers.Constants;

namespace EOS.UI.iOS.Components
{
    public partial class Section : UITableViewHeaderFooterView, IEOSThemeControl
    {
        #region fields

        private bool _subscribed;
        private CALayer _underlineLayer;

        public static readonly NSString Key = new NSString("Section");
        public static readonly UINib Nib;

        #endregion

        #region constructors

        static Section()
        {
            Nib = UINib.FromName("Section", NSBundle.MainBundle);
        }

        public Section(IntPtr handle) : base(handle)
        {

        }

        #endregion

        #region customization

        public Action SectionAction { get; set; }

        private FontStyleItem _sectionFontStyle;
        public FontStyleItem SectionFontStyle
        {
            get => _sectionFontStyle;
            set
            {
                _sectionFontStyle = value;
                SetSectionFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        private FontStyleItem _buttonFontStyle;
        public FontStyleItem ButtonFontStyle
        {
            get => _buttonFontStyle;
            set
            {
                _buttonFontStyle = value;
                SetButtonFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }


        private string _sectionName;
        public string SectionName
        {
            get => _sectionName;
            set
            {
                _sectionName = value;
                var attributedString = new NSAttributedString(value ?? string.Empty);
                sectionName.AttributedText = attributedString;
                SetSectionFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        private string _buttonTitle;
        public string ButtonText
        {
            get => _buttonTitle;
            set
            {
                _buttonTitle = value;
                sectionButton.SetAttributedTitle(new NSAttributedString(value ?? string.Empty), UIControlState.Normal);
                IsEOSCustomizationIgnored = true;
            }
        }

        public float SectionTextSize
        {
            get => SectionFontStyle.Size;
            set
            {
                SectionFontStyle.Size = value;
                SectionNameFont = SectionNameFont.WithSize(value);
                SetSectionFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public float ButtonTextSize
        {
            get => ButtonFontStyle.Size;
            set
            {
                ButtonFontStyle.Size = value;
                SetButtonFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public float SectionTextLetterSpacing
        {
            get => SectionFontStyle.LetterSpacing;
            set
            {
                SectionFontStyle.LetterSpacing = value;
                SetSectionFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public float ButtonTextLetterSpacing
        {
            get => ButtonFontStyle.LetterSpacing;
            set
            {
                ButtonFontStyle.LetterSpacing = value;
                SetButtonFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public UIFont SectionNameFont
        {
            get => SectionFontStyle.Font;
            set
            {
                SectionFontStyle.Font = value.WithSize(SectionTextSize);
                SetSectionFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public UIFont ButtonNameFont
        {
            get => ButtonFontStyle.Font;
            set
            {
                ButtonFontStyle.Font = value.WithSize(ButtonTextSize);
                SetButtonFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public UIColor SectionNameColor
        {
            get => SectionFontStyle.Color;
            set
            {
                SectionFontStyle.Color = value;
                SetSectionFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public UIColor ButtonNameColor
        {
            get => ButtonFontStyle.Color;
            set
            {
                ButtonFontStyle.Color = value;
                SetButtonFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        private bool _hasBorder;
        public bool HasBorder
        {
            get => _hasBorder;
            set
            {
                _hasBorder = value;
                ToggleBorderVisibility();
            }
        }

        public new UIColor BackgroundColor
        {
            get => UIColor.FromCGColor(Layer.BackgroundColor);
            set
            {
                Layer.BackgroundColor = value?.CGColor;
                IsEOSCustomizationIgnored = true;
            }
        }

        private int _borderWidth;
        public int BorderWidth
        {
            get => _borderWidth;
            set
            {
                _borderWidth = value;
                ToggleBorderVisibility();
                IsEOSCustomizationIgnored = true;
            }
        }

        private UIColor _borderColor;
        public UIColor BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                ToggleBorderVisibility();
                IsEOSCustomizationIgnored = true;
            }
        }

        public void SetPaddings(int left, int top, int right, int bottom)
        {
            paddingTop.Constant = top;
            paddingBottom.Constant = bottom;
            paddingLeft.Constant = left;
            paddingRight.Constant = right;
            IsEOSCustomizationIgnored = true;
        }

        public bool HasButton
        {
            get => sectionButton.Hidden;
            set
            {
                sectionButton.Hidden = !value;
                IsEOSCustomizationIgnored = true;
            }
        }

        #endregion

        #region utility methods

        public void Initialize()
        {
            if (sectionButton != null)
            {
                sectionButton.SetAttributedTitle(new NSAttributedString(ButtonText ?? string.Empty), UIControlState.Normal);
                sectionName.AttributedText = new NSAttributedString(SectionName ?? string.Empty);

                sectionButton.LineBreakMode = UILineBreakMode.TailTruncation;

                if (!_subscribed)
                {
                    sectionButton.TouchDown += delegate
                    {
                        SectionAction?.Invoke();
                    };
                    _subscribed = true;
                }
            }
        }

        private void SetButtonTextColor(UIColor color)
        {
            if (color != null)
            {
                var attrString = new NSMutableAttributedString(sectionButton.GetAttributedTitle(UIControlState.Normal));
                var range = new NSRange(0, attrString.Length);
                attrString.AddAttribute(UIStringAttributeKey.ForegroundColor, color, range);
                sectionButton.SetAttributedTitle(attrString, UIControlState.Normal);
            }
        }

        private void ToggleBorderVisibility()
        {
            UpdateDivider(HasBorder);
            Layer.MasksToBounds = true;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (_underlineLayer == null)
            {
                _underlineLayer = new CALayer
                {
                    BackgroundColor = BorderColor == null ? UIColor.Clear.CGColor : BorderColor.CGColor,
                    Frame = new CGRect(0, 0, Frame.Size.Width, BorderWidth),
                    Name = InputConstants.BorderName
                };
                Layer.AddSublayer(_underlineLayer);
                Layer.MasksToBounds = true;
            }

            UpdateDivider(HasBorder);
        }

        private void UpdateDivider(bool isVisible)
        {
            var underlineLayer = Layer.Sublayers.FirstOrDefault(item => item.Name == InputConstants.BorderName);
            if (underlineLayer != null)
            {
                underlineLayer.BackgroundColor = BorderColor == null || !isVisible ? UIColor.Clear.CGColor : BorderColor.CGColor;
                underlineLayer.Frame = new CGRect(0, 0, Frame.Size.Width, isVisible ? BorderWidth : 0);
            }
        }

        #endregion

        #region IEOSThemeControl implementation

        public bool IsEOSCustomizationIgnored { get; set; }

        public IEOSThemeProvider GetThemeProvider()
        {
            return EOSThemeProvider.Instance;
        }

        public void UpdateAppearance()
        {
            if (!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                HasBorder = provider.GetEOSProperty<bool>(this, EOSConstants.HasSectionBorder);
                HasButton = provider.GetEOSProperty<bool>(this, EOSConstants.HasSectionAction);
                SectionName = provider.GetEOSProperty<string>(this, EOSConstants.SectionTitle);
                ButtonText = provider.GetEOSProperty<string>(this, EOSConstants.SectionActionTitle);
                SectionFontStyle = provider.GetEOSProperty<FontStyleItem>(this, EOSConstants.R2C3);
                ButtonFontStyle = provider.GetEOSProperty<FontStyleItem>(this, EOSConstants.R2C1);
                BackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor5);
                BorderColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor4);
                BorderWidth = provider.GetEOSProperty<int>(this, EOSConstants.BorderWidth);
                var leftPadding = provider.GetEOSProperty<int>(this, EOSConstants.LeftPadding);
                var topPadding = provider.GetEOSProperty<int>(this, EOSConstants.TopPadding);
                var rightPadding = provider.GetEOSProperty<int>(this, EOSConstants.RightPadding);
                var bottomPadding = provider.GetEOSProperty<int>(this, EOSConstants.BottomPadding);
                SetPaddings(leftPadding, topPadding, rightPadding, bottomPadding);
                IsEOSCustomizationIgnored = false;
            }
        }

        public void ResetCustomization()
        {
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public IEOSStyle GetCurrentEOSStyle()
        {
            return null;
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {

        }

        private void SetSectionFontStyle()
        {
            if (SectionFontStyle == null)
                return;
            sectionName.SetTextSize(SectionTextSize);
            sectionName.SetLetterSpacing(SectionTextLetterSpacing);
            sectionName.Font = SectionNameFont;
            sectionName.TextColor = SectionNameColor;
        }

        private void SetButtonFontStyle()
        {
            if (ButtonFontStyle == null)
                return;
            sectionButton.SetTextSize(ButtonTextSize);
            sectionButton.SetLetterSpacing(ButtonTextLetterSpacing);
            sectionButton.Font = ButtonNameFont;
            SetButtonTextColor(ButtonNameColor);
        }

        #endregion
    }
}
