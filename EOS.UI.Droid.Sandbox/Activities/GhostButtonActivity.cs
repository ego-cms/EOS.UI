using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using EOS.UI.Droid.Controls;
using EOS.UI.Droid.Sandbox.Controls;
using EOS.UI.Shared.Sandbox.Helpers;
using EOS.UI.Shared.Themes.Themes;
using static EOS.UI.Droid.Sandbox.Helpers.Constants;
using static EOS.UI.Shared.Sandbox.Helpers.Constants;

namespace EOS.UI.Droid.Sandbox.Activities
{
    [Activity(Label = ControlNames.GhostButton, Theme = "@style/Sandbox.Main")]
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
                    UpdateApperaence();
                }
            };

            var theme = ghostButton.GetThemeProvider().GetCurrentTheme();
            if (theme is LightEOSTheme)
                themeDropDown.SetSpinnerSelection(1);
            if (theme is DarkEOSTheme)
                themeDropDown.SetSpinnerSelection(2);

            fontDropDown.Name = Fields.Font;
            fontDropDown.SetupAdapter(Fonts.FontsCollection.Select(item => item.Key).ToList());
            fontDropDown.ItemSelected += (position) =>
            {
                if (position > 0)
                    ghostButton.Typeface = Typeface.CreateFromAsset(Assets, Fonts.FontsCollection.ElementAt(position).Value);
            };

            letterSpacingDropDown.Name = Fields.LetterSpacing;
            letterSpacingDropDown.SetupAdapter(Sizes.LetterSpacingCollection.Select(item => item.Key).ToList());
            letterSpacingDropDown.ItemSelected += (position) =>
            {
                if (position > 0)
                    ghostButton.LetterSpacing = Sizes.LetterSpacingCollection.ElementAt(position).Value;
            };

            enabledColorDropDown.Name = Fields.EnabledTextColor;
            enabledColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            enabledColorDropDown.ItemSelected += (position) =>
            {
                if (position > 0)
                    ghostButton.EnabledTextColor = Colors.ColorsCollection.ElementAt(position).Value;
            };

            disabledColorDropDown.Name = Fields.DisabledTextColor;
            disabledColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            disabledColorDropDown.ItemSelected += (position) =>
            {
                if (position > 0)
                    ghostButton.DisabledTextColor = Colors.ColorsCollection.ElementAt(position).Value;
            };

            textSizeDropDown.Name = Fields.TextSize;
            textSizeDropDown.SetupAdapter(Sizes.TextSizeCollection.Select(item => item.Key).ToList());
            textSizeDropDown.ItemSelected += (position) =>
            {
                if (position > 0)
                    ghostButton.TextSize = Sizes.TextSizeCollection.ElementAt(position).Value;
            };

            rippleColorDropDown.Name = Fields.RippleColor;
            rippleColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            rippleColorDropDown.ItemSelected += (position) =>
            {
                if (position > 0)
                    ghostButton.RippleColor = Colors.ColorsCollection.ElementAt(position).Value;
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
