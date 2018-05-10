using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.iOS.Sandbox.Helpers;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using EOS.UI.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Helpers;
using EOS.UI.iOS.Sandbox.Controls.Pickers;

namespace EOS.UI.iOS.Sandbox
{
    public partial class BadgeLabelView : BaseViewController
    {
        public const string Identifier = "BadgeLabelView";

        private List<UITextField> _textFields;

        public BadgeLabelView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var label = new BadgeLabel();
            label.Text = "Default Text";

            _textFields = new List<UITextField>()
            {
                backgroundColorField,
                letterSpacingField,
                themeField,
                fontField,
                fontColorField,
                fontSizeField,
                cornerRadiusField
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _textFields.ForEach(f => f.ResignFirstResponder());
            }));

            containerView.ConstrainLayout(() => label.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                                label.Frame.GetCenterY() == containerView.Frame.GetCenterY(), label);

            var rect = new CGRect(0, 0, 100, 150);

            var themePicker = new UIPickerView(rect);
            themePicker.ShowSelectionIndicator = true;
			themePicker.DataSource = new DictionaryPickerSource<string, EOSThemeEnumeration>(Constants.Themes);
            var themePickerDelegate = new DictionaryPickerDelegate<String ,EOSThemeEnumeration>(Constants.Themes);
            themePickerDelegate.DidSelected += (object sender, KeyValuePair<string, EOSThemeEnumeration> e) =>
            {
                themeField.Text = e.Key;
                var provider = label.GetThemeProvider();
                provider.SetCurrentTheme(e.Value);
                label.ResetCustomization();
                _textFields.Except(new[] { themeField }).ToList().ForEach(f => f.Text = String.Empty);
            };
            themeField.Text = label.GetThemeProvider().GetCurrentTheme().ThemeValues[EOSConstants.PrimaryColor] == UIColor.White ?
                "Light" : "Dark";
            themePicker.Delegate = themePickerDelegate;
            themeField.InputView = themePicker;

            var colorPicker = new UIPickerView(rect);
            colorPicker.ShowSelectionIndicator = true;
            colorPicker.DataSource = new ColorPickerSource();
            var backgroundColorPickerDelegate = new ColorPickerDelegate();
            backgroundColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                label.BackgroundColor = e.Value;
                backgroundColorField.Text = e.Key;
            };
            backgroundColorField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)colorPicker.SelectedRowInComponent(0));
                label.BackgroundColor = colorPair.Value;
                backgroundColorField.Text = colorPair.Key;
            };
            colorPicker.Delegate = backgroundColorPickerDelegate;
            backgroundColorField.InputView = colorPicker;

            var fontPicker = new UIPickerView(rect);
            fontPicker.ShowSelectionIndicator = true;
            fontPicker.DataSource = new ValuePickerSource<UIFont>(Constants.Fonts);
			var fontPickerDelegate = new ValuePickerDelegate<UIFont>(Constants.Fonts);
            fontPickerDelegate.DidSelected += (object sender, UIFont e) =>
            {
                label.Font = e;
                fontField.Text = e.Name;
            };
            fontField.EditingDidBegin += (sender, e) =>
            {
                var font = Constants.Fonts.ElementAt((int)fontPicker.SelectedRowInComponent(0));
                label.Font = font;
                fontField.Text = font.Name;
            };
            fontPicker.Delegate = fontPickerDelegate;
            fontField.InputView = fontPicker;

            var fontColorPicker = new UIPickerView(rect);
            fontColorPicker.ShowSelectionIndicator = true;
            fontColorPicker.DataSource = new ColorPickerSource();
            var fontColorPickerDelegate = new ColorPickerDelegate();
            fontColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                label.TextColor = e.Value;
                fontColorField.Text = e.Key;
            };
            fontColorField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)fontColorPicker.SelectedRowInComponent(0));
                label.TextColor = colorPair.Value;
                fontColorField.Text = colorPair.Key;
            };
            fontColorPicker.Delegate = fontColorPickerDelegate;
            fontColorField.InputView = fontColorPicker;

            var letterSpacingPicker = new UIPickerView(rect);
            letterSpacingPicker.ShowSelectionIndicator = true;
            letterSpacingPicker.DataSource = new ValuePickerSource<int>(Constants.LetterSpacingValues);
            var letterSpacingPickerDelegate = new ValuePickerDelegate<int>(Constants.LetterSpacingValues);
            letterSpacingPickerDelegate.DidSelected += (object sender, int e) =>
            {
                label.LetterSpacing = e;
                letterSpacingField.Text = e.ToString();
            };
            letterSpacingField.EditingDidBegin += (sender, e) =>
            {
                var spacing = Constants.LetterSpacingValues[(int)letterSpacingPicker.SelectedRowInComponent(0)];
                label.LetterSpacing = spacing;
                letterSpacingField.Text = spacing.ToString();
            };
            letterSpacingPicker.Delegate = letterSpacingPickerDelegate;
            letterSpacingField.InputView = letterSpacingPicker;


            var fontSizePicker = new UIPickerView(rect);
            fontSizePicker.ShowSelectionIndicator = true;
            fontSizePicker.DataSource = new ValuePickerSource<int>(Constants.FontSizeValues);
            var fontSizePickerDelegate = new ValuePickerDelegate<int>(Constants.FontSizeValues);
            fontSizePickerDelegate.DidSelected += (object sender, int e) =>
            {
                label.TextSize = e;
                fontSizeField.Text = e.ToString();
            };
            fontSizeField.EditingDidBegin += (sender, e) =>
            {
                var size = Constants.FontSizeValues[(int)fontSizePicker.SelectedRowInComponent(0)];
                label.TextSize = size;
                fontSizeField.Text = size.ToString();
            };
            fontSizePicker.Delegate = fontSizePickerDelegate;
            fontSizeField.InputView = fontSizePicker;

            var cornerRadiusPicker = new UIPickerView(rect);
            cornerRadiusPicker.ShowSelectionIndicator = true;
            cornerRadiusPicker.DataSource = new ValuePickerSource<int>(Constants.CornerRadiusValues);
            var cornerRadiusPickerDelegate = new ValuePickerDelegate<int>(Constants.CornerRadiusValues);
            cornerRadiusPickerDelegate.DidSelected += (object sender, int e) =>
            {
                label.CornerRadius = e;
                cornerRadiusField.Text = e.ToString();
            };
            cornerRadiusField.EditingDidBegin += (sender, e) =>
            {
                var size = Constants.CornerRadiusValues[(int)cornerRadiusPicker.SelectedRowInComponent(0)];
                label.CornerRadius = size;
                cornerRadiusField.Text = size.ToString();
            };
            cornerRadiusPicker.Delegate = cornerRadiusPickerDelegate;
            cornerRadiusField.InputView = cornerRadiusPicker;

            resetButton.TouchUpInside += (sender, e) =>
            {
                label.ResetCustomization();
                _textFields.Except(new List<UITextField>() { themeField }).ToList().ForEach(f => f.Text = String.Empty);
            };
        }
    }
}