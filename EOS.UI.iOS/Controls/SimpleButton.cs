using System;
using System.Collections.Generic;
using Airbnb.Lottie;
using CoreAnimation;
using CoreGraphics;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Helpers;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Controls
{
    [Register("SimpleButton")]
    public class SimpleButton : UIButton, IEOSThemeControl
    {
        private LOTAnimationView _snakeAnimation;
        private const string _snakeAnimationKey = "Animations/preloader-snake";
        private const double _360degrees = 6.28319;//value in radians
        private Dictionary<UIControlState, NSAttributedString> _attributedTitles = new Dictionary<UIControlState, NSAttributedString>();
        private const double _verticalPaddingRatio = 0.25;
        private UIView _animationView;

        #region constructor

        public SimpleButton()
        {
            Initialization();
        }

        #endregion

        #region customization

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

        public UIColor TextColor
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

        private UIColor _backgroundColor;
        public override UIColor BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                if (Enabled)
                    base.BackgroundColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private UIColor _disabledBackgroundColor;
        public UIColor DisabledBackgroundColor
        {
            get => _disabledBackgroundColor;
            set
            {
                _disabledBackgroundColor = value;
                if (!Enabled)
                    base.BackgroundColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private UIColor _pressedBackgroundColor;
        public UIColor PressedBackgroundColor
        {
            get => _pressedBackgroundColor;
            set
            {
                _pressedBackgroundColor = value;
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
                if (Enabled != value)
                    ToggleState(value);
                base.Enabled = value;
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

        public StateEnum ButtonState { get; set; }

        private bool _inProgress;
        public bool InProgress
        {
            get => _inProgress;
            private set
            {
                _inProgress = value;
                UserInteractionEnabled = !value;
            }
        }

        private ShadowConfig _shadowConfig;
        public ShadowConfig ShadowConfig
        {
            get => _shadowConfig;
            set
            {
                _shadowConfig = value;
                IsEOSCustomizationIgnored = true;
                SetShadowConfig(Enabled ? _shadowConfig : null);
            }
        }

        #endregion

        #region utility methods

        private void Initialization()
        {
            TitleLabel.Lines = 1;
            TitleLabel.LineBreakMode = UILineBreakMode.TailTruncation;
            ContentEdgeInsets = new UIEdgeInsets(ContentEdgeInsets.Top, 10, ContentEdgeInsets.Bottom, 10);
            base.SetAttributedTitle(new NSAttributedString(string.Empty), UIControlState.Normal);
            _snakeAnimation = LOTAnimationView.AnimationNamed(_snakeAnimationKey);
            _snakeAnimation.LoopAnimation = true;
            _animationView = new UIView()
            {
                Frame = new CGRect(0, 0, 0, 0),
                BackgroundColor = UIColor.Clear,
                Hidden = true
            };
            _animationView.AddSubview(_snakeAnimation);
            AddSubview(_animationView);
            UpdateAppearance();
        }

        public override bool Highlighted
        {
            get => base.Highlighted;
            set
            {
                base.BackgroundColor = value ? PressedBackgroundColor : BackgroundColor;
                base.Highlighted = value;
            }
        }

        private void ToggleState(bool enabled)
        {
            var state = enabled ? UIControlState.Normal : UIControlState.Disabled;
            SetTitle(Title(state), state);
            base.BackgroundColor = enabled ? BackgroundColor : DisabledBackgroundColor;
        }

        public override void SetTitleColor(UIColor color, UIControlState forState)
        {
            var attrString = new NSMutableAttributedString(GetAttributedTitle(UIControlState.Normal));
            attrString.AddAttribute(UIStringAttributeKey.ForegroundColor, color, new NSRange(0, attrString.Length));
            SetAttributedTitle(attrString, forState);
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
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, TextColor, range);
                    SetAttributedTitle(resultString, UIControlState.Normal);

                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, TextColor, range);
                    SetAttributedTitle(resultString, UIControlState.Highlighted);

                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, DisabledTextColor, range);
                    SetAttributedTitle(resultString, UIControlState.Disabled);
                    break;
                case UIControlState.Disabled:
                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, DisabledTextColor, range);
                    SetAttributedTitle(resultString, UIControlState.Disabled);
                    break;
                case UIControlState.Highlighted:
                    resultString = new NSMutableAttributedString(attrString);
                    resultString.AddAttribute(UIStringAttributeKey.ForegroundColor, TextColor, range);
                    SetAttributedTitle(resultString, UIControlState.Highlighted);
                    break;
            }
        }

        #endregion

        #region IEOSThemeControl implementation

        public bool IsEOSCustomizationIgnored { get; protected set; }


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

        public virtual void UpdateAppearance()
        {
            if (!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                FontStyle = provider.GetEOSProperty<FontStyleItem>(this, EOSConstants.R3C1);
                DisabledFontStyle = provider.GetEOSProperty<FontStyleItem>(this, EOSConstants.R3C4);
                BackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                DisabledBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor4);
                PressedBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColorVariant1);
                CornerRadius = provider.GetEOSProperty<int>(this, EOSConstants.ButtonCornerRadius);
                RippleColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.RippleColor);
                ShadowConfig = provider.GetEOSProperty<ShadowConfig>(this, EOSConstants.SimpleButtonShadow);
                Enabled = base.Enabled;
                IsEOSCustomizationIgnored = false;
            }
        }

        public void StartProgressAnimation()
        {
            InProgress = true;
            SaveTitles();
            SetTitle(string.Empty, UIControlState.Normal);
            UpdateAnimationFrame();
            _animationView.Hidden = false;
            _snakeAnimation.Play();
        }

        public void StopProgressAnimation()
        {
            _snakeAnimation.Stop();
            _animationView.Hidden = true;
            RestoreTitles();
            InProgress = false;
        }

        private void UpdateAnimationFrame()
        {
            var padding = (nfloat)(_verticalPaddingRatio * Frame.Height);
            var heightWidth = Frame.Height - padding * 2;
            var x = (Frame.Width / 2) - heightWidth / 2;
            var y = padding;
            var newFrame = new CGRect(x, y, heightWidth, heightWidth);
            _animationView.Frame = newFrame;
            _snakeAnimation.Frame = _animationView.Bounds;
        }

        private void SaveTitles()
        {
            _attributedTitles.Clear();
            _attributedTitles.Add(UIControlState.Normal, GetAttributedTitle(UIControlState.Normal));
            _attributedTitles.Add(UIControlState.Disabled, GetAttributedTitle(UIControlState.Disabled));
            _attributedTitles.Add(UIControlState.Highlighted, GetAttributedTitle(UIControlState.Highlighted));
        }

        private void RestoreTitles()
        {
            SetAttributedTitle(_attributedTitles[UIControlState.Normal], UIControlState.Normal);
            SetAttributedTitle(_attributedTitles[UIControlState.Disabled], UIControlState.Disabled);
            SetAttributedTitle(_attributedTitles[UIControlState.Highlighted], UIControlState.Highlighted);
        }

        private void SetShadowConfig(ShadowConfig config)
        {
            if (config != null)
            {
                Layer.ShadowColor = config.Color.CGColor;
                Layer.ShadowOffset = new CGSize(config.Offset);
                Layer.ShadowRadius = config.Blur / 2;
                //Layer.ShadowOpacity = (float)config.Color.CGColor.Alpha;
                Layer.ShadowOpacity = 1.0f;
            }
            else
            {
                Layer.ShadowColor = UIColor.Clear.CGColor;
                Layer.ShadowOffset = new CGSize();
                Layer.ShadowRadius = 0;
                Layer.ShadowOpacity = 0;
            }
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
            ImageView.TintColor = FontStyle.Color;
            //letter spacing
            this.SetLetterSpacing(FontStyle.LetterSpacing);
        }

        private void SetDisabledFontStyle()
        {
            //text color
            SetTitleColor(DisabledFontStyle.Color, UIControlState.Disabled);

        }

        #endregion
    }
}
