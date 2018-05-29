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
using EOS.UI.Android.Sandbox.Controls;
using System.Collections.Generic;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.BadgeLabel) ]
    public class BadgeLabelActivity : BaseActivity
    {
        private BadgeLabel _badge;
        private DropDown _themeDropDown;
        private DropDown _backgroundColorDropDown;
        private DropDown _textColorDropDown;
        private DropDown _fontDropDown;
        private DropDown _letterSpacingDropDown;
        private DropDown _textSizeDropDown;
        private DropDown _cornerRadiusDropDown;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BadgeLabelLayout);

            _badge = FindViewById<BadgeLabel>(Resource.Id.badgeLabel);
            _badge.UpdateAppearance();

            _themeDropDown = FindViewById<DropDown>(Resource.Id.themeDropDown);
            _backgroundColorDropDown = FindViewById<DropDown>(Resource.Id.backgroundDropDown);
            _textColorDropDown = FindViewById<DropDown>(Resource.Id.textColorDropDown);
            _fontDropDown = FindViewById<DropDown>(Resource.Id.fontDropDown);
            _letterSpacingDropDown = FindViewById<DropDown>(Resource.Id.letterSpacingDropDown);
            _textSizeDropDown = FindViewById<DropDown>(Resource.Id.textSizeDropDown);
            _cornerRadiusDropDown = FindViewById<DropDown>(Resource.Id.cornerRadiusDropDown);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);

            _themeDropDown.Name = Fields.Theme;
            _themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            _themeDropDown.ItemSelected += ThemeItemSelected;

            _backgroundColorDropDown.Name = Fields.Background;
            _backgroundColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            _backgroundColorDropDown.ItemSelected += BackgroundColorItemSelected;

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

            _cornerRadiusDropDown.Name = Fields.ConerRadius;
            _cornerRadiusDropDown.SetupAdapter(Sizes.CornerRadusCollection.Select(item => item.Key).ToList());
            _cornerRadiusDropDown.ItemSelected += CornerRadiusItemSelected;

            SetCurrenTheme(_badge.GetThemeProvider().GetCurrentTheme());

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
                _badge.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                ResetCustomValues();
            }
        }

        private void CornerRadiusItemSelected(int position)
        {
            if(position > 0)
                _badge.CornerRadius = Sizes.CornerRadusCollection.ElementAt(position).Value;
        }

        private void TextSizeItemSelected(int position)
        {
            if(position > 0)
                _badge.TextSize = Sizes.TextSizeCollection.ElementAt(position).Value;
        }

        private void LetterSpacingItemSelected(int position)
        {
            if(position > 0)
                _badge.LetterSpacing = Sizes.LetterSpacingCollection.ElementAt(position).Value;
        }

        private void FontItemSelected(int position)
        {
            if(position > 0)
                _badge.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(position).Value);
        }

        private void TextColorItemSelected(int position)
        {
            if(position > 0)
                _badge.TextColor = Colors.ColorsCollection.ElementAt(position).Value;
        }

        private void BackgroundColorItemSelected(int position)
        {
            if(position > 0)
                _badge.BackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
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