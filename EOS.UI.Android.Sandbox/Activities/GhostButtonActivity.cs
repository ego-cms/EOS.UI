using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using EOS.UI.Android.Controls;
using EOS.UI.Android.Sandbox.Adapters;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using static EOS.UI.Android.Sandbox.Helpers.Constants;
using R = Android.Resource;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.GhostButton)]
    public class GhostButtonActivity : BaseActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GhostButtonLayout);

            var ghostButton = FindViewById<GhostButton>(Resource.Id.ghostButton);
            var themeSpinner = FindViewById<Spinner>(Resource.Id.spinnerTheme);
            var disabledColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerDisabledTextColor);
            var pressedColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerPressedTextColor);
            var enabledColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerEnabledTextColor);
            var fontSpinner = FindViewById<Spinner>(Resource.Id.spinnerFont);
            var letterSpacingSpinner = FindViewById<Spinner>(Resource.Id.spinnerLetterSpacing);
            var textSizeSpinner = FindViewById<Spinner>(Resource.Id.spinnerTextSize);
            var stateSwitch = FindViewById<Switch>(Resource.Id.stateSwitch);
            ghostButton.ResetCustomization();

            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            var spinners = new List<Spinner>()
            {
                themeSpinner,
                disabledColorSpinner,
                pressedColorSpinner,
                enabledColorSpinner,
                fontSpinner,
                letterSpacingSpinner,
                textSizeSpinner
            };
          
            themeSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            themeSpinner.ItemSelected += (sender, e) =>
            {
                if (e.Position > 0)
                {
                    ghostButton.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(e.Position).Value);
                    ghostButton.ResetCustomization();
                    spinners.Except(new[] { themeSpinner }).ToList().ForEach(s => s.SetSelection(0));
                }
            };

            var theme = ghostButton.GetThemeProvider().GetCurrentTheme();
            if (theme is LightEOSTheme)
                themeSpinner.SetSelection(1);
            if (theme is DarkEOSTheme)
                themeSpinner.SetSelection(2);


            fontSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Fonts.FontsCollection.Select(item => item.Key).ToList());
            fontSpinner.ItemSelected += (sender, e) =>
            {
                if (e.Position > 0)
                {
                    ghostButton.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(e.Position).Value);
                }
            };

            letterSpacingSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.LetterSpacingCollection.Select(item => item.Key).ToList());
            letterSpacingSpinner.ItemSelected += (sender, e) =>
            {
                if (e.Position > 0)
                    ghostButton.LetterSpacing = Sizes.LetterSpacingCollection.ElementAt(e.Position).Value;
            };

            enabledColorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            enabledColorSpinner.ItemSelected += (sender, e) =>
            {
                if (e.Position > 0)
                    ghostButton.EnabledTextColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
            };

            disabledColorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            disabledColorSpinner.ItemSelected += (sender, e) =>
            {
                if (e.Position > 0)
                    ghostButton.DisabledTextColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
            };

            pressedColorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            pressedColorSpinner.ItemSelected += (sender, e) =>
            {
                if (e.Position > 0)
                    ghostButton.PressedStateTextColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
            };

            textSizeSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            textSizeSpinner.ItemSelected += (sender, e) => 
            {
                if (e.Position > 0)
                    ghostButton.TextSize = Sizes.TextSizeCollection.ElementAt(e.Position).Value;
            };;

            stateSwitch.CheckedChange += (sender, e) => 
            {
                ghostButton.Enabled = stateSwitch.Checked;
            };

            resetButton.Click += delegate
            {
                spinners.Except(new[] { themeSpinner }).ToList().ForEach(s => s.SetSelection(0));
                ghostButton.ResetCustomization();
            };
        }
    }
}