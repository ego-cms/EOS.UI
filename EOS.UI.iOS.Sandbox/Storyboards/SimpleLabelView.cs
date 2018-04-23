using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Helpers;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Themes.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;
using UIFrameworks.Shared.Themes.Helpers;
using UIKit;
using static EOS.UI.iOS.Sandbox.BadgeLabelView;

namespace EOS.UI.iOS.Sandbox
{
    public partial class SimpleLabelView : BaseViewController
    {
        public const string Identifier = "SimpleLabelView";
        private List<UITextField> _textFields;
        private SimpleLabel _simpleLabel;

        public SimpleLabelView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _simpleLabel = new SimpleLabel
            {
                Text = "Some text"
            };

            _textFields = new List<UITextField>()
            {
                themeField,
                fontField,
                textColorField,
                textSizeField,
                letterSpacingField
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _textFields.ForEach(field => field.ResignFirstResponder());
            }));

            containerView.ConstrainLayout(() => _simpleLabel.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                                _simpleLabel.Frame.GetCenterY() == containerView.Frame.GetCenterY(), _simpleLabel);

            var frame = new CGRect(0, 0, 100, 150);

            InitThemePicker(frame);
            InitTextSizePicker(frame);
            InitFontPicker(frame);
            InitTextColorPicker(frame);
            InitLetterSpacingPicker(frame);

            resetButton.TouchUpInside += (sender, e) =>
            {
                _simpleLabel.ResetCustomization();
                _textFields.Except(new List<UITextField>() { themeField }).ToList().ForEach(f => f.Text = String.Empty);
            };
        }

        private void InitThemePicker(CGRect frame)
        {
            var themePicker = new UIPickerView(frame)
            {
                ShowSelectionIndicator = true,
                DataSource = new ThemePickerSource()
            };
            var themePickerDelegate = new ThemePickerDelegate();
            themePickerDelegate.DidSelected += (object sender, KeyValuePair<string, EOSThemeEnumeration> e) =>
            {
                themeField.Text = e.Key;
                var provider = _simpleLabel.GetThemeProvider();
                provider.SetCurrentTheme(e.Value);
                _simpleLabel.UpdateAppearance();
                fontField.Text = string.Empty;
                textColorField.Text = string.Empty;
                textSizeField.Text = string.Empty;
            };
            themePicker.Delegate = themePickerDelegate;
            themeField.InputView = themePicker;
            themeField.Text = _simpleLabel.GetThemeProvider().GetCurrentTheme().ThemeValues[EOSConstants.BackgroundColor] == UIColor.White ?
            "Light" : "Dark";
        }

        private void InitTextSizePicker(CGRect frame)
        {
            var textSizePicker = new UIPickerView(frame);
            textSizePicker.ShowSelectionIndicator = true;
            textSizePicker.DataSource = new FontSizesPickerSource();
            var fontSizePickerDelegate = new FontSizesPickerDelegate();
            fontSizePickerDelegate.DidSelected += (object sender, int e) =>
            {
                _simpleLabel.TextSize = e;
                textSizeField.Text = e.ToString();
            };
            textSizePicker.Delegate = fontSizePickerDelegate;
            textSizeField.InputView = textSizePicker;
        }

        private void InitFontPicker(CGRect frame)
        {
            var fontPicker = new UIPickerView(frame)
            {
                ShowSelectionIndicator = true,
                DataSource = new FontPickerSource()
            };
            var fontPickerDelegate = new FontPickerDelegate();
            fontPickerDelegate.DidSelected += (object sender, UIFont e) =>
            {
                _simpleLabel.Font = e;
                fontField.Text = e.Name;
            };
            fontPicker.Delegate = fontPickerDelegate;
            fontField.InputView = fontPicker;
        }

        private void InitTextColorPicker(CGRect frame)
        {
            var textColorPicker = new UIPickerView(frame)
            {
                ShowSelectionIndicator = true,
                DataSource = new ColorPickerSource()
            };
            var textColorPickerDelegate = new ColorPickerDelegate();
            textColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)textColorPicker.SelectedRowInComponent(0));
                _simpleLabel.TextColor = colorPair.Value;
            };
            textColorPicker.Delegate = textColorPickerDelegate;
            textColorField.InputView = textColorPicker;
        }

        private void InitLetterSpacingPicker(CGRect frame)
        {
            var letterSpacingPicker = new UIPickerView(frame)
            {
                ShowSelectionIndicator = true,
                DataSource = new LetterSpacingPickerSource()
            };
            var letterSpacingPickerDelegate = new LetterSpacingPickerDelegate();
            letterSpacingPickerDelegate.DidSelected += (object sender, int e) =>
            {
                _simpleLabel.LetterSpacing = e;
                letterSpacingField.Text = e.ToString();
            };
            letterSpacingPicker.Delegate = letterSpacingPickerDelegate;
            letterSpacingField.InputView = letterSpacingPicker;
        }
    }
}