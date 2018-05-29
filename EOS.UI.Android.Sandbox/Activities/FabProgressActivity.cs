using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
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
    [Activity(Label = ControlNames.FabProgress)]
    public class FabProgressActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FabProgressLayout);

            var fab = FindViewById<FabProgress>(Resource.Id.fabProgress);
            var themeSpinner = FindViewById<Spinner>(Resource.Id.spinnerTheme);
            var backgroundColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerBackgroundColor);
            var disabledColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerDisabled);
            var pressedColorSpinner = FindViewById<Spinner>(Resource.Id.spinnerPressed);
            var sizeSpinner = FindViewById<Spinner>(Resource.Id.spinnerSize);
            var stateSwitch = FindViewById<Switch>(Resource.Id.stateSwitch);
            var resetButton = FindViewById<Button>(Resource.Id.buttonResetCustomization);
            fab.Click += async (sender, e) =>
            {
                if (fab.InProgress)
                    return;
                themeSpinner.Enabled = false;
                resetButton.Enabled = false;
                fab.StartProgressAnimation();
                await Task.Delay(5000);
                fab.StopProgressAnimation();
                themeSpinner.Enabled = true;
                resetButton.Enabled = true;
            };

            var spinners = new List<Spinner>()
            {
                themeSpinner,
                disabledColorSpinner,
                pressedColorSpinner,
                backgroundColorSpinner,
                sizeSpinner
            };

            themeSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, ThemeTypes.ThemeCollection.Select(item => item.Key).ToList());
            themeSpinner.ItemSelected += (sender, e) =>
            {
                if (e.Position > 0)
                {
                    fab.GetThemeProvider().SetCurrentTheme(ThemeTypes.ThemeCollection.ElementAt(e.Position).Value);
                    fab.ResetCustomization();
                    spinners.Except(new[] { themeSpinner }).ToList().ForEach(s => s.SetSelection(0));
                }
            };
            var theme = fab.GetThemeProvider().GetCurrentTheme();
            if (theme is LightEOSTheme)
                themeSpinner.SetSelection(1);
            if (theme is DarkEOSTheme)
                themeSpinner.SetSelection(2);

            backgroundColorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            backgroundColorSpinner.ItemSelected += (sender, e) =>
            {
                if (e.Position > 0)
                    fab.BackgroundColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
            };

            disabledColorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            disabledColorSpinner.ItemSelected += (sender, e) =>
            {
                if (e.Position > 0)
                    fab.DisabledBackgroundColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
            };

            pressedColorSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Colors.ColorsCollection.Select(item => item.Key).ToList());
            pressedColorSpinner.ItemSelected += (sender, e) =>
            {
                if (e.Position > 0)
                    fab.PressedBackgroundColor = Colors.ColorsCollection.ElementAt(e.Position).Value;
            };

            sizeSpinner.Adapter = new SpinnerAdapter(this, R.Layout.SimpleSpinnerItem, Sizes.FabProgressSizes.Select(i => i.Key).ToList());
            sizeSpinner.ItemSelected += (sender, e) =>
            {
                if (e.Position > 0)
                    fab.ButtonSize = Sizes.FabProgressSizes.ElementAt(e.Position).Value;
            };

            stateSwitch.CheckedChange += (sender, e) =>
            {
                fab.Enabled = stateSwitch.Checked;
            };

            resetButton.Click += delegate
            {
                spinners.Except(new[] { themeSpinner }).ToList().ForEach(s => s.SetSelection(0));
                fab.ResetCustomization();
            };
        }
    }
}