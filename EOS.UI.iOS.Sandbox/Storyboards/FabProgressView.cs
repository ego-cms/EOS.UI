using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Helpers;
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
using Constants = EOS.UI.iOS.Sandbox.Helpers.Constants;

namespace EOS.UI.iOS.Sandbox
{
    public partial class FabProgressView : BaseViewController
    {
        public const string Identifier = "FabProgressView";
        private List<UITextField> _textFields;
        private FabProgress _fab;

        public FabProgressView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _fab = new FabProgress();

            UpdateFrame();
            containerView.AddSubview(_fab);

            _textFields = new List<UITextField>()
            {
                backgroundField,
                themeField,
                sizeField,
                disabledField,
                pressedField,
            };
            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _textFields.ForEach(f => f.ResignFirstResponder());
            }));

            var rect = new CGRect(0, 0, 100, 150);
            var themePicker = new UIPickerView(rect);
            themePicker.ShowSelectionIndicator = true;
			themePicker.DataSource = new DictionaryPickerSource<String, EOSThemeEnumeration>(Constants.Themes);
			var themePickerDelegate = new DictionaryPickerDelegate<String ,EOSThemeEnumeration>(Constants.Themes);
            themePickerDelegate.DidSelected += (object sender, KeyValuePair<string, EOSThemeEnumeration> e) =>
            {
                themeField.Text = e.Key;
                var provider = _fab.GetThemeProvider();
                provider.SetCurrentTheme(e.Value);
                _fab.ResetCustomization();
                _textFields.Except(new[] { themeField }).ToList().ForEach(f => f.Text = String.Empty);
            };
            themeField.Text = _fab.GetThemeProvider().GetCurrentTheme().ThemeValues[EOSConstants.PrimaryColor] == UIColor.White ?
                "Light" : "Dark";
            themePicker.Delegate = themePickerDelegate;
            themeField.InputView = themePicker;

            var colorPicker = new UIPickerView(rect);
            colorPicker.ShowSelectionIndicator = true;
            colorPicker.DataSource = new ColorPickerSource();
            var backgroundColorPickerDelegate = new ColorPickerDelegate();
            backgroundColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _fab.BackgroundColor = e.Value;
                backgroundField.Text = e.Key;
            };
            backgroundField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)colorPicker.SelectedRowInComponent(0));
                _fab.BackgroundColor = colorPair.Value;
                backgroundField.Text = colorPair.Key;
            };
            colorPicker.Delegate = backgroundColorPickerDelegate;
            backgroundField.InputView = colorPicker;

            var pressedColorPicker = new UIPickerView(rect);
            pressedColorPicker.ShowSelectionIndicator = true;
            pressedColorPicker.DataSource = new ColorPickerSource();
            var pressedColorDelegate = new ColorPickerDelegate();
            pressedColorDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _fab.PressedBackgroundColor = e.Value;
                pressedField.Text = e.Key;
            };
            pressedField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)pressedColorPicker.SelectedRowInComponent(0));
                _fab.PressedBackgroundColor = colorPair.Value;
                pressedField.Text = colorPair.Key;
            };
            pressedColorPicker.Delegate = pressedColorDelegate;
            pressedField.InputView = pressedColorPicker;

            var disabledColorPicker = new UIPickerView(rect);
            disabledColorPicker.ShowSelectionIndicator = true;
            disabledColorPicker.DataSource = new ColorPickerSource();
            var disabledColorDelegate = new ColorPickerDelegate();
            disabledColorDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _fab.DisabledBackgroundColor = e.Value;
                disabledField.Text = e.Key;
            };
            disabledField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)disabledColorPicker.SelectedRowInComponent(0));
                _fab.DisabledBackgroundColor = colorPair.Value;
                disabledField.Text = colorPair.Key;
            };
            disabledColorPicker.Delegate = disabledColorDelegate;
            disabledField.InputView = disabledColorPicker;

            var sizePicker = new UIPickerView(rect);
            sizePicker.ShowSelectionIndicator = true;
            sizePicker.DataSource = new ValuePickerSource<int>(Constants.FabProgressSizes);
            var sizePickerDelegate = new ValuePickerDelegate<int>(Constants.FabProgressSizes);
            sizePickerDelegate.DidSelected += (object sender, int e) =>
            {
                _fab.ButtonSize = e;
                sizeField.Text = e.ToString();
                UpdateFrame();
            };
            sizeField.EditingDidBegin += (sender, e) =>
            {
                var size = Constants.FabProgressSizes[(int)sizePicker.SelectedRowInComponent(0)];
                _fab.ButtonSize = size;
                sizeField.Text = size.ToString();
                UpdateFrame();
            };
            sizePicker.Delegate = sizePickerDelegate;
            sizeField.InputView = sizePicker;

			var shadowPicker = new UIPickerView(rect);
			shadowPicker.ShowSelectionIndicator = true;
			shadowPicker.DataSource = new DictionaryPickerSource<String, ShadowConfig>(Constants.ShadowConfigs);
			var shadowPickerDelegate = new DictionaryPickerDelegate<String, ShadowConfig>(Constants.ShadowConfigs);
			shadowPickerDelegate.DidSelected += (object sender, KeyValuePair<String, ShadowConfig> e) =>
            {
				_fab.ShadowConfig = e.Value;
                shadowField.Text = e.Key;
            };
			shadowField.EditingDidBegin += (sender, e) =>
            {
				var configPair = Constants.ShadowConfigs.ElementAt((int)shadowPicker.SelectedRowInComponent(0));
				_fab.ShadowConfig = configPair.Value;
				shadowField.Text = configPair.Key;
            };
			shadowPicker.Delegate = shadowPickerDelegate;
			shadowField.InputView = shadowPicker;

            enableSwitch.ValueChanged += (sender, e) =>
            {
                _fab.Enabled = enableSwitch.On;
            };

            resetButton.TouchUpInside += (sender, e) =>
            {
                _fab.ResetCustomization();
                _textFields.Except(new List<UITextField>() { themeField }).ToList().ForEach(f => f.Text = String.Empty);
                UpdateFrame();
            };
        }

        protected void UpdateFrame()
        {
            var frame = new CGRect(containerView.Frame.Width / 2 - _fab.ButtonSize / 2, containerView.Frame.Height / 2 - _fab.ButtonSize / 2, _fab.ButtonSize, _fab.ButtonSize);
            _fab.Frame = frame;
        }
    }
}