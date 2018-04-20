using EOS.UI.iOS.Controls;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Sandbox.Storyboards;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class BadgeLabelView : BaseViewController
    {
        public const string Identifier = "BadgeLabelView";

        private static List<UIFont> Fonts = new List<UIFont>();
        private static Dictionary<string, UIColor> FontColors = new Dictionary<string, UIColor>()
        {{"Red", UIColor.Red}, {"Green", UIColor.Green}, {"Blue", UIColor.Blue}, {"Gray", UIColor.Gray}, {"Yellow", UIColor.Yellow}, {"Orange", UIColor.Orange}};
        private static List<int> CornerRadiusValues = new List<int>() { 1, 2, 3, 4, 5, 7 };
        private static List<int> FontSizeValues = new List<int>() { 17, 19, 24, 32, 40 };
        private static List<int> LetterSpacingValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };

        public BadgeLabelView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var label = new BadgeLabel();
            label.BackgroundColor = UIColor.Red;
            label.TextColor = UIColor.White;
            label.CornerRadius = 5;
            label.Text = "Some text";

            foreach (var familyName in UIFont.FamilyNames)
            {
                foreach (var fontName in UIFont.FontNamesForFamilyName(familyName))
                {
                    var font = UIFont.FromName(fontName, label.TextSize);
                    if (font != null)
                        Fonts.Add(font);
                }
            }


            containerView.ConstrainLayout(() => label.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                          label.Frame.GetCenterY() == containerView.Frame.GetCenterY(), label);

            fontSizePicker.DataSource = new FonSizesSource();
            fontSizePicker.Delegate = new FontSizesPickerDelegate();
            var row = FontSizeValues.IndexOf(label.TextSize);
            fontSizePicker.Select(row, 0, false);

            letterSpacingPicker.DataSource = new LetterSpacingSource();
            letterSpacingPicker.Delegate = new LetterSpacingPickerDelegate();
            row = LetterSpacingValues.IndexOf(label.LetterSpacing);
            letterSpacingPicker.Select(row, 0, false);

            cornerRadiusPicker.DataSource = new CornerRadiusSource();
            cornerRadiusPicker.Delegate = new CornerRadiusPickerDelegate();
            row = CornerRadiusValues.IndexOf(label.CornerRadius);
            cornerRadiusPicker.Select(row, 0, false);

            fontPicker.DataSource = new FontPickerSource();
            fontPicker.Delegate = new FontPickerDelegate();
            row = Fonts.IndexOf(label.Font);
            fontPicker.Select(row, 0, false);

            colorPicker.DataSource = new ColorPickerSource();
            colorPicker.Delegate = new ColorPickerDelegate();
            row = FontColors.Values.ToList().IndexOf(label.BackgroundColor);
            colorPicker.Select(row, 0, false);

            applyButton.TouchUpInside += (sender, e) =>
            {
                var color = FontColors.ElementAt((int)colorPicker.SelectedRowInComponent(0)).Value;
                if (colorSegmentedControl.SelectedSegment == 0)
                    label.BackgroundColor = color;
                else
                    label.TextColor = color;
                label.Font = Fonts.ElementAt((int)fontPicker.SelectedRowInComponent(0));
                label.TextSize = FontSizeValues.ElementAt((int)fontSizePicker.SelectedRowInComponent(0));
                label.CornerRadius = CornerRadiusValues.ElementAt((int)cornerRadiusPicker.SelectedRowInComponent(0));
                label.LetterSpacing = LetterSpacingValues.ElementAt((int)letterSpacingPicker.SelectedRowInComponent(0));
            };
            resetButton.TouchUpInside += (sender, e) => label.ResetCustomization();
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
                return Fonts.Count;
            }
        }

        public class FontPickerDelegate : UIPickerViewDelegate
        {
            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
            {
                return Fonts[(int)row].Name;
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
                return FontColors.Count;
            }
        }

        public class ColorPickerDelegate : UIPickerViewDelegate
        {
            public override NSAttributedString GetAttributedTitle(UIPickerView pickerView, nint row, nint component)
            {
                var pair = FontColors.ElementAt((int)row);
                var attributedString = new NSMutableAttributedString(pair.Key);
                attributedString.AddAttribute(UIStringAttributeKey.ForegroundColor, pair.Value, new NSRange(0, attributedString.Length));
                return attributedString;
            }
        }

        //fontsizes picker
        public class FonSizesSource : UIPickerViewDataSource
        {
            public override nint GetComponentCount(UIPickerView pickerView)
            {
                return 1;
            }

            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            {
                return FontSizeValues.Count;
            }
        }

        public class FontSizesPickerDelegate : UIPickerViewDelegate
        {
            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
            {
                return FontSizeValues[(int)row].ToString();
            }
        }

        //letterSpacing picker
        public class LetterSpacingSource : UIPickerViewDataSource
        {
            public override nint GetComponentCount(UIPickerView pickerView)
            {
                return 1;
            }

            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            {
                return LetterSpacingValues.Count;
            }
        }

        public class LetterSpacingPickerDelegate : UIPickerViewDelegate
        {
            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
            {
                return LetterSpacingValues[(int)row].ToString();
            }
        }

        //cornerradius picker
        public class CornerRadiusSource : UIPickerViewDataSource
        {
            public override nint GetComponentCount(UIPickerView pickerView)
            {
                return 1;
            }

            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            {
                return CornerRadiusValues.Count;
            }
        }

        public class CornerRadiusPickerDelegate: UIPickerViewDelegate
        {
            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
            {
                return CornerRadiusValues[(int)row].ToString();
            }
        }
    }
}