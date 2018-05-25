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
using static EOS.UI.iOS.Sandbox.Helpers.Constants;

namespace EOS.UI.iOS.Sandbox
{
    public partial class BadgeLabelView : BaseViewController
    {
        public const string Identifier = "BadgeLabelView";

        private List<CustomDropDown> _textFields;

        public BadgeLabelView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var label = new BadgeLabel();
            label.Text = "Default Text";

            _textFields = new List<CustomDropDown>()
            {
                backgroundColorDropDown,
                letterSpacingDropDown,
                themeDropDown,
                fontDropDown,
                textColorDropDown,
                textSizeDropDown,
                cornerRadiusDropDown
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _textFields.ForEach(dropDown => dropDown.CloseInputControl());
            }));

            containerView.ConstrainLayout(() => label.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                                label.Frame.GetCenterY() == containerView.Frame.GetCenterY(), label);

            var rect = new CGRect(0, 0, 100, 150);

            themeDropDown.InitSource(
                Constants.Themes,
                (theme) => 
                {
                    label.GetThemeProvider().SetCurrentTheme(theme);
                    label.ResetCustomization();
                    _textFields.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                },
                Fields.Theme,
                rect);
            themeDropDown.SetTextFieldText(label.GetThemeProvider().GetCurrentTheme().ThemeValues[EOSConstants.PrimaryColor] == UIColor.White ? "Light" : "Dark");

            backgroundColorDropDown.InitSource(
                color => label.BackgroundColor = color,
                Fields.Background,
                rect);

            textColorDropDown.InitSource(
                color => label.TextColor = color,
                Fields.TextColor,
                rect);

            fontDropDown.InitSource(
                Fonts,
                font => label.Font = font,
                Fields.Font,
                rect);

            letterSpacingDropDown.InitSource(
                LetterSpacingValues,
                spacing => label.LetterSpacing = spacing,
                Fields.LetterSpacing,
                rect);

            textSizeDropDown.InitSource(
                FontSizeValues,
                size => label.TextSize = size,
                Fields.TextSize,
                rect);

            cornerRadiusDropDown.InitSource(
                CornerRadiusValues,
                radius => label.CornerRadius = radius,
                Fields.ConerRadius,
                rect);

            resetButton.TouchUpInside += (sender, e) =>
            {
                label.ResetCustomization();
                _textFields.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
            };
        }
    }
}