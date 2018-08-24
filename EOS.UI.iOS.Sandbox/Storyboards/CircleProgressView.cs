using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Sandbox.Helpers;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Themes.Themes;
using UIKit;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.iOS.Sandbox
{
    public partial class CircleProgressView : BaseViewController
    {
        public const string Identifier = "CircleProgressView";
        private CircleProgress _circleProgress;
        private PlatformTimer _timer;
        private int _percents = 0;
        private List<EOSSandboxDropDown> _dropDowns;
        private int _circleProgressSize;

        public CircleProgressView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _timer = new PlatformTimer();
            _timer.Setup(TimeSpan.FromMilliseconds(100), TimerAction);

            _circleProgress = new CircleProgress();
            _circleProgress.Frame = new CGRect(containerView.Frame.Width / 2 - _circleProgressSize / 2,
                                               containerView.Frame.Height / 2 - _circleProgressSize / 2, _circleProgressSize, _circleProgressSize);

            _circleProgress.Started += (sender, e) =>
            {
                if (_percents == 100)
                    _percents = 0;
                _timer.Start();
            };
            _circleProgress.Stopped += (sender, e) =>
            {
                _timer.Stop();
                _percents = 0;
                _circleProgress.Progress = 0;
            };
            _circleProgress.Finished += (sender, e) =>
            {
                _timer.Stop();
            };
            containerView.AddSubview(_circleProgress);

            _dropDowns = new List<EOSSandboxDropDown>()
            {
                themeDropDown,
                fontDropDown,
                colorDropDown,
                alternativeColorDropDown,
                textSizeDropDown,
                fillColorDropDown
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _dropDowns.ForEach(dropDown => dropDown.CloseInputControl());
            }));


            var rect = new CGRect(0, 0, 100, 100);

            themeDropDown.InitSource(
                ThemeTypes.ThemeCollection,
                (theme) =>
                {
                    _circleProgress.GetThemeProvider().SetCurrentTheme(theme);
                    _circleProgress.ResetCustomization();
                    _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                    _circleProgress.Progress = 0;
                    showProgressSwitch.On = true;
                    UpdateApperaence();
                },
                Fields.Theme,
                rect);
            themeDropDown.SetTextFieldText(_circleProgress.GetThemeProvider().GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");

            fontDropDown.InitSource(
                Fonts.GetCircleProgressFonts().ToList(),
                font => _circleProgress.Font = font,
                Fields.Font,
                rect);

            textSizeDropDown.InitSource(
                Sizes.TextSizeCollection,
                size => _circleProgress.TextSize = size,
                Fields.TextSize,
                rect);

            colorDropDown.InitSource(
                Colors.MainColorsCollection,
                color => _circleProgress.Color = color,
                Fields.Color,
                rect);

            alternativeColorDropDown.InitSource(
                Colors.MainColorsCollection,
                color => _circleProgress.AlternativeColor = color,
                Fields.AlternativeColor,
                rect);
            
            fillColorDropDown.InitSource(
                Colors.MainColorsCollection,
                color => _circleProgress.FillColor = color,
                Fields.FillColor,
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