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

namespace EOS.UI.iOS.Sandbox
{
    public partial class BadgeLabelView : BaseViewController
    {
        public const string Identifier = "BadgeLabelView";

        private List<UITextField> _textFields;

        public BadgeLabelView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var label = new BadgeLabel();
            label.Text = "Default Text";

            _textFields = new List<UITextField>()
            {
                backgroundColorField,
                letterSpacingField,
                themeField,
                fontField,
                fontColorField,
                fontSizeField,
                cornerRadiusField
            };

            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                _textFields.ForEach(f => f.ResignFirstResponder());
            }));

            containerView.ConstrainLayout(() => label.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                                label.Frame.GetCenterY() == containerView.Frame.GetCenterY(), label);

            var rect = new CGRect(0, 0, 100, 150);

            var themePicker = new UIPickerView(rect);
            themePicker.ShowSelectionIndicator = true;
            themePicker.DataSource = new ThemePickerSource();
            var themePickerDelegate = new ThemePickerDelegate();
            themePickerDelegate.DidSelected += (object sender, KeyValuePair<string, EOSThemeEnumeration> e) =>
            {
                themeField.Text = e.Key;
                var provider = label.GetThemeProvider();
                provider.SetCurrentTheme(e.Value);
                label.UpdateAppearance();
                backgroundColorField.Text = String.Empty;
                fontField.Text = String.Empty;
                fontColorField.Text = String.Empty;
                fontSizeField.Text = String.Empty;
                cornerRadiusField.Text = String.Empty;
            };
            themeField.Text = label.GetThemeProvider().GetCurrentTheme().ThemeValues[EOSConstants.PrimaryColor] == UIColor.White ?
                "Light" : "Dark";


            themePicker.Delegate = themePickerDelegate;
            themeField.InputView = themePicker;

            var colorPicker = new UIPickerView(rect);
            colorPicker.ShowSelectionIndicator = true;
            colorPicker.DataSource = new ColorPickerSource();
            var backgroundColorPickerDelegate = new ColorPickerDelegate();
            backgroundColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                label.BackgroundColor = e.Value;
                backgroundColorField.Text = e.Key;
            };
            backgroundColorField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)colorPicker.SelectedRowInComponent(0));
                label.BackgroundColor = colorPair.Value;
                backgroundColorField.Text = colorPair.Key;
            };
            colorPicker.Delegate = backgroundColorPickerDelegate;
            backgroundColorField.InputView = colorPicker;

            var fontPicker = new UIPickerView(rect);
            fontPicker.ShowSelectionIndicator = true;
            fontPicker.DataSource = new FontPickerSource();
            var fontPickerDelegate = new FontPickerDelegate();
            fontPickerDelegate.DidSelected += (object sender, UIFont e) =>
            {
                label.Font = e;
                fontField.Text = e.Name;
            };
            fontField.EditingDidBegin += (sender, e) =>
            {
                var font = Constants.Fonts.ElementAt((int)fontPicker.SelectedRowInComponent(0));
                label.Font = font;
                fontField.Text = font.Name;
            };
            fontPicker.Delegate = fontPickerDelegate;
            fontField.InputView = fontPicker;

            var fontColorPicker = new UIPickerView(rect);
            fontColorPicker.ShowSelectionIndicator = true;
            fontColorPicker.DataSource = new ColorPickerSource();
            var fontColorPickerDelegate = new ColorPickerDelegate();
            fontColorPickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                label.TextColor = e.Value;
                fontColorField.Text = e.Key;
            };
            fontColorField.EditingDidBegin += (sender, e) =>
            {
                var colorPair = Constants.Colors.ElementAt((int)fontColorPicker.SelectedRowInComponent(0));
                label.TextColor = colorPair.Value;
                fontColorField.Text = colorPair.Key;
            };
            fontColorPicker.Delegate = fontColorPickerDelegate;
            fontColorField.InputView = fontColorPicker;

            var letterSpacingPicker = new UIPickerView(rect);
            letterSpacingPicker.ShowSelectionIndicator = true;
            letterSpacingPicker.DataSource = new LetterSpacingPickerSource();
            var letterSpacingPickerDelegate = new LetterSpacingPickerDelegate();
            letterSpacingPickerDelegate.DidSelected += (object sender, int e) =>
            {
                label.LetterSpacing = e;
                letterSpacingField.Text = e.ToString();
            };
            letterSpacingField.EditingDidBegin += (sender, e) =>
            {
                var spacing = Constants.LetterSpacingValues[(int)letterSpacingPicker.SelectedRowInComponent(0)];
                label.LetterSpacing = spacing;
                letterSpacingField.Text = spacing.ToString();
            };
            letterSpacingPicker.Delegate = letterSpacingPickerDelegate;
            letterSpacingField.InputView = letterSpacingPicker;


            var fontSizePicker = new UIPickerView(rect);
            fontSizePicker.ShowSelectionIndicator = true;
            fontSizePicker.DataSource = new FontSizesPickerSource();
            var fontSizePickerDelegate = new FontSizesPickerDelegate();
            fontSizePickerDelegate.DidSelected += (object sender, int e) =>
            {
                label.TextSize = e;
                fontSizeField.Text = e.ToString();
            };
            fontSizeField.EditingDidBegin += (sender, e) =>
            {
                var size = Constants.FontSizeValues[(int)fontSizePicker.SelectedRowInComponent(0)];
                label.TextSize = size;
                fontSizeField.Text = size.ToString();
            };
            fontSizePicker.Delegate = fontSizePickerDelegate;
            fontSizeField.InputView = fontSizePicker;

            var cornerRadiusPicker = new UIPickerView(rect);
            cornerRadiusPicker.ShowSelectionIndicator = true;
            cornerRadiusPicker.DataSource = new CornerRadiusPickerSource();
            var cornerRadiusPickerDelegate = new CornerRadiusPickerDelegate();
            cornerRadiusPickerDelegate.DidSelected += (object sender, int e) =>
            {
                label.CornerRadius = e;
                cornerRadiusField.Text = e.ToString();
            };
            cornerRadiusField.EditingDidBegin += (sender, e) =>
            {
                var size = Constants.CornerRadiusValues[(int)cornerRadiusPicker.SelectedRowInComponent(0)];
                label.CornerRadius = size;
                cornerRadiusField.Text = size.ToString();
            };
            cornerRadiusPicker.Delegate = cornerRadiusPickerDelegate;
            cornerRadiusField.InputView = cornerRadiusPicker;

            resetButton.TouchUpInside += (sender, e) =>
            {
                label.ResetCustomization();
                _textFields.Except(new List<UITextField>() { themeField }).ToList().ForEach(f => f.Text = String.Empty);
            };
        }

        //font picker
        public class FontPickerSource : UIPickerViewDataSource
        {
            public override nint GetComponentCount(UIPickerView pickerView)
            {
                return 1;
            }

            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            {
                return Constants.Fonts.Count;
            }
        }

        public class FontPickerDelegate : UIPickerViewDelegate
        {
            public event EventHandler<UIFont> DidSelected;

            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
            {
                return Constants.Fonts[(int)row].Name;
            }

            public override void Selected(UIPickerView pickerView, nint row, nint component)
            {
                DidSelected?.Invoke(this, Constants.Fonts[(int)row]);
            }
        }

        //color picker
        public class ColorPickerSource : UIPickerViewDataSource
        {
            public override nint GetComponentCount(UIPickerView pickerView)
            {
                return 1;
            }

            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            {
                return Constants.Colors.Count;
            }
        }

        public class ColorPickerDelegate : UIPickerViewDelegate
        {
            public event EventHandler<KeyValuePair<String, UIColor>> DidSelected;

            public override NSAttributedString GetAttributedTitle(UIPickerView pickerView, nint row, nint component)
            {
                var pair = Constants.Colors.ElementAt((int)row);
                var attributedString = new NSMutableAttributedString(pair.Key);
                attributedString.AddAttribute(UIStringAttributeKey.ForegroundColor, pair.Value, new NSRange(0, attributedString.Length));
                return attributedString;
            }

            public override void Selected(UIPickerView pickerView, nint row, nint component)
            {
                DidSelected?.Invoke(this, Constants.Colors.ElementAt((int)row));
            }
        }

        //fontsizes picker
        public class FontSizesPickerSource : UIPickerViewDataSource
        {
            public override nint GetComponentCount(UIPickerView pickerView)
            {
                return 1;
            }

            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            {
                return Constants.FontSizeValues.Count;
            }
        }

        public class FontSizesPickerDelegate : UIPickerViewDelegate
        {
            public event EventHandler<int> DidSelected;

            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
            {
                return Constants.FontSizeValues[(int)row].ToString();
            }

            public override void Selected(UIPickerView pickerView, nint row, nint component)
            {
                DidSelected?.Invoke(this, Constants.FontSizeValues[(int)row]);
            }
        }

        //letterSpacing picker
        public class LetterSpacingPickerSource : UIPickerViewDataSource
        {
            public override nint GetComponentCount(UIPickerView pickerView)
            {
                return 1;
            }

            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            {
                return Constants.LetterSpacingValues.Count;
            }
        }

        public class LetterSpacingPickerDelegate : UIPickerViewDelegate
        {
            public event EventHandler<int> DidSelected;

            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
            {
                return Constants.LetterSpacingValues[(int)row].ToString();
            }

            public override void Selected(UIPickerView pickerView, nint row, nint component)
            {
                DidSelected?.Invoke(this, Constants.LetterSpacingValues[(int)row]);
            }
        }

        //cornerradius picker
        public class CornerRadiusPickerSource : UIPickerViewDataSource
        {
            public override nint GetComponentCount(UIPickerView pickerView)
            {
                return 1;
            }

            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            {
                return Constants.CornerRadiusValues.Count;
            }
        }

        public class CornerRadiusPickerDelegate : UIPickerViewDelegate
        {
            public event EventHandler<int> DidSelected;

            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
            {
                return Constants.CornerRadiusValues[(int)row].ToString();
            }

            public override void Selected(UIPickerView pickerView, nint row, nint component)
            {
                DidSelected?.Invoke(this, Constants.CornerRadiusValues[(int)row]);
            }
        }

        //theme picker
        public class ThemePickerSource : UIPickerViewDataSource
        {
            public override nint GetComponentCount(UIPickerView pickerView)
            {
                return 1;
            }

            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            {
                return Constants.Themes.Count;
            }
        }

        public class ThemePickerDelegate : UIPickerViewDelegate
        {
            public event EventHandler<KeyValuePair<string, EOSThemeEnumeration>> DidSelected;

            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
            {
                return Constants.Themes.ElementAt((int)row).Key;
            }

            public override void Selected(UIPickerView pickerView, nint row, nint component)
            {
                DidSelected?.Invoke(this, Constants.Themes.ElementAt((int)row));
            }
        }
    }
}