using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Helpers;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Themes.Themes;
using UIKit;
using static EOS.UI.iOS.Sandbox.Helpers.Constants;

namespace EOS.UI.iOS.Sandbox
{
    public partial class CTAButtonView : BaseViewController
    {
        public const string Identifier = "CTAButtonView";
        private CTAButton _ctaButton;
        private List<CustomDropDown> _dropDowns;

        public CTAButtonView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _ctaButton = new CTAButton();
            _ctaButton.SetTitle("CTA button", UIControlState.Normal);
            
            containerView.ConstrainLayout(() => _ctaButton.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                              _ctaButton.Frame.GetCenterY() == containerView.Frame.GetCenterY() &&
                              _ctaButton.Frame.Left == containerView.Frame.Left &&
                              _ctaButton.Frame.Right == containerView.Frame.Right, _ctaButton);
            
            _ctaButton.TouchUpInside += async (sender, e) =>
            {
                _ctaButton.StartProgressAnimation();
                await Task.Delay(5000);
                _ctaButton.StopProgressAnimation();
            };
            
            _dropDowns = new List<CustomDropDown>()
            {
                themeDropDown,
                fontDropDown,
                letterSpacingDropDown,
                textSizeDropDown,
                enabledTextColorDropDown,
                disabledTextColorDropDown,
                pressedTextColorDropDown,
                enabledBackgroundDropDown,
                disabledBackgroundDropDown,
                pressedTextColorDropDown,
                pressedBackgroundDropdown,
                cornerRadiusDropDown
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _dropDowns.ForEach(dropDown => dropDown.CloseInputControl());
            }));
            
            var rect = new CGRect(0, 0, 100, 150);
            InitThemeDropDown(rect);
            InitFontDropDown(rect);
            InitLetterSpacingDropDown(rect);
            InitTextSizeDropDown(rect);
            InitTextColorEnabledDropDown(rect);
            InitTextColorDisabledDropDown(rect);
            InitTextColorPressedDropDown(rect);
            InitBackgroundColorEnabledDropDown(rect);
            InitBackgroundColorDisabledDropDown(rect);
            InitBackgroundColorPressedDropDown(rect);
            InitCornerRadiusDropDown(rect);
            InitDisabledSwitch();
            InitResetButton();
        }
        
        private void InitThemeDropDown(CGRect rect)
        {
            themeDropDown.InitSource(
                Constants.Themes,
                (theme) =>
                {
                    _ctaButton.GetThemeProvider().SetCurrentTheme(theme);
                    _ctaButton.ResetCustomization();
                    _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                },
                Fields.Theme,
                rect);
            themeDropDown.SetTextFieldText(_ctaButton.GetThemeProvider().GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");
        }

        private void InitFontDropDown(CGRect rect)
        {
            fontDropDown.InitSource(
                Fonts,
                font => _ctaButton.Font = font,
                Fields.Font,
                rect);
        }

        private void InitLetterSpacingDropDown(CGRect rect)
        {
            letterSpacingDropDown.InitSource(
                LetterSpacingValues,
                spacing => _ctaButton.LetterSpacing = spacing,
                Fields.LetterSpacing,
                rect);
        }

        private void InitTextSizeDropDown(CGRect rect)
        {
            textSizeDropDown.InitSource(
                FontSizeValues,
                size => _ctaButton.TextSize = size,
                Fields.TextSize,
                rect);
        }

        private void InitTextColorEnabledDropDown(CGRect rect)
        {
            enabledTextColorDropDown.InitSource(
                color => _ctaButton.TextColor = color,
                Fields.EnabledTextColor,
                rect);
        }

        private void InitTextColorDisabledDropDown(CGRect rect)
        {
            disabledTextColorDropDown.InitSource(
                color => _ctaButton.DisabledTextColor = color,
                Fields.DisabledTextColor,
                rect);
        }

        private void InitTextColorPressedDropDown(CGRect rect)
        {
            pressedTextColorDropDown.InitSource(
                color => _ctaButton.PressedTextColor = color,
                Fields.PressedTextColor,
                rect);
        }

        private void InitBackgroundColorEnabledDropDown(CGRect rect)
        {
            enabledBackgroundDropDown.InitSource(
                color => _ctaButton.BackgroundColor = color,
                Fields.EnabledBackground,
                rect);
        }

        private void InitBackgroundColorDisabledDropDown(CGRect rect)
        {
            disabledBackgroundDropDown.InitSource(
                color => _ctaButton.DisabledBackgroundColor = color,
                Fields.DisabledBackground,
                rect);
        }

        private void InitBackgroundColorPressedDropDown(CGRect rect)
        {
            pressedBackgroundDropdown.InitSource(
                color => _ctaButton.PressedBackgroundColor = color,
                Fields.PressedBackground,
                rect);
        }

        private void InitCornerRadiusDropDown(CGRect rect)
        {
            cornerRadiusDropDown.InitSource(
                CornerRadiusValues,
                radius => _ctaButton.CornerRadius = radius,
                Fields.ConerRadius,
                rect);
        }

        private void InitDisabledSwitch()
        {
            enableSwitch.On = true;
            enableSwitch.ValueChanged += (sender, e) =>
            {
                _ctaButton.Enabled = enableSwitch.On;
            };
        }

        private void InitResetButton()
        {
            resetButton.TouchUpInside += (sender, e) =>
            {
                _ctaButton.ResetCustomization();
                ResetFields();
            };
        }

        private void ResetFields()
        {
            _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
        }
    }
}