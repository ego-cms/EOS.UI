using System;
using System.Linq;
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

        private string _sectionName;
        public string SectionName
        {
            get => _sectionName;
            set
            {
                _sectionName = value;
                sectionName.AttributedText = new NSAttributedString(value ?? string.Empty);
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

        private int _sectionTextSize;
        public int SectionTextSize
        {
            get => _sectionTextSize;
            set
            {
                _sectionTextSize = value;
                sectionName.SetTextSize(value);
                IsEOSCustomizationIgnored = true;
            }
        }

        private int _buttonTextSize;
        public int ButtonTextSize
        {
            get => _buttonTextSize;
            set
            {
                _buttonTextSize = value;
                sectionButton.SetTextSize(value);
                IsEOSCustomizationIgnored = true;
            }
        }

        private int _sectionTextLetterSpacing;
        public int SectionTextLetterSpacing
        {
            get => _sectionTextLetterSpacing;
            set
            {
                _sectionTextLetterSpacing = value;
                sectionName.SetLetterSpacing(value);
                IsEOSCustomizationIgnored = true;
            }
        }

        public int _buttonTextLetterSpacing;
        public int ButtonTextLetterSpacing
        {
            get => _buttonTextLetterSpacing;
            set
            {
                _buttonTextLetterSpacing = value;
                sectionButton.SetLetterSpacing(value);
                IsEOSCustomizationIgnored = true;
            }
        }

        public UIFont SectionNameFont
        {
            get => sectionName.Font;
            set
            {
                sectionName.Font = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private UIFont _buttonFont;
        public UIFont ButtonNameFont
        {
            get => _buttonFont;
            set
            {
                _buttonFont = value.WithSize(ButtonTextSize);
                sectionButton.SetFont(_buttonFont);
                sectionButton.Font = _buttonFont;
                IsEOSCustomizationIgnored = true;
            }
        }

        public UIColor SectionNameColor
        {
            get => sectionName.TextColor;
            set
            {
                sectionName.TextColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private UIColor _buttonNameColor;
        public UIColor ButtonNameColor
        {
            get => _buttonNameColor;
            set
            {
                _buttonNameColor = value;
                SetButtonTextColor(value);
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
            if(sectionButton != null)
            {
                sectionButton.SetAttributedTitle(new NSAttributedString(ButtonText ?? string.Empty), UIControlState.Normal);
                sectionName.AttributedText = new NSAttributedString(SectionName ?? string.Empty);

                sectionButton.LineBreakMode = UILineBreakMode.TailTruncation;

                if(!_subscribed)
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
            if(color != null)
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

            if(_underlineLayer == null)
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
            if(underlineLayer != null)
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
            if(!IsEOSCustomizationIgnored)
            {
                HasBorder = GetThemeProvider().GetEOSProperty<bool>(this, EOSConstants.HasSectionBorder);
                HasButton = GetThemeProvider().GetEOSProperty<bool>(this, EOSConstants.HasSectionAction);
                SectionName = GetThemeProvider().GetEOSProperty<string>(this, EOSConstants.SectionTitle);
                ButtonText = GetThemeProvider().GetEOSProperty<string>(this, EOSConstants.SectionActionTitle);
                SectionTextSize = GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.TextSize);
                ButtonTextSize = GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.SecondaryTextSize);
                SectionTextLetterSpacing = GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.LetterSpacing);
                ButtonTextLetterSpacing = GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.SecondaryLetterSpacing);
                SectionNameFont = GetThemeProvider().GetEOSProperty<UIFont>(this, EOSConstants.Font);
                ButtonNameFont = GetThemeProvider().GetEOSProperty<UIFont>(this, EOSConstants.SecondaryFont);
                BackgroundColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.PrimaryColor);
                SectionNameColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.SecondaryColor);
                ButtonNameColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.TertiaryColor);
                BorderColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.QuaternaryColor);
                BorderWidth = GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.BorderWidth);
                var leftPadding = GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.LeftPadding);
                var topPadding = GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.TopPadding);
                var rightPadding = GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.RightPadding);
                var bottomPadding = GetThemeProvider().GetEOSProperty<int>(this, EOSConstants.BottomPadding);
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

        #endregion
    }
}
