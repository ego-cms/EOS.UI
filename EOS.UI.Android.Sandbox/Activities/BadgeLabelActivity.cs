using Android.App;
using Android.OS;
using UIFrameworks.Shared.Themes.Helpers;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = Controls.BadgeLabel)]
    public class BadgeLabelActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BadgeLabelLayout);
        }
    }
}