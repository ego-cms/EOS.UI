using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Helpers;
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
        private ShadowConfig _defaultShadow;
        private double? _opacity;

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
            InitDisabledSwitch();
            InitResetButton();
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
                    _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                    UpdateApperaence();
                    SetupSimpleButtonStyle();
                },
                Fields.Theme,
                rect);
            themeDropDown.SetTextFieldText(_simpleButton.GetThemeProvider().GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");
        }

        private void InitFontDropDown(CGRect rect)
        {
            fontDropDown.InitSource(
                Fonts.GetButtonLabelFonts().ToList(),
                font => _simpleButton.Font = font,
                Fields.Font,
                rect);
        }

        private void InitLetterSpacingDropDown(CGRect rect)
        {
            letterSpacingDropDown.InitSource(
                Sizes.LetterSpacingCollection,
                spacing => _simpleButton.LetterSpacing = spacing,
                Fields.LetterSpacing,
                rect);
        }

        private void InitTextSizeDropDown(CGRect rect)
        {
            textSizeDropDown.InitSource(
                Sizes.TextSizeCollection,
                size => _simpleButton.TextSize = size,
                Fields.TextSize,
                rect);
        }

        private void InitTextColorEnabledDropDown(CGRect rect)
        {
            enabledTextColorDropDown.InitSource(
                Colors.FontColorsCollection,
                color => _simpleButton.TextColor = color,
                Fields.EnabledTextColor,
                rect);
        }

        private void InitTextColorDisabledDropDown(CGRect rect)
        {
            disabledTextColorDropDown.InitSource(
                Colors.FontColorsCollection,
                color => _simpleButton.DisabledTextColor = color,
                Fields.DisabledTextColor,
                rect);
        }

        private void InitBackgroundColorEnabledDropDown(CGRect rect)
        {
            enabledBackgrDropDown.InitSource(
                Colors.MainColorsCollection,
                color => _simpleButton.BackgroundColor = color,
                Fields.EnabledBackground,
                rect);
        }

        private void InitBackgroundColorDisabledDropDown(CGRect rect)
        {
            disabledBackgroundDropDown.InitSource(
                Colors.MainColorsCollection,
                color => _simpleButton.DisabledBackgroundColor = color,
                Fields.DisabledBackground,
                rect);
        }

        private void InitBackgroundColorPressedDropDown(CGRect rect)
        {
            pressedBackgroundDropDown.InitSource(
                Colors.MainColorsCollection,
                color => _simpleButton.PressedBackgroundColor = color,
                Fields.PressedBackground,
                rect);
        }

        private void InitCornerRadiusDropDown(CGRect rect)
        {
            cornerRadiusDropDown.InitSource(
                Sizes.CornerRadiusCollection,
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
                    switch (type)
                    {
                        case SimpleButtonTypeEnum.Simple:
                            SetupSimpleButtonStyle();
                            break;
                        case SimpleButtonTypeEnum.FullBleed:
                            SetupFullBleedButtonStyle();
                            break;
                    }
                },
                Fields.ButtonType,
                rect);
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
                Colors.MainColorsCollection,
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
                Shadow.OffsetCollection,
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
                Shadow.OffsetCollection,
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
                Shadow.RadiusCollection,
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
                Shadow.OpacityCollection,
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
                if (!enableSwitch.On)
                {
                    _defaultShadow = _simpleButton.ShadowConfig;
                    _simpleButton.ShadowConfig = null;
                }
                else
                {
                    _simpleButton.ShadowConfig = _defaultShadow;
                }
            };
        }

        private void InitResetButton()
        {
            resetButton.TouchUpInside += (sender, e) =>
            {
                _opacity = null;
                _simpleButton.ResetCustomization();
                _simpleButton.SetTitle(Buttons.Simple, UIControlState.Normal);
                containerView.RemoveConstraints(containerView.Constraints);
                containerView.AddConstraints(_defaultConstraints);
                ResetFields();
            };
        }

        private void ResetFields()
        {
            _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
        }
    }
}