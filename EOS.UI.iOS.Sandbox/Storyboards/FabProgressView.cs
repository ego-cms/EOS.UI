using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Helpers;
using EOS.UI.iOS.Sandbox.Controls.Pickers;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Themes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIFrameworks.Shared.Themes.Helpers;
using UIKit;
using static EOS.UI.iOS.Sandbox.Helpers.Constants;
using Constants = EOS.UI.iOS.Sandbox.Helpers.Constants;

namespace EOS.UI.iOS.Sandbox
{
    public partial class FabProgressView : BaseViewController
    {
        public const string Identifier = "FabProgressView";
        private List<CustomDropDown> _dropDowns;
        private FabProgress _fab;

        public FabProgressView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _fab = new FabProgress();
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

            _dropDowns = new List<CustomDropDown>()
            {
                backgroundDropDown,
                themeDropDown,
                sizeDropDown,
                disabledColorDropDown,
                pressedColorDropDown,
                shadowDropDown
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
                },
                Fields.Theme,
                rect);
            themeDropDown.SetTextFieldText(_fab.GetThemeProvider().GetCurrentTheme().ThemeValues[EOSConstants.PrimaryColor] == UIColor.White ? "Light" : "Dark");

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
                shadow => _fab.ShadowConfig = shadow,
                Fields.Shadow,
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
                _dropDowns.Except(new [] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
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