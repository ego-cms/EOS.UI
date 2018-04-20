using Foundation;
using System;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Controls
{
    [Register("BadgeLabel")]
    public class BadgeLabel : UILabel, IEOSThemeControl
    {
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

        public BadgeLabel()
        {
            Text = String.Empty;
            Layer.MasksToBounds = true;
            _isEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public BadgeLabel(IntPtr handle) : base(handle)
        {
            UpdateAppearance();
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
                BackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BackgroundColor);
                Font = provider.GetEOSProperty<UIFont>(this, EOSConstants.Font);
                TextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.TextColor);
                TextSize = provider.GetEOSProperty<int>(this, EOSConstants.TextSize);
                LetterSpacing = provider.GetEOSProperty<int>(this, EOSConstants.LetterSpacing);
                _isEOSCustomizationIgnored = false;
                SizeToFit();
            }
        }
    }
}