using System;
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
    [Register("GhostButton")]
    public class GhostButton : UIButton, IEOSThemeControl
    {
        public bool IsEOSCustomizationIgnored { get; private set; }

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

        private UIColor _enabledTextColor;
        public UIColor EnabledTextColor
        {
            get => _enabledTextColor;
            set
            {
                _enabledTextColor = value;
                SetTitleColor(_enabledTextColor, UIControlState.Normal);
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

        private UIColor _pressedStateTextColor;
        public UIColor PressedStateTextColor
        {
            get => _pressedStateTextColor;
            set
            {
                _pressedStateTextColor = value;
                SetTitleColor(_pressedStateTextColor, UIControlState.Highlighted);
                IsEOSCustomizationIgnored = true;
            }
        }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                if (value)
                {
                    SetTitle(Title(UIControlState.Normal), UIControlState.Normal);
                }
                else
                {
                    SetTitle(Title(UIControlState.Disabled), UIControlState.Disabled);
                }
            }
        }

        public GhostButton()
        {
            Layer.MasksToBounds = true;
            Layer.CornerRadius = 5;
            BackgroundColor = UIColor.Clear;
            TitleLabel.Lines = 1;
            TitleLabel.LineBreakMode = UILineBreakMode.TailTruncation;
            base.SetAttributedTitle(new NSAttributedString(String.Empty), UIControlState.Normal);
            UpdateAppearance();
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
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, EnabledTextColor, range);
                    SetAttributedTitle(resultString, UIControlState.Normal);

                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, DisabledTextColor, range);
                    SetAttributedTitle(resultString, UIControlState.Disabled);

                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, PressedStateTextColor, range);
                    SetAttributedTitle(resultString, UIControlState.Highlighted);
                    break;
                case UIControlState.Disabled:
                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, DisabledTextColor, range);
                    SetAttributedTitle(resultString, forState);
                    break;
                case UIControlState.Highlighted:
                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, PressedStateTextColor, range);
                    SetAttributedTitle(resultString, forState);
                    break;
            }
        }

        public override void SetTitleColor(UIColor color, UIControlState forState)
        {
            var attrString = new NSMutableAttributedString(GetAttributedTitle(UIControlState.Normal));
            var range = new NSRange(0, attrString.Length);
            attrString.AddAttribute(UIStringAttributeKey.ForegroundColor, color, range);
            SetAttributedTitle(attrString, forState);
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
            throw new NotImplementedException();
        }

        public void UpdateAppearance()
        {
            if (!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                Font = provider.GetEOSProperty<UIFont>(this, EOSConstants.Font);
                EnabledTextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                DisabledTextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3);
                PressedStateTextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6);
                TextSize = provider.GetEOSProperty<int>(this, EOSConstants.TextSize);
                LetterSpacing = provider.GetEOSProperty<int>(this, EOSConstants.LetterSpacing);
                Enabled = base.Enabled;
                IsEOSCustomizationIgnored = false;
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            var touch = touches.AnyObject as UITouch;
            var location = touch.LocationInView(this);
            this.RippleAnimate(location);
        }
    }
}
