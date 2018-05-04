using Android.App;
using Android.OS;
using Android.Widget;
using EOS.UI.Android.Controls;
using UIFrameworks.Shared.Themes.Helpers;
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
        }
    }
}