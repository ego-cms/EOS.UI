using System;
using System.Collections.Generic;
using System.Linq;
using EOS.UI.iOS.Sandbox.Helpers;
using EOS.UI.Shared.Themes.Helpers;
using Foundation;
using UIKit;

namespace EOS.UI.iOS.Sandbox.Controls.Pickers
{
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

    //disabled/enabled picker
    public class StatePickerSource : UIPickerViewDataSource
    {
        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return Constants.States.Count;
        }
    }

    public class StatePickerDelegate : UIPickerViewDelegate
    {
        public event EventHandler<KeyValuePair<string, bool>> DidSelected;

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return Constants.States.ElementAt((int)row).Key;
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            DidSelected?.Invoke(this, Constants.States.ElementAt((int)row));
        }
    }
}
