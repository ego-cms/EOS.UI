using System;
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
        private List<CustomDropDown> _dropDowns;
        private List<object> _dataSource;

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

            sectionTableView.Source = new SectionTableSource(sectionTableView, _dataSource);

            _dropDowns = new List<CustomDropDown>()
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

            (sectionTableView.Source as SectionTableSource).SectionModel.ResetCustomization = true;
            sectionTableView.ReloadData();
            ResetFields();
        }

        private void InitResetButton()
        {
            resetCustomizationButton.TouchUpInside += (sender, e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.ResetCustomization = true;
                sectionTableView.ReloadData();
                ResetFields();
            };
        }

        private void InitHasBorder()
        {
            hasBorderSwitch.On = true;
            hasBorderSwitch.ValueChanged += delegate
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.HasBorder = hasBorderSwitch.On;
                sectionTableView.ReloadData();
            };
        }

        private void InitHasButton()
        {
            hasButtonSwitch.On = true;
            hasButtonSwitch.ValueChanged += delegate
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.HasButton = hasButtonSwitch.On;
                sectionTableView.ReloadData();
            };
        }

        private void InitPaddingRightDropDown(CGRect rect)
        {
            paddingRightDropDown.InitSource(
                PaddingValues,
                padding =>
                {
                    (sectionTableView.Source as SectionTableSource).SectionModel.RightPadding = padding;
                    sectionTableView.ReloadData();
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
                    (sectionTableView.Source as SectionTableSource).SectionModel.LeftPadding = padding;
                    sectionTableView.ReloadData();
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
                    (sectionTableView.Source as SectionTableSource).SectionModel.BottonPadding = padding;
                    sectionTableView.ReloadData();
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
                    (sectionTableView.Source as SectionTableSource).SectionModel.TopPadding = padding;
                    sectionTableView.ReloadData();
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
                    (sectionTableView.Source as SectionTableSource).SectionModel.BorderWidth = width;
                    sectionTableView.ReloadData();
                },
                Fields.BorderWidth,
                rect);
        }

        private void InitBorderColorDropDown(CGRect rect)
        {
            borderColorDropDown.InitSource(
                color =>
                {
                    (sectionTableView.Source as SectionTableSource).SectionModel.BorderColor = color;
                    sectionTableView.ReloadData();
                },
                Fields.BorderColor,
                rect);
        }

        private void InitBackgroundColorDropDown(CGRect rect)
        {
            backgoundColorDropDown.InitSource(
                color =>
                {
                    (sectionTableView.Source as SectionTableSource).SectionModel.BackgroundColor = color;
                    sectionTableView.ReloadData();
                },
                Fields.BackgroundColor,
                rect);
        }

        private void InitButtonTextColorDropDown(CGRect rect)
        {
            buttonTextColorDropDown.InitSource(
                color =>
                {
                    (sectionTableView.Source as SectionTableSource).SectionModel.ButtonNameColor = color;
                    sectionTableView.ReloadData();
                },
                Fields.ButtonTextColor,
                rect);
        }

        private void InitSectionTextColorDropDown(CGRect rect)
        {
            sectionTextColorDropDown.InitSource(
                color =>
                {
                    (sectionTableView.Source as SectionTableSource).SectionModel.SectionNameColor = color;
                    sectionTableView.ReloadData();
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
                    (sectionTableView.Source as SectionTableSource).SectionModel.ButtonTextTextSize = size;
                    sectionTableView.ReloadData();
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
                    (sectionTableView.Source as SectionTableSource).SectionModel.SectionNameTextSize = size;
                    sectionTableView.ReloadData();
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
                    (sectionTableView.Source as SectionTableSource).SectionModel.ButtonTextLetterSpacing = spacing;
                    sectionTableView.ReloadData();
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
                    (sectionTableView.Source as SectionTableSource).SectionModel.SectionTextLetterSpacing = spacing;
                    sectionTableView.ReloadData();
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
                    (sectionTableView.Source as SectionTableSource).SectionModel.ButtonNameFont = font;
                    sectionTableView.ReloadData();
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
                    (sectionTableView.Source as SectionTableSource).SectionModel.SectionNameFont = font;
                    sectionTableView.ReloadData();
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
                    (sectionTableView.Source as SectionTableSource).SectionModel.ButtonText = title;
                    sectionTableView.ReloadData();
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
                    (sectionTableView.Source as SectionTableSource).SectionModel.SectionName = title;
                    sectionTableView.ReloadData();
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
                    (sectionTableView.Source as SectionTableSource).SectionModel.ResetCustomization = true;
                    sectionTableView.ReloadData();
                    _dropDowns.Except(new[] { themeDropDown }).ToList().ForEach(dropDown => dropDown.ResetValue());
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