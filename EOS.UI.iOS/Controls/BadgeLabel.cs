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
using EOS.UI.Shared.Themes.DataModels;

namespace EOS.UI.iOS.Controls
{
    [Register("BadgeLabel")]
    public class BadgeLabel : UILabel, IEOSThemeControl
    {
        private UIEdgeInsets _insets;

        public bool IsEOSCustomizationIgnored { get; private set; }

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
            get => FontStyle?.Size ?? (int)base.Font.PointSize;
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
                //overrided property, that calls in constructor
                if (FontStyle == null)
                {
                    base.Font = value;
                    return;
                }

                FontStyle.Font = value.WithSize(FontStyle.Size);
                SetFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public override UIColor TextColor
        {
            get => FontStyle?.Color;
            set
            {
                if (FontStyle == null)
                {
                    base.TextColor = value;
                    return;
                }

                FontStyle.Color = value;
                SetFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }
        
        public int CornerRadius
        {
            get => (int)this.Layer.CornerRadius;
            set
            {
                this.Layer.CornerRadius = value;
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
            _insets = new UIEdgeInsets(2, 15, 2, 15);
            Lines = 1;
            LineBreakMode = UILineBreakMode.TailTruncation;
            IsEOSCustomizationIgnored = false;
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
                FontStyle = provider.GetEOSProperty<FontStyleItem>(this, EOSConstants.R2C5);
                CornerRadius = provider.GetEOSProperty<int>(this, EOSConstants.LabelCornerRadius);
                BackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                IsEOSCustomizationIgnored = false;
                SizeToFit();
            }
        }

        public override void DrawText(CGRect rect)
        {
            rect = new CGRect(rect.X + _insets.Left, rect.Y - _insets.Top,
                              rect.Width + _insets.Left + _insets.Right, rect.Height + _insets.Top + _insets.Bottom);
            base.DrawText(rect);
        }

        public override CGRect TextRectForBounds(CGRect bounds, nint numberOfLines)
        {
            var textRect = base.TextRectForBounds(bounds, numberOfLines);
            var requredRect = new CGRect(textRect.GetMinX() + _insets.Left, textRect.GetMinY() - _insets.Top,
                           textRect.Width + _insets.Left + _insets.Right, textRect.Height + _insets.Bottom + _insets.Top);
            return requredRect;
        }
        
        private void SetFontStyle()
        {
            base.Font = this.Font.WithSize(TextSize);
            this.SetTextSize(TextSize);
            base.TextColor = this.TextColor;
            this.SetLetterSpacing(LetterSpacing);
        }
    }
}