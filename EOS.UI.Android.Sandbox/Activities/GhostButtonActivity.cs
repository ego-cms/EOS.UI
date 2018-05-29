using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using EOS.UI.Android.Controls;
using EOS.UI.Android.Sandbox.Adapters;
using EOS.UI.Android.Sandbox.Controls;
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
            var themeDropDown = FindViewById<DropDown>(Resource.Id.themeDropDown);
            var disabledColorDropDown = FindViewById<DropDown>(Resource.Id.enabledTextColorDropDown);
            var pressedColorDropDown = FindViewById<DropDown>(Resource.Id.disabledTextColorDropDown);
            var enabledColorDropDown = FindViewById<DropDown>(Resource.Id.pressedTextColorDropDown);
            var fontDropDown = FindViewById<DropDown>(Resource.Id.fontDropDown);
            var letterSpacingDropDown = FindViewById<DropDown>(Resource.Id.letterSpacingDropDown);
            var textSizeDropDown = FindViewById<DropDown>(Resource.Id.textSizeDropDown);
            var stateSwitch = FindViewById<Switch>(Resource.Id.stateSwitch);
            ghostButton.ResetCustomization();

            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            var spinners = new List<DropDown>()
            {
                themeDropDown,
                disabledColorDropDown,
                pressedColorDropDown,
                enabledColorDropDown,
                fontDropDown,
                letterSpacingDropDown,
                textSizeDropDown
            };

            themeDropDown.Name = Fields.Theme;
            themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            themeDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                {
                    ghostButton.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                    ghostButton.ResetCustomization();
                    spinners.Except(new[] { themeDropDown }).ToList().ForEach(s => s.SetSpinnerSelection(0));
                }
            };

            var theme = ghostButton.GetThemeProvider().GetCurrentTheme();
            if(theme is LightEOSTheme)
                themeDropDown.SetSpinnerSelection(1);
            if(theme is DarkEOSTheme)
                themeDropDown.SetSpinnerSelection(2);

            fontDropDown.Name = Fields.Font;
            fontDropDown.SetupAdapter(Fonts.FontsCollection.Select(item => item.Key).ToList());
            fontDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    ghostButton.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(position).Value);
            };

            letterSpacingDropDown.Name = Fields.LetterSpacing;
            letterSpacingDropDown.SetupAdapter(Sizes.LetterSpacingCollection.Select(item => item.Key).ToList());
            letterSpacingDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    ghostButton.LetterSpacing = Sizes.LetterSpacingCollection.ElementAt(position).Value;
            };

            enabledColorDropDown.Name = Fields.EnabledTextColor;
            enabledColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            enabledColorDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    ghostButton.EnabledTextColor = Colors.ColorsCollection.ElementAt(position).Value;
            };

            disabledColorDropDown.Name = Fields.DisabledTextColor;
            disabledColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            disabledColorDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    ghostButton.DisabledTextColor = Colors.ColorsCollection.ElementAt(position).Value;
            };

            pressedColorDropDown.Name = Fields.PressedTextColor;
            pressedColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            pressedColorDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    ghostButton.PressedStateTextColor = Colors.ColorsCollection.ElementAt(position).Value;
            };

            textSizeDropDown.Name = Fields.TextSize;
            textSizeDropDown.SetupAdapter(Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            textSizeDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    ghostButton.TextSize = Sizes.TextSizeCollection.ElementAt(position).Value;
            };
            ;

            stateSwitch.CheckedChange += (sender, e) =>
            {
                ghostButton.Enabled = stateSwitch.Checked;
            };

            resetButton.Click += delegate
            {
                spinners.Except(new[] { themeDropDown }).ToList().ForEach(s => s.SetSpinnerSelection(0));
                ghostButton.ResetCustomization();
            };
        }
    }
}