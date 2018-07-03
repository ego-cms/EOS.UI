﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Models;
using EOS.UI.iOS.Sandbox.Controls.Pickers;
using EOS.UI.iOS.Sandbox.Helpers;
using EOS.UI.iOS.Sandbox.TableSources;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIKit;
using static EOS.UI.iOS.Sandbox.Helpers.Constants;

namespace EOS.UI.iOS.Sandbox.Storyboards
{
    public partial class SectionComponentView : BaseViewController
    {
        public const string Identifier = "SectionView";
        private List<EOSSandboxDropDown> _dropDowns;
        private List<object> _dataSource;
        private SectionTableSource _source;

        public SectionComponentView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLayoutSubviews()
        {
            sectionScroll.ContentSize = new CGSize(sectionScroll.ContentSize.Width, 1050);
            base.ViewDidLayoutSubviews();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _dataSource = new List<object>()
            {
                new SectionModel()
                {
                    SectionAction = () => { new UIAlertView ("Action", "Action invoked", null, "Ok", null).Show(); },
                    HasBorder= (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionBorder],
                    HasButton = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionAction]
                },
                "First item",
                "Second item",
                "Third item"
            };
            _source = new SectionTableSource(sectionTableView, _dataSource);
            sectionTableView.Source = _source;

            _dropDowns = new List<EOSSandboxDropDown>()
            {
                themeDropDown,
                sectionNameDropDown,
                buttonTextDropDown,
                sectionNameFontDropDown,
                buttonTextFontDropDown,
                sectionNameLetterSpacingDropDown,
                buttonTextLetterSpacingDropDown,
                sectionTextSizeDropDown,
                buttonTextSizeDropDown,
                sectionTextColorDropDown,
                buttonTextColorDropDown,
                backgoundColorDropDown,
                borderColorDropDown,
                borderWidthDropDown,
                paddingTopDropDown,
                paddingBottomDropDown,
                paddingLeftDropDown,
                paddingRightDropDown
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _dropDowns.ForEach(dropDown => dropDown.CloseInputControl());
            }));

            var rect = new CGRect(0, 0, 100, 150);

            InitThemeDropDown(rect);
            InitSectionNameDropDown(rect);
            InitButtonTextDropDown(rect);
            InitSectionNameFontDropDown(rect);
            InitButtonTextFontDropDown(rect);
            InitSectionNameLetterSpacingDropDown(rect);
            InitButtonTextLetterSpacingDropDown(rect);
            InitSectionTextSizeDropDown(rect);
            InitButtonTextSizeDropDown(rect);
            InitSectionTextColorDropDown(rect);
            InitButtonTextColorDropDown(rect);
            InitBackgroundColorDropDown(rect);
            InitBorderColorDropDown(rect);
            InitBorderWidthDropDown(rect);
            InitPaddingTopDropDown(rect);
            InitPaddingBottomDropDown(rect);
            InitPaddingLeftDropDown(rect);
            InitPaddingRightDropDown(rect);
            InitHasButton();
            InitHasBorder();
            InitResetButton();

            _source.SectionModel.ResetCustomization = true;
            ResetFields();
        }

        private void InitResetButton()
        {
            resetCustomizationButton.TouchUpInside += (sender, e) =>
            {
                var defaultModel = new SectionModel()
                {
                    SectionAction = () => { new UIAlertView("Action", "Action invoked", null, "Ok", null).Show(); },
                    HasBorder = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionBorder],
                    HasButton = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionAction]
                };

                _source.SectionModel.CopyData(defaultModel);
                _source.SectionModel.ResetCustomization = true;
                ResetFields();
            };
        }

        private void InitHasBorder()
        {
            hasBorderSwitch.On = true;
            hasBorderSwitch.ValueChanged += delegate
            {
                _source.SectionModel.HasBorder = hasBorderSwitch.On;
            };
        }

        private void InitHasButton()
        {
            hasButtonSwitch.On = true;
            hasButtonSwitch.ValueChanged += delegate
            {
                _source.SectionModel.HasButton = hasButtonSwitch.On;
            };
        }

        private void InitPaddingRightDropDown(CGRect rect)
        {
            paddingRightDropDown.InitSource(
                PaddingValues,
                padding =>
                {
                    _source.SectionModel.RightPadding = padding;
                },
                Fields.PaddingRight,
                rect);
        }

        private void InitPaddingLeftDropDown(CGRect rect)
        {
            paddingLeftDropDown.InitSource(
                PaddingValues,
                padding =>
                {
                    _source.SectionModel.LeftPadding = padding;
                },
                Fields.PaddingLeft,
                rect);
        }

        private void InitPaddingBottomDropDown(CGRect rect)
        {
            paddingBottomDropDown.InitSource(
                PaddingValues,
                padding =>
                {
                    _source.SectionModel.BottonPadding = padding;
                },
                Fields.PaddingBottom,
                rect);
        }

        private void InitPaddingTopDropDown(CGRect rect)
        {
            paddingTopDropDown.InitSource(
                PaddingValues,
                padding =>
                {
                    _source.SectionModel.TopPadding = padding;
                },
                Fields.PaddingTop,
                rect);
        }

        private void InitBorderWidthDropDown(CGRect rect)
        {
            borderWidthDropDown.InitSource(
                WidthValues,
                width =>
                {
                    _source.SectionModel.BorderWidth = width;
                },
                Fields.BorderWidth,
                rect);
        }

        private void InitBorderColorDropDown(CGRect rect)
        {
            borderColorDropDown.InitSource(
                color =>
                {
                    _source.SectionModel.BorderColor = color;
                },
                Fields.BorderColor,
                rect);
        }

        private void InitBackgroundColorDropDown(CGRect rect)
        {
            backgoundColorDropDown.InitSource(
                color =>
                {
                    _source.SectionModel.BackgroundColor = color;
                },
                Fields.BackgroundColor,
                rect);
        }

        private void InitButtonTextColorDropDown(CGRect rect)
        {
            buttonTextColorDropDown.InitSource(
                color =>
                {
                    _source.SectionModel.ButtonNameColor = color;
                },
                Fields.ButtonTextColor,
                rect);
        }

        private void InitSectionTextColorDropDown(CGRect rect)
        {
            sectionTextColorDropDown.InitSource(
                color =>
                {
                    _source.SectionModel.SectionNameColor = color;
                },
                Fields.SectionTextColor,
                rect);
        }

        private void InitButtonTextSizeDropDown(CGRect rect)
        {
            buttonTextSizeDropDown.InitSource(
                FontSizeValues,
                size =>
                {
                    _source.SectionModel.ButtonTextSize = size;
                },
                Fields.ButtonTextSize,
                rect);
        }

        private void InitSectionTextSizeDropDown(CGRect rect)
        {
            sectionTextSizeDropDown.InitSource(
                FontSizeValues,
                size =>
                {
                    _source.SectionModel.SectionNameTextSize = size;
                },
                Fields.SectionTextSize,
                rect);
        }

        private void InitButtonTextLetterSpacingDropDown(CGRect rect)
        {
            buttonTextLetterSpacingDropDown.InitSource(
                LetterSpacingValues,
                spacing =>
                {
                    _source.SectionModel.ButtonTextLetterSpacing = spacing;
                },
                Fields.ButtonTextLetterSpacing,
                rect);
        }

        private void InitSectionNameLetterSpacingDropDown(CGRect rect)
        {
            sectionNameLetterSpacingDropDown.InitSource(
                LetterSpacingValues,
                spacing =>
                {
                    _source.SectionModel.SectionTextLetterSpacing = spacing;
                },
                Fields.SectionNameLetterSpacing,
                rect);
        }

        private void InitButtonTextFontDropDown(CGRect rect)
        {
            buttonTextFontDropDown.InitSource(
                Fonts,
                font =>
                {
                    _source.SectionModel.ButtonNameFont = font;
                },
                Fields.ButtonTextFont,
                rect);
        }

        private void InitSectionNameFontDropDown(CGRect rect)
        {
            sectionNameFontDropDown.InitSource(
                Fonts,
                font =>
                {
                    _source.SectionModel.SectionNameFont = font;
                },
                Fields.SectionNameFont,
                rect);
        }

        private void InitButtonTextDropDown(CGRect rect)
        {
            buttonTextDropDown.InitSource(
                Titles,
                title =>
                {
                    _source.SectionModel.ButtonText = title;
                },
                Fields.ButtonText,
                rect);
        }

        private void InitSectionNameDropDown(CGRect rect)
        {
            sectionNameDropDown.InitSource(
                Titles,
                title =>
                {
                    _source.SectionModel.SectionName = title;
                },
                Fields.SectionName,
                rect);
        }

        private void InitThemeDropDown(CGRect rect)
        {
            themeDropDown.InitSource(
                Constants.Themes,
                (theme) =>
                {
                    EOSThemeProvider.Instance.SetCurrentTheme(theme);
                    var defaultModel = new SectionModel()
                    {
                        SectionAction = () => { new UIAlertView("Action", "Action invoked", null, "Ok", null).Show(); },
                        HasBorder = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionBorder],
                        HasButton = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionAction]
                    };
                    _source.SectionModel.CopyData(defaultModel);
                    _source.SectionModel.ResetCustomization = true;
                    _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
                    UpdateApperaence();
                },
                Fields.Theme,
                rect);
            themeDropDown.SetTextFieldText(EOSThemeProvider.Instance.GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark");
        }

        private void ResetFields()
        {
            _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
            hasButtonSwitch.On = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionAction];
            hasBorderSwitch.On = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionBorder];
        }
    }
}