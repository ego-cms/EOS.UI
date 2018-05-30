using System;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using EOS.UI.Android.Controls;
using EOS.UI.Android.Sandbox.Adapters;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using static Android.Widget.CompoundButton;
using static EOS.UI.Android.Sandbox.Helpers.Constants;
using R = Android.Resource;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.SimpleButton)]
    public class SimpleButtonActivity : BaseActivity, IOnCheckedChangeListener
    {
        private SimpleButton _simpleButton;
        private Spinner _themeSpinner;
        private Spinner _fontSpinner;
        private Spinner _letterSpacingSpinner;
        private Spinner _textSizeSpinner;
        private Spinner _textColorEnabledSpinner;
        private Spinner _textColorDisabledSpinner;
        private Spinner _textColorPressedSpinner;
        private Spinner _backgroundColorEnabledSpinner;
        private Spinner _backgroundColorDisabledSpinner;
        private Spinner _backgroundColorPressedSpinner;
        private Spinner _cornerRadiusSpinner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SimpleButtonLayout);

            _simpleButton = FindViewById<SimpleButton>(Resource.Id.simpleButton);
            _simpleButton.UpdateAppearance();

            _themeSpinner = FindViewById<Spinner>(Resource.Id.spinnerTheme);
            _fontSpinner = FindViewById<Spinner>(Resource.Id.spinnerFont);
            _letterSpacingSpinner = FindViewById<Spinner>(Resource.Id.spinnerLetterSpacing);
            _textSizeSpinner = FindViewById<Spinner>(Resource.Id.spinnerTextSize);
            _textColorEnabledSpinner = FindViewById<Spinner>(Resource.Id.spinnerTextColorEnabled);
            _textColorDisabledSpinner = FindViewById<Spinner>(Resource.Id.spinnerTextColorDisabled);
            _textColorPressedSpinner = FindViewById<Spinner>(Resource.Id.spinnerTextColorPressed);
            _backgroundColorEnabledSpinner = FindViewById<Spinner>(Resource.Id.spinnerBackgroundColorEnabled);
            _backgroundColorDisabledSpinner = FindViewById<Spinner>(Resource.Id.spinnerBackgroundColorDisabled);
            _backgroundColorPressedSpinner = FindViewById<Spinner>(Resource.Id.spinnerBackgroundColorPressed);
            _cornerRadiusSpinner = FindViewById<Spinner>(Resource.Id.spinnerCornerRadius);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            var disableSwitch = FindViewById<Switch>(Resource.Id.switchDisabled);

            _themeSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeSpinner.ItemSelected += ThemeSpinner_ItemSelected;

            _fontSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Fonts.FontsCollection.Select(item => item.Key).ToList());
            _fontSpinner.ItemSelected += FontSpinner_ItemSelected;

            _letterSpacingSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.LetterSpacingCollection.Select(item => item.Key).ToList());
            _letterSpacingSpinner.ItemSelected += LetterSpacingView_ItemSelected;

            _textSizeSpinner.Adapter= new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            _textSizeSpinner.ItemSelected += TextSizeSpinner_ItemSelected;

            _textColorEnabledSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _textColorEnabledSpinner.ItemSelected += TextColorEnabledSpinner_ItemSelected;

            _textColorDisabledSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _textColorDisabledSpinner.ItemSelected += TextColorDisabledSpinner_ItemSelected;

            _textColorPressedSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _textColorPressedSpinner.ItemSelected += TextColorPressedSpinner_ItemSelected;

            _backgroundColorEnabledSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _backgroundColorEnabledSpinner.ItemSelected += BackgroundColorEnabledSpinner_ItemSelected;

            _backgroundColorDisabledSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _backgroundColorDisabledSpinner.ItemSelected += BackgroundColorDisabledSpinner_ItemSelected;

            _backgroundColorPressedSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _backgroundColorPressedSpinner.ItemSelected += BackgroundColorPressedSpinner_ItemSelected;

            _cornerRadiusSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.CornerRadusCollection.Select(item => item.Key).ToList());
            _cornerRadiusSpinner.ItemSelected += CornerRadiurSpinner_ItemSelected;

            resetButton.Click += delegate
            {
                ResetCustomValues();
            };

            disableSwitch.SetOnCheckedChangeListener(this);

            SetCurrenTheme(_simpleButton.GetThemeProvider().GetCurrentTheme());

            _simpleButton.Click += delegate
            {
                _simpleButton.StartAnimation();
            };
        }

        private void ThemeSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                _simpleButton.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(e.Position).Value);
                ResetCustomValues();
            }
        }

        private void FontSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _simpleButton.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(e.Position).Value);
        }

        private void LetterSpacingView_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _simpleButton.LetterSpacing = Sizes.LetterSpacingCollection.ElementAt(e.Position).Value;
        }

        private void TextSizeSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _simpleButton.TextSize = Sizes.TextSizeCollection.ElementAt(e.Position).Value;
        }

        private void TextColorEnabledSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _simpleButton.TextColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
        }

        private void TextColorDisabledSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _simpleButton.DisabledTextColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
        }

        private void TextColorPressedSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _simpleButton.PressedTextColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
        }

        private void BackgroundColorEnabledSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _simpleButton.BackgroundColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
        }

        private void BackgroundColorDisabledSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _simpleButton.DisabledBackgroundColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
        }

        private void BackgroundColorPressedSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _simpleButton.PressedBackgroundColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
        }

        private void CornerRadiurSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _simpleButton.CornerRadius = Sizes.CornerRadusCollection.ElementAt(e.Position).Value;
        }

        private void SetCurrenTheme(IEOSTheme iEOSTheme)
        {
            if(iEOSTheme is LightEOSTheme)
                _themeSpinner.SetSelection(1);
            if(iEOSTheme is DarkEOSTheme)
                _themeSpinner.SetSelection(2);
        }

        private void ResetCustomValues()
        {
            _simpleButton.ResetCustomization();
            _fontSpinner.SetSelection(0);
            _letterSpacingSpinner.SetSelection(0);
            _textSizeSpinner.SetSelection(0);
            _textColorEnabledSpinner.SetSelection(0);
            _textColorDisabledSpinner.SetSelection(0);
            _textColorPressedSpinner.SetSelection(0);
            _backgroundColorEnabledSpinner.SetSelection(0);
            _backgroundColorDisabledSpinner.SetSelection(0);
            _backgroundColorPressedSpinner.SetSelection(0);
            _cornerRadiusSpinner.SetSelection(0);
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            _simpleButton.Enabled = isChecked;
        }
    }
}