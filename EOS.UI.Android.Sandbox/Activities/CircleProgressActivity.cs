using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using EOS.UI.Android.Controls;
using EOS.UI.Android.Sandbox.Adapters;
using EOS.UI.Android.Sandbox.Helpers;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using static EOS.UI.Android.Sandbox.Helpers.Constants;
using R = Android.Resource;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.CircleProgress)]
    public class CircleProgressActivity : BaseActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CircleProgressLayout);
            var circleProgressFragment = (CircleProgress) FragmentManager.FindFragmentById(Resource.Id.circleProgress);
            var themeSpinner = FindViewById<Spinner>(Resource.Id.spinnerTheme);
            var colorSpinner = FindViewById<Spinner>(Resource.Id.spinnerColor);
            var alternativeColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerAlternativeColor);
            var fontSpinner = FindViewById<Spinner>(Resource.Id.spinnerFont);
            var textSizeSpinner = FindViewById<Spinner>(Resource.Id.spinnerTextSize);
            var showProgressSwitch = FindViewById<Switch>(Resource.Id.showProgressSwitch);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            var spinners = new List<Spinner>()
            {
                themeSpinner,
                colorSpinner,
                fontSpinner,
                textSizeSpinner
            };

            int percents = 0;
            var timer = new PlatformTimer();
            timer.Setup(TimeSpan.FromMilliseconds(100), () => 
            {
                percents += 1;
                circleProgressFragment.Progress = percents;
            });
            circleProgressFragment.Started += (sender, e) =>
            {
                if (percents == 100)
                    percents = 0;
                timer.Start();
            };
            circleProgressFragment.Stopped += (sender, e) =>
            {
                timer.Stop();
            };
            circleProgressFragment.Finished += (sender, e) =>
            {
                timer.Stop();
            };


            themeSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            themeSpinner.ItemSelected += (sender, e) =>
            {
                if (e.Position > 0)
                {
                    circleProgressFragment.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(e.Position).Value);
                    circleProgressFragment.ResetCustomization();
                    spinners.Except(new[] { themeSpinner }).ToList().ForEach(s => s.SetSelection(0));
                }
            };

            var theme = circleProgressFragment.GetThemeProvider().GetCurrentTheme();
            if (theme is LightEOSTheme)
                themeSpinner.SetSelection(1);
            if (theme is DarkEOSTheme)
                themeSpinner.SetSelection(2);


            fontSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Fonts.FontsCollection.Select(item => item.Key).ToList());
            fontSpinner.ItemSelected += (sender, e) =>
            {
                if (e.Position > 0)
                {
                    circleProgressFragment.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(e.Position).Value);
                }
            };

            colorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            colorSpinner.ItemSelected += (sender, e) =>
            {
                if (e.Position > 0)
                    circleProgressFragment.Color = Colors.ColorsCollection.ElementAt(e.Position).Value;
            };

            alternativeColorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            alternativeColorSpinner.ItemSelected += (sender, e) =>
            {
                if (e.Position > 0)
                    circleProgressFragment.AlternativeColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
            };


            textSizeSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            textSizeSpinner.ItemSelected += (sender, e) =>
            {
                if (e.Position > 0)
                    circleProgressFragment.TextSize = Sizes.TextSizeCollection.ElementAt(e.Position).Value;
            }; ;

            showProgressSwitch.CheckedChange += (sender, e) =>
            {
                circleProgressFragment.ShowProgress = showProgressSwitch.Checked;
            };

            resetButton.Click += delegate
            {
                spinners.Except(new[] { themeSpinner }).ToList().ForEach(s => s.SetSelection(0));
                circleProgressFragment.ResetCustomization();
            };
        }
    }
}
