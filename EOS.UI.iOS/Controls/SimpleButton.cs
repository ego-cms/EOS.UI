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
            if(title != null)
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
            switch(forState)
            {
                case UIControlState.Normal:
                    attrString.AddAttribute(UIStringAttributeKey.ForegroundColor, TextColor, range);
                    break;
                case UIControlState.Disabled:
                    attrString.AddAttribute(UIStringAttributeKey.ForegroundColor, DisabledTextColor, range);
                    break;
            }
            SetAttributedTitle(attrString, forState);
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            this.RippleAnimate((touches.AnyObject as UITouch).LocationInView(this));
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
                TextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.SecondaryColor);
                DisabledTextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.SecondaryColorDisabled);
                PressedTextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.SecondaryColorPressed);
                TextSize = provider.GetEOSProperty<int>(this, EOSConstants.TextSize);
                LetterSpacing = provider.GetEOSProperty<int>(this, EOSConstants.LetterSpacing);
                BackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.PrimaryColor);
                DisabledBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.PrimaryColorDisabled);
                PressedBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.PrimaryColorPressed);
                CornerRadius = provider.GetEOSProperty<int>(this, EOSConstants.CornerRadius);
                Enabled = base.Enabled;
                IsEOSCustomizationIgnored = false;
                SizeToFit();
            }
        }

        #endregion
    }
}