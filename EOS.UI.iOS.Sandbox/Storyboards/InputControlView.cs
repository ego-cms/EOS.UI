using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class InputControlView : BaseViewController
    {
        public const string Identifier = "InputControlView";

        private Input _inputTop;
        private Input _inputBotton;

        private List<EOSSandboxDropDown> _dropDowns;

        public InputControlView (IntPtr handle) : base (handle)
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
                unfocusedUnderlineColorDropDown,
                disabledUnderlineColorDropDown
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

            var rect = new CGRect(0, 0, 100, 150);

            InitThemeTextField(rect);
            InitFontTextField(rect);
            InitLetterSpacing(rect);
            InitTextSizeTextField(rect);
            InitTextColorTextField(rect);
            InitTextColorDisabledTextField(rect);
            InitPlaceholderTextField(rect);
            InitPlaceholderDisabledTextField(rect);
            InitIconFocusedTextField(rect);
            InitUnderlineFocusedColorTextField(rect);
            InitUnderlineUnfocusedColorTextField(rect);
            InitUnderlineDisabledColorTextField(rect);
            InitDisabledSwitch();
            InitValidationSwitches();
            InitResetButton();
        }

        private void ResetFields()
        {
            _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
        }

        private void InitThemeTextField(CGRect rect)
        {
            themeDropDown.InitSource(
                Constants.Themes,
                (theme) =>
                {
                    _inputTop.GetThemeProvider().SetCurrentTheme(theme);
                    _inputTop.ResetCustomization();
                    _inputBotton.ResetCustomization();
                    _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                    UpdateApperaence();
                },
                Fields.Theme,
                rect);
            themeDropDown.SetTextFieldText(_inputTop.GetThemeProvider().GetCurrentTheme() is LightEOSTheme  ? "Light" : "Dark");
        }

        private void InitFontTextField(CGRect rect)
        {
            fontDropDown.InitSource(
                Fonts,
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
                LetterSpacingValues,
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
                FontSizeValues,
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
                color =>
                {
                    _inputTop.TextColor = color;
                    _inputBotton.TextColor = color;
                },
                Fields.EnabledTextColor,
                rect);
        }

        private void InitTextColorDisabledTextField(CGRect rect)
        {
            disabledTextColorDropDown.InitSource(
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
                color =>
                {
                    _inputTop.PlaceholderColorDisabled = color;
                    _inputBotton.PlaceholderColorDisabled = color;
                },
                Fields.HintTextColorDisabled,
                rect);
        }

        private void InitIconFocusedTextField(CGRect rect)
        {
            focusedIconDropDown.InitSource(
                Icons,
                icon =>
                {
                    _inputTop.LeftImage = UIImage.FromBundle(icon);
                    _inputBotton.LeftImage = UIImage.FromBundle(icon);
                },
                Fields.IconFocused,
                rect);
        }

        private void InitUnderlineFocusedColorTextField(CGRect rect)
        {
            focusedUnderlineColorDropDown.InitSource(
                color =>
                {
                    _inputTop.UnderlineColorFocused = color;
                    _inputBotton.UnderlineColorFocused = color;
                },
                Fields.UnderlineColorFocused,
                rect);
        }

        private void InitUnderlineUnfocusedColorTextField(CGRect rect)
        {
            unfocusedUnderlineColorDropDown.InitSource(
                color =>
                {
                    _inputTop.UnderlineColorUnfocused = color;
                    _inputBotton.UnderlineColorUnfocused = color;
                },
                Fields.UnderlineColorUnfocused,
                rect);
        }

        private void InitUnderlineDisabledColorTextField(CGRect rect)
        {
            disabledUnderlineColorDropDown.InitSource(
                color =>
                {
                    _inputTop.UnderlineColorDisabled = color;
                    _inputBotton.UnderlineColorDisabled = color;
                },
                Fields.UnderlineColorDisabled,
                rect);
        }

        private void InitDisabledSwitch()
        {
            switchDisabled.On = true;
            switchDisabled.ValueChanged += (sender, e) => 
            {
                _inputBotton.Enabled = switchDisabled.On;
                _inputBotton.ResignFirstResponder();
                _inputTop.Enabled = switchDisabled.On;
                _inputTop.ResignFirstResponder();
            };
        }
        
        private void InitValidationSwitches()
        {
            topInputValidSwitch.On = true;
            bottomInputValidSwitch.On = true;
            topInputValidSwitch.ValueChanged += (sender, e) => 
            {
                _inputTop.IsValid = topInputValidSwitch.On;
            };
            bottomInputValidSwitch.ValueChanged += (sender, e) =>
            {
                _inputBotton.IsValid = bottomInputValidSwitch.On;
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
                ResetFields();
            };
        }
	}
}
