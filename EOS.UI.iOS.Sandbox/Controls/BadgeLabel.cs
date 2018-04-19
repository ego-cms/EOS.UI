using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using System;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class BadgeLabel : UILabel, IEOSThemeControl
    {
        public bool IsEOSCustomizationIgnored
        {
            get
            {
                var provider = GetThemeProvider();
                return !(CornerRadius == provider.GetEOSProperty<int>(this, EOSConstants.CornerRadius) &&
                         BackgroundColor == provider.GetEOSProperty<UIColor>(this, EOSConstants.BackgroundColor) &&
                         Font == provider.GetEOSProperty<UIFont>(this, EOSConstants.Font) &&
                         TextColor == provider.GetEOSProperty<UIColor>(this, EOSConstants.TextColor) &&
                         TextSize == provider.GetEOSProperty<int>(this, EOSConstants.TextSize) &&
                         LetterSpacing == provider.GetEOSProperty<int>(this, EOSConstants.LetterSpacing));
            }
        }

        public int CornerRadius
        {
            get => (int)this.Layer.CornerRadius;
            set => this.Layer.CornerRadius = value;
        }

        private int _letterSpacing;
        public int LetterSpacing
        {
            get => _letterSpacing;
            set
            {
                SetLetterSpacing(value);
                _letterSpacing = value;

            }
        }

        public int TextSize
        {
            get => (int)Font.PointSize;
            set => SetTextSize(value);
        }

        public BadgeLabel()
        {
            Text = String.Empty;
            Layer.MasksToBounds = true;
            ResetCustomization();
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
            var provider = GetThemeProvider();
            CornerRadius = provider.GetEOSProperty<int>(this, EOSConstants.CornerRadius);
            BackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BackgroundColor);
            Font = provider.GetEOSProperty<UIFont>(this, EOSConstants.Font);
            TextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.TextColor);
            TextSize = provider.GetEOSProperty<int>(this, EOSConstants.TextSize);
            LetterSpacing = provider.GetEOSProperty<int>(this, EOSConstants.LetterSpacing);
            SizeToFit();
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {
        }

        public void UpdateAppearance()
        {
            if (IsEOSCustomizationIgnored)
            {
                ResetCustomization();
            }
        }

        private void SetLetterSpacing(int spacing)
        {
            var attributedString = new NSMutableAttributedString(AttributedText);
            attributedString.AddAttribute(UIStringAttributeKey.KerningAdjustment, new NSNumber(spacing), new NSRange(0, Text.Length));
            AttributedText = attributedString;
            SizeToFit();
        }

        private void SetTextSize(int value)
        {
            var attributedString = new NSMutableAttributedString(AttributedText);
            attributedString.AddAttribute(UIStringAttributeKey.Font, Font.WithSize(value), new NSRange(0, Text.Length));
            AttributedText = attributedString;
            SizeToFit();
        }
    }
}