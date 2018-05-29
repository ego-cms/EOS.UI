using System.Linq;
using System.Globalization;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using EOS.UI.Android.Controls;
using static EOS.UI.Android.Sandbox.Helpers.Constants;
using R = Android.Resource;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using EOS.UI.Android.Sandbox.Adapters;
using EOS.UI.Shared.Themes.Themes;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.SimpleLabel)]
    public class SimpleLabelActivity : BaseActivity
    {
        private SimpleLabel _simpleLabel;
        private Spinner _themeSpinner;
        private Spinner _textColorSpinner;
        private Spinner _fontSpinner;
        private Spinner _letterSpacingView;
        private Spinner _textSizeView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SimpleLabelLayout);

            _simpleLabel = FindViewById<SimpleLabel>(Resource.Id.simpleLabel);

            _themeSpinner = FindViewById<Spinner>(Resource.Id.spinnerTheme);
            _textColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerTextColor);
            _fontSpinner = FindViewById<Spinner>(Resource.Id.spinnerFont);
            _letterSpacingView = FindViewById<Spinner>(Resource.Id.spinnerLetterSpacing);
            _textSizeView = FindViewById<Spinner>(Resource.Id.spinnerTextSize);
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

            SetCurrenTheme(_simpleLabel.GetThemeProvider().GetCurrentTheme());

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
            _simpleLabel.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(e.Position).Value);
            ResetCustomValues();
        }

        private void TextSizeView_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _simpleLabel.TextSize = Sizes.TextSizeCollection.ElementAt(e.Position).Value;
        }

        private void LetterSpacingView_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _simpleLabel.LetterSpacing = Sizes.LetterSpacingCollection.ElementAt(e.Position).Value;
        }

        private void FontSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _simpleLabel.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(e.Position).Value);
        }

        private void TextColorSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _simpleLabel.TextColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
        }

        private void ResetCustomValues()
        {
            _simpleLabel.ResetCustomization();
            _textColorSpinner.SetSelection(0);
            _fontSpinner.SetSelection(0);
            _letterSpacingView.SetSelection(0);
            _textSizeView.SetSelection(0);
        }

    }
}