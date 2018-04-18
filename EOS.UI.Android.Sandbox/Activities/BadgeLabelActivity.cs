using Android.App;
using Android.OS;
using C = UIFrameworks.Shared.Themes.Helpers.Controls;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = C.BadgeLabel)]
    public class BadgeLabelActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BadgeLabelLayout);
        }
    }
}