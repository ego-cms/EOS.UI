using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Themes.Themes;
using UIKit;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.iOS.Sandbox
{
    public partial class BadgeLabelView : BaseViewController
    {
        public const string Identifier = "BadgeLabelView";

        private List<EOSSandboxDropDown> _dropDowns;

        public BadgeLabelView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UpdateApperaence();

            var label = new BadgeLabel();
            label.Text = "Label";
            _dropDowns = new List<EOSSandboxDropDown>()
            {
                backgroundColorDropDown,
                letterSpaceDropDown,
                themeDropDown,
                fontDropDown,
                textColorDropDown,
                textSizeDropDown,
                cornerRadiusDropDown
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _dropDowns.ForEach(dropDown => dropDown.CloseInputControl());
            }));

            containerView.ConstrainLayout(() => label.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                                label.Frame.GetCenterY() == containerView.Frame.GetCenterY(), label);

            var rect = new CGRect(0, 0, 100, 150);

            themeDropDown.InitSource(
                ThemeTypes.ThemeCollection,
                (theme) => 
                {
                    label.GetThemeProvider().SetCurrentTheme(theme);
                    label.ResetCustomization();
                    _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                    UpdateApperaence();
                },
                Fields.Theme,
                rect);
            themeDropDown.SetTextFieldText(label.GetThemeProvider().GetCurrentTheme() is LightEOSTheme  ? "Light" : "Dark");

            backgroundColorDropDown.InitSource(
                color => label.BackgroundColor = color,
                Fields.Background,
                rect);

            textColorDropDown.InitSource(
                color => label.TextColor = color,
                Fields.TextColor,
                rect);

            fontDropDown.InitSource(
                Fonts.FontsCollection,
                font => label.Font = font,
                Fields.Font,
                rect);

            letterSpaceDropDown.InitSource(
                Sizes.LetterSpacingCollection,
                spacing => label.LetterSpacing = spacing,
                Fields.LetterSpacing,
                rect);

            textSizeDropDown.InitSource(
                Sizes.TextSizeCollection,
                size => label.TextSize = size,
                Fields.TextSize,
                rect);
            
            cornerRadiusDropDown.InitSource(
                Sizes.CornerRadiusCollection,
                radius => label.CornerRadius = (int)radius,
                Fields.ConerRadius,
                rect);

            resetButton.TouchUpInside += (sender, e) =>
            {
                label.ResetCustomization();
                _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
            };
        }
    }
}