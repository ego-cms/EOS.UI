using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using EOS.UI.Android.Controls;
using static EOS.UI.Android.Sandbox.Helpers.Constants;
using R = Android.Resource;
using UIFrameworks.Shared.Themes.Helpers;
using EOS.UI.Android.Sandbox.Adapters;
using UIFrameworks.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.BadgeLabel) ]
    public class BadgeLabelActivity : BaseActivity
    {
        private BadgeLabel _badge;
        private Spinner _themeSpinner;
        private Spinner _backgroundColorSpinner;
        private Spinner _textColorSpinner;
        private Spinner _fontSpinner;
        private Spinner _letterSpacingView;
        private Spinner _textSizeView;
        private Spinner _cornerRadiusView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BadgeLabelLayout);

            _badge = FindViewById<BadgeLabel>(Resource.Id.badgeLabel);
            _badge.UpdateAppearance();

            _themeSpinner = FindViewById<Spinner>(Resource.Id.spinnerTheme);
            _backgroundColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerBackgroundColor);
            _textColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerTextColor);
            _fontSpinner = FindViewById<Spinner>(Resource.Id.spinnerFont);
            _letterSpacingView = FindViewById<Spinner>(Resource.Id.spinnerLetterSpacing);
            _textSizeView = FindViewById<Spinner>(Resource.Id.spinnerTextSize);
            _cornerRadiusView = FindViewById<Spinner>(Resource.Id.spinnerCornerRadius);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);


            _themeSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeSpinner.ItemSelected += ThemeSpinner_ItemSelected;

            _backgroundColorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _backgroundColorSpinner.ItemSelected += BackgroundColorSpinner_ItemSelected;

            _textColorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            _textColorSpinner.ItemSelected += TextColorSpinner_ItemSelected;

            _fontSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Fonts.FontsCollection.Select(item => item.Key).ToList());
            _fontSpinner.ItemSelected += FontSpinner_ItemSelected;

            _letterSpacingView.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.LetterSpacingCollection.Select(item => item.Key).ToList());
            _letterSpacingView.ItemSelected += LetterSpacingView_ItemSelected;

            _textSizeView.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            _textSizeView.ItemSelected += TextSizeView_ItemSelected;

            _cornerRadiusView.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.CornerRadusCollection.Select(item => item.Key).ToList());
            _cornerRadiusView.ItemSelected += CornerRadiusView_ItemSelected;

            SetCurrenTheme(_badge.GetThemeProvider().GetCurrentTheme());

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
                _badge.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(e.Position).Value);
                ResetCustomValues();
            }
        }

        private void CornerRadiusView_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _badge.CornerRadius = Sizes.CornerRadusCollection.ElementAt(e.Position).Value;
        }

        private void TextSizeView_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _badge.TextSize = Sizes.TextSizeCollection.ElementAt(e.Position).Value;
        }

        private void LetterSpacingView_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _badge.LetterSpacing = Sizes.LetterSpacingCollection.ElementAt(e.Position).Value;
        }

        private void FontSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _badge.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(e.Position).Value);
        }

        private void TextColorSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _badge.TextColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
        }

        private void BackgroundColorSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position > 0)
                _badge.BackgroundColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
        }

        private void ResetCustomValues()
        {
            _badge.ResetCustomization();
            _backgroundColorSpinner.SetSelection(0);
            _textColorSpinner.SetSelection(0);
            _fontSpinner.SetSelection(0);
            _letterSpacingView.SetSelection(0);
            _textSizeView.SetSelection(0);
            _cornerRadiusView.SetSelection(0);
        }
    }
}