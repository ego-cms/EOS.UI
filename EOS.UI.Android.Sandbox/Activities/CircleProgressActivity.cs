using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using EOS.UI.Android.Controls;
using EOS.UI.Android.Sandbox.Controls;
using EOS.UI.Android.Sandbox.Helpers;
using EOS.UI.Shared.Themes.Themes;
using UIFrameworks.Shared.Themes.Helpers;
using static EOS.UI.Android.Sandbox.Helpers.Constants;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.CircleProgress, Theme = "@style/Sandbox.Main")]
    public class CircleProgressActivity : BaseActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CircleProgressLayout);
            var circleProgressFragment = FindViewById<CircleProgress>(Resource.Id.circleProgress);
            var themeDropDown = FindViewById<EOSDropDown>(Resource.Id.themeDropDown);
            var colorDropDown = FindViewById<EOSDropDown>(Resource.Id.colorDropDown);
            var alternativeColorDropDown = FindViewById<EOSDropDown>(Resource.Id.alternativeColorDropDown);
            var fontDropDown = FindViewById<EOSDropDown>(Resource.Id.fontDropDown);
            var textSizeDropDown = FindViewById<EOSDropDown>(Resource.Id.textSizeDropDown);
            var showProgressSwitch = FindViewById<Switch>(Resource.Id.showProgressSwitch);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            var spinners = new List<EOSDropDown>()
            {
                themeDropDown,
                colorDropDown,
                fontDropDown,
                textSizeDropDown,
                alternativeColorDropDown
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
                if(percents == 100)
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

            themeDropDown.Name = Fields.Theme;
            themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            themeDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                {
                    circleProgressFragment.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                    circleProgressFragment.ResetCustomization();
                    spinners.Except(new[] { themeDropDown }).ToList().ForEach(s => s.SetSpinnerSelection(0));
                    UpdateApperaence();
                }
            };

            var theme = circleProgressFragment.GetThemeProvider().GetCurrentTheme();
            if(theme is LightEOSTheme)
                themeDropDown.SetSpinnerSelection(1);
            if(theme is DarkEOSTheme)
                themeDropDown.SetSpinnerSelection(2);

            fontDropDown.Name = Fields.Font;
            fontDropDown.SetupAdapter(Fonts.FontsCollection.Select(item => item.Key).ToList());
            fontDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    circleProgressFragment.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(position).Value);
            };

            colorDropDown.Name = Fields.Color;
            colorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            colorDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    circleProgressFragment.Color = Colors.ColorsCollection.ElementAt(position).Value;
            };

            alternativeColorDropDown.Name = Fields.AlternativeColor;
            alternativeColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            alternativeColorDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    circleProgressFragment.AlternativeColor = Colors.ColorsCollection.ElementAt(position).Value;
            };

            textSizeDropDown.Name = Fields.TextSize;
            textSizeDropDown.SetupAdapter(Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            textSizeDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    circleProgressFragment.TextSize = Sizes.TextSizeCollection.ElementAt(position).Value;
            };

            showProgressSwitch.CheckedChange += (sender, e) =>
            {
                circleProgressFragment.ShowProgress = showProgressSwitch.Checked;
            };

            resetButton.Click += delegate
            {
                spinners.Except(new[] { themeDropDown }).ToList().ForEach(s => s.SetSpinnerSelection(0));
                showProgressSwitch.Checked = true;
                circleProgressFragment.ResetCustomization();
            };
        }
    }
}