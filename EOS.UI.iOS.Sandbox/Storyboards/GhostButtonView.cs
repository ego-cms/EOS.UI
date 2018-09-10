using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Sandbox.ControlConstants.iOS;
using EOS.UI.Shared.Sandbox.Helpers;
using EOS.UI.Shared.Themes.Themes;
using UIKit;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.iOS.Sandbox
{
    public partial class GhostButtonView : BaseViewController
    {
        public const string Identifier = "GhostButtonView";
        private List<EOSSandboxDropDown> _dropDowns;
        private GhostButton _ghostButton;

        public GhostButtonView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _ghostButton = new GhostButton();
            _ghostButton.SetTitle(ControlNames.GhostButton, UIControlState.Normal);
            containerView.ConstrainLayout(() => _ghostButton.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                          _ghostButton.Frame.GetCenterY() == containerView.Frame.GetCenterY(), _ghostButton);
            _ghostButton.ContentEdgeInsets = new UIEdgeInsets(6, 16, 6, 16);

            _dropDowns = new List<EOSSandboxDropDown>()
            {
                themeDropDown,
                fontDropDown,
                letterSpacingDropDown,
                enabledTextColorDropDown,
                disabledTextColorDropDown,
                textSizeDropDown,
                rippleColorDropDown
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _dropDowns.ForEach(dropDown => dropDown.CloseInputControl());
            }));

            var rect = new CGRect(0, 0, 100, 100);

            InitThemeDropDown(rect);
            themeDropDown.SetTextFieldText(_ghostButton.GetThemeProvider().GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");
            InitSources(rect);
            stateSwitch.ValueChanged += (sender, e) =>
            {
                _ghostButton.Enabled = stateSwitch.On;
            };

            resetButton.TouchUpInside += (sender, e) =>
            {
                _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                _ghostButton.ResetCustomization();
            };
        }

        private void InitSources(CGRect rect)
        {
            InitFontDropDown(rect);
            InitLetterSpacingDropDown(rect);
            InitEnabledTextColorDropDown(rect);
            InitDisabledTextColorDropDown(rect);
            InitTextSizeDropDown(rect);
            InitRippleColorDropDown(rect);
        }

        private void InitThemeDropDown(CGRect rect)
        {
            themeDropDown.InitSource(
               ThemeTypes.ThemeCollection,
               (theme) =>
               {
                   _ghostButton.GetThemeProvider().SetCurrentTheme(theme);
                   _ghostButton.ResetCustomization();
                   _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                   InitSources(rect);
                   UpdateAppearance();
               },
               Fields.Theme,
               rect);
        }

        private void InitFontDropDown(CGRect rect)
        {
            fontDropDown.InitSource(
               GhostButtonConstants.GhostButtonFonts,
               font => _ghostButton.Font = font,
               Fields.Font,
               rect);
        }

        private void InitLetterSpacingDropDown(CGRect rect)
        {
            letterSpacingDropDown.InitSource(
                GhostButtonConstants.LetterSpacings,
                spacing => _ghostButton.LetterSpacing = spacing,
                Fields.LetterSpacing,
                rect);
        }

        private void InitEnabledTextColorDropDown(CGRect rect)
        {
            enabledTextColorDropDown.InitSource(
                GhostButtonConstants.FontColors,
                color => _ghostButton.TextColor = color,
                Fields.EnabledTextColor,
                rect);
        }

        private void InitDisabledTextColorDropDown(CGRect rect)
        {
            disabledTextColorDropDown.InitSource(
               GhostButtonConstants.DisabledFontColors,
               color => _ghostButton.DisabledTextColor = color,
               Fields.DisabledTextColor,
               rect);
        }

        private void InitTextSizeDropDown(CGRect rect)
        {
            textSizeDropDown.InitSource(
                GhostButtonConstants.TextSizes,
                size => _ghostButton.TextSize = size,
                Fields.TextSize,
                rect);
        }

        private void InitRippleColorDropDown(CGRect rect)
        {
            rippleColorDropDown.InitSource(
                GhostButtonConstants.RippleColors,
                color => _ghostButton.RippleColor = color,
                Fields.RippleColor,
                rect);
        }
    }
}