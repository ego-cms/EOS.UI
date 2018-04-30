using System;
using EOS.UI.iOS.Sandbox.Helpers;
using UIKit;

namespace EOS.UI.iOS.Sandbox.Pickers
{
    public class IconPickerSource: UIPickerViewDataSource
    {
        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return Constants.Icons.Count;
        }
    }
}
