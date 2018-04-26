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
    [Register("Input")]
    public class Input : UITextField, IEOSThemeControl
    {
        #region constructors

        public Input()
        {
            Initialize();
        }

        public Input(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        #endregion

        #region customization

        private int _letterSpacing;
        public int LetterSpacing
        {
            get => _letterSpacing;
            set
            {
                SetLetterSpacing(value);
                _letterSpacing = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public int TextSize
        {
            get => (int)Font.PointSize;
            set
            {
                SetTextSize(value);
                IsEOSCustomizationIgnored = true;
            }
        }

        public override UIFont Font
        {
            get => base.Font;
            set
            {
                base.Font = value.WithSize(TextSize);
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

        public override UIColor TextColor
        {
            get => base.TextColor;
            set
            {
                base.TextColor = value;
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

        #endregion

        #region utility methods

        private void Initialize()
        {
            Text = " ";
            Layer.MasksToBounds = true;
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
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

        #endregion
    }
}