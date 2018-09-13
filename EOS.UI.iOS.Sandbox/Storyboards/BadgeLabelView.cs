using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Sandbox.ControlConstants.iOS;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Themes;
using UIKit;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.iOS.Sandbox
{
    public partial class BadgeLabelView : BaseViewController
    {
        public const string Identifier = "BadgeLabelView";
        private List<EOSSandboxDropDown> _dropDowns;
        BadgeLabel _label;

        public BadgeLabelView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UpdateAppearance();

            _label = new BadgeLabel();
            _label.Text = "Label";
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

            containerView.ConstrainLayout(() => _label.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                                _label.Frame.GetCenterY() == containerView.Frame.GetCenterY(), _label);

            var frame = new CGRect(0, 0, 100, 150);
            InitThemeDropDown(frame);
            themeDropDown.SetTextFieldText(_label.GetThemeProvider().GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");
            resetButton.TouchUpInside += (sender, e) =>
            {
                _label.ResetCustomization();
                _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
            };
            InitSources(frame);
        }

        private void InitSources(CGRect rect)
        {
            InitBackgroundColorDropDown(rect);
            InitTextColorDropDown(rect);
            InitFontDropDown(rect);
            InitLetterSpacingDropDown(rect);
            InitTextSizeDropDown(rect);
            InitCornerRadiusDropDown(rect);
        }

        private void InitThemeDropDown(CGRect rect)
        {
            themeDropDown.InitSource(
               ThemeTypes.ThemeCollection,
               (theme) =>
               {
                   _label.GetThemeProvider().SetCurrentTheme(theme);
                   _label.ResetCustomization();
                   _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                   InitSources(rect);
                   UpdateAppearance();
               },
               Fields.Theme,
               rect);
        }

        private void InitBackgroundColorDropDown(CGRect rect)
        {
            backgroundColorDropDown.InitSource(
                BadgeLabelConstants.BackgroundColors,
                color => _label.BackgroundColor = color,
                Fields.Background,
                rect);
        }

        private void InitTextColorDropDown(CGRect rect)
        {
            textColorDropDown.InitSource(
                BadgeLabelConstants.FontColors,
                color => _label.TextColor = color,
                Fields.TextColor,
                rect);
        }

        private void InitFontDropDown(CGRect rect)
        {
            fontDropDown.InitSource(
               BadgeLabelConstants.BadgeLabelFonts,
               font => _label.Font = font,
               Fields.Font,
               rect);
        }

        private void InitLetterSpacingDropDown(CGRect rect)
        {
            letterSpaceDropDown.InitSource(
                BadgeLabelConstants.LetterSpacings,
                spacing => _label.LetterSpacing = spacing,
                Fields.LetterSpacing,
                rect);
        }

        private void InitTextSizeDropDown(CGRect rect)
        {
            textSizeDropDown.InitSource(
               BadgeLabelConstants.TextSizes,
               size => _label.TextSize = size,
               Fields.TextSize,
               rect);
        }

        private void InitCornerRadiusDropDown(CGRect rect)
        {
            cornerRadiusDropDown.InitSource(
                BadgeLabelConstants.CornerRadiusCollection,
                radius => _label.CornerRadius = (int)radius,
                Fields.ConerRadius,
                rect);
        }
    }
}