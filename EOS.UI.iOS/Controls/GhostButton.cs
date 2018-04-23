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
        private bool _isEOSCustomizationIgnored = false;
        public bool IsEOSCustomizationIgnored => _isEOSCustomizationIgnored;

        public override UIFont Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                _isEOSCustomizationIgnored = true;
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
                _isEOSCustomizationIgnored = true;
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
                _isEOSCustomizationIgnored = true;
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
                _isEOSCustomizationIgnored = true;
            }
        }

        public int TextSize
        {
            get => (int)Font.PointSize;
            set
            {
                Font = Font.WithSize(value);
                _isEOSCustomizationIgnored = true;
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
                _isEOSCustomizationIgnored = true;
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
            switch (forState)
            {
                case UIControlState.Normal:
                    attrString.AddAttribute(UIStringAttributeKey.ForegroundColor, EnabledTextColor, range);
                    break;
                case UIControlState.Disabled:
                    attrString.AddAttribute(UIStringAttributeKey.ForegroundColor, DisabledTextColor, range);
                    break;
            }
            SetAttributedTitle(attrString, forState);
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
            throw new NotImplementedException();
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
                BackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BackgroundColor);
                Font = provider.GetEOSProperty<UIFont>(this, EOSConstants.Font);
                EnabledTextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.TextColor);
                DisabledTextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.DisabledTextColor);
                PressedStateTextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.PressedStateTextColor);
                TextSize = provider.GetEOSProperty<int>(this, EOSConstants.TextSize);
                LetterSpacing = provider.GetEOSProperty<int>(this, EOSConstants.LetterSpacing);
                SizeToFit();
            }
        }
    }
}
