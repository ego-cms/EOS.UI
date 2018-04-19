using Android.App;
using Android.OS;
using C = UIFrameworks.Shared.Themes.Helpers.Controls;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = C.FabProgress)]
    public class FabProgressActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FabProgressLayout);
        }
    }
}