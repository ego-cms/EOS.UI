using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Enums;
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
        private SimpleButton _simpleButton;
        private List<EOSSandboxDropDown> _dropDowns;
        private NSLayoutConstraint[] _defaultConstraints;

        public CTAButtonView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _simpleButton = new SimpleButton();
            _simpleButton.SetTitle("CTA button", UIControlState.Normal);

            containerView.ConstrainLayout(() => _simpleButton.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                                _simpleButton.Frame.GetCenterY() == containerView.Frame.GetCenterY() &&
                                                _simpleButton.Frame.Height == 50 &&
                                                _simpleButton.Frame.Width == 340, _simpleButton);
            _defaultConstraints = containerView.Constraints;

            _simpleButton.TouchUpInside += async (sender, e) =>
            {
                _simpleButton.StartProgressAnimation();
                await Task.Delay(5000);
                _simpleButton.StopProgressAnimation();
            };

            _dropDowns = new List<EOSSandboxDropDown>()
            {
                themeDropDown,
                fontDropDown,
                letterSpacingDropDown,
                textSizeDropDown,
                enabledTextColorDropDown,
                disabledTextColorDropDown,
                enabledBackgroundDropDown,
                disabledBackgroundDropDown,
                pressedBackgroundDropdown,
                cornerRadiusDropDown,
                shadowColorDropdown,
                shadowOffsetXDropDown,
                shadowOffsetYDropDown,
                shadowOpacityDropDown,
                shadowRadiusDropDown,
                buttonTypeDropDown
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
            InitBackgroundColorEnabledDropDown(rect);
            InitBackgroundColorDisabledDropDown(rect);
            InitBackgroundColorPressedDropDown(rect);
            InitCornerRadiusDropDown(rect);
            InitButtonTypeDropDown(rect);
            InitShadowColorDropDown(rect);
            InitShadowOffsetXDropDown(rect);
            InitShadowOffsetYDropDown(rect);
            InitShadowOpacityDropDown(rect);
            InitShadowRadiusDropDown(rect);
            InitDisabledSwitch();
            InitResetButton();
        }

        private void InitThemeDropDown(CGRect rect)
        {
            themeDropDown.InitSource(
                Constants.Themes,
                (theme) =>
                {
                    _simpleButton.GetThemeProvider().SetCurrentTheme(theme);
                    _simpleButton.ResetCustomization();
                    _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                    UpdateApperaence();
                },
                Fields.Theme,
                rect);
            themeDropDown.SetTextFieldText(_simpleButton.GetThemeProvider().GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");
        }

        private void InitFontDropDown(CGRect rect)
        {
            fontDropDown.InitSource(
                Fonts,
                font => _simpleButton.Font = font,
                Fields.Font,
                rect);
        }

        private void InitLetterSpacingDropDown(CGRect rect)
        {
            letterSpacingDropDown.InitSource(
                LetterSpacingValues,
                spacing => _simpleButton.LetterSpacing = spacing,
                Fields.LetterSpacing,
                rect);
        }

        private void InitTextSizeDropDown(CGRect rect)
        {
            textSizeDropDown.InitSource(
                FontSizeValues,
                size => _simpleButton.TextSize = size,
                Fields.TextSize,
                rect);
        }

        private void InitTextColorEnabledDropDown(CGRect rect)
        {
            enabledTextColorDropDown.InitSource(
                color => _simpleButton.TextColor = color,
                Fields.EnabledTextColor,
                rect);
        }

        private void InitTextColorDisabledDropDown(CGRect rect)
        {
            disabledTextColorDropDown.InitSource(
                color => _simpleButton.DisabledTextColor = color,
                Fields.DisabledTextColor,
                rect);
        }

        private void InitBackgroundColorEnabledDropDown(CGRect rect)
        {
            enabledBackgroundDropDown.InitSource(
                color => _simpleButton.BackgroundColor = color,
                Fields.EnabledBackground,
                rect);
        }

        private void InitBackgroundColorDisabledDropDown(CGRect rect)
        {
            disabledBackgroundDropDown.InitSource(
                color => _simpleButton.DisabledBackgroundColor = color,
                Fields.DisabledBackground,
                rect);
        }

        private void InitBackgroundColorPressedDropDown(CGRect rect)
        {
            pressedBackgroundDropdown.InitSource(
                color => _simpleButton.PressedBackgroundColor = color,
                Fields.PressedBackground,
                rect);
        }

        private void InitCornerRadiusDropDown(CGRect rect)
        {
            cornerRadiusDropDown.InitSource(
                CornerRadiusValues,
                radius => _simpleButton.CornerRadius = radius,
                Fields.ConerRadius,
                rect);
        }


        private void InitButtonTypeDropDown(CGRect rect)
        {
            buttonTypeDropDown.InitSource(
                ButtonTypes,
                type =>
                {
                    ResetFields();
                    _simpleButton.ResetCustomization();
                    switch (type)
                    {
                        case SimpleButtonTypeEnum.Simple:
                            containerView.RemoveConstraints(containerView.Constraints);
                            containerView.AddConstraints(_defaultConstraints);
                            break;
                        case SimpleButtonTypeEnum.FullBleed:
                            containerView.RemoveConstraints(containerView.Constraints);
                            View.ConstrainLayout(() => containerView.Frame.Height == 150);
                            containerView.ConstrainLayout(() => _simpleButton.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                                                _simpleButton.Frame.GetCenterY() == containerView.Frame.GetCenterY() &&
                                                                _simpleButton.Frame.Left == containerView.Frame.Left &&
                                                                _simpleButton.Frame.Right == containerView.Frame.Right);
                            _simpleButton.ContentEdgeInsets = new UIEdgeInsets();
                            _simpleButton.CornerRadius = 0;
                            _simpleButton.ShadowConfig = null;
                            break;
                    }
                },
                Fields.ButtonType,
                rect);
        }
        
        private void InitShadowColorDropDown(CGRect rect)
        {
            shadowColorDropdown.InitSource(
                color =>
                {
                    var config = _simpleButton.ShadowConfig;
                    config.Color = color.CGColor;
                    _simpleButton.ShadowConfig = config;
                },
                Fields.ShadowColor,
                rect);
        }

        private void InitShadowOffsetXDropDown(CGRect rect)
        {
            shadowOffsetXDropDown.InitSource(
                ShadowOffsetValues,
                offset =>
                {
                    var config = _simpleButton.ShadowConfig;
                    config.Offset = new CGSize(offset, config.Offset.Height);
                    _simpleButton.ShadowConfig = config;
                },
                Fields.ShadowOffsetX,
                rect);
        }

        private void InitShadowOffsetYDropDown(CGRect rect)
        {
            shadowOffsetYDropDown.InitSource(
                ShadowOffsetValues,
                offset =>
                {
                    var config = _simpleButton.ShadowConfig;
                    config.Offset = new CGSize(config.Offset.Width, offset);
                    _simpleButton.ShadowConfig = config;
                },
                Fields.ShadowOffsetY,
                rect);
        }

        private void InitShadowRadiusDropDown(CGRect rect)
        {
            shadowRadiusDropDown.InitSource(
                ShadowRadiusValues,
                radius =>
                {
                    var config = _simpleButton.ShadowConfig;
                    config.Radius = radius;
                    _simpleButton.ShadowConfig = config;
                },
                Fields.ShadowRadius,
                rect);
        }

        private void InitShadowOpacityDropDown(CGRect rect)
        {
            shadowOpacityDropDown.InitSource(
                ShadowOpacityValues,
                opacity =>
                {
                    var config = _simpleButton.ShadowConfig;
                    config.Opacity = (float)opacity;
                    _simpleButton.ShadowConfig = config;
                },
                Fields.ShadowOpacity,
                rect);
        }

        private void InitDisabledSwitch()
        {
            enableSwitch.On = true;
            enableSwitch.ValueChanged += (sender, e) =>
            {
                _simpleButton.Enabled = enableSwitch.On;
            };
        }

        private void InitResetButton()
        {
            resetButton.TouchUpInside += (sender, e) =>
            {
                _simpleButton.ResetCustomization();
                ResetFields();
            };
        }

        private void ResetFields()
        {
            _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
        }
    }
}