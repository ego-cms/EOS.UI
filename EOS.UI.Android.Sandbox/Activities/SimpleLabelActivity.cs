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
using UIFrameworks.Android.Themes;
using EOS.UI.Android.Sandbox.Adapters;
using EOS.UI.Android.Sandbox.Controls;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.SimpleLabel)]
    public class SimpleLabelActivity : BaseActivity
    {
        private SimpleLabel _simpleLabel;
        private SendboxDropDown _themeDropDown;
        private SendboxDropDown _textColorDropDown;
        private SendboxDropDown _fontDropDown;
        private SendboxDropDown _letterSpacingDropDown;
        private SendboxDropDown _textSizeDropDown;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SimpleLabelLayout);

            _simpleLabel = FindViewById<SimpleLabel>(Resource.Id.simpleLabel);

            _themeDropDown = FindViewById<SendboxDropDown>(Resource.Id.themeDropDown);
            _textColorDropDown = FindViewById<SendboxDropDown>(Resource.Id.textColorDropDown);
            _fontDropDown = FindViewById<SendboxDropDown>(Resource.Id.fontDropDown);
            _letterSpacingDropDown = FindViewById<SendboxDropDown>(Resource.Id.letterSpacingDropDown);
            _textSizeDropDown = FindViewById<SendboxDropDown>(Resource.Id.textSizeDropDown);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);

            _themeDropDown.Name = Fields.Theme;
            _themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeDropDown.ItemSelected += ThemeItemSelected;

            _textColorDropDown.Name = Fields.TextColor;
            _textColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _textColorDropDown.ItemSelected += TextColorItemSelected;

            _fontDropDown.Name = Fields.Font;
            _fontDropDown.SetupAdapter(Fonts.FontsCollection.Select(item => item.Key).ToList());
            _fontDropDown.ItemSelected += FontItemSelected;

            _letterSpacingDropDown.Name = Fields.LetterSpacing;
            _letterSpacingDropDown.SetupAdapter(Sizes.LetterSpacingCollection.Select(item => item.Key).ToList());
            _letterSpacingDropDown.ItemSelected += LetterSpacingItemSelected;

            _textSizeDropDown.Name = Fields.TextSize;
            _textSizeDropDown.SetupAdapter(Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            _textSizeDropDown.ItemSelected += TextSizeItemSelected;

            SetCurrenTheme(_simpleLabel.GetThemeProvider().GetCurrentTheme());

            resetButton.Click += delegate
            {
                ResetCustomValues();
            };
        }

        private void SetCurrenTheme(IEOSTheme iEOSTheme)
        {
            if(iEOSTheme is LightEOSTheme)
                _themeDropDown.SetSpinnerSelection(1);
            if(iEOSTheme is DarkEOSTheme)
                _themeDropDown.SetSpinnerSelection(2);
        }

        private void ThemeItemSelected(int position)
        {
            _simpleLabel.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
            ResetCustomValues();
        }

        private void TextSizeItemSelected(int position)
        {
            if(position > 0)
                _simpleLabel.TextSize = Sizes.TextSizeCollection.ElementAt(position).Value;
        }

        private void LetterSpacingItemSelected(int position)
        {
            if(position > 0)
                _simpleLabel.LetterSpacing = Sizes.LetterSpacingCollection.ElementAt(position).Value;
        }

        private void FontItemSelected(int position)
        {
            if(position > 0)
                _simpleLabel.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(position).Value);
        }

        private void TextColorItemSelected(int position)
        {
            if(position > 0)
                _simpleLabel.TextColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void ResetCustomValues()
        {
            _simpleLabel.ResetCustomization();
            _textColorDropDown.SetSpinnerSelection(0);
            _fontDropDown.SetSpinnerSelection(0);
            _letterSpacingDropDown.SetSpinnerSelection(0);
            _textSizeDropDown.SetSpinnerSelection(0);
        }
    }
}