using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Controls.Pickers;
using EOS.UI.iOS.Sandbox.Helpers;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Themes.Helpers;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using UIFrameworks.Shared.Themes.Helpers;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class GhostButtonView : BaseViewController
    {
        public const string Identifier = "GhostButtonView";
        private List<UITextField> _textFields;

        public GhostButtonView (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
            base.ViewDidLoad();

            var ghostButton = new GhostButton();
            ghostButton.SetTitle("DEFAULT TEXT", UIControlState.Normal);

            containerView.ConstrainLayout(() => ghostButton.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                          ghostButton.Frame.GetCenterY() == containerView.Frame.GetCenterY() &&
                                          ghostButton.Frame.Left == containerView.Frame.Left &&
                                          ghostButton.Frame.Right == containerView.Frame.Right, ghostButton);

            _textFields = new List<UITextField>()
            {
                themeField,
                fontField,
                letterSpacingField,
                enabledColorField,
                disabledColorField,
                pressedColorField,
                fontSizeField,
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _textFields.ForEach(f => f.ResignFirstResponder());
            }));

            var rect = new CGRect(0, 0, 100, 100);
            var themePicker = new UIPickerView(rect);
            themePicker.ShowSelectionIndicator = true;
			themePicker.DataSource = new DictionaryPickerSource<String, EOSThemeEnumeration>(Constants.Themes);
			var themePickerDelegate = new DictionaryPickerDelegate<String, EOSThemeEnumeration>(Constants.Themes);
            themePickerDelegate.DidSelected += (object sender, KeyValuePair<string, EOSThemeEnumeration> e) =>
            {
                themeField.Text = e.Key;
                var provider = ghostButton.GetThemeProvider();
                provider.SetCurrentTheme(e.Value);
                ghostButton.UpdateAppearance();
                _textFields.Except(new []{themeField}).ToList().ForEach(f => f.Text = String.Empty);
            };
            themeField.Text = ghostButton.GetThemeProvider().GetCurrentTheme().ThemeValues[EOSConstants.PrimaryColor] == UIColor.White ?
                "Light" : "Dark";
            themePicker.Delegate = themePickerDelegate;
            themeField.InputView = themePicker;

            var fontPicker = new UIPickerView(rect);
            fontPicker.ShowSelectionIndicator = true;
			fontPicker.DataSource = new ValuePickerSource<UIFont>(Constants.Fonts);
			var fontPickerDelegate = new ValuePickerDelegate<UIFont>(Constants.Fonts);
            fontPickerDelegate.DidSelected += (object sender, UIFont e) =>
            {
                ghostButton.Font = e;
                fontField.Text = e.Name;
            };
            fontField.EditingDidBegin += (sender, e) =>
            {
                var font = Constants.Fonts.ElementAt((int)fontPicker.SelectedRowInComponent(0));
                ghostButton.Font = font;
                fontField.Text = font.Name;
            };
            fontPicker.Delegate = fontPickerDelegate;
            fontField.InputView = fontPicker;

            var letterSpacingPicker = new UIPickerView(rect);
            letterSpacingPicker.ShowSelectionIndicator = true;
			letterSpacingPicker.DataSource = new ValuePickerSource<int>(Constants.LetterSpacingValues);
			var letterSpacingPickerDelegate = new ValuePickerDelegate<int>(Constants.LetterSpacingValues);
            letterSpacingPickerDelegate.DidSelected += (object sender, int e) =>
            {
                ghostButton.LetterSpacing = e;
                letterSpacingField.Text = e.ToString();
            };
            letterSpacingField.EditingDidBegin += (sender, e) =>
            {
                var spacing = Constants.LetterSpacingValues[(int)letterSpacingPicker.SelectedRowInComponent(0)];
                ghostButton.LetterSpacing = spacing;
                letterSpacingField.Text = spacing.ToString();
            };
            letterSpacingPicker.Delegate = letterSpacingPickerDelegate;
            letterSpacingField.InputView = letterSpacingPicker;

            var enabledColorPicker = new UIPickerView(rect);
            enabledColorPicker.ShowSelectionIndicator = true;
            enabledColorPicker.DataSource = new ColorPickerSource();
            var enabledColorDelegate = new ColorPickerDelegate();
            enabledColorDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                ghostButton.EnabledTextColor = e.Value;
                enabledColorField.Text = e.Key;
            };
            enabledColorField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)enabledColorPicker.SelectedRowInComponent(0));
                ghostButton.EnabledTextColor = colorPair.Value;
                enabledColorField.Text = colorPair.Key;
            };
            enabledColorPicker.Delegate = enabledColorDelegate;
            enabledColorField.InputView = enabledColorPicker;

            var disabledColorPicker = new UIPickerView(rect);
            disabledColorPicker.ShowSelectionIndicator = true;
            disabledColorPicker.DataSource = new ColorPickerSource();
            var disabledColorDelegate = new ColorPickerDelegate();
            disabledColorDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                ghostButton.DisabledTextColor = e.Value;
                disabledColorField.Text = e.Key;
            };
            disabledColorField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)disabledColorPicker.SelectedRowInComponent(0));
                ghostButton.DisabledTextColor = colorPair.Value;
                disabledColorField.Text = colorPair.Key;
            };
            disabledColorPicker.Delegate = disabledColorDelegate;
            disabledColorField.InputView = disabledColorPicker;

            var fontSizePicker = new UIPickerView(rect);
            fontSizePicker.ShowSelectionIndicator = true;
			fontSizePicker.DataSource = new ValuePickerSource<int>(Constants.FontSizeValues);
			var fontSizePickerDelegate = new ValuePickerDelegate<int>(Constants.FontSizeValues);
            fontSizePickerDelegate.DidSelected += (object sender, int e) =>
            {
                ghostButton.TextSize = e;
                fontSizeField.Text = e.ToString();
            };
            fontSizeField.EditingDidBegin += (sender, e) =>
            {
                var size = Constants.FontSizeValues[(int)fontSizePicker.SelectedRowInComponent(0)];
                ghostButton.TextSize = size;
                fontSizeField.Text = size.ToString();
            };
            fontSizePicker.Delegate = fontSizePickerDelegate;
            fontSizeField.InputView = fontSizePicker;

            var pressedColorPicker = new UIPickerView(rect);
            pressedColorPicker.ShowSelectionIndicator = true;
            pressedColorPicker.DataSource = new ColorPickerSource();
            var pressedColorPickerDelegate = new ColorPickerDelegate();
            pressedColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                ghostButton.PressedStateTextColor = e.Value;
                pressedColorField.Text = e.Key;
            };
            pressedColorField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)pressedColorPicker.SelectedRowInComponent(0));
                ghostButton.PressedStateTextColor = colorPair.Value;
                pressedColorField.Text = colorPair.Key;
            };
            pressedColorPicker.Delegate = pressedColorPickerDelegate;
            pressedColorField.InputView = pressedColorPicker;

            stateSwitch.ValueChanged += (sender, e) => 
            {
                ghostButton.Enabled = stateSwitch.On;
            };

            resetButton.TouchUpInside += (sender, e) =>
            {
                _textFields.Except(new List<UITextField>() { themeField }).ToList().ForEach(f => f.Text = String.Empty);
                ghostButton.ResetCustomization();
                var v = ghostButton.Enabled;
            };
		}
	}
}