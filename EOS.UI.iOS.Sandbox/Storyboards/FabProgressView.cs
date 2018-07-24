using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Themes.Themes;
using UIKit;
using static EOS.UI.iOS.Sandbox.Helpers.Constants;
using Constants = EOS.UI.iOS.Sandbox.Helpers.Constants;

namespace EOS.UI.iOS.Sandbox
{
    public partial class FabProgressView : BaseViewController
    {
        public const string Identifier = "FabProgressView";
        private List<EOSSandboxDropDown> _dropDowns;
        private FabProgress _fab;
        private double? _opacity;

        public FabProgressView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _fab = new FabProgress();
            var frame = _fab.Frame;
            _fab.TouchUpInside += async (sender, e) =>
            {
                if (_fab.InProgress)
                    return;
                themeDropDown.Enabled = false;
                resetButton.Enabled = false;
                _fab.StartProgressAnimation();
                await Task.Delay(5000);
                _fab.StopProgressAnimation();
                themeDropDown.Enabled = true;
                resetButton.Enabled = true;
            };

            UpdateFrame();
            containerView.AddSubview(_fab);

            _dropDowns = new List<EOSSandboxDropDown>()
            {
                backgroundDropDown,
                themeDropDown,
                disabledColorDropDown,
                pressedColorDropDown,
                shadowColorDropDown,
                shadowRadiusDropDown,
                shadowOffsetXDropDown,
                shadowOffsetYDropDown,
                shadowOpacityDropDown,
            };
            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _dropDowns.ForEach(dropDown => dropDown.CloseInputControl());
            }));

            var rect = new CGRect(0, 0, 100, 150);

            themeDropDown.InitSource(
                Constants.Themes,
                (theme) =>
                {
                    _fab.GetThemeProvider().SetCurrentTheme(theme);
                    _fab.ResetCustomization();
                    _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                    UpdateApperaence();
                },
                Fields.Theme,
                rect);
            themeDropDown.SetTextFieldText(_fab.GetThemeProvider().GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");

            backgroundDropDown.InitSource(
                color => _fab.BackgroundColor = color,
                Fields.Background,
                rect);

            pressedColorDropDown.InitSource(
                color => _fab.PressedBackgroundColor = color,
                Fields.PressedColor,
                rect);

            disabledColorDropDown.InitSource(
                color => _fab.DisabledBackgroundColor = color,
                Fields.DisabledColor,
                rect);

            shadowColorDropDown.InitSource(
                color =>
                {
                    var config = _fab.ShadowConfig;
                    if (_opacity != null)
                    {
                        config.Color = color.ColorWithAlpha((nfloat)_opacity); 
                    }
                    else
                    {
                        config.Color = color;
                    }
                    _fab.ShadowConfig = config;
                },
                Fields.ShadowColor,
                rect);

            shadowOffsetXDropDown.InitSource(
                ShadowOffsetValues,
                offset =>
                {
                    var config = _fab.ShadowConfig;
                    config.Offset = new CGPoint(offset, config.Offset.Y);
                    _fab.ShadowConfig = config;
                },
                Fields.ShadowOffsetX,
                rect);


            shadowOffsetYDropDown.InitSource(
                ShadowOffsetValues,
                offset =>
                {
                    var config = _fab.ShadowConfig;
                    config.Offset = new CGPoint(config.Offset.X, offset);
                    _fab.ShadowConfig = config;
                },
                Fields.ShadowOffsetY,
                rect);

            shadowRadiusDropDown.InitSource(
                ShadowRadiusValues,
                blur =>
                {
                    var config = _fab.ShadowConfig;
                    config.Blur = blur;
                    _fab.ShadowConfig = config;
                },
                Fields.ShadowRadius,
                rect);

            shadowOpacityDropDown.InitSource(
                ShadowOpacityValues,
                opacity =>
                {
                    var config = _fab.ShadowConfig;
                    _opacity = opacity;
                    config.Color = config.Color.ColorWithAlpha((nfloat)opacity); 
                    _fab.ShadowConfig = config;
                },
                Fields.ShadowOpacity,
                rect);

            enableSwitch.ValueChanged += (sender, e) =>
            {
                _fab.Enabled = enableSwitch.On;
            };

            resetButton.TouchUpInside += (sender, e) =>
            {
                if (_fab.InProgress)
                    return;

                _opacity = null;
                _fab.ResetCustomization();
                _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                UpdateFrame();
            };
        }

        protected void UpdateFrame()
        {
            var buttonSize = 52;
            var frame = new CGRect(containerView.Frame.Width / 2 - buttonSize / 2, containerView.Frame.Height / 2 - buttonSize / 2, buttonSize, buttonSize);
            _fab.Frame = frame;
        }
    }
}