using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Sandbox.Helpers;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Themes.Themes;
using UIKit;
using static EOS.UI.iOS.Sandbox.Helpers.Constants;

namespace EOS.UI.iOS.Sandbox
{
    public partial class CircleProgressView : BaseViewController
    {
        public const string Identifier = "CircleProgressView";
        private CircleProgress _circleProgress;
        private PlatformTimer _timer;
        private int _percents = 0;
        private List<CustomDropDown> _dropDowns;

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

            _dropDowns = new List<CustomDropDown>()
            {
                themeDropDown,
                fontDropDown,
                colorDropDown,
                alternativeColorDropDown,
                textSizeDropDown
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _dropDowns.ForEach(dropDown => dropDown.CloseInputControl());
            }));


            var rect = new CGRect(0, 0, 100, 100);

            themeDropDown.InitSource(
                Constants.Themes,
                (theme) =>
                {
                    _circleProgress.GetThemeProvider().SetCurrentTheme(theme);
                    _circleProgress.ResetCustomization();
                    _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                },
                Fields.Theme,
                rect);
            themeDropDown.SetTextFieldText(_circleProgress.GetThemeProvider().GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");

            fontDropDown.InitSource(
                Fonts,
                font => _circleProgress.Font = font,
                Fields.Font,
                rect);

            textSizeDropDown.InitSource(
                FontSizeValues,
                size => _circleProgress.TextSize = size,
                Fields.TextSize,
                rect);

            colorDropDown.InitSource(
                color => _circleProgress.Color = color,
                Fields.Color,
                rect);

            alternativeColorDropDown.InitSource(
                color => _circleProgress.AlternativeColor = color,
                Fields.AlternativeColor,
                rect);

            showProgressSwitch.ValueChanged += (sender, e) =>
            {
                _circleProgress.ShowProgress = showProgressSwitch.On;
            };

            resetButton.TouchUpInside += (sender, e) =>
            {
                _dropDowns.Except(new [] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
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