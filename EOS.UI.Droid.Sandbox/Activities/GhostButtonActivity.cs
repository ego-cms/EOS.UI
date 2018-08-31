using System.Collections.Generic;
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
using EOS.UI.Shared.Themes.Themes;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Droid.Sandbox.Activities
{
    [Activity(Label = ControlNames.GhostButton, Theme = "@style/Sandbox.Main", ScreenOrientation = ScreenOrientation.Portrait)]
    public class GhostButtonActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GhostButtonLayout);

            var ghostButton = FindViewById<GhostButton>(Resource.Id.ghostButton);
            var themeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.themeDropDown);
            var disabledColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.disabledTextColorDropDown);
            var enabledColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.enabledTextColorDropDown);
            var fontDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.fontDropDown);
            var letterSpacingDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.letterSpacingDropDown);
            var textSizeDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.textSizeDropDown);
            var rippleColorDropDown = FindViewById<EOSSandboxDropDown>(Resource.Id.rippleColorDropDown);
            var stateSwitch = FindViewById<Switch>(Resource.Id.stateSwitch);
            ghostButton.ResetCustomization();

            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            var spinners = new List<EOSSandboxDropDown>()
            {
                themeDropDown,
                disabledColorDropDown,
                enabledColorDropDown,
                fontDropDown,
                letterSpacingDropDown,
                textSizeDropDown,
                rippleColorDropDown
            };

            themeDropDown.Name = Fields.Theme;
            themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            themeDropDown.ItemSelected += (position) =>
            {
                if (position > 0)
                {
                    ghostButton.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                    ghostButton.ResetCustomization();
                    spinners.Except(new[] { themeDropDown }).ToList().ForEach(s => s.SetSpinnerSelection(0));
                    UpdateAppearance();
                }
            };

            var theme = ghostButton.GetThemeProvider().GetCurrentTheme();
            if (theme is LightEOSTheme)
                themeDropDown.SetSpinnerSelection(1);
            if (theme is DarkEOSTheme)
                themeDropDown.SetSpinnerSelection(2);

            fontDropDown.Name = Fields.Font;
            fontDropDown.SetupAdapter(GhostButtonConstants.GhostButtonFonts.Select(item => item.Key).ToList());
            fontDropDown.ItemSelected += (position) =>
            {
                ghostButton.Typeface = Typeface.CreateFromAsset(Assets, GhostButtonConstants.GhostButtonFonts.ElementAt(position).Value);
            };

            letterSpacingDropDown.Name = Fields.LetterSpacing;
            letterSpacingDropDown.SetupAdapter(GhostButtonConstants.LetterSpacings.Select(item => item.Key).ToList());
            letterSpacingDropDown.ItemSelected += (position) =>
            {
                ghostButton.LetterSpacing = GhostButtonConstants.LetterSpacings.ElementAt(position).Value;
            };

            enabledColorDropDown.Name = Fields.EnabledTextColor;
            enabledColorDropDown.SetupAdapter(GhostButtonConstants.FontColors.Select(item => item.Key).ToList());
            enabledColorDropDown.ItemSelected += (position) =>
            {
                ghostButton.TextColor = GhostButtonConstants.FontColors.ElementAt(position).Value;
            };

            disabledColorDropDown.Name = Fields.DisabledTextColor;
            disabledColorDropDown.SetupAdapter(GhostButtonConstants.DisabledFontColors.Select(item => item.Key).ToList());
            disabledColorDropDown.ItemSelected += (position) =>
            {
                ghostButton.DisabledTextColor = GhostButtonConstants.DisabledFontColors.ElementAt(position).Value;
            };

            textSizeDropDown.Name = Fields.TextSize;
            textSizeDropDown.SetupAdapter(GhostButtonConstants.TextSizes.Select(item => item.Key).ToList());
            textSizeDropDown.ItemSelected += (position) =>
            {
                ghostButton.TextSize = GhostButtonConstants.TextSizes.ElementAt(position).Value;
            };

            rippleColorDropDown.Name = Fields.RippleColor;
            rippleColorDropDown.SetupAdapter(GhostButtonConstants.RippleColors.Select(item => item.Key).ToList());
            rippleColorDropDown.ItemSelected += (position) =>
            {
                ghostButton.RippleColor = GhostButtonConstants.RippleColors.ElementAt(position).Value;
            };

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
