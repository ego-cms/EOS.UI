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
    [Register("SimpleLabel")]
    public class SimpleLabel : UILabel, IEOSThemeControl
    {
        private bool _isEOSCustomizationIgnored;
        public bool IsEOSCustomizationIgnored => _isEOSCustomizationIgnored;

        public int CornerRadius
        {
            get => (int)Layer.CornerRadius;
            set
            {
                Layer.CornerRadius = value;
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
                if(_text != value)
                {
                    _text = value;
                    var attributedString = AttributedText != null ?
                           new NSMutableAttributedString(AttributedText)
                           : new NSMutableAttributedString(_text);
                    attributedString.MutableString.SetString(new NSString(_text));
                    AttributedText = attributedString;
                }
            }
        }

        public SimpleLabel()
        {
            Text = " ";
            Layer.MasksToBounds = true;
            Lines = 1;
            LineBreakMode = UILineBreakMode.TailTruncation;
            _isEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public SimpleLabel(IntPtr handle) : base(handle)
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
                Font = provider.GetEOSProperty<UIFont>(this, EOSConstants.Font);
                TextColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.PrimaryColor);
                TextSize = provider.GetEOSProperty<int>(this, EOSConstants.TextSize);
                LetterSpacing = provider.GetEOSProperty<int>(this, EOSConstants.LetterSpacing);
                _isEOSCustomizationIgnored = false;
                SizeToFit();
            }
        }
    }
}