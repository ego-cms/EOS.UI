using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Sandbox.ControlConstants.iOS;
using EOS.UI.Shared.Themes.Themes;
using UIKit;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.iOS.Sandbox
{
    public partial class InputControlView : BaseViewController
    {
        public const string Identifier = "InputControlView";
        private Input _inputTop;
        private Input _inputBotton;
        private Predicate<String> _validateRule;

        private List<EOSSandboxDropDown> _dropDowns;

        public InputControlView(IntPtr handle) : base(handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _inputTop = new Input();
            _inputTop.Placeholder = "Enter text";
            _inputTop.UpdateAppearance();

            _inputBotton = new Input();
            _inputBotton.Placeholder = "Enter text";
            _inputBotton.UpdateAppearance();

            _dropDowns = new List<EOSSandboxDropDown>()
            {
                themeDropDown,
                fontDropDown,
                letterSpacingDropDown,
                textSizeDropDown,
                textColorDropDown,
                disabledTextColorDropDown,
                hintTextColorDropDown,
                disabledHintTextColorDropDown,
                focusedIconDropDown,
                focusedUnderlineColorDropDown,
                disabledUnderlineColorDropDown,
                normalIconColorDropDown,
                normalUnderlineColorDropDown,
                populatedIconColorDropDown,
                populatedUnderlineColorDropDown,
                validatingRulesDropDown
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _dropDowns.ForEach(dropDown => dropDown.CloseInputControl());
                _inputTop.ResignFirstResponder();
                _inputBotton.ResignFirstResponder();
            }));

            containerTopView.ConstrainLayout(() => _inputTop.Frame.GetCenterX() == containerTopView.Frame.GetCenterX() &&
                                          _inputTop.Frame.GetCenterY() == containerTopView.Frame.GetCenterY(), _inputTop);

            View.AddConstraint(NSLayoutConstraint.Create(_inputTop, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.Width, 1, 150.0f));

            containerBottomView.ConstrainLayout(() => _inputBotton.Frame.GetCenterX() == containerBottomView.Frame.GetCenterX() &&
                                          _inputBotton.Frame.GetCenterY() == containerBottomView.Frame.GetCenterY(), _inputBotton);

            View.AddConstraint(NSLayoutConstraint.Create(_inputBotton, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.Width, 1, 150.0f));

            _inputTop.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            _inputBotton.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            _inputTop.EditingChanged += (sender, e) =>
            {
                var result = _validateRule?.Invoke(_inputTop.Text);
                _inputTop.IsValid = result ?? true;
            };

            _inputBotton.EditingChanged += (sender, e) =>
            {
                var result = _validateRule?.Invoke(_inputBotton.Text);
                _inputBotton.IsValid = result ?? true;
            };

            var rect = new CGRect(0, 0, 100, 150);
            InitThemeDropDown(rect);
            InitSources(rect);
            InitValidatingRulesDropDown(rect);
            InitDisabledSwitch();
            InitResetButton();
        }

        private void InitSources(CGRect rect)
        {
            InitFontDropDown(rect);
            InitLetterSpacing(rect);
            InitTextSizeDropDown(rect);
            InitTextColorDropDown(rect);
            InitTextColorDisabledDropDown(rect);
            InitPlaceholderDropDown(rect);
            InitPlaceholderDisabledDropDown(rect);
            InitIconDropDown(rect);
            InitFocusedColorDropDown(rect);
            InitPopulatedUndrlineColorDropDown(rect);
            InitDisabledColorDropDown(rect);
            InitNormalIconColorDropDown(rect);
            InitNormalUnderlineColorDropDown(rect);
            InitPopulatedIconColorDropDown(rect);
            InitPopulatedUndrlineColorDropDown(rect);
        }

        private void ResetFields()
        {
            _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
        }

        private void InitThemeDropDown(CGRect rect)
        {
            themeDropDown.InitSource(
                ThemeTypes.ThemeCollection,
                (theme) =>
                {
                    _inputTop.GetThemeProvider().SetCurrentTheme(theme);
                    _inputTop.ResetCustomization();
                    _inputBotton.ResetCustomization();
                    _inputTop.IsValid = true;
                    _inputBotton.IsValid = true;
                    _validateRule = null;
                    _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                    InitSources(rect);
                    UpdateAppearance();
                },
                Fields.Theme,
                rect);
            themeDropDown.SetTextFieldText(_inputTop.GetThemeProvider().GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");
        }

        private void InitFontDropDown(CGRect rect)
        {
            fontDropDown.InitSource(
                InputConstants.InputFonts,
                font =>
                {
                    _inputTop.Font = font;
                    _inputBotton.Font = font;
                },
                Fields.Font,
                rect);
        }

        private void InitLetterSpacing(CGRect rect)
        {
            letterSpacingDropDown.InitSource(
                InputConstants.LetterSpacings,
                spacing =>
                {
                    _inputTop.LetterSpacing = spacing;
                    _inputBotton.LetterSpacing = spacing;
                },
                Fields.LetterSpacing,
                rect);
        }

        private void InitTextSizeDropDown(CGRect rect)
        {
            textSizeDropDown.InitSource(
                InputConstants.TextSizes,
                size =>
                {
                    _inputTop.TextSize = size;
                    _inputBotton.TextSize = size;
                },
                Fields.TextSize,
                rect);
        }

        private void InitTextColorDropDown(CGRect rect)
        {
            textColorDropDown.InitSource(
                InputConstants.FontColors,
                color =>
                {
                    _inputTop.TextColor = color;
                    _inputBotton.TextColor = color;
                },
                Fields.TextColor,
                rect);
        }

        private void InitTextColorDisabledDropDown(CGRect rect)
        {
            disabledTextColorDropDown.InitSource(
                InputConstants.DisabledFontColors,
                color =>
                {
                    _inputTop.TextColorDisabled = color;
                    _inputBotton.TextColorDisabled = color;
                },
                Fields.DisabledTextColor,
                rect);
        }

        private void InitPlaceholderDropDown(CGRect rect)
        {
            hintTextColorDropDown.InitSource(
                InputConstants.PlaceholderColors,
                color =>
                {
                    _inputTop.PlaceholderColor = color;
                    _inputBotton.PlaceholderColor = color;
                },
                Fields.HintTextColor,
                rect);
        }

        private void InitPlaceholderDisabledDropDown(CGRect rect)
        {
            disabledHintTextColorDropDown.InitSource(
                InputConstants.DisabledPlaceholderColors,
                color =>
                {
                    _inputTop.PlaceholderColorDisabled = color;
                    _inputBotton.PlaceholderColorDisabled = color;
                },
                Fields.HintTextColorDisabled,
                rect);
        }

        private void InitIconDropDown(CGRect rect)
        {
            focusedIconDropDown.InitSource(
                Icons.IconsCollection,
                iconName =>
                {
                    _inputTop.LeftImage = UIImage.FromBundle(iconName);
                    _inputBotton.LeftImage = UIImage.FromBundle(iconName);
                },
                Fields.Icon,
                rect);
        }

        private void InitFocusedColorDropDown(CGRect rect)
        {
            focusedUnderlineColorDropDown.InitSource(
                InputConstants.FocusedColors,
                color =>
                {
                    _inputTop.FocusedColor = color;
                    _inputBotton.FocusedColor = color;
                },
                Fields.FocusedColor,
                rect);
        }

        private void InitDisabledColorDropDown(CGRect rect)
        {
            disabledUnderlineColorDropDown.InitSource(
                InputConstants.DisabledColors,
                color =>
                {
                    _inputTop.DisabledColor = color;
                    _inputBotton.DisabledColor = color;
                },
                Fields.DisabledColor,
                rect);
        }

        private void InitNormalIconColorDropDown(CGRect rect)
        {
            normalIconColorDropDown.InitSource(
                InputConstants.IconColors,
                color =>
                {
                    _inputTop.NormalIconColor = color;
                    _inputBotton.NormalIconColor = color;
                },
                Fields.NormalIconColor,
                rect);
        }

        private void InitNormalUnderlineColorDropDown(CGRect rect)
        {
            normalUnderlineColorDropDown.InitSource(
                InputConstants.UnderlineColors,
                color =>
                {
                    _inputTop.NormalUnderlineColor = color;
                    _inputBotton.NormalUnderlineColor = color;
                },
                Fields.NormalUnderlineColor,
                rect);
        }

        private void InitPopulatedIconColorDropDown(CGRect rect)
        {
            populatedIconColorDropDown.InitSource(
                InputConstants.PopulatedIconColors,
                color =>
                {
                    _inputTop.PopulatedIconColor = color;
                    _inputBotton.PopulatedIconColor = color;
                },
                Fields.PopulatedIconColor,
                rect);
        }

        private void InitPopulatedUndrlineColorDropDown(CGRect rect)
        {
            populatedUnderlineColorDropDown.InitSource(
                InputConstants.PopulatedUnderlineColors,
                color =>
                {
                    _inputTop.PopulatedUnderlineColor = color;
                    _inputBotton.PopulatedUnderlineColor = color;
                },
                Fields.PopulatedUnderlineColor,
                rect);
        }

        private void InitValidatingRulesDropDown(CGRect rect)
        {
            validatingRulesDropDown.InitSource(Validation.ValidationCollection,
                rule =>
                {
                    _validateRule = rule;
                    _inputTop.IsValid = _validateRule?.Invoke(_inputTop.Text) ?? true;
                    _inputBotton.IsValid = _validateRule?.Invoke(_inputBotton.Text) ?? true;
                },
                Fields.ValidationRules,
                rect);
        }

        private void InitDisabledSwitch()
        {
            switchDisabled.On = true;
            switchDisabled.ValueChanged += (sender, e) =>
            {
                _inputBotton.Enabled = switchDisabled.On;
                _inputTop.Enabled = switchDisabled.On;
            };
        }

        private void InitResetButton()
        {
            resetButton.TouchUpInside += (sender, e) =>
            {
                //_inputTop.Text = string.Empty;
                //_inputBotton.Text = string.Empty;
                _inputTop.ResetCustomization();
                _inputBotton.ResetCustomization();
                _validateRule = null;
                _inputTop.IsValid = true;
                _inputBotton.IsValid = true;
                ResetFields();
            };
        }
    }
}
