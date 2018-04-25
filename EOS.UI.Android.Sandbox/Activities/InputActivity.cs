using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Widget;
using EOS.UI.Android.Controls;
using EOS.UI.Android.Sandbox.Adapters;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using static EOS.UI.Android.Sandbox.Helpers.Constants;
using R = Android.Resource;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.Input)]
    public class InputActivity : BaseActivity
    {
        private Input _input;
        private Spinner _themeSpinner;
        private Spinner _textColorSpinner;
        private Spinner _fontSpinner;
        private Spinner _letterSpacingView;
        private Spinner _textSizeView;
        private Spinner _hintTextSize;
        private Spinner _HintTextColor;
        private Spinner _leftDrawableFocused;
        private Spinner _leftDrawableUnfocused;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.InputLayout);

            _input = FindViewById<Input>(Resource.Id.input);
            _input.UpdateAppearance();

            _themeSpinner = FindViewById<Spinner>(Resource.Id.spinnerTheme);
            _textColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerTextColor);
            _fontSpinner = FindViewById<Spinner>(Resource.Id.spinnerFont);
            _letterSpacingView = FindViewById<Spinner>(Resource.Id.spinnerLetterSpacing);
            _textSizeView = FindViewById<Spinner>(Resource.Id.spinnerTextSize);
            _hintTextSize = FindViewById<Spinner>(Resource.Id.spinnerHintTextSize);
            _HintTextColor = FindViewById<Spinner>(Resource.Id.spinnerHintTextColor);
            _leftDrawableFocused = FindViewById<Spinner>(Resource.Id.spinnerDrawableFocused);
            _leftDrawableUnfocused = FindViewById<Spinner>(Resource.Id.spinnerDrawableUnfocused);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);

            _themeSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeSpinner.ItemSelected += ThemeSpinner_ItemSelected;


            _textColorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _textColorSpinner.ItemSelected += TextColorSpinner_ItemSelected;

            _fontSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Fonts.FontsCollection.Select(item => item.Key).ToList());
            _fontSpinner.ItemSelected += FontSpinner_ItemSelected;

            _letterSpacingView.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.LetterSpacingCollection.Select(item => item.Key).ToList());
            _letterSpacingView.ItemSelected += LetterSpacingView_ItemSelected;

            _textSizeView.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            _textSizeView.ItemSelected += TextSizeView_ItemSelected;

            _hintTextSize.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            _hintTextSize.ItemSelected += HintTextSize_ItemSelected;

            _HintTextColor.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _HintTextColor.ItemSelected += HintTextColor_ItemSelected;

            _leftDrawableFocused.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, new List<Drawable>());
            _leftDrawableFocused.ItemSelected += LeftDrawableFocused_ItemSelected;

            _leftDrawableUnfocused.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, new List<Drawable>());
            _leftDrawableUnfocused.ItemSelected += LeftDrawableUnfocused_ItemSelected;

            SetCurrenTheme(_input.GetThemeProvider().GetCurrentTheme());

            resetButton.Click += delegate
            {
                ResetCustomValues();
            };
        }

        private void SetCurrenTheme(IEOSTheme iEOSTheme)
        {
            if(iEOSTheme is LightEOSTheme)
                _themeSpinner.SetSelection(1);
            if(iEOSTheme is DarkEOSTheme)
                _themeSpinner.SetSelection(2);
        }

        private void ThemeSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
            {
                _input.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(e.Position).Value);
                ResetCustomValues();
            }
        }

        private void TextSizeView_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _input.TextSize = Sizes.TextSizeCollection.ElementAt(e.Position).Value;
        }

        private void LetterSpacingView_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _input.LetterSpacing = Sizes.LetterSpacingCollection.ElementAt(e.Position).Value;
        }

        private void FontSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _input.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(e.Position).Value);
        }

        private void TextColorSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _input.TextColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
        }

        private void HintTextColor_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _input.HintTextColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
        }

        private void HintTextSize_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _input.HintTextSize = Sizes.TextSizeCollection.ElementAt(e.Position).Value;
        }

        private void LeftDrawableUnfocused_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
        }

        private void LeftDrawableFocused_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
        }

        private void ResetCustomValues()
        {
            _input.ResetCustomization();
            _textColorSpinner.SetSelection(0);
            _fontSpinner.SetSelection(0);
            _letterSpacingView.SetSelection(0);
            _textSizeView.SetSelection(0);
            _hintTextSize.SetSelection(0);
            _HintTextColor.SetSelection(0);
            _leftDrawableFocused.SetSelection(0);
            _leftDrawableUnfocused.SetSelection(0);
        }
    }
}