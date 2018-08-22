using Foundation;
using System;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIKit;
using EOS.UI.Shared.Themes.DataModels;

namespace EOS.UI.iOS.Controls
{
    [Register("SimpleLabel")]
    public class SimpleLabel : UILabel, IEOSThemeControl
    {
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

        public int CornerRadius
        {
            get => (int)Layer.CornerRadius;
            set
            {
                Layer.CornerRadius = value;
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
                if(FontStyle == null)
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
                if(FontStyle == null)
                {
                    base.TextColor = value;
                    return;
                }
                
                FontStyle.Color = value;
                SetFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        private string _text;
        public override string Text
        {
            get => _text;
            set
            {
                if (_text != value)
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
            IsEOSCustomizationIgnored = false;
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
                FontStyle = provider.GetEOSProperty<FontStyleItem>(this, EOSConstants.R2C1S);
                IsEOSCustomizationIgnored = false;
            }
        }

        private void SetFontStyle()
        {
            //set font
            base.Font = this.Font.WithSize(TextSize);
            //size
            this.SetTextSize(TextSize);
            //text color
            base.TextColor = this.TextColor;
            //letter spacing
            this.SetLetterSpacing(LetterSpacing);
        }
    }
}