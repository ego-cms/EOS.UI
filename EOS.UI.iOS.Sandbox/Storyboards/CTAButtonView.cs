﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Helpers;
using EOS.UI.Shared.Sandbox.ControlConstants.iOS;
using EOS.UI.Shared.Sandbox.Helpers;
using EOS.UI.Shared.Themes.Themes;
using UIKit;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.iOS.Sandbox
{
    public partial class CTAButtonView : BaseViewController
    {
        public const string Identifier = "CTAButtonView";
        private SimpleButton _simpleButton;
        private List<EOSSandboxDropDown> _dropDowns;
        private NSLayoutConstraint[] _defaultConstraints;
        private double? _opacity;
        private SimpleButtonTypeEnum _currentButtonState = SimpleButtonTypeEnum.Simple;

        public CTAButtonView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _simpleButton = new SimpleButton();
            _simpleButton.SetTitle(Buttons.CTA, UIControlState.Normal);

            containerView.ConstrainLayout(() => _simpleButton.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                                _simpleButton.Frame.GetCenterY() == containerView.Frame.GetCenterY() &&
                                                _simpleButton.Frame.Height == 50 &&
                                                _simpleButton.Frame.Width == 340, _simpleButton);
            _defaultConstraints = containerView.Constraints;

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
                buttonTypeDropDown,
            };

            _simpleButton.TouchUpInside += async (sender, e) =>
            {
                if (_simpleButton.InProgress)
                    return;
                _simpleButton.StartProgressAnimation();
                ToggleAllControlsEnabled(false, _dropDowns, resetButton, enableSwitch);
                await Task.Delay(5000);
                ToggleAllControlsEnabled(true, _dropDowns, resetButton, enableSwitch);
                ToggleSimpleButtonFields(_currentButtonState == SimpleButtonTypeEnum.Simple);
                _simpleButton.StopProgressAnimation();
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _dropDowns.ForEach(dropDown => dropDown.CloseInputControl());
            }));

            var frame = new CGRect(0, 0, 100, 150);
            InitThemeDropDown(frame);
            InitSources(frame);
            InitDisabledSwitch();
            InitResetButton();
        }
        
        private void InitSources(CGRect rect)
        {
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
        }

        private void InitThemeDropDown(CGRect rect)
        {
            themeDropDown.InitSource(
                ThemeTypes.ThemeCollection,
                (theme) =>
                {
                    _simpleButton.GetThemeProvider().SetCurrentTheme(theme);
                    _simpleButton.ResetCustomization();
                    ResetFields();
                    UpdateAppearance();
                    ApplySimpleButtonViewBehavior();
                    InitSources(rect);
                },
                Fields.Theme,
                rect);
            themeDropDown.SetTextFieldText(_simpleButton.GetThemeProvider().GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");
        }

        private void InitFontDropDown(CGRect rect)
        {
            fontDropDown.InitSource(
                SimpleButtonConstants.SimpleButtonFonts,
                font => _simpleButton.Font = font,
                Fields.Font,
                rect);
        }

        private void InitLetterSpacingDropDown(CGRect rect)
        {
            letterSpacingDropDown.InitSource(
                SimpleButtonConstants.LetterSpacings,
                spacing => _simpleButton.LetterSpacing = spacing,
                Fields.LetterSpacing,
                rect);
        }

        private void InitTextSizeDropDown(CGRect rect)
        {
            textSizeDropDown.InitSource(
                SimpleButtonConstants.TextSizes,
                size => _simpleButton.TextSize = size,
                Fields.TextSize,
                rect);
        }

        private void InitTextColorEnabledDropDown(CGRect rect)
        {
            enabledTextColorDropDown.InitSource(
                SimpleButtonConstants.FontColors,
                color => _simpleButton.TextColor = color,
                Fields.EnabledTextColor,
                rect);
        }

        private void InitTextColorDisabledDropDown(CGRect rect)
        {
            disabledTextColorDropDown.InitSource(
                SimpleButtonConstants.DisabledFontColors,
                color => _simpleButton.DisabledTextColor = color,
                Fields.DisabledTextColor,
                rect);
        }

        private void InitBackgroundColorEnabledDropDown(CGRect rect)
        {
            enabledBackgroundDropDown.InitSource(
                SimpleButtonConstants.BackgroundColors,
                color => _simpleButton.BackgroundColor = color,
                Fields.EnabledBackground,
                rect);
        }

        private void InitBackgroundColorDisabledDropDown(CGRect rect)
        {
            disabledBackgroundDropDown.InitSource(
                SimpleButtonConstants.DisabledBackgroundColors,
                color => _simpleButton.DisabledBackgroundColor = color,
                Fields.DisabledBackground,
                rect);
        }

        private void InitBackgroundColorPressedDropDown(CGRect rect)
        {
            pressedBackgroundDropdown.InitSource(
                SimpleButtonConstants.PressedBackgroundColors,
                color => _simpleButton.PressedBackgroundColor = color,
                Fields.PressedBackground,
                rect);
        }

        private void InitCornerRadiusDropDown(CGRect rect)
        {
            cornerRadiusDropDown.InitSource(
                SimpleButtonConstants.CornerRadiusCollection,
                radius => _simpleButton.CornerRadius = (int)radius,
                Fields.ConerRadius,
                rect);
        }

        private void InitButtonTypeDropDown(CGRect rect)
        {
            buttonTypeDropDown.InitSource(
                Buttons.CTAButtonTypeCollection,
                type =>
                {
                    ResetFields();
                    _simpleButton.ResetCustomization();
                    _currentButtonState = type;
                    switch (type)
                    {
                        case SimpleButtonTypeEnum.Simple:
                            ApplySimpleButtonViewBehavior();
                            break;
                        case SimpleButtonTypeEnum.FullBleed:
                            SetupFullBleedButtonStyle();
                            ToggleSimpleButtonFields(false);
                            break;
                    }
                },
                Fields.ButtonType,
                rect);
        }

        private void ApplySimpleButtonViewBehavior()
        {
            SetupSimpleButtonStyle();
            ToggleSimpleButtonFields(true);
        }

        private void ToggleSimpleButtonFields(bool enable)
        {
            cornerRadiusDropDown.Enabled = enable;
            shadowColorDropdown.Enabled = enable;
            shadowRadiusDropDown.Enabled = enable;
            shadowOffsetXDropDown.Enabled = enable;
            shadowOffsetYDropDown.Enabled = enable;
            shadowOpacityDropDown.Enabled = enable;
        }

        private void SetupFullBleedButtonStyle()
        {
            containerView.RemoveConstraints(containerView.Constraints);
            View.ConstrainLayout(() => containerView.Frame.Height == 150);
            containerView.ConstrainLayout(() => _simpleButton.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                                _simpleButton.Frame.GetCenterY() == containerView.Frame.GetCenterY() &&
                                                _simpleButton.Frame.Height == 50 &&
                                                _simpleButton.Frame.Left == containerView.Frame.Left &&
                                                _simpleButton.Frame.Right == containerView.Frame.Right);
            _simpleButton.ContentEdgeInsets = new UIEdgeInsets();
            _simpleButton.CornerRadius = 0;
            _simpleButton.ShadowConfig = null;
            _simpleButton.SetTitle(Buttons.FullBleed, UIControlState.Normal);
        }

        private void SetupSimpleButtonStyle()
        {
            containerView.RemoveConstraints(containerView.Constraints);
            containerView.AddConstraints(_defaultConstraints);
            _simpleButton.SetTitle(Buttons.CTA, UIControlState.Normal);
        }

        private void InitShadowColorDropDown(CGRect rect)
        {
            shadowColorDropdown.InitSource(
                SimpleButtonConstants.ShadowColors,
                color =>
                {
                    var config = _simpleButton.ShadowConfig;
                    if (_opacity != null)
                    {
                        config.Color = color.ColorWithAlpha((nfloat)_opacity);
                    }
                    else
                    {
                        config.Color = color;
                    }
                    _simpleButton.ShadowConfig = config;
                },
                Fields.ShadowColor,
                rect);
        }

        private void InitShadowOffsetXDropDown(CGRect rect)
        {
            shadowOffsetXDropDown.InitSource(
                SimpleButtonConstants.ShadowOffsetXCollection,
                offset =>
                {
                    var config = _simpleButton.ShadowConfig;
                    config.Offset = new CGPoint(offset, config.Offset.Y);
                    _simpleButton.ShadowConfig = config;
                },
                Fields.ShadowOffsetX,
                rect);
        }

        private void InitShadowOffsetYDropDown(CGRect rect)
        {
            shadowOffsetYDropDown.InitSource(
                SimpleButtonConstants.ShadowOffsetYCollection,
                offset =>
                {
                    var config = _simpleButton.ShadowConfig;
                    config.Offset = new CGPoint(config.Offset.X, offset);
                    _simpleButton.ShadowConfig = config;
                },
                Fields.ShadowOffsetY,
                rect);
        }

        private void InitShadowRadiusDropDown(CGRect rect)
        {
            shadowRadiusDropDown.InitSource(
                SimpleButtonConstants.ShadowRadiusCollection,
                radius =>
                {
                    var config = _simpleButton.ShadowConfig;
                    config.Blur = radius;
                    _simpleButton.ShadowConfig = config;
                },
                Fields.ShadowRadius,
                rect);
        }

        private void InitShadowOpacityDropDown(CGRect rect)
        {
            shadowOpacityDropDown.InitSource(
                SimpleButtonConstants.ShadowOpacityCollection,
                opacity =>
                {
                    var config = _simpleButton.ShadowConfig;
                    _opacity = opacity;
                    config.Color = config.Color.ColorWithAlpha((nfloat)opacity);
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
                _opacity = null;
                _simpleButton.ResetCustomization();
                _currentButtonState = SimpleButtonTypeEnum.Simple;
                ResetFields();
                ApplySimpleButtonViewBehavior();
            };
        }

        private void ResetFields()
        {
            _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
        }

        private void ToggleAllControlsEnabled(bool enabled, List<EOSSandboxDropDown> spinners, UIButton resetUIButton, UISwitch enabledSwitch)
        {
            spinners.ToList().ForEach(s => s.Enabled = enabled);
            resetUIButton.Enabled = enabled;
            enabledSwitch.Enabled = enabled;
        }
    }
}
