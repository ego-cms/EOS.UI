using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using EOS.UI.Droid.Controls;
using EOS.UI.Droid.Sandbox.Controls;
using EOS.UI.Shared.Sandbox.ControlConstants.Android;
using EOS.UI.Shared.Sandbox.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using EOS.UI.Shared.Themes.Themes;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Droid.Sandbox.Activities
{
    [Activity(Label = ControlNames.BadgeLabel, Theme = "@style/Sandbox.Main", ScreenOrientation = ScreenOrientation.Portrait)]
    public class BadgeLabelActivity : BaseActivity
    {
        private BadgeLabel _badge;
        private EOSSandboxDropDown _themeDropDown;
        private EOSSandboxDropDown _backgroundColorDropDown;
        private EOSSandboxDropDown _textColorDropDown;
        private EOSSandboxDropDown _fontDropDown;
        private EOSSandboxDropDown _letterSpacingDropDown;
        private EOSSandboxDropDown _textSizeDropDown;
        private EOSSandboxDropDown _cornerRadiusDropDown;
        private ScrollView _rootView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BadgeLabelLayout);

            _badge = FindViewById<BadgeLabel>(Resource.Id.badgeLabel);

            _rootView = FindViewById<ScrollView>(Resource.Id.rootView);
            _themeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.themeDropDown);
            _backgroundColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.backgroundDropDown);
            _textColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.textColorDropDown);
            _fontDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.fontDropDown);
            _letterSpacingDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.letterSpacingDropDown);
            _textSizeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.textSizeDropDown);
            _cornerRadiusDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.cornerRadiusDropDown);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);

            _themeDropDown.Name = Fields.Theme;
            _themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeDropDown.ItemSelected += ThemeItemSelected;

            _backgroundColorDropDown.Name = Fields.Background;
            _backgroundColorDropDown.SetupAdapter(BadgeLabelConstants.BackgroundColors.Select(item => item.Key).ToList());
            _backgroundColorDropDown.ItemSelected += BackgroundColorItemSelected;

            _textColorDropDown.Name = Fields.TextColor;
            _textColorDropDown.SetupAdapter(BadgeLabelConstants.FontColors.Select(item => item.Key).ToList());
            _textColorDropDown.ItemSelected += TextColorItemSelected;

            _fontDropDown.Name = Fields.Font;
            _fontDropDown.SetupAdapter(BadgeLabelConstants.BadgeLabelFonts.Select(item => item.Key).ToList());
            _fontDropDown.ItemSelected += FontItemSelected;

            _letterSpacingDropDown.Name = Fields.LetterSpacing;
            _letterSpacingDropDown.SetupAdapter(BadgeLabelConstants.LetterSpacings.Select(item => item.Key).ToList());
            _letterSpacingDropDown.ItemSelected += LetterSpacingItemSelected;

            _textSizeDropDown.Name = Fields.TextSize;
            _textSizeDropDown.SetupAdapter(BadgeLabelConstants.TextSizes.Select(item => item.Key).ToList());
            _textSizeDropDown.ItemSelected += TextSizeItemSelected;

            _cornerRadiusDropDown.Name = Fields.ConerRadius;
            _cornerRadiusDropDown.SetupAdapter(BadgeLabelConstants.CornerRadiusCollection.Select(item => item.Key).ToList());
            _cornerRadiusDropDown.ItemSelected += CornerRadiusItemSelected;

            SetCurrenTheme(_badge.GetThemeProvider().GetCurrentTheme());

            resetButton.Click += delegate
            {
                ResetCustomValues();
            };
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            _badge.BackgroundColor = new Color(savedInstanceState.GetInt("color"));
            _badge.CornerRadius = savedInstanceState.GetFloat("cornerRadius");
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("color", _badge.BackgroundColor.ToArgb());
            outState.PutFloat("cornerRadius", _badge.CornerRadius);
        }

        private void SetCurrenTheme(IEOSTheme iEOSTheme)
        {
            if (iEOSTheme is LightEOSTheme)
                _themeDropDown.SetSpinnerSelection(1);
            if (iEOSTheme is DarkEOSTheme)
                _themeDropDown.SetSpinnerSelection(2);
        }

        private void ThemeItemSelected(int position)
        {
            if (position > 0)
            {
                _badge.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                ResetCustomValues();
                UpdateAppearance();
            }
        }

        private void CornerRadiusItemSelected(int position)
        {
            _badge.CornerRadius = BadgeLabelConstants.CornerRadiusCollection.ElementAt(position).Value * Resources.DisplayMetrics.Density;
        }

        private void TextSizeItemSelected(int position)
        {
            _badge.TextSize = BadgeLabelConstants.TextSizes.ElementAt(position).Value;
        }

        private void LetterSpacingItemSelected(int position)
        {
            _badge.LetterSpacing = BadgeLabelConstants.LetterSpacings.ElementAt(position).Value;
        }

        private void FontItemSelected(int position)
        {
            _badge.Typeface = Typeface.CreateFromAsset(Assets, BadgeLabelConstants.BadgeLabelFonts.ElementAt(position).Value);
        }

        private void TextColorItemSelected(int position)
        {
            _badge.TextColor = BadgeLabelConstants.FontColors.ElementAt(position).Value;
        }

        private void BackgroundColorItemSelected(int position)
        {
            _badge.BackgroundColor = BadgeLabelConstants.BackgroundColors.ElementAt(position).Value;
        }

        private void ResetCustomValues()
        {
            _badge.ResetCustomization();
            _backgroundColorDropDown.SetSpinnerSelection(0);
            _textColorDropDown.SetSpinnerSelection(0);
            _fontDropDown.SetSpinnerSelection(0);
            _letterSpacingDropDown.SetSpinnerSelection(0);
            _textSizeDropDown.SetSpinnerSelection(0);
            _cornerRadiusDropDown.SetSpinnerSelection(0);
        }
    }
}
