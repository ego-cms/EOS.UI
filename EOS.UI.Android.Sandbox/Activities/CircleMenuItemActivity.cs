using Android.App;
using Android.OS;
using EOS.UI.Android.Sandbox.Controls;
using UIFrameworks.Shared.Themes.Helpers;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.CircleMenuItem, Theme = "@style/Sandbox.Main")]
    public class CircleMenuItemActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CircleMenuItemLayout);
            var logo = FindViewById<EOSSandboxImageView>(Resource.Id.logo);
            logo.SetImageResource(Intent.GetIntExtra("logoId", Resource.Drawable.AccountCircle));
        }
    }
}
