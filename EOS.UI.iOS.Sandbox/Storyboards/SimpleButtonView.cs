using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class SimpleButtonView : BaseViewController
    {
        public const string Identifier = "SimpleButtonView";

        private SimpleButton _simpleButton;
        private List<EOSSandboxDropDown> _dropDowns;
        private NSLayoutConstraint[] _defaultConstraints;
        private double? _opacity;
        private SimpleButtonTypeEnum _currentButtonState = SimpleButtonTypeEnum.Simple;

        public SimpleButtonView(IntPtr handle) : base(handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _simpleButton = new SimpleButton();
            _simpleButton.SetTitle(ControlNames.SimpleButton, UIControlState.Normal);

            _dropDowns = new List<EOSSandboxDropDown>()
            {
                themeDropDown,
                fontDropDown,
                letterSpacingDropDown,
                textSizeDropDown,
                enabledTextColorDropDown,
                disabledTextColorDropDown,
                enabledBackgrDropDown,
                disabledBackgroundDropDown,
                pressedBackgroundDropDown,
                cornerRadiusDropDown,
                shadowColorDropDown,
                shadowRadiusDropDown,
                shadowOffsetXDropDown,
                shadowOffsetYDropDown,
                shadowOpacityDropDown,
                buttonTypeDropDown
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _dropDowns.ForEach(dropDown => dropDown.CloseInputControl());
            }));

            containerView.ConstrainLayout(() => _simpleButton.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                               _simpleButton.Frame.GetCenterY() == containerView.Frame.GetCenterY() &&
                                               _simpleButton.Frame.Height == 50 &&
                                               _simpleButton.Frame.Width == 340, _simpleButton);
            _defaultConstraints = containerView.Constraints;


            var frame = new CGRect(0, 0, 100, 150);
            InitThemeDropDown(frame);
            InitSources(frame);
            InitDisabledSwitch();
            InitResetButton();
        }

        private void InitSources(CGRect frame)
        {
            InitFontDropDown(frame);
            InitLetterSpacingDropDown(frame);
            InitTextSizeDropDown(frame);
            InitTextColorEnabledDropDown(frame);
            InitTextColorDisabledDropDown(frame);
            InitBackgroundColorEnabledDropDown(frame);
            InitBackgroundColorDisabledDropDown(frame);
            InitBackgroundColorPressedDropDown(frame);
            InitCornerRadiusDropDown(frame);
            InitButtonTypeDropDown(frame);
            InitShadowColorDropDown(frame);
            InitShadowOffsetXDropDown(frame);
            InitShadowOffsetYDropDown(frame);
            InitShadowOpacityDropDown(frame);
            InitShadowRadiusDropDown(frame);
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
                    UpdateApperaence();
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
                SimpleButtonConstants.LetterSpacingCollection,
                spacing => _simpleButton.LetterSpacing = spacing,
                Fields.LetterSpacing,
                rect);
        }

        private void InitTextSizeDropDown(CGRect rect)
        {
            textSizeDropDown.InitSource(
                SimpleButtonConstants.TextSizeCollection,
                size => _simpleButton.TextSize = size,
                Fields.TextSize,
                rect);
        }

        private void InitTextColorEnabledDropDown(CGRect rect)
        {
            enabledTextColorDropDown.InitSource(
                SimpleButtonConstants.FontColorCollection,
                color => _simpleButton.TextColor = color,
                Fields.EnabledTextColor,
                rect);
        }

        private void InitTextColorDisabledDropDown(CGRect rect)
        {
            disabledTextColorDropDown.InitSource(
                SimpleButtonConstants.DisabledFontColorCollection,
                color => _simpleButton.DisabledTextColor = color,
                Fields.DisabledTextColor,
                rect);
        }

        private void InitBackgroundColorEnabledDropDown(CGRect rect)
        {
            enabledBackgrDropDown.InitSource(
                SimpleButtonConstants.BackgroundColorCollection,
                color => _simpleButton.BackgroundColor = color,
                Fields.EnabledBackground,
                rect);
        }

        private void InitBackgroundColorDisabledDropDown(CGRect rect)
        {
            disabledBackgroundDropDown.InitSource(
                SimpleButtonConstants.DisabledBackgroundColorCollection,
                color => _simpleButton.DisabledBackgroundColor = color,
                Fields.DisabledBackground,
                rect);
        }

        private void InitBackgroundColorPressedDropDown(CGRect rect)
        {
            pressedBackgroundDropDown.InitSource(
                SimpleButtonConstants.PressedBackgroundColorCollection,
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
                Buttons.SimpleButtonTypeCollection,
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
            shadowColorDropDown.Enabled = enable;
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
            _simpleButton.SetTitle(Buttons.Simple, UIControlState.Normal);
        }

        private void InitShadowColorDropDown(CGRect rect)
        {
            shadowColorDropDown.InitSource(
                SimpleButtonConstants.ShadowColorCollection,
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
                blur =>
                {
                    var config = _simpleButton.ShadowConfig;
                    config.Blur = blur;
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
    }
}