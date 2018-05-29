using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
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
    [Activity(Label = ControlNames.FabProgress)]
    public class FabProgressActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FabProgressLayout);

            var fab = FindViewById<FabProgress>(Resource.Id.fabProgress);
            var themeDropDown = FindViewById<DropDown>(Resource.Id.themeDropDown);
            var backgroundColorDropDown = FindViewById<DropDown>(Resource.Id.backgroundDropDown);
            var disabledColorDropDown = FindViewById<DropDown>(Resource.Id.disabledColorDropDown);
            var pressedColorDropDown = FindViewById<DropDown>(Resource.Id.pressedColorDropDown);
            var sizeDropDown = FindViewById<DropDown>(Resource.Id.sizeDropDown);
            var stateSwitch = FindViewById<Switch>(Resource.Id.stateSwitch);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            fab.Click += async (sender, e) =>
            {
                if(fab.InProgress)
                    return;
                themeDropDown.Enabled = false;
                resetButton.Enabled = false;
                fab.StartProgressAnimation();
                await Task.Delay(5000);
                fab.StopProgressAnimation();
                themeDropDown.Enabled = true;
                resetButton.Enabled = true;
            };

            var spinners = new List<DropDown>()
            {
                themeDropDown,
                disabledColorDropDown,
                pressedColorDropDown,
                backgroundColorDropDown,
                sizeDropDown
            };

            themeDropDown.Name = Fields.Theme;
            themeDropDown.SetupAdapter(ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            themeDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                {
                    fab.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(position).Value);
                    fab.ResetCustomization();
                    spinners.Except(new[] { themeDropDown }).ToList().ForEach(s => s.SetSpinnerSelection(0));
                }
            };
            var theme = fab.GetThemeProvider().GetCurrentTheme();
            if(theme is LightEOSTheme)
                themeDropDown.SetSpinnerSelection(1);
            if(theme is DarkEOSTheme)
                themeDropDown.SetSpinnerSelection(2);

            backgroundColorDropDown.Name = Fields.Background;
            backgroundColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            backgroundColorDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    fab.BackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
            };

            disabledColorDropDown.Name = Fields.DisabledColor;
            disabledColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            disabledColorDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    fab.DisabledBackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
            };

            pressedColorDropDown.Name = Fields.PressedColor;
            pressedColorDropDown.SetupAdapter(Colors.ColorsCollection.Select(item => item.Key).ToList());
            pressedColorDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    fab.PressedBackgroundColor = Colors.ColorsCollection.ElementAt(position).Value;
            };

            sizeDropDown.Name = Fields.Size;
            sizeDropDown.SetupAdapter(Sizes.FabProgressSizes.Select(i => i.Key).ToList());
            sizeDropDown.ItemSelected += (position) =>
            {
                if(position > 0)
                    fab.ButtonSize = Sizes.FabProgressSizes.ElementAt(position).Value;
            };

            stateSwitch.CheckedChange += (sender, e) =>
            {
                fab.Enabled = stateSwitch.Checked;
            };

            resetButton.Click += delegate
            {
                spinners.Except(new[] { themeDropDown }).ToList().ForEach(s => s.SetSpinnerSelection(0));
                fab.ResetCustomization();
            };
        }
    }
}