using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using EOS.UI.Droid.Controls;
using EOS.UI.Droid.Sandbox.Controls;
using EOS.UI.Droid.Sandbox.Helpers;
using EOS.UI.Shared.Sandbox.ControlConstants.Android;
using EOS.UI.Shared.Sandbox.Helpers;
using EOS.UI.Shared.Themes.Themes;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Droid.Sandbox.Activities
{
    [Activity(Label = ControlNames.CircleProgress, Theme = "@style/Sandbox.Main", ScreenOrientation = ScreenOrientation.Portrait)]
    public class CircleProgressActivity : BaseActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CircleProgressLayout);
            var circleProgress = FindViewById<CircleProgress>(Resource.Id.circleProgress);
            var themeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.themeDropDown);
            var colorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.colorDropDown);
            var alternativeColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.alternativeColorDropDown);
            var fontDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.fontDropDown);
            var textSizeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.textSizeDropDown);
            var fillColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.backgroundColorDropDown);
            var showProgressSwitch = FindViewById<Switch>(Resource.Id.showProgressSwitch);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            var spinners = new List<EOSSandboxDropDown>()
            {
                themeDropDown,
                colorDropDown,
                fontDropDown,
                textSizeDropDown,
                alternativeColorDropDown,
                fillColorDropDown
            };

            int percents = 0;
            var timer = new PlatformTimer();
            timer.Setup(TimeSpan.FromMilliseconds(100), () =>
            {
                percents += 1;
                circleProgress.Progress = percents;
            });
            circleProgress.Started += (sender, e) =>
            {
                if(percents == 100)
                    percents = 0;
                timer.Start();
            };
            circleProgress.Stopped += (sender, e) =>
            {
                timer.Stop();
                percents = 0;
                circleProgress.Progress = 0;
            };
            circleProgress.Finished += (sender, e) =>
            {
                timer.Stop();
            };

            themeDropDown.Name = Fields.Theme;
            themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            themeDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                {
                    circleProgress.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                    circleProgress.ResetCustomization();
                    spinners.Except(new[] { themeDropDown }).ToList().ForEach(s => s.SetSpinnerSelection(0));
                    circleProgress.Progress = 0;
                    showProgressSwitch.Checked = true;
                    UpdateAppearance();
                }
            };

            var theme = circleProgress.GetThemeProvider().GetCurrentTheme();
            if(theme is LightEOSTheme)
                themeDropDown.SetSpinnerSelection(1);
            if(theme is DarkEOSTheme)
                themeDropDown.SetSpinnerSelection(2);

            fontDropDown.Name = Fields.Font;
            fontDropDown.SetupAdapter(CircleProgressConstants.CircleProgressFonts.Select(item => item.Key).ToList());
            fontDropDown.ItemSelected += (position) =>
            {
                    circleProgress.Typeface = Typeface.CreateFromAsset(Assets, CircleProgressConstants.CircleProgressFonts.ElementAt(position).Value);
            };

            colorDropDown.Name = Fields.Color;
            colorDropDown.SetupAdapter(CircleProgressConstants.CircleProgressColors.Select(item => item.Key).ToList());
            colorDropDown.ItemSelected += (position) =>
            {
                    circleProgress.Color = CircleProgressConstants.CircleProgressColors.ElementAt(position).Value;
            };

            alternativeColorDropDown.Name = Fields.AlternativeColor;
            alternativeColorDropDown.SetupAdapter(CircleProgressConstants.AlternativeColors.Select(item => item.Key).ToList());
            alternativeColorDropDown.ItemSelected += (position) =>
            {
                    circleProgress.AlternativeColor = CircleProgressConstants.AlternativeColors.ElementAt(position).Value;
            };

            fillColorDropDown.Name = Fields.FillColor;
            fillColorDropDown.SetupAdapter(CircleProgressConstants.FillColors.Select(item => item.Key).ToList());
            fillColorDropDown.ItemSelected += (position) =>
            {
                    circleProgress.FillColor = CircleProgressConstants.FillColors.ElementAt(position).Value;
            };

            textSizeDropDown.Name = Fields.TextSize;
            textSizeDropDown.SetupAdapter(CircleProgressConstants.TextSizes.Select(item => item.Key).ToList());
            textSizeDropDown.ItemSelected += (position) =>
            {
                    circleProgress.TextSize = CircleProgressConstants.TextSizes.ElementAt(position).Value;
            };

            showProgressSwitch.CheckedChange += (sender, e) =>
            {
                circleProgress.ShowProgress = showProgressSwitch.Checked;
            };

            resetButton.Click += delegate
            {
                spinners.Except(new[] { themeDropDown }).ToList().ForEach(s => s.SetSpinnerSelection(0));
                showProgressSwitch.Checked = true;
                circleProgress.ResetCustomization();
            };
        }
    }
}