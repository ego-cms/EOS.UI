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

        public BadgeLabelView (IntPtr handle) : base (handle)
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

            foreach(var familyName in UIFont.FamilyNames)
            {
                foreach(var fontName in UIFont.FontNamesForFamilyName(familyName))
                {
                    var font = UIFont.FromName(fontName, label.TextSize);
                    if (font != null)
                        Fonts.Add(font);
                }
            }


            containerView.ConstrainLayout(() => label.Frame.GetCenterX() == containerView.Frame.GetCenterX() &&
                                          label.Frame.GetCenterY() == containerView.Frame.GetCenterY(), label);

            fontSizeField.Text = label.TextSize.ToString();
            fontSizeField.PrimaryActionTriggered += (sender, e) => fontSizeField.ResignFirstResponder();
            cornerRadiusField.Text = label.CornerRadius.ToString();
            cornerRadiusField.PrimaryActionTriggered += (sender, e) => cornerRadiusField.ResignFirstResponder();
            letterSpacingField.Text = label.LetterSpacing.ToString();
            letterSpacingField.PrimaryActionTriggered += (sender, e) => letterSpacingField.ResignFirstResponder();

            fontPicker.DataSource = new FontPickerSource();
            fontPicker.Delegate = new FontPickerDelegate();

            colorPicker.DataSource = new ColorPickerSource();
            colorPicker.Delegate = new ColorPickerDelegate();

            var row = FontColors.Values.ToList().IndexOf(label.BackgroundColor);
            colorPicker.Select(row, 0, false);

            applyButton.TouchUpInside += (sender, e) =>
            {
                var color = FontColors.ElementAt((int)colorPicker.SelectedRowInComponent(0)).Value;
                if (colorSegmentedControl.SelectedSegment == 0)
                    label.BackgroundColor = color;
                else
                    label.TextColor = color;

                label.Font = Fonts.ElementAt((int)fontPicker.SelectedRowInComponent(0));
                if (letterSpacingField.Text != string.Empty)
                    label.LetterSpacing = Convert.ToInt32(letterSpacingField.Text);
                if(cornerRadiusField.Text != string.Empty)
                    label.CornerRadius = Convert.ToInt32(cornerRadiusField.Text);
                if(fontSizeField.Text != String.Empty)
                    label.TextSize = Convert.ToInt32(fontSizeField.Text);
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

        public class FontPickerDelegate: UIPickerViewDelegate
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

        public class ColorPickerDelegate: UIPickerViewDelegate
        {
			public override NSAttributedString GetAttributedTitle(UIPickerView pickerView, nint row, nint component)
			{
                var pair = FontColors.ElementAt((int)row);
                var attributedString = new NSMutableAttributedString(pair.Key);
                attributedString.AddAttribute(UIStringAttributeKey.ForegroundColor, pair.Value, new NSRange(0, attributedString.Length));
                return attributedString;
			}
		}
	}
}