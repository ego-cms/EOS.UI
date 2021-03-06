﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Sandbox.ControlConstants.iOS;
using EOS.UI.Shared.Themes.Themes;
using UIKit;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

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

            var rect = new CGRect(0, 0, 100, 150);
            InitThemeDropDown(rect);
            InitSources(rect);
            resetButton.TouchUpInside += (sender, e) =>
            {
                _simpleLabel.ResetCustomization();
                _dropDowns.Except(new [] { themesDropDown }).ToList().ForEach(d => d.ResetValue());
            };
        }

        private void InitSources(CGRect rect)
        {
            InitTextSizeDropDown(rect);
            InitFontDropDown(rect);
            InitTextColorDropDown(rect);
            InitLetterSpacingDropDown(rect);
        }
        
        private void InitThemeDropDown(CGRect rect)
        {
            themesDropDown.InitSource(
                ThemeTypes.ThemeCollection,
                (theme) =>
                {
                    _simpleLabel.GetThemeProvider().SetCurrentTheme(theme);
                    _simpleLabel.ResetCustomization();
                    _dropDowns.Except(new[] { themesDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                    InitSources(rect);
                    UpdateAppearance();
                },
                Fields.Theme,
                rect);
            themesDropDown.SetTextFieldText(_simpleLabel.GetThemeProvider().GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");
        }

        private void InitTextSizeDropDown(CGRect rect)
        {
            textSizeDropDown.InitSource(
                SimpleLabelConstants.TextSizes,
                size => _simpleLabel.TextSize = size,
                Fields.TextSize,
                rect);
        }

        private void InitFontDropDown(CGRect rect)
        {
            fontDropDown.InitSource(
                SimpleLabelConstants.SimpleLabelFonts,
                font => _simpleLabel.Font = font,
                Fields.Font,
                rect);
        }

        private void InitTextColorDropDown(CGRect rect)
        {
            textColorDropDown.InitSource(
                SimpleLabelConstants.FontColors,
                color => _simpleLabel.TextColor = color,
                Fields.TextColor,
                rect);
        }

        private void InitLetterSpacingDropDown(CGRect rect)
        {
            letterSpacingDropDown.InitSource(
                SimpleLabelConstants.LetterSpacings,
                spacing => _simpleLabel.LetterSpacing = spacing,
                Fields.LetterSpacing,
                rect);
        }
    }
}