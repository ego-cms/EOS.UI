using Android.App;
using Android.OS;
using UIFrameworks.Shared.Themes.Helpers;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.Input)]
    public class InputActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.InputLayout);
        }
    }
}