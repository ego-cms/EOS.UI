using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Sandbox.Controls.Pickers;
using EOS.UI.iOS.Sandbox.Helpers;
using Foundation;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    [DesignTimeVisible(true)]
    public partial class CustomDropDown : UIView
    {
        public bool Enabled
        {
            get => textField.Enabled;
            set => textField.Enabled = value;
        }

        [Export("initWithCoder:")]
        public CustomDropDown(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        private void Initialize()
        {
            NSBundle.MainBundle.LoadNib("CustomDropDown", this, null);
            AddSubview(rootView);
            rootView.Frame = Bounds;
            BackgroundColor = UIColor.Clear;
        }

        public void InitSource<T>(List<T> source, Action<T> action, string title, CGRect rectangle)
        {
            label.Text = title;
            var picker = new UIPickerView(rectangle)
            {
                ShowSelectionIndicator = true,
                DataSource = new ValuePickerSource<T>(source)
            };
            var pickerDelegate = new ValuePickerDelegate<T>(source);
            pickerDelegate.DidSelected += (object sender, T e) =>
            {
                action?.Invoke(e);
                textField.Text = e.ToString();
            };
            textField.EditingDidBegin += (sender, e) =>
            {
                var item = source.ElementAt((int)picker.SelectedRowInComponent(0));
                action?.Invoke(item);
                textField.Text = item.ToString();
            };
            picker.Delegate = pickerDelegate;
            textField.InputView = picker;
        }

        public void InitSource<TKey, TValue>(Dictionary<TKey, TValue> source, Action<TValue> action, string title, CGRect rectangle)
        {
            label.Text = title;
            var picker = new UIPickerView(rectangle)
            {
                ShowSelectionIndicator = true,
                DataSource = new DictionaryPickerSource<TKey, TValue>(source)
            };
            var pickerDelegate = new DictionaryPickerDelegate<TKey, TValue>(source);
            pickerDelegate.DidSelected += (object sender, KeyValuePair<TKey, TValue> e) =>
            {
                action?.Invoke(e.Value);
                textField.Text = e.Key.ToString();
            };
            textField.EditingDidBegin += (sender, e) =>
            {
                var item = source.ElementAt((int)picker.SelectedRowInComponent(0));
                action?.Invoke(item.Value);
                textField.Text = item.Key.ToString();
            };
            picker.Delegate = pickerDelegate;
            textField.InputView = picker;
        }

        public void InitSource(Action<UIColor> action, string title, CGRect rectangle)
        {
            label.Text = title;
            var picker = new UIPickerView(rectangle)
            {
                ShowSelectionIndicator = true,
                DataSource = new ColorPickerSource()
            };
            var pickerDelegate = new ColorPickerDelegate();
            pickerDelegate.DidSelected += (object sender, KeyValuePair<string, UIColor> e) =>
            {
                action?.Invoke(e.Value);
                textField.Text = e.Key.ToString();
            };
            textField.EditingDidBegin += (sender, e) =>
            {
                var item = Constants.Colors.ElementAt((int)picker.SelectedRowInComponent(0));
                action?.Invoke(item.Value);
                textField.Text = item.Key.ToString();
            };
            picker.Delegate = pickerDelegate;
            textField.InputView = picker;
        }

        public void ResetValue()
        {
            textField.Text = string.Empty;
        }

        public void CloseInputControl()
        {
            textField.ResignFirstResponder();
        }

        public void SetTextFieldText(string text)
        {
            textField.Text = text;
        }
    }
}