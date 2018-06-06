using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoreGraphics;
using EOS.UI.iOS.Sandbox.Controls.Pickers;
using EOS.UI.iOS.Sandbox.Helpers;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    [DesignTimeVisible(true)]
    public partial class EOSSandboxDropDown : UIView, IEOSThemeControl
    {
        public bool Enabled
        {
            get => textField.Enabled;
            set => textField.Enabled = value;
        }

        [Export("initWithCoder:")]
        public EOSSandboxDropDown(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        private void Initialize()
        {
            NSBundle.MainBundle.LoadNib("EOSSandboxDropDown", this, null);
            AddSubview(rootView);
            rootView.Frame = Bounds;
            BackgroundColor = UIColor.Clear;
        }

        public void InitSource<T>(List<T> source, Action<T> action, string title, CGRect rectangle)
        {
            InitSource(source.ToDictionary((arg) => arg), action, title, rectangle);
        }

        public void InitSource<TKey, TValue>(Dictionary<TKey, TValue> source, Action<TValue> action, string title, CGRect rectangle)
        {
            label.Text = title;
            var picker = new UIPickerView(rectangle)
            {
                ShowSelectionIndicator = true,
                DataSource = new ValuePickerSource<KeyValuePair<TKey, TValue>>(source)
            };
            var pickerDelegate = new ValuePickerDelegate<TKey, TValue>(source);
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
            UpdateAppearance();
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
            UpdateAppearance();
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

        #region IEOSThemeControl implementation

        public bool IsEOSCustomizationIgnored { get; set; }

        public IEOSStyle GetCurrentEOSStyle()
        {
            return null;
        }

        public IEOSThemeProvider GetThemeProvider()
        {
            return EOSThemeProvider.Instance;
        }

        public void ResetCustomization()
        {
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {
        }

        public void UpdateAppearance()
        {
            if(!IsEOSCustomizationIgnored)
            {
                label.TextColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor1);
                textField.TextColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3);
                textField.BackgroundColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6);
                textField.Layer.BorderColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor3).CGColor;

                if(textField.InputView != null)
                {
                    (textField.InputView as UIPickerView).BackgroundColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6);
                    (textField.InputView as UIPickerView).Layer.BorderColor = GetThemeProvider().GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor5).CGColor;
                }
            }
        }

        #endregion
    }
}