using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Storyboards;
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

            InitThemeTextField(rect);
            InitFontTextField(rect);
            InitLetterSpacing(rect);
            InitTextSizeTextField(rect);
            InitTextColorTextField(rect);
            InitTextColorDisabledTextField(rect);
            InitPlaceholderTextField(rect);
            InitPlaceholderDisabledTextField(rect);
            InitIconTextField(rect);
            InitFocusedColorTextField(rect);
            InitPopulatedUndrlineColorTextField(rect);
            InitDisabledColorTextField(rect);
            InitNormalIconColorTextField(rect);
            InitNormalUnderlineColorTextField(rect);
            InitPopulatedIconColorTextField(rect);
            InitPopulatedUndrlineColorTextField(rect);
            InitValidatingRulesTextField(rect);
            InitDisabledSwitch();
            InitResetButton();
        }

        private void ResetFields()
        {
            _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
        }

        private void InitThemeTextField(CGRect rect)
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
                    UpdateAppearance();
                },
                Fields.Theme,
                rect);
            themeDropDown.SetTextFieldText(_inputTop.GetThemeProvider().GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");
        }

        private void InitFontTextField(CGRect rect)
        {
            fontDropDown.InitSource(
                Fonts.GetInputFonts().ToList(),
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
                Sizes.LetterSpacingCollection,
                spacing =>
                {
                    _inputTop.LetterSpacing = spacing;
                    _inputBotton.LetterSpacing = spacing;
                },
                Fields.LetterSpacing,
                rect);
        }

        private void InitTextSizeTextField(CGRect rect)
        {
            textSizeDropDown.InitSource(
                Sizes.TextSizeCollection,
                size =>
                {
                    _inputTop.TextSize = size;
                    _inputBotton.TextSize = size;
                },
                Fields.TextSize,
                rect);
        }

        private void InitTextColorTextField(CGRect rect)
        {
            textColorDropDown.InitSource(
                Colors.FontColorsCollection,
                color =>
                {
                    _inputTop.TextColor = color;
                    _inputBotton.TextColor = color;
                },
                Fields.TextColor,
                rect);
        }

        private void InitTextColorDisabledTextField(CGRect rect)
        {
            disabledTextColorDropDown.InitSource(
                Colors.FontColorsCollection,
                color =>
                {
                    _inputTop.TextColorDisabled = color;
                    _inputBotton.TextColorDisabled = color;
                },
                Fields.DisabledTextColor,
                rect);
        }

        private void InitPlaceholderTextField(CGRect rect)
        {
            hintTextColorDropDown.InitSource(
                Colors.FontColorsCollection,
                color =>
                {
                    _inputTop.PlaceholderColor = color;
                    _inputBotton.PlaceholderColor = color;
                },
                Fields.HintTextColor,
                rect);
        }

        private void InitPlaceholderDisabledTextField(CGRect rect)
        {
            disabledHintTextColorDropDown.InitSource(
                Colors.FontColorsCollection,
                color =>
                {
                    _inputTop.PlaceholderColorDisabled = color;
                    _inputBotton.PlaceholderColorDisabled = color;
                },
                Fields.HintTextColorDisabled,
                rect);
        }

        private void InitIconTextField(CGRect rect)
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

        private void InitFocusedColorTextField(CGRect rect)
        {
            focusedUnderlineColorDropDown.InitSource(
                Colors.MainColorsCollection,
                color =>
                {
                    _inputTop.FocusedColor = color;
                    _inputBotton.FocusedColor = color;
                },
                Fields.FocusedColor,
                rect);
        }

        private void InitDisabledColorTextField(CGRect rect)
        {
            disabledUnderlineColorDropDown.InitSource(
                Colors.MainColorsCollection,
                color =>
                {
                    _inputTop.DisabledColor = color;
                    _inputBotton.DisabledColor = color;
                },
                Fields.DisabledColor,
                rect);
        }

        private void InitNormalIconColorTextField(CGRect rect)
        {
            normalIconColorDropDown.InitSource(
                Colors.MainColorsCollection,
                color =>
                {
                    _inputTop.NormalIconColor = color;
                    _inputBotton.NormalIconColor = color;
                },
                Fields.NormalIconColor,
                rect);
        }

        private void InitNormalUnderlineColorTextField(CGRect rect)
        {
            normalUnderlineColorDropDown.InitSource(
                Colors.MainColorsCollection,
                color =>
                {
                    _inputTop.NormalUnderlineColor = color;
                    _inputBotton.NormalUnderlineColor = color;
                },
                Fields.NormalUnderlineColor,
                rect);
        }

        private void InitPopulatedIconColorTextField(CGRect rect)
        {
            populatedIconColorDropDown.InitSource(
                Colors.MainColorsCollection,
                color =>
                {
                    _inputTop.PopulatedIconColor = color;
                    _inputBotton.PopulatedIconColor = color;
                },
                Fields.PopulatedIconColor,
                rect);
        }

        private void InitPopulatedUndrlineColorTextField(CGRect rect)
        {
            populatedUnderlineColorDropDown.InitSource(
                Colors.MainColorsCollection,
                color =>
                {
                    _inputTop.PopulatedUnderlineColor = color;
                    _inputBotton.PopulatedUnderlineColor = color;
                },
                Fields.PopulatedUnderlineColor,
                rect);
        }

        private void InitValidatingRulesTextField(CGRect rect)
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
                _inputTop.Text = string.Empty;
                _inputBotton.Text = string.Empty;
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
