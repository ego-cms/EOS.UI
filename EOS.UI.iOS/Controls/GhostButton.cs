﻿using System;
using CoreAnimation;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using UIKit;

namespace EOS.UI.iOS.Controls
{
    [Register("GhostButton")]
    public class GhostButton : UIButton, IEOSThemeControl
    {
        private CAAnimationGroup _rippleAnimations;
        private CALayer _rippleLayer;
        private const string _rippleAnimationKey = "rippleAnimation";

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

        private FontStyleItem _disabledFontStyle;
        public FontStyleItem DisabledFontStyle
        {
            get => _disabledFontStyle;
            set
            {
                _disabledFontStyle = value;
                SetDisabledFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public override UIFont Font
        {
            get => FontStyle?.Font ?? base.Font;
            set
            {
                FontStyle.Font = value.WithSize(FontStyle.Size);
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

        public UIColor EnabledTextColor
        {
            get => FontStyle?.Color;
            set
            {
                FontStyle.Color = value;
                SetFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        public UIColor DisabledTextColor
        {
            get => DisabledFontStyle.Color;
            set
            {
                DisabledFontStyle.Color = value;
                SetDisabledFontStyle();
                IsEOSCustomizationIgnored = true;
            }
        }

        private UIColor _rippleColor;
        public UIColor RippleColor
        {
            get => _rippleColor;
            set
            {
                _rippleColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                if (value)
                {
                    SetTitle(Title(UIControlState.Normal), UIControlState.Normal);
                }
                else
                {
                    SetTitle(Title(UIControlState.Disabled), UIControlState.Disabled);
                }
            }
        }

        public GhostButton()
        {
            Layer.MasksToBounds = true;
            Layer.CornerRadius = 5;
            BackgroundColor = UIColor.Clear;
            TitleLabel.Lines = 1;
            TitleLabel.LineBreakMode = UILineBreakMode.TailTruncation;
            base.SetAttributedTitle(new NSAttributedString(String.Empty), UIControlState.Normal);
            UpdateAppearance();
        }

        public override void SetTitle(string title, UIControlState forState)
        {
            NSMutableAttributedString attrString;
            if (title != null)
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

            NSMutableAttributedString resultString = null;
            switch (forState)
            {
                case UIControlState.Normal:
                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, EnabledTextColor, range);
                    SetAttributedTitle(resultString, UIControlState.Normal);

                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, DisabledTextColor, range);
                    SetAttributedTitle(resultString, UIControlState.Disabled);

                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, EnabledTextColor, range);
                    SetAttributedTitle(resultString, UIControlState.Highlighted);
                    break;
                case UIControlState.Disabled:
                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, DisabledTextColor, range);
                    SetAttributedTitle(resultString, forState);
                    break;
                case UIControlState.Highlighted:
                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, EnabledTextColor, range);
                    SetAttributedTitle(resultString, forState);
                    break;
            }
        }

        public override void SetTitleColor(UIColor color, UIControlState forState)
        {
            var attrString = new NSMutableAttributedString(GetAttributedTitle(UIControlState.Normal));
            var range = new NSRange(0, attrString.Length);
            attrString.AddAttribute(UIStringAttributeKey.ForegroundColor, color, range);
            SetAttributedTitle(attrString, forState);
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
                FontStyle = provider.GetEOSProperty<FontStyleItem>(this, EOSConstants.R2C1);
                DisabledFontStyle = provider.GetEOSProperty<FontStyleItem>(this, EOSConstants.R3C4S);
                RippleColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.RippleColor);
                Enabled = base.Enabled;
                IsEOSCustomizationIgnored = false;
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            var touch = touches.AnyObject as UITouch;
            var location = touch.LocationInView(this);
            _rippleAnimations = this.CreateRippleAnimations(location);
            _rippleLayer = this.CrateRippleAnimationLayer(location, RippleColor);
            _rippleAnimations.SetValueForKey(_rippleLayer, new NSString("animationLayer"));
            _rippleLayer.AddAnimation(_rippleAnimations, _rippleAnimationKey);
        }
        
        private void SetFontStyle()
        {
            //set font
            this.SetFont(FontStyle.Font);
            base.Font = FontStyle.Font;
            //size
            this.SetTextSize(FontStyle.Size);
            //text color
            SetTitleColor(FontStyle.Color, UIControlState.Normal);
            //letter spacing
            this.SetLetterSpacing(FontStyle.LetterSpacing);
        }

        private void SetDisabledFontStyle()
        {
            //text color
            SetTitleColor(DisabledFontStyle.Color, UIControlState.Disabled);
        }
    }
}
