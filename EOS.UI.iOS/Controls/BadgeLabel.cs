using Foundation;
using System;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;
using CoreGraphics;

namespace EOS.UI.iOS.Controls
{
    [Register("BadgeLabel")]
    public class BadgeLabel : UILabel, IEOSThemeControl
    {
        private UIEdgeInsets _insets;

        private bool _isEOSCustomizationIgnored;
        public bool IsEOSCustomizationIgnored => _isEOSCustomizationIgnored;

        public int CornerRadius
        {
            get => (int)this.Layer.CornerRadius;
            set
            {
                this.Layer.CornerRadius = value;
                _isEOSCustomizationIgnored = true;
            }
        }

        private int _letterSpacing;
        public int LetterSpacing
        {
            get => _letterSpacing;
            set
            {
                this.SetLetterSpacing(value);
                _letterSpacing = value;
                _isEOSCustomizationIgnored = true;
            }
        }

        public int TextSize
        {
            get => (int)Font.PointSize;
            set
            {
                this.SetTextSize(value);
                _isEOSCustomizationIgnored = true;
            }
        }

        public override UIFont Font
        {
            get => base.Font;
            set
            {
                base.Font = value.WithSize(TextSize);
                _isEOSCustomizationIgnored = true;
            }
        }

        public override UIColor BackgroundColor
        {
            get => base.BackgroundColor;
            set
            {
                base.BackgroundColor = value;
                _isEOSCustomizationIgnored = true;
            }
        }

        public override UIColor TextColor
        {
            get => base.TextColor;
            set
            {
                base.TextColor = value;
                _isEOSCustomizationIgnored = true;
            }
        }

        private string _text;
        public override string Text
        {
            get => _text;
            set
            {
                if (_text == value)
                    return;

                _text = value;
                NSMutableAttributedString attributedString = AttributedText != null ?
                       new NSMutableAttributedString(AttributedText) : new NSMutableAttributedString(_text);
                attributedString.MutableString.SetString(new NSString(_text));
                AttributedText = attributedString;
            }
        }

        public BadgeLabel()
        {
            this.Text = " ";
            Layer.MasksToBounds = true;
            _insets = new UIEdgeInsets(0, 15, 0, 15);
            Lines = 1;
            LineBreakMode = UILineBreakMode.TailTruncation;
            _isEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public BadgeLabel(IntPtr handle) : base(handle)
        {
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
            _isEOSCustomizationIgnored = false;
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
                CornerRadius = provider.GetEOSProperty<int>(this, EOSConstants.CornerRadius);
                BackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.PrimaryColor);
                Font = provider.GetEOSProperty<UIFont>(this, EOSConstants.Font);
                TextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.SecondaryColor);
                TextSize = provider.GetEOSProperty<int>(this, EOSConstants.TextSize);
                LetterSpacing = provider.GetEOSProperty<int>(this, EOSConstants.LetterSpacing);
                _isEOSCustomizationIgnored = false;
                SizeToFit();
            }
        }

        public override void DrawText(CGRect rect)
        {
            rect = new CGRect(rect.X + _insets.Left, rect.Y + _insets.Top,
                              rect.Width + _insets.Left + _insets.Right, rect.Height + _insets.Top + _insets.Bottom);
            base.DrawText(rect);
        }

        public override CGRect TextRectForBounds(CGRect bounds, nint numberOfLines)
        {
            var textRect = base.TextRectForBounds(bounds, numberOfLines);
            var requredRect = new CGRect(textRect.GetMinX() + _insets.Left, textRect.GetMinY() + _insets.Top,
                           textRect.Width + _insets.Left + _insets.Right, textRect.Height + _insets.Bottom + _insets.Top);
            return requredRect;
        }
    }
}