using Android.App;
using Android.OS;
using Android.Views;
using EOS.UI.Android.Components;
using UIFrameworks.Shared.Themes.Helpers;
using A = Android;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.CircleMenu, Theme = "@style/Sandbox.Main")]
    public class CircleMenuActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CircleMenuLayout);

            var circleMenu = new CircleMenu(BaseContext);
            circleMenu.Attach(Window.DecorView.FindViewById(A.Resource.Id.Content) as ViewGroup);
        }
    }
}
