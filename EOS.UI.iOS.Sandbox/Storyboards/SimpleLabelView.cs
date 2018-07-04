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
using EOS.UI.iOS.Sandbox.Controls.Pickers;
using static EOS.UI.iOS.Sandbox.Helpers.Constants;
using EOS.UI.Shared.Themes.Themes;

namespace EOS.UI.iOS.Sandbox
{
    public partial class SimpleLabelView : BaseViewController
    {
        public const string Identifier = "SimpleLabelView";
        private List<EOSSandboxDropDown> _dropDowns;
        private SimpleLabel _simpleLabel;

        public SimpleLabelView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _simpleLabel = new SimpleLabel
            {
                Text = "Label"
            };

            _dropDowns = new List<EOSSandboxDropDown>()
            {
                themesDropDown,
                fontDropDown,
                textColorDropDown,
                textSizeDropDown,
                letterSpacingDropDown
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _dropDowns.ForEach(field => field.CloseInputControl());
            }));
            _simpleLabel.TextAlignment = UITextAlignment.Center;
            containerView.ConstrainLayout(() => _simpleLabel.Frame.GetCenterY() == containerView.Frame.GetCenterY() &&
                                          _simpleLabel.Frame.Left == containerView.Frame.Left &&
                                          _simpleLabel.Frame.Right == containerView.Frame.Right, _simpleLabel);

            var frame = new CGRect(0, 0, 100, 150);

            InitThemePicker(frame);
            InitTextSizePicker(frame);
            InitFontPicker(frame);
            InitTextColorPicker(frame);
            InitLetterSpacingPicker(frame);

            resetButton.TouchUpInside += (sender, e) =>
            {
                _simpleLabel.ResetCustomization();
                _dropDowns.Except(new [] { themesDropDown }).ToList().ForEach(d => d.ResetValue());
            };
        }

        private void InitThemePicker(CGRect frame)
        {
            themesDropDown.InitSource(
                Constants.Themes,
                (theme) =>
                {
                    _simpleLabel.GetThemeProvider().SetCurrentTheme(theme);
                    _simpleLabel.ResetCustomization();
                    _dropDowns.Except(new[] { themesDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                    UpdateApperaence();
                },
                Fields.Theme,
                frame);
            themesDropDown.SetTextFieldText(_simpleLabel.GetThemeProvider().GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");
        }

        private void InitTextSizePicker(CGRect frame)
        {
            textSizeDropDown.InitSource(
                FontSizeValues,
                size => _simpleLabel.TextSize = size,
                Fields.TextSize,
                frame);
        }

        private void InitFontPicker(CGRect frame)
        {
            fontDropDown.InitSource(
                Fonts,
                font => _simpleLabel.Font = font,
                Fields.Font,
                frame);
        }

        private void InitTextColorPicker(CGRect frame)
        {
            textColorDropDown.InitSource(
                color => _simpleLabel.TextColor = color,
                Fields.TextColor,
                frame);
        }

        private void InitLetterSpacingPicker(CGRect frame)
        {
            letterSpacingDropDown.InitSource(
                LetterSpacingValues,
                spacing => _simpleLabel.LetterSpacing = spacing,
                Fields.LetterSpacing,
                frame);
        }
    }
}