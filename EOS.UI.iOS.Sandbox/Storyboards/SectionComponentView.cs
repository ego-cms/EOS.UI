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

namespace EOS.UI.iOS.Sandbox.Storyboards
{
    public partial class SectionComponentView : BaseViewController
    {
        public const string Identifier = "SectionView";
        private List<UITextField> _textFields;
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

            _textFields = new List<UITextField>()
            {
                themeTextField,
                sectionNameTextField,
                buttonTextTextField,
                sectionNameFontTextField,
                buttonTextFontTextField,
                sectionNameLetterSpacingTextField,
                buttonTextLetterSpacingTextField,
                sectionTextSizeTextField,
                buttonTextSizeTextField,
                sectionTextColorTextField,
                buttonTextColorTextField,
                backgroundColorTextField,
                borderColorTextField,
                borderWidthTextField,
                paddingTopTextField,
                paddingBottomTextField,
                paddingLeftTextField,
                paddingRightTextField
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _textFields.ForEach(f => f.ResignFirstResponder());
            }));

            var rect = new CGRect(0, 0, 100, 150);

            InitThemeTextField(rect);
            InitSectionNameTextField(rect);
            InitButtonTextTextField(rect);
            InitSectionNameFontTextField(rect);
            InitButtonTextFontTextField(rect);
            InitSectionNameLetterSpacingTextField(rect);
            InitButtonTextLetterSpacingTextField(rect);
            InitSectionTextSizeTextField(rect);
            InitButtonTextSizeTextField(rect);
            InitSectionTextColorTextField(rect);
            InitButtonTextColorTextField(rect);
            InitBackgroundColorTextField(rect);
            InitBorderColorTextField(rect);
            InitBorderWidthTextField(rect);
            InitPaddingTopTextField(rect);
            InitPaddingBottomTextField(rect);
            InitPaddingLeftTextField(rect);
            InitPaddingRightTextField(rect);
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

        private void InitPaddingRightTextField(CGRect rect)
        {
            var numericPicker = new UIPickerView(rect);
            numericPicker.ShowSelectionIndicator = true;
            numericPicker.DataSource = new ValuePickerSource<int>(Constants.PaddingValues);
            var numericPickerDelegate = new ValuePickerDelegate<int>(Constants.PaddingValues);
            numericPickerDelegate.DidSelected += (object sender, int e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.RightPadding = e;
                sectionTableView.ReloadData();
                paddingRightTextField.Text = e.ToString();
            };
            paddingRightTextField.EditingDidBegin += (sender, e) =>
            {
                var padding = Constants.PaddingValues[(int)numericPicker.SelectedRowInComponent(0)];
                (sectionTableView.Source as SectionTableSource).SectionModel.RightPadding = padding;
                sectionTableView.ReloadData();
                paddingRightTextField.Text = padding.ToString();
            };
            numericPicker.Delegate = numericPickerDelegate;
            paddingRightTextField.InputView = numericPicker;
        }

        private void InitPaddingLeftTextField(CGRect rect)
        {
            var numericPicker = new UIPickerView(rect);
            numericPicker.ShowSelectionIndicator = true;
            numericPicker.DataSource = new ValuePickerSource<int>(Constants.PaddingValues);
            var numericPickerDelegate = new ValuePickerDelegate<int>(Constants.PaddingValues);
            numericPickerDelegate.DidSelected += (object sender, int e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.LeftPadding = e;
                sectionTableView.ReloadData();
                paddingLeftTextField.Text = e.ToString();
            };
            paddingLeftTextField.EditingDidBegin += (sender, e) =>
            {
                var padding = Constants.PaddingValues[(int)numericPicker.SelectedRowInComponent(0)];
                (sectionTableView.Source as SectionTableSource).SectionModel.LeftPadding = padding;
                sectionTableView.ReloadData();
                paddingLeftTextField.Text = padding.ToString();
            };
            numericPicker.Delegate = numericPickerDelegate;
            paddingLeftTextField.InputView = numericPicker;
        }

        private void InitPaddingBottomTextField(CGRect rect)
        {
            var numericPicker = new UIPickerView(rect);
            numericPicker.ShowSelectionIndicator = true;
            numericPicker.DataSource = new ValuePickerSource<int>(Constants.PaddingValues);
            var numericPickerDelegate = new ValuePickerDelegate<int>(Constants.PaddingValues);
            numericPickerDelegate.DidSelected += (object sender, int e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.BottonPadding = e;
                sectionTableView.ReloadData();
                paddingBottomTextField.Text = e.ToString();
            };
            paddingBottomTextField.EditingDidBegin += (sender, e) =>
            {
                var padding = Constants.PaddingValues[(int)numericPicker.SelectedRowInComponent(0)];
                (sectionTableView.Source as SectionTableSource).SectionModel.BottonPadding = padding;
                sectionTableView.ReloadData();
                paddingBottomTextField.Text = padding.ToString();
            };
            numericPicker.Delegate = numericPickerDelegate;
            paddingBottomTextField.InputView = numericPicker;
        }

        private void InitPaddingTopTextField(CGRect rect)
        {
            var numericPicker = new UIPickerView(rect);
            numericPicker.ShowSelectionIndicator = true;
            numericPicker.DataSource = new ValuePickerSource<int>(Constants.PaddingValues);
            var numericPickerDelegate = new ValuePickerDelegate<int>(Constants.PaddingValues);
            numericPickerDelegate.DidSelected += (object sender, int e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.TopPadding = e;
                sectionTableView.ReloadData();
                paddingTopTextField.Text = e.ToString();
            };
            paddingTopTextField.EditingDidBegin += (sender, e) =>
            {
                var padding = Constants.PaddingValues[(int)numericPicker.SelectedRowInComponent(0)];
                (sectionTableView.Source as SectionTableSource).SectionModel.TopPadding = padding;
                sectionTableView.ReloadData();
                paddingTopTextField.Text = padding.ToString();
            };
            numericPicker.Delegate = numericPickerDelegate;
            paddingTopTextField.InputView = numericPicker;
        }

        private void InitBorderWidthTextField(CGRect rect)
        {
            var numericPicker = new UIPickerView(rect);
            numericPicker.ShowSelectionIndicator = true;
            numericPicker.DataSource = new ValuePickerSource<int>(Constants.WidthValues);
            var numericPickerDelegate = new ValuePickerDelegate<int>(Constants.WidthValues);
            numericPickerDelegate.DidSelected += (object sender, int e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.BorderWidth = e;
                sectionTableView.ReloadData();
                borderWidthTextField.Text = e.ToString();
            };
            borderWidthTextField.EditingDidBegin += (sender, e) =>
            {
                var width = Constants.WidthValues[(int)numericPicker.SelectedRowInComponent(0)];
                (sectionTableView.Source as SectionTableSource).SectionModel.BorderWidth = width;
                sectionTableView.ReloadData();
                borderWidthTextField.Text = width.ToString();
            };
            numericPicker.Delegate = numericPickerDelegate;
            borderWidthTextField.InputView = numericPicker;
        }

        private void InitBorderColorTextField(CGRect rect)
        {
            var colorPicker = new UIPickerView(rect);
            colorPicker.ShowSelectionIndicator = true;
            colorPicker.DataSource = new ColorPickerSource();
            var colorPickerDelegate = new ColorPickerDelegate();
            colorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.BorderColor = e.Value;
                sectionTableView.ReloadData();
                borderColorTextField.Text = e.Key;
            };
            borderColorTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)colorPicker.SelectedRowInComponent(0));
                (sectionTableView.Source as SectionTableSource).SectionModel.BorderColor = colorPair.Value;
                sectionTableView.ReloadData();
                borderColorTextField.Text = colorPair.Key;
            };
            colorPicker.Delegate = colorPickerDelegate;
            borderColorTextField.InputView = colorPicker;
        }

        private void InitBackgroundColorTextField(CGRect rect)
        {
            var colorPicker = new UIPickerView(rect);
            colorPicker.ShowSelectionIndicator = true;
            colorPicker.DataSource = new ColorPickerSource();
            var colorPickerDelegate = new ColorPickerDelegate();
            colorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.BackgroundColor = e.Value;
                sectionTableView.ReloadData();
                backgroundColorTextField.Text = e.Key;
            };
            backgroundColorTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)colorPicker.SelectedRowInComponent(0));
                (sectionTableView.Source as SectionTableSource).SectionModel.BackgroundColor = colorPair.Value;
                sectionTableView.ReloadData();
                backgroundColorTextField.Text = colorPair.Key;
            };
            colorPicker.Delegate = colorPickerDelegate;
            backgroundColorTextField.InputView = colorPicker;
        }

        private void InitButtonTextColorTextField(CGRect rect)
        {
            var textColorPicker = new UIPickerView(rect);
            textColorPicker.ShowSelectionIndicator = true;
            textColorPicker.DataSource = new ColorPickerSource();
            var textColorPickerDelegate = new ColorPickerDelegate();
            textColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.ButtonNameColor = e.Value;
                sectionTableView.ReloadData();
                buttonTextColorTextField.Text = e.Key;
            };
            buttonTextColorTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)textColorPicker.SelectedRowInComponent(0));
                (sectionTableView.Source as SectionTableSource).SectionModel.ButtonNameColor = colorPair.Value;
                sectionTableView.ReloadData();
                buttonTextColorTextField.Text = colorPair.Key;
            };
            textColorPicker.Delegate = textColorPickerDelegate;
            buttonTextColorTextField.InputView = textColorPicker;
        }

        private void InitSectionTextColorTextField(CGRect rect)
        {
            var textColorPicker = new UIPickerView(rect);
            textColorPicker.ShowSelectionIndicator = true;
            textColorPicker.DataSource = new ColorPickerSource();
            var textColorPickerDelegate = new ColorPickerDelegate();
            textColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.SectionNameColor = e.Value;
                sectionTableView.ReloadData();
                sectionTextColorTextField.Text = e.Key;
            };
            sectionTextColorTextField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)textColorPicker.SelectedRowInComponent(0));
                (sectionTableView.Source as SectionTableSource).SectionModel.SectionNameColor = colorPair.Value;
                sectionTableView.ReloadData();
                sectionTextColorTextField.Text = colorPair.Key;
            };
            textColorPicker.Delegate = textColorPickerDelegate;
            sectionTextColorTextField.InputView = textColorPicker;
        }

        private void InitButtonTextSizeTextField(CGRect rect)
        {
            var textSizePicker = new UIPickerView(rect);
            textSizePicker.ShowSelectionIndicator = true;
            textSizePicker.DataSource = new ValuePickerSource<int>(Constants.FontSizeValues);
            var textSizePickerDelegate = new ValuePickerDelegate<int>(Constants.FontSizeValues);
            textSizePickerDelegate.DidSelected += (object sender, int e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.ButtonTextTextSize = e;
                sectionTableView.ReloadData();
                buttonTextSizeTextField.Text = e.ToString();
            };
            buttonTextSizeTextField.EditingDidBegin += (sender, e) =>
            {
                var size = Constants.FontSizeValues[(int)textSizePicker.SelectedRowInComponent(0)];
                (sectionTableView.Source as SectionTableSource).SectionModel.ButtonTextTextSize = size;
                sectionTableView.ReloadData();
                buttonTextSizeTextField.Text = size.ToString();
            };
            textSizePicker.Delegate = textSizePickerDelegate;
            buttonTextSizeTextField.InputView = textSizePicker;
        }

        private void InitSectionTextSizeTextField(CGRect rect)
        {
            var textSizePicker = new UIPickerView(rect);
            textSizePicker.ShowSelectionIndicator = true;
            textSizePicker.DataSource = new ValuePickerSource<int>(Constants.FontSizeValues);
            var textSizePickerDelegate = new ValuePickerDelegate<int>(Constants.FontSizeValues);
            textSizePickerDelegate.DidSelected += (object sender, int e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.SectionNameTextSize = e;
                sectionTableView.ReloadData();
                sectionTextSizeTextField.Text = e.ToString();
            };
            sectionTextSizeTextField.EditingDidBegin += (sender, e) =>
            {
                var size = Constants.FontSizeValues[(int)textSizePicker.SelectedRowInComponent(0)];
                (sectionTableView.Source as SectionTableSource).SectionModel.SectionNameTextSize = size;
                sectionTableView.ReloadData();
                sectionTextSizeTextField.Text = size.ToString();
            };
            textSizePicker.Delegate = textSizePickerDelegate;
            sectionTextSizeTextField.InputView = textSizePicker;
        }

        private void InitButtonTextLetterSpacingTextField(CGRect rect)
        {
            var letterSpacingPicker = new UIPickerView(rect);
            letterSpacingPicker.ShowSelectionIndicator = true;
            letterSpacingPicker.DataSource = new ValuePickerSource<int>(Constants.LetterSpacingValues);
            var letterSpacingPickerDelegate = new ValuePickerDelegate<int>(Constants.LetterSpacingValues);
            letterSpacingPickerDelegate.DidSelected += (object sender, int e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.ButtonTextLetterSpacing = e;
                sectionTableView.ReloadData();
                buttonTextLetterSpacingTextField.Text = e.ToString();
            };
            buttonTextLetterSpacingTextField.EditingDidBegin += (sender, e) =>
            {
                var spacing = Constants.LetterSpacingValues[(int)letterSpacingPicker.SelectedRowInComponent(0)];
                (sectionTableView.Source as SectionTableSource).SectionModel.ButtonTextLetterSpacing = spacing;
                sectionTableView.ReloadData();
                buttonTextLetterSpacingTextField.Text = spacing.ToString();
            };
            letterSpacingPicker.Delegate = letterSpacingPickerDelegate;
            buttonTextLetterSpacingTextField.InputView = letterSpacingPicker;
        }

        private void InitSectionNameLetterSpacingTextField(CGRect rect)
        {
            var letterSpacingPicker = new UIPickerView(rect);
            letterSpacingPicker.ShowSelectionIndicator = true;
            letterSpacingPicker.DataSource = new ValuePickerSource<int>(Constants.LetterSpacingValues);
            var letterSpacingPickerDelegate = new ValuePickerDelegate<int>(Constants.LetterSpacingValues);
            letterSpacingPickerDelegate.DidSelected += (object sender, int e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.SectionTextLetterSpacing = e;
                sectionTableView.ReloadData();
                sectionNameLetterSpacingTextField.Text = e.ToString();
            };
            sectionNameLetterSpacingTextField.EditingDidBegin += (sender, e) =>
            {
                var spacing = Constants.LetterSpacingValues[(int)letterSpacingPicker.SelectedRowInComponent(0)];
                (sectionTableView.Source as SectionTableSource).SectionModel.SectionTextLetterSpacing = spacing;
                sectionTableView.ReloadData();
                sectionNameLetterSpacingTextField.Text = spacing.ToString();
            };
            letterSpacingPicker.Delegate = letterSpacingPickerDelegate;
            sectionNameLetterSpacingTextField.InputView = letterSpacingPicker;
        }

        private void InitButtonTextFontTextField(CGRect rect)
        {
            var fontPicker = new UIPickerView(rect);
            fontPicker.ShowSelectionIndicator = true;
            fontPicker.DataSource = new ValuePickerSource<UIFont>(Constants.Fonts);
            var fontPickerDelegate = new ValuePickerDelegate<UIFont>(Constants.Fonts);
            fontPickerDelegate.DidSelected += (object sender, UIFont e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.ButtonNameFont = e;
                sectionTableView.ReloadData();
                buttonTextFontTextField.Text = e.Name;
            };
            buttonTextFontTextField.EditingDidBegin += (sender, e) =>
            {
                var font = Constants.Fonts.ElementAt((int)fontPicker.SelectedRowInComponent(0));
                (sectionTableView.Source as SectionTableSource).SectionModel.ButtonNameFont = font;
                sectionTableView.ReloadData();
                buttonTextFontTextField.Text = font.Name;
            };
            fontPicker.Delegate = fontPickerDelegate;
            buttonTextFontTextField.InputView = fontPicker;
        }

        private void InitSectionNameFontTextField(CGRect rect)
        {
            var fontPicker = new UIPickerView(rect);
            fontPicker.ShowSelectionIndicator = true;
            fontPicker.DataSource = new ValuePickerSource<UIFont>(Constants.Fonts);
            var fontPickerDelegate = new ValuePickerDelegate<UIFont>(Constants.Fonts);
            fontPickerDelegate.DidSelected += (object sender, UIFont e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.SectionNameFont = e;
                sectionTableView.ReloadData();
                sectionNameFontTextField.Text = e.Name;
            };
            sectionNameFontTextField.EditingDidBegin += (sender, e) =>
            {
                var font = Constants.Fonts.ElementAt((int)fontPicker.SelectedRowInComponent(0));
                (sectionTableView.Source as SectionTableSource).SectionModel.SectionNameFont = font;
                sectionTableView.ReloadData();
                sectionNameFontTextField.Text = font.Name;
            };
            fontPicker.Delegate = fontPickerDelegate;
            sectionNameFontTextField.InputView = fontPicker;
        }

        private void InitButtonTextTextField(CGRect rect)
        {
            var textPicker = new UIPickerView(rect);
            textPicker.ShowSelectionIndicator = true;
            textPicker.DataSource = new ValuePickerSource<string>(Constants.Titles);
            var buttonTextPickerDelegate = new ValuePickerDelegate<string>(Constants.Titles);
            buttonTextPickerDelegate.DidSelected += (object sender, string e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.ButtonText = e;
                sectionTableView.ReloadData();
                buttonTextTextField.Text = e;
            };
            buttonTextTextField.EditingDidBegin += (sender, e) =>
            {
                var title = Constants.Titles[(int)textPicker.SelectedRowInComponent(0)];
                (sectionTableView.Source as SectionTableSource).SectionModel.ButtonText = title;
                sectionTableView.ReloadData();
                buttonTextTextField.Text = title;
            };
            textPicker.Delegate = buttonTextPickerDelegate;
            buttonTextTextField.InputView = textPicker;
        }

        private void InitSectionNameTextField(CGRect rect)
        {
            var textPicker = new UIPickerView(rect);
            textPicker.ShowSelectionIndicator = true;
            textPicker.DataSource = new ValuePickerSource<string>(Constants.Titles);
            var sectionNamePickerDelegate = new ValuePickerDelegate<string>(Constants.Titles);
            sectionNamePickerDelegate.DidSelected += (object sender, string e) =>
            {
                (sectionTableView.Source as SectionTableSource).SectionModel.SectionName = e;
                sectionTableView.ReloadData();
                sectionNameTextField.Text = e;
            };
            sectionNameTextField.EditingDidBegin += (sender, e) =>
            {
                var title = Constants.Titles[(int)textPicker.SelectedRowInComponent(0)];
                (sectionTableView.Source as SectionTableSource).SectionModel.SectionName = title;
                sectionTableView.ReloadData();
                sectionNameTextField.Text = title;
            };
            textPicker.Delegate = sectionNamePickerDelegate;
            sectionNameTextField.InputView = textPicker;
        }

        private void InitThemeTextField(CGRect rect)
        {
            var themePicker = new UIPickerView(rect);
            themePicker.ShowSelectionIndicator = true;
            themePicker.DataSource = new DictionaryPickerSource<String, EOSThemeEnumeration>(Constants.Themes);
            var themePickerDelegate = new DictionaryPickerDelegate<String, EOSThemeEnumeration>(Constants.Themes);
            themePickerDelegate.DidSelected += (object sender, KeyValuePair<string, EOSThemeEnumeration> e) =>
            {
                themeTextField.Text = e.Key;
                EOSThemeProvider.Instance.SetCurrentTheme(e.Value);
                (sectionTableView.Source as SectionTableSource).SectionModel.ResetCustomization = true;
                sectionTableView.ReloadData();
                ResetFields();
            };
            themeTextField.Text = EOSThemeProvider.Instance.GetCurrentTheme() is LightEOSTheme ? "Light" : "Dark";

            themePicker.Delegate = themePickerDelegate;
            themeTextField.InputView = themePicker;
        }

        private void ResetFields()
        {
            _textFields.Except(new[] { themeTextField }).ToList().ForEach(field => field.Text = string.Empty);
            hasButtonSwitch.On = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionAction];
            hasBorderSwitch.On = (bool)EOSThemeProvider.Instance.GetCurrentTheme().ThemeValues[EOSConstants.HasSectionBorder];
        }
    }
}