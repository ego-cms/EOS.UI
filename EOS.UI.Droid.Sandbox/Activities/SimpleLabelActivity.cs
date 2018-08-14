using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using EOS.UI.Droid.Controls;
using EOS.UI.Droid.Sandbox.Controls;
using EOS.UI.Shared.Sandbox.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Themes;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Droid.Sandbox.Activities
{
    [Activity(Label = ControlNames.SimpleLabel, Theme = "@style/Sandbox.Main", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SimpleLabelActivity : BaseActivity
    {
        private SimpleLabel _simpleLabel;
        private EOSSandboxDropDown _themeDropDown;
        private EOSSandboxDropDown _textColorDropDown;
        private EOSSandboxDropDown _fontDropDown;
        private EOSSandboxDropDown _letterSpacingDropDown;
        private EOSSandboxDropDown _textSizeDropDown;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SimpleLabelLayout);

            _simpleLabel = FindViewById<SimpleLabel>(Resource.Id.simpleLabel);

            _themeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.themeDropDown);
            _textColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.textColorDropDown);
            _fontDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.fontDropDown);
            _letterSpacingDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.letterSpacingDropDown);
            _textSizeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.textSizeDropDown);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);

            _themeDropDown.Name = Fields.Theme;
            _themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeDropDown.ItemSelected += ThemeItemSelected;

            _textColorDropDown.Name = Fields.TextColor;
            _textColorDropDown.SetupAdapter(Colors.FontColorsCollection.Select(item => item.Key).ToList());
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
            if(position > 0)
            {
                _simpleLabel.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                ResetCustomValues();
                UpdateAppearance();
            }
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
                _simpleLabel.TextColor = Colors.FontColorsCollection.ElementAt(position).Value;
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
