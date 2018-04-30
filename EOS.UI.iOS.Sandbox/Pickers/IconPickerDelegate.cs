using System;
using System.Collections.Generic;
using System.Linq;
using EOS.UI.iOS.Sandbox.Helpers;
using UIKit;

namespace EOS.UI.iOS.Sandbox.Pickers
{
    public class IconPickerDelegate: UIPickerViewDelegate
    {
        public event EventHandler<KeyValuePair<String, UIImage>> DidSelected;

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return Constants.Icons.ElementAt((int)row).Key;
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            DidSelected?.Invoke(this, 
                new KeyValuePair<string, UIImage>(
                    Constants.Icons.ElementAt((int)row).Key, 
                    UIImage.FromBundle(Constants.Icons.ElementAt((int)row).Value)));
        }

    }
}
