using System;
using System.Collections.Generic;
using System.Linq;
using EOS.UI.iOS.Sandbox.Helpers;
using EOS.UI.Shared.Themes.Helpers;
using Foundation;
using UIKit;

namespace EOS.UI.iOS.Sandbox.Controls.Pickers
{
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

    //generic picker
    public class ValuePickerSource<T> : UIPickerViewDataSource
    {
        private IEnumerable<T> _source;

        public ValuePickerSource(IEnumerable<T> source)
        {
            _source = source;
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return _source.Count();
        }
    }

    public class ValuePickerDelegate<T> : UIPickerViewDelegate
    {
        private IEnumerable<T> _source;

        public event EventHandler<T> DidSelected;

        public ValuePickerDelegate(IEnumerable<T> source)
        {
            _source = source;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return _source.ElementAt((int)row).ToString();
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            DidSelected?.Invoke(this, (T)_source.ElementAt((int)row));
        }
    }

    public class DictionaryPickerSource<T1, T2> : UIPickerViewDataSource
    {
        private IDictionary<T1, T2> _source;

        public DictionaryPickerSource(Dictionary<T1, T2> source)
        {
            _source = source;
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return _source.Count();
        }
    }

    public class DictionaryPickerDelegate<T1, T2> : UIPickerViewDelegate
    {
        private Dictionary<T1, T2> _source;

        public event EventHandler<KeyValuePair<T1, T2>> DidSelected;

        public DictionaryPickerDelegate(Dictionary<T1, T2> source)
        {
            _source = source;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return _source.ElementAt((int)row).Key.ToString();
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            DidSelected?.Invoke(this, _source.ElementAt((int)row));
        }
    }
}
