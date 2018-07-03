using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Helpers;
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
                sizeDropDown,
                disabledColorDropDown,
                pressedColorDropDown,
                shadowDropDown,
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

            sizeDropDown.InitSource(
                FabProgressSizes,
                size => _fab.ButtonSize = size,
                Fields.Size,
                rect);

            shadowDropDown.InitSource(
                ShadowConfigs,
                shadow => _fab.ShadowConfig = new ShadowConfig()
                {
                    Color = shadow.Color,
                    Offset = shadow.Offset,
                    Opacity = shadow.Opacity,
                    Radius = shadow.Radius
                },
                Fields.Shadow,
                rect);

            shadowColorDropDown.InitSource(
                color =>
                {
                    var config = _fab.ShadowConfig;
                    config.Color = color.CGColor;
                    _fab.ShadowConfig = config;
                },
                Fields.ShadowColor,
                rect);

            shadowOffsetXDropDown.InitSource(
                ShadowOffsetValues,
                offset =>
                {
                    var config = _fab.ShadowConfig;
                    config.Offset = new CGSize(offset, config.Offset.Height);
                    _fab.ShadowConfig = config;
                },
                Fields.ShadowOffsetX,
                rect);


            shadowOffsetYDropDown.InitSource(
                ShadowOffsetValues,
                offset =>
                {
                    var config = _fab.ShadowConfig;
                    config.Offset = new CGSize(config.Offset.Width, offset);
                    _fab.ShadowConfig = config;
                },
                Fields.ShadowOffsetY,
                rect);

            shadowRadiusDropDown.InitSource(
                ShadowRadiusValues,
                radius =>
                {
                    var config = _fab.ShadowConfig;
                    config.Radius = radius;
                    _fab.ShadowConfig = config;
                },
                Fields.ShadowRadius,
                rect);

            shadowOpacityDropDown.InitSource(
                ShadowOpacityValues,
                opacity =>
                {
                    var config = _fab.ShadowConfig;
                    config.Opacity = (float)opacity;
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
                _fab.ResetCustomization();
                _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
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