using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

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
            return Colors.ColorsCollection.Count;
        }
    }

    public class ColorPickerDelegate : UIPickerViewDelegate
    {
        public event EventHandler<KeyValuePair<String, UIColor>> DidSelected;

        public override NSAttributedString GetAttributedTitle(UIPickerView pickerView, nint row, nint component)
        {
            var pair = Colors.ColorsCollection.ElementAt((int)row);
            var attributedString = new NSMutableAttributedString(pair.Key);
            attributedString.AddAttribute(UIStringAttributeKey.ForegroundColor, pair.Value, new NSRange(0, attributedString.Length));
            attributedString.AddAttribute(UIStringAttributeKey.Shadow, new NSShadow() { ShadowOffset = new CoreGraphics.CGSize(2, 2), ShadowBlurRadius = 3 }, new NSRange(0, attributedString.Length));
            return attributedString;
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            DidSelected?.Invoke(this, Colors.ColorsCollection.ElementAt((int)row));
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

    public class ValuePickerDelegate<TKey, TValue> : UIPickerViewDelegate
    {
        private IDictionary<TKey, TValue> _source;

        public event EventHandler<KeyValuePair<TKey,TValue>> DidSelected;

        public ValuePickerDelegate(IDictionary<TKey, TValue> source)
        {
            _source = source;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return _source.Keys.ElementAt((int)row).ToString();
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            DidSelected?.Invoke(this, _source.ElementAt((int)row));
        }
    }
}
