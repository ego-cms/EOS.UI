using System;
using CoreGraphics;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using UIKit;

namespace EOS.UI.iOS.Controls
{
    [Register("BadgeLabel")]
    public class BadgeLabel : UILabel, IEOSThemeControl
    {
        private UIEdgeInsets _insets = new UIEdgeInsets(2, 15, 2, 15);

        public bool IsEOSCustomizationIgnored { get; private set; }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            UpdateAppearance();
            UpdateFrame();
        }

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
                Layer.MasksToBounds = true;
                Layer.CornerRadius = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public override UIColor BackgroundColor
        {
            get => base.BackgroundColor;
            set
            {
                base.BackgroundColor = value;
                if (FontStyle != null)
                {
                    IsEOSCustomizationIgnored = true;
                }
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
                var attributedString = AttributedText != null ?
                       new NSMutableAttributedString(AttributedText) : new NSMutableAttributedString(_text);
                attributedString.MutableString.SetString(new NSString(_text));
                AttributedText = attributedString;
            }
        }

        #region .ctors

        public BadgeLabel()
        {
            Initialize();
        }

        public BadgeLabel(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public BadgeLabel(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public BadgeLabel(NSObjectFlag t) : base(t)
        {
            Initialize();
        }

        public BadgeLabel(CGRect frame) : base(frame)
        {
            Initialize();
        }

        #endregion

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
                FontStyle = provider.GetEOSProperty<FontStyleItem>(this, EOSConstants.R2C5S);
                CornerRadius = provider.GetEOSProperty<int>(this, EOSConstants.LabelCornerRadius);
                BackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                IsEOSCustomizationIgnored = false;
            }
        }

        public override void DrawText(CGRect rect)
        {
            var x = rect.X;
            if (Math.Floor(Frame.Width) == Math.Floor(AttributedText.Size.Width + _insets.Left + _insets.Right))
            {
                x += _insets.Left;
            }
            else
            {
                x = Frame.Width / 2 - AttributedText.Size.Width / 2;
            }
            var newRect = new CGRect(x, rect.Y - _insets.Top,
                              rect.Width + _insets.Left + _insets.Right, rect.Height + _insets.Top + _insets.Bottom);
            base.DrawText(newRect);
        }

        public override CGRect TextRectForBounds(CGRect bounds, nint numberOfLines)
        {
            var textRect = base.TextRectForBounds(bounds, numberOfLines);
            var requredRect = new CGRect(textRect.GetMinX() + _insets.Left, textRect.GetMinY() - _insets.Top,
                           textRect.Width + _insets.Left + _insets.Right, textRect.Height + _insets.Bottom + _insets.Top);
            return requredRect;
        }

        private void Initialize()
        {
            //AttributedText applies only for non-empty string. 
            //For attributed text initialization should have something here
            Text = AttributedText?.Value ?? " ";
            Lines = 1;
            LineBreakMode = UILineBreakMode.TailTruncation;
            UpdateAppearance();
        }

        private void UpdateFrame()
        {
            var rect = AttributedText.GetBoundingRect(AttributedText.Size, NSStringDrawingOptions.UsesLineFragmentOrigin, null);
            TextRectForBounds(rect, 1);
            SizeToFit();
        }

        private void SetFontStyle()
        {
            base.Font = this.Font.WithSize(TextSize);
            this.SetTextSize(TextSize);
            base.TextColor = FontStyle.Color;
            this.SetLetterSpacing(LetterSpacing);
        }
    }
}