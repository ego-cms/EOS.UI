using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Controls.Pickers;
using EOS.UI.iOS.Sandbox.Helpers;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Themes.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;
using UIFrameworks.Shared.Themes.Helpers;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class SimpleButtonView : BaseViewController
    {
        public const string Identifier = "SimpleButtonView";

        private SimpleButton _simpleButton;
        private List<UITextField> _textFields;

        public SimpleButtonView (IntPtr handle) : base (handle)
        {
              
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _simpleButton = new SimpleButton();
            _simpleButton.SetTitle("Simple button", UIControlState.Normal);

            _simpleButton.UpdateAppearance();

            _textFields = new List<UITextField>()
            {
                themeTextField,
                fontTextField,
                letterSpacingTextField,
                textSizeTextField,
                textColorEnabledTextField,
                textColorDisabledTextField,
                textColorPressedTextField,
                backgroundColorEnabledTextField,
                backgroundColorDisabledTextField,
                backgroundColorPressedTextField
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _textFields.ForEach(f => f.ResignFirstResponder());
            }));

            containerView.ConstrainLayout(() => _simpleButton.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                                _simpleButton.Frame.GetCenterY() == containerView.Frame.GetCenterY(), _simpleButton);

            var rect = new CGRect(0, 0, 100, 150);

            InitThemeTextField(rect);
            InitFontTextField(rect);
            InitLetterSpacing(rect);
            InitTextSizeTextField(rect);
            InitTextColorEnabledTextField(rect);
            InitTextColorDisabledTextField(rect);
            InitTextColorPressedTextField(rect);
            InitBackgroundColorEnabledTextField(rect);
            InitBackgroundColorDisabledTextField(rect);
            InitBackgroundColorPressedTextField(rect);
            InitDisabledSwitch();
            InitResetButton();
        }

        private void InitThemeTextField(CGRect rect)
        {
            var themePicker = new UIPickerView(rect);
            themePicker.ShowSelectionIndicator = true;
            themePicker.DataSource = new ThemePickerSource();
            var themePickerDelegate = new ThemePickerDelegate();
            themePickerDelegate.DidSelected += (object sender, KeyValuePair<string, EOSThemeEnumeration> e) =>
            {
                themeTextField.Text = e.Key;
                _simpleButton.GetThemeProvider().SetCurrentTheme(e.Value);
                _simpleButton.UpdateAppearance();

                ResetFields();
            };
            themeTextField.Text = _simpleButton.GetThemeProvider().GetCurrentTheme().ThemeValues[EOSConstants.PrimaryColor] == UIColor.White ?
                "Light" : "Dark";

            themePicker.Delegate = themePickerDelegate;
            themeTextField.InputView = themePicker;
        }

        private void InitFontTextField(CGRect rect)
        {
            var fontPicker = new UIPickerView(rect);
            fontPicker.ShowSelectionIndicator = true;
            fontPicker.DataSource = new FontPickerSource();
            var fontPickerDelegate = new FontPickerDelegate();
            fontPickerDelegate.DidSelected += (object sender, UIFont e) =>
            {
                _simpleButton.Font = e;
                fontTextField.Text = e.Name;
            };
            fontTextField.EditingDidBegin += (sender, e) =>
            {
                var font = Constants.Fonts.ElementAt((int)fontPicker.SelectedRowInComponent(0));
                _simpleButton.Font = font;
                fontTextField.Text = font.Name;
            };
            fontPicker.Delegate = fontPickerDelegate;
            fontTextField.InputView = fontPicker;
        }

        private void InitLetterSpacing(CGRect rect)
        {
            var letterSpacingPicker = new UIPickerView(rect);
            letterSpacingPicker.ShowSelectionIndicator = true;
            letterSpacingPicker.DataSource = new LetterSpacingPickerSource();
            var letterSpacingPickerDelegate = new LetterSpacingPickerDelegate();
            letterSpacingPickerDelegate.DidSelected += (object sender, int e) =>
            {
                _simpleButton.LetterSpacing = e;
                letterSpacingTextField.Text = e.ToString();
            };
            letterSpacingTextField.EditingDidBegin += (sender, e) =>
            {
                var spacing = Constants.LetterSpacingValues[(int)letterSpacingPicker.SelectedRowInComponent(0)];
                _simpleButton.LetterSpacing = spacing;
                letterSpacingTextField.Text = spacing.ToString();
            };
            letterSpacingPicker.Delegate = letterSpacingPickerDelegate;
            letterSpacingTextField.InputView = letterSpacingPicker;
        }

        private void InitTextSizeTextField(CGRect rect)
        {
            var textSizePicker = new UIPickerView(rect);
            textSizePicker.ShowSelectionIndicator = true;
            textSizePicker.DataSource = new FontSizesPickerSource();
            var fontSizePickerDelegate = new FontSizesPickerDelegate();
            fontSizePickerDelegate.DidSelected += (object sender, int e) =>
            {
                _simpleButton.TextSize = e;
                textSizeTextField.Text = e.ToString();
            };
            textSizeTextField.EditingDidBegin += (sender, e) =>
            {
                var size = Constants.FontSizeValues[(int)textSizePicker.SelectedRowInComponent(0)];
                _simpleButton.TextSize = size;
                textSizeTextField.Text = size.ToString();
            };
            textSizePicker.Delegate = fontSizePickerDelegate;
            textSizeTextField.InputView = textSizePicker;
        }

        private void InitTextColorEnabledTextField(CGRect rect)
        {
            var textColorPicker = new UIPickerView(rect);
            textColorPicker.ShowSelectionIndicator = true;
            textColorPicker.DataSource = new ColorPickerSource();
            var textColorPickerDelegate = new ColorPickerDelegate();
            textColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _simpleButton.TextColor = e.Value;
                textColorEnabledTextField.Text = e.Key;
            };
            textColorEnabledTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)textColorPicker.SelectedRowInComponent(0));
                _simpleButton.TextColor = colorPair.Value;
                textColorEnabledTextField.Text = colorPair.Key;
            };
            textColorPicker.Delegate = textColorPickerDelegate;
            textColorEnabledTextField.InputView = textColorPicker;
        }

        private void InitTextColorDisabledTextField(CGRect rect)
        {
            var textColorDisabledPicker = new UIPickerView(rect);
            textColorDisabledPicker.ShowSelectionIndicator = true;
            textColorDisabledPicker.DataSource = new ColorPickerSource();
            var textColorDisabledPickerDelegate = new ColorPickerDelegate();
            textColorDisabledPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _simpleButton.DisabledTextColor = e.Value;
                textColorDisabledTextField.Text = e.Key;
            };
            textColorDisabledTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)textColorDisabledPicker.SelectedRowInComponent(0));
                _simpleButton.DisabledTextColor = colorPair.Value;
                textColorDisabledTextField.Text = colorPair.Key;
            };
            textColorDisabledPicker.Delegate = textColorDisabledPickerDelegate;
            textColorDisabledTextField.InputView = textColorDisabledPicker;
        }

        private void InitTextColorPressedTextField(CGRect rect)
        {
            var textColorDisabledPicker = new UIPickerView(rect);
            textColorDisabledPicker.ShowSelectionIndicator = true;
            textColorDisabledPicker.DataSource = new ColorPickerSource();
            var textColorDisabledPickerDelegate = new ColorPickerDelegate();
            textColorDisabledPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _simpleButton.PressedTextColor = e.Value;
                textColorPressedTextField.Text = e.Key;
            };
            textColorPressedTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)textColorDisabledPicker.SelectedRowInComponent(0));
                _simpleButton.PressedTextColor = colorPair.Value;
                textColorPressedTextField.Text = colorPair.Key;
            };
            textColorDisabledPicker.Delegate = textColorDisabledPickerDelegate;
            textColorPressedTextField.InputView = textColorDisabledPicker;
        }

        private void InitBackgroundColorEnabledTextField(CGRect rect)
        {
            var backgroundColorPicker = new UIPickerView(rect);
            backgroundColorPicker.ShowSelectionIndicator = true;
            backgroundColorPicker.DataSource = new ColorPickerSource();
            var backgroundColorPickerDelegate = new ColorPickerDelegate();
            backgroundColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _simpleButton.BackgroundColor = e.Value;
                backgroundColorEnabledTextField.Text = e.Key;
            };
            backgroundColorEnabledTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)backgroundColorPicker.SelectedRowInComponent(0));
                _simpleButton.BackgroundColor = colorPair.Value;
                backgroundColorEnabledTextField.Text = colorPair.Key;
            };
            backgroundColorPicker.Delegate = backgroundColorPickerDelegate;
            backgroundColorEnabledTextField.InputView = backgroundColorPicker;
        }

        private void InitBackgroundColorDisabledTextField(CGRect rect)
        {
            var backgroundColorPicker = new UIPickerView(rect);
            backgroundColorPicker.ShowSelectionIndicator = true;
            backgroundColorPicker.DataSource = new ColorPickerSource();
            var backgroundColorPickerDelegate = new ColorPickerDelegate();
            backgroundColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _simpleButton.DisabledBackgroundColor = e.Value;
                backgroundColorDisabledTextField.Text = e.Key;
            };
            backgroundColorDisabledTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)backgroundColorPicker.SelectedRowInComponent(0));
                _simpleButton.DisabledBackgroundColor = colorPair.Value;
                backgroundColorDisabledTextField.Text = colorPair.Key;
            };
            backgroundColorPicker.Delegate = backgroundColorPickerDelegate;
            backgroundColorDisabledTextField.InputView = backgroundColorPicker;
        }

        private void InitBackgroundColorPressedTextField(CGRect rect)
        {
            var backgroundColorPicker = new UIPickerView(rect);
            backgroundColorPicker.ShowSelectionIndicator = true;
            backgroundColorPicker.DataSource = new ColorPickerSource();
            var backgroundColorPickerDelegate = new ColorPickerDelegate();
            backgroundColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _simpleButton.PressedBackgroundColor = e.Value;
                backgroundColorPressedTextField.Text = e.Key;
            };
            backgroundColorPressedTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)backgroundColorPicker.SelectedRowInComponent(0));
                _simpleButton.PressedBackgroundColor = colorPair.Value;
                backgroundColorPressedTextField.Text = colorPair.Key;
            };
            backgroundColorPicker.Delegate = backgroundColorPickerDelegate;
            backgroundColorPressedTextField.InputView = backgroundColorPicker;
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
            fontTextField.Text = string.Empty;
            letterSpacingTextField.Text = string.Empty;
            textSizeTextField.Text = string.Empty;
            textColorEnabledTextField.Text = string.Empty;
            textColorDisabledTextField.Text = string.Empty;
            textColorPressedTextField.Text = string.Empty;
            backgroundColorEnabledTextField.Text = string.Empty;
            backgroundColorDisabledTextField.Text = string.Empty;
            backgroundColorPressedTextField.Text = string.Empty;
        }

    }
}