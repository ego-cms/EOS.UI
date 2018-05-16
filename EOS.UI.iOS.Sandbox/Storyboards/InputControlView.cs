using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Controls.Pickers;
using EOS.UI.iOS.Sandbox.Helpers;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Themes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using UIFrameworks.Shared.Themes.Helpers;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class InputControlView : BaseViewController
    {
        public const string Identifier = "InputControlView";

        private Input _inputTop;
        private Input _inputBotton;

        private List<UITextField> _textFields;

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

            _textFields = new List<UITextField>()
            {
                themeTextField,
                fontTextField,
                letterSpacingTextField,
                textSizeTextField,
                textColorTextField,
                textColorDisabledTextField,
                placeholderColorTextField,
                placeholderColorDisabledTextField,
                iconFocusedTextField,
                iconUnfocusedTextField,
                iconDisabledTextField,
                underlineColorFocusedTextField,
                underlineColorUnfocusedTextField,
                underlineColorDisabledTextField
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _textFields.ForEach(f => f.ResignFirstResponder());
            }));

            containerTopView.ConstrainLayout(() => _inputTop.Frame.GetCenterX() == containerTopView.Frame.GetCenterX() &&
                                                   _inputTop.Frame.GetCenterY() == containerTopView.Frame.GetCenterY(), _inputTop);

            containerBottomView.ConstrainLayout(() => _inputBotton.Frame.GetCenterX() == containerBottomView.Frame.GetCenterX() &&
                                                      _inputBotton.Frame.GetCenterY() == containerBottomView.Frame.GetCenterY(), _inputBotton);

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
            InitIconUnfocusedTextField(rect);
            InitIconDisabledTextField(rect);
            InitUnderlineFocusedColorTextField(rect);
            InitUnderlineUnfocusedColorTextField(rect);
            InitUnderlineDisabledColorTextField(rect);
            InitDisabledSwitch();
            InitResetButton();
        }

        private void ResetFields()
        {
            fontTextField.Text = string.Empty;
            letterSpacingTextField.Text = string.Empty;
            textSizeTextField.Text = string.Empty;
            textColorTextField.Text = string.Empty;
            textColorDisabledTextField.Text = string.Empty;
            placeholderColorTextField.Text = string.Empty;
            placeholderColorDisabledTextField.Text = string.Empty;
            iconFocusedTextField.Text = string.Empty;
            iconUnfocusedTextField.Text = string.Empty;
            iconDisabledTextField.Text = string.Empty;
            underlineColorFocusedTextField.Text = string.Empty;
            underlineColorUnfocusedTextField.Text = string.Empty;
            underlineColorDisabledTextField.Text = string.Empty;
        }

        private void InitThemeTextField(CGRect rect)
        {
            var themePicker = new UIPickerView(rect);
            themePicker.ShowSelectionIndicator = true;
			themePicker.DataSource = new DictionaryPickerSource<String, EOSThemeEnumeration>(Constants.Themes);
			var themePickerDelegate = new DictionaryPickerDelegate<String, EOSThemeEnumeration>(Constants.Themes);
            themePickerDelegate.DidSelected += (object sender, KeyValuePair<string, EOSThemeEnumeration> e) =>
            {
                themeTextField.Text = e.Key;
                _inputTop.GetThemeProvider().SetCurrentTheme(e.Value);
                _inputTop.UpdateAppearance();
                _inputBotton.UpdateAppearance();

                ResetFields();
            };
            themeTextField.Text = _inputTop.GetThemeProvider().GetCurrentTheme().ThemeValues[EOSConstants.PrimaryColor] == UIColor.White ?
                "Light" : "Dark";

            themePicker.Delegate = themePickerDelegate;
            themeTextField.InputView = themePicker;
        }

        private void InitFontTextField(CGRect rect)
        {
            var fontPicker = new UIPickerView(rect);
            fontPicker.ShowSelectionIndicator = true;
			fontPicker.DataSource = new ValuePickerSource<UIFont>(Constants.Fonts);
			var fontPickerDelegate = new ValuePickerDelegate<UIFont>(Constants.Fonts);
            fontPickerDelegate.DidSelected += (object sender, UIFont e) =>
            {
                _inputTop.Font = e;
                _inputBotton.Font = e;
                fontTextField.Text = e.Name;
            };
            fontTextField.EditingDidBegin += (sender, e) =>
            {
                var font = Constants.Fonts.ElementAt((int)fontPicker.SelectedRowInComponent(0));
                _inputTop.Font = font;
                _inputBotton.Font = font;
                fontTextField.Text = font.Name;
            };
            fontPicker.Delegate = fontPickerDelegate;
            fontTextField.InputView = fontPicker;
        }

        private void InitLetterSpacing(CGRect rect)
        {
            var letterSpacingPicker = new UIPickerView(rect);
            letterSpacingPicker.ShowSelectionIndicator = true;
            letterSpacingPicker.DataSource = new ValuePickerSource<int>(Constants.LetterSpacingValues);
            var letterSpacingPickerDelegate = new ValuePickerDelegate<int>(Constants.LetterSpacingValues);
            letterSpacingPickerDelegate.DidSelected += (object sender, int e) =>
            {
                _inputTop.LetterSpacing = e;
                _inputBotton.LetterSpacing = e;
                letterSpacingTextField.Text = e.ToString();
            };
            letterSpacingTextField.EditingDidBegin += (sender, e) =>
            {
                var spacing = Constants.LetterSpacingValues[(int)letterSpacingPicker.SelectedRowInComponent(0)];
                _inputBotton.LetterSpacing = spacing;
                _inputTop.LetterSpacing = spacing;
                letterSpacingTextField.Text = spacing.ToString();
            };
            letterSpacingPicker.Delegate = letterSpacingPickerDelegate;
            letterSpacingTextField.InputView = letterSpacingPicker;
        }

        private void InitTextSizeTextField(CGRect rect)
        {
            var textSizePicker = new UIPickerView(rect);
            textSizePicker.ShowSelectionIndicator = true;
            textSizePicker.DataSource = new ValuePickerSource<int>(Constants.FontSizeValues);
			var fontSizePickerDelegate = new ValuePickerDelegate<int>(Constants.FontSizeValues);
            fontSizePickerDelegate.DidSelected += (object sender, int e) =>
            {
                _inputTop.TextSize = e;
                _inputBotton.TextSize = e;
                textSizeTextField.Text = e.ToString();
            };
            textSizeTextField.EditingDidBegin += (sender, e) =>
            {
                var size = Constants.FontSizeValues[(int)textSizePicker.SelectedRowInComponent(0)];
                _inputTop.TextSize = size;
                _inputBotton.TextSize = size;
                textSizeTextField.Text = size.ToString();
            };
            textSizePicker.Delegate = fontSizePickerDelegate;
            textSizeTextField.InputView = textSizePicker;
        }

        private void InitTextColorTextField(CGRect rect)
        {
            var textColorPicker = new UIPickerView(rect);
            textColorPicker.ShowSelectionIndicator = true;
            textColorPicker.DataSource = new ColorPickerSource();
            var textColorPickerDelegate = new ColorPickerDelegate();
            textColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _inputTop.TextColor = e.Value;
                _inputBotton.TextColor = e.Value;
                textColorTextField.Text = e.Key;
            };
            textColorTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)textColorPicker.SelectedRowInComponent(0));
                _inputTop.TextColor = colorPair.Value;
                _inputBotton.TextColor = colorPair.Value;
                textColorTextField.Text = colorPair.Key;
            };
            textColorPicker.Delegate = textColorPickerDelegate;
            textColorTextField.InputView = textColorPicker;
        }

        private void InitTextColorDisabledTextField(CGRect rect)
        {
            var textColorDisabledPicker = new UIPickerView(rect);
            textColorDisabledPicker.ShowSelectionIndicator = true;
            textColorDisabledPicker.DataSource = new ColorPickerSource();
            var textColorDisabledPickerDelegate = new ColorPickerDelegate();
            textColorDisabledPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _inputTop.TextColorDisabled = e.Value;
                _inputBotton.TextColorDisabled = e.Value;
                textColorDisabledTextField.Text = e.Key;
            };
            textColorDisabledTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)textColorDisabledPicker.SelectedRowInComponent(0));
                _inputTop.TextColorDisabled = colorPair.Value;
                _inputBotton.TextColorDisabled = colorPair.Value;
                textColorDisabledTextField.Text = colorPair.Key;
            };
            textColorDisabledPicker.Delegate = textColorDisabledPickerDelegate;
            textColorDisabledTextField.InputView = textColorDisabledPicker;
        }

        private void InitPlaceholderTextField(CGRect rect)
        {
            var placeholderColorPicker = new UIPickerView(rect);
            placeholderColorPicker.ShowSelectionIndicator = true;
            placeholderColorPicker.DataSource = new ColorPickerSource();
            var placeholderColorPickerDelegate = new ColorPickerDelegate();
            placeholderColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _inputTop.PlaceholderColor = e.Value;
                _inputBotton.PlaceholderColor = e.Value;
                placeholderColorTextField.Text = e.Key;
            };
            placeholderColorTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)placeholderColorPicker.SelectedRowInComponent(0));
                _inputTop.PlaceholderColor = colorPair.Value;
                _inputBotton.PlaceholderColor = colorPair.Value;
                placeholderColorTextField.Text = colorPair.Key;
            };
            placeholderColorPicker.Delegate = placeholderColorPickerDelegate;
            placeholderColorTextField.InputView = placeholderColorPicker;
        }

        private void InitPlaceholderDisabledTextField(CGRect rect)
        {
            var placeholderDisabledColorPicker = new UIPickerView(rect);
            placeholderDisabledColorPicker.ShowSelectionIndicator = true;
            placeholderDisabledColorPicker.DataSource = new ColorPickerSource();
            var placeholderDisabledColorPickerDelegate = new ColorPickerDelegate();
            placeholderDisabledColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _inputTop.PlaceholderColorDisabled = e.Value;
                _inputBotton.PlaceholderColorDisabled = e.Value;
                placeholderColorDisabledTextField.Text = e.Key;
            };
            placeholderColorDisabledTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)placeholderDisabledColorPicker.SelectedRowInComponent(0));
                _inputTop.PlaceholderColorDisabled = colorPair.Value;
                _inputBotton.PlaceholderColorDisabled = colorPair.Value;
                placeholderColorDisabledTextField.Text = colorPair.Key;
            };
            placeholderDisabledColorPicker.Delegate = placeholderDisabledColorPickerDelegate;
            placeholderColorDisabledTextField.InputView = placeholderDisabledColorPicker;
        }

        private void InitIconFocusedTextField(CGRect rect)
        {
            var iconFocusedPicker = new UIPickerView(rect);
            iconFocusedPicker.ShowSelectionIndicator = true;
			iconFocusedPicker.DataSource = new ValuePickerSource<String>(Constants.Icons);
			var iconFocusedPickerDelegate = new ValuePickerDelegate<String>(Constants.Icons);
            iconFocusedPickerDelegate.DidSelected += (object sender, string e) =>
            {
				_inputTop.LeftImageFocused = UIImage.FromBundle(e);
				_inputBotton.LeftImageFocused = UIImage.FromBundle(e);
				iconFocusedTextField.Text = e;
            };
            iconFocusedTextField.EditingDidBegin += (sender, e) =>
            {
				var iconName = Constants.Icons.ElementAt((int)iconFocusedPicker.SelectedRowInComponent(0));
                _inputTop.LeftImageFocused = UIImage.FromBundle(iconName);
                _inputBotton.LeftImageFocused = UIImage.FromBundle(iconName);
                iconFocusedTextField.Text = iconName;
            };
            iconFocusedPicker.Delegate = iconFocusedPickerDelegate;
            iconFocusedTextField.InputView = iconFocusedPicker;
        }

        private void InitIconUnfocusedTextField(CGRect rect)
        {
            var iconUnfocusedPicker = new UIPickerView(rect);
            iconUnfocusedPicker.ShowSelectionIndicator = true;
			iconUnfocusedPicker.DataSource = new ValuePickerSource<String>(Constants.Icons);
			var iconUnfocusedPickerDelegate = new ValuePickerDelegate<String>(Constants.Icons);
            iconUnfocusedPickerDelegate.DidSelected += (object sender, String e) =>
            {
                _inputTop.LeftImageUnfocused = UIImage.FromBundle(e);
				_inputBotton.LeftImageUnfocused = UIImage.FromBundle(e);
                iconUnfocusedTextField.Text = e;
            };
            iconUnfocusedTextField.EditingDidBegin += (sender, e) =>
            {
				var iconName = Constants.Icons.ElementAt((int)iconUnfocusedPicker.SelectedRowInComponent(0));
                _inputTop.LeftImageUnfocused = UIImage.FromBundle(iconName);
                _inputBotton.LeftImageUnfocused = UIImage.FromBundle(iconName);
                iconUnfocusedTextField.Text = iconName;
            };
            iconUnfocusedPicker.Delegate = iconUnfocusedPickerDelegate;
            iconUnfocusedTextField.InputView = iconUnfocusedPicker;
        }

        private void InitIconDisabledTextField(CGRect rect)
        {
            var iconDisabledPicker = new UIPickerView(rect);
            iconDisabledPicker.ShowSelectionIndicator = true;
			iconDisabledPicker.DataSource = new ValuePickerSource<String>(Constants.Icons);
			var iconDisabledPickerDelegate = new ValuePickerDelegate<String>(Constants.Icons);
            iconDisabledPickerDelegate.DidSelected += (object sender, String e) =>
            {
                _inputTop.LeftImageDisabled = UIImage.FromBundle(e);
                _inputBotton.LeftImageDisabled = UIImage.FromBundle(e);
                iconDisabledTextField.Text = e;
            };
            iconDisabledTextField.EditingDidBegin += (sender, e) =>
            {
				var iconName = Constants.Icons.ElementAt((int)iconDisabledPicker.SelectedRowInComponent(0));
                _inputTop.LeftImageDisabled = UIImage.FromBundle(iconName);
                _inputBotton.LeftImageDisabled = UIImage.FromBundle(iconName);
				iconDisabledTextField.Text = iconName;
            };
            iconDisabledPicker.Delegate = iconDisabledPickerDelegate;
            iconDisabledTextField.InputView = iconDisabledPicker;
        }

        private void InitUnderlineFocusedColorTextField(CGRect rect)
        {
            var underlineFocusedColorPicker = new UIPickerView(rect);
            underlineFocusedColorPicker.ShowSelectionIndicator = true;
            underlineFocusedColorPicker.DataSource = new ColorPickerSource();
            var underlineFocusedColorPickerDelegate = new ColorPickerDelegate();
            underlineFocusedColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _inputTop.UnderlineColorFocused = e.Value;
                _inputBotton.UnderlineColorFocused = e.Value;
                underlineColorFocusedTextField.Text = e.Key;
            };
            underlineColorFocusedTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)underlineFocusedColorPicker.SelectedRowInComponent(0));
                _inputTop.UnderlineColorFocused = colorPair.Value;
                _inputBotton.UnderlineColorFocused = colorPair.Value;
                underlineColorFocusedTextField.Text = colorPair.Key;
            };
            underlineFocusedColorPicker.Delegate = underlineFocusedColorPickerDelegate;
            underlineColorFocusedTextField.InputView = underlineFocusedColorPicker;
        }

        private void InitUnderlineUnfocusedColorTextField(CGRect rect)
        {
            var underlineUnfocusedColorPicker = new UIPickerView(rect);
            underlineUnfocusedColorPicker.ShowSelectionIndicator = true;
            underlineUnfocusedColorPicker.DataSource = new ColorPickerSource();
            var underlineUnfocusedColorPickerDelegate = new ColorPickerDelegate();
            underlineUnfocusedColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _inputTop.UnderlineColorUnfocused = e.Value;
                _inputBotton.UnderlineColorUnfocused = e.Value;
                underlineColorUnfocusedTextField.Text = e.Key;
            };
            underlineColorUnfocusedTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)underlineUnfocusedColorPicker.SelectedRowInComponent(0));
                _inputTop.UnderlineColorUnfocused = colorPair.Value;
                _inputBotton.UnderlineColorUnfocused = colorPair.Value;
                underlineColorUnfocusedTextField.Text = colorPair.Key;
            };
            underlineUnfocusedColorPicker.Delegate = underlineUnfocusedColorPickerDelegate;
            underlineColorUnfocusedTextField.InputView = underlineUnfocusedColorPicker;
        }

        private void InitUnderlineDisabledColorTextField(CGRect rect)
        {
            var underlineDisabledColorPicker = new UIPickerView(rect);
            underlineDisabledColorPicker.ShowSelectionIndicator = true;
            underlineDisabledColorPicker.DataSource = new ColorPickerSource();
            var underlineDisabledColorPickerDelegate = new ColorPickerDelegate();
            underlineDisabledColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _inputTop.UnderlineColorDisabled = e.Value;
                _inputBotton.UnderlineColorDisabled = e.Value;
                underlineColorDisabledTextField.Text = e.Key;
            };
            underlineColorDisabledTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)underlineDisabledColorPicker.SelectedRowInComponent(0));
                _inputTop.UnderlineColorDisabled = colorPair.Value;
                _inputBotton.UnderlineColorDisabled = colorPair.Value;
                underlineColorDisabledTextField.Text = colorPair.Key;
            };
            underlineDisabledColorPicker.Delegate = underlineDisabledColorPickerDelegate;
            underlineColorDisabledTextField.InputView = underlineDisabledColorPicker;
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
                _inputTop.ResetCustomization();
                _inputBotton.ResetCustomization();
                ResetFields();
            };
        }
	}
}