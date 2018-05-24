using CoreGraphics;
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
    public partial class CircleProgressView : BaseViewController
    {
        public const string Identifier = "CircleProgressView";
        private CircleProgress _circleProgress;
        private PlatformTimer _timer;
        private int _percents = 0;
        private List<UITextField> _textFields;

        public CircleProgressView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _timer = new PlatformTimer();
            _timer.Setup(TimeSpan.FromMilliseconds(100), TimerAction);

            _circleProgress = CircleProgress.Create();
            _circleProgress.Frame = new CGRect(containerView.Frame.Width / 2 - _circleProgress.Frame.Width / 2,
                                               containerView.Frame.Height / 2 - _circleProgress.Frame.Height / 2, _circleProgress.Frame.Width, _circleProgress.Frame.Width);

            _circleProgress.Started += (sender, e) =>
            {
                if (_percents == 100)
                    _percents = 0;
                _timer.Start();
            };
            _circleProgress.Stopped += (sender, e) =>
            {
                _timer.Stop();
            };
            _circleProgress.Finished += (sender, e) =>
            {
                _timer.Stop();
            };
            containerView.AddSubview(_circleProgress);

            _textFields = new List<UITextField>()
            {
                themeField,
                fontField,
                colorField,
                checkmarkColorField,
                textSizeField
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _textFields.ForEach(f => f.ResignFirstResponder());
            }));


            var rect = new CGRect(0, 0, 100, 100);
            var themePicker = new UIPickerView(rect);
            themePicker.ShowSelectionIndicator = true;
            themePicker.DataSource = new DictionaryPickerSource<string, EOSThemeEnumeration>(Constants.Themes);
            var themePickerDelegate = new DictionaryPickerDelegate<string, EOSThemeEnumeration>(Constants.Themes);
            themePickerDelegate.DidSelected += (object sender, KeyValuePair<string, EOSThemeEnumeration> e) =>
            {
                themeField.Text = e.Key;
                var provider = _circleProgress.GetThemeProvider();
                provider.SetCurrentTheme(e.Value);
                _circleProgress.ResetCustomization();
                _textFields.Except(new[] { themeField }).ToList().ForEach(f => f.Text = String.Empty);
            };
            themeField.Text = _circleProgress.GetThemeProvider().GetCurrentTheme().ThemeValues[EOSConstants.BrandPrimaryColor] == UIColor.White ?
                "Light" : "Dark";
            themePicker.Delegate = themePickerDelegate;
            themeField.InputView = themePicker;

            var fontPicker = new UIPickerView(rect);
            fontPicker.ShowSelectionIndicator = true;
            fontPicker.DataSource = new ValuePickerSource<UIFont>(Constants.Fonts);
            var fontPickerDelegate = new ValuePickerDelegate<UIFont>(Constants.Fonts);
            fontPickerDelegate.DidSelected += (object sender, UIFont e) =>
            {
                _circleProgress.Font = e;
                fontField.Text = e.Name;
            };
            fontField.EditingDidBegin += (sender, e) =>
            {
                var font = Constants.Fonts.ElementAt((int)fontPicker.SelectedRowInComponent(0));
                _circleProgress.Font = font;
                fontField.Text = font.Name;
            };
            fontPicker.Delegate = fontPickerDelegate;
            fontField.InputView = fontPicker;

            var fontSizePicker = new UIPickerView(rect);
            fontSizePicker.ShowSelectionIndicator = true;
            fontSizePicker.DataSource = new ValuePickerSource<int>(Constants.FontSizeValues);
            var fontSizePickerDelegate = new ValuePickerDelegate<int>(Constants.FontSizeValues);
            fontSizePickerDelegate.DidSelected += (object sender, int e) =>
            {
                _circleProgress.TextSize = e;
                textSizeField.Text = e.ToString();
            };
            textSizeField.EditingDidBegin += (sender, e) =>
            {
                var size = Constants.FontSizeValues[(int)fontSizePicker.SelectedRowInComponent(0)];
                _circleProgress.TextSize = size;
                textSizeField.Text = size.ToString();
            };
            fontSizePicker.Delegate = fontSizePickerDelegate;
            textSizeField.InputView = fontSizePicker;

            var colorPicker = new UIPickerView(rect);
            colorPicker.ShowSelectionIndicator = true;
            colorPicker.DataSource = new ColorPickerSource();
            var colorPickerDelegate = new ColorPickerDelegate();
            colorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _circleProgress.Color = e.Value;
                colorField.Text = e.Key;
            };
            colorField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)colorPicker.SelectedRowInComponent(0));
                _circleProgress.Color = colorPair.Value;
                colorField.Text = colorPair.Key;
            };
            colorPicker.Delegate = colorPickerDelegate;
            colorField.InputView = colorPicker;

            var checkmarkColorPicker = new UIPickerView(rect);
            checkmarkColorPicker.ShowSelectionIndicator = true;
            checkmarkColorPicker.DataSource = new ColorPickerSource();
            var checkmarkColorPickerDelegate = new ColorPickerDelegate();
            checkmarkColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                _circleProgress.AlternativeColor = e.Value;
                checkmarkColorField.Text = e.Key;
            };
            checkmarkColorField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)checkmarkColorPicker.SelectedRowInComponent(0));
                _circleProgress.AlternativeColor = colorPair.Value;
                checkmarkColorField.Text = colorPair.Key;
            };
            checkmarkColorPicker.Delegate = checkmarkColorPickerDelegate;
            checkmarkColorField.InputView = checkmarkColorPicker;

            showProgressSwitch.ValueChanged += (sender, e) =>
            {
                _circleProgress.ShowProgress = showProgressSwitch.On;
            };

            resetButton.TouchUpInside += (sender, e) =>
            {
                _textFields.Except(new List<UITextField>() { themeField }).ToList().ForEach(f => f.Text = String.Empty);
                showProgressSwitch.On = true;
                _circleProgress.ResetCustomization();
            };
        }

        public void TimerAction()
        {
            _percents += 1;
            _circleProgress.Progress = _percents;
        }
    }
}