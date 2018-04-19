using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using System;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class BadgeLabel : UILabel, IEOSThemeControl
    {
        public bool IsEOSCustomizationIgnored => throw new NotImplementedException();

        public int CornerRadius
        {
            get => (int)this.Layer.CornerRadius;
            set => this.Layer.CornerRadius = value;
        }

        public int LetterSpacing
        {
            get => 0;
            set
            {
                SetLetterSpacing(value);
            }
        }

        public BadgeLabel()
        {
            this.Layer.MasksToBounds = true;
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
            var provider = EOSThemeProvider.Instance;
            return (IEOSThemeProvider) provider;
        }

        public void ResetCustomization()
        {
            throw new NotImplementedException();
        }

        public IEOSStyle SetEOSStyle(EOSStyleEnumeration style)
        {
            return null;
        }

        public void UpdateAppearance()
        {
            SizeToFit();
        }

        private void SetLetterSpacing(int spacing)
        {
            NSString str = new NSString(Text);
            var attributedString = new NSMutableAttributedString(Text);
            attributedString.AddAttribute(UIStringAttributeKey.KerningAdjustment, new NSNumber(spacing), new NSRange(0, Text.Length));
            this.AttributedText = attributedString;
        }
    }
}