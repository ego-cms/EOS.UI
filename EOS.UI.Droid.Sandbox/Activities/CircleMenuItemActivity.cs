using Android.App;
using Android.OS;
using EOS.UI.Droid.Sandbox.Controls;
using EOS.UI.Shared.Sandbox.Helpers;

namespace EOS.UI.Droid.Sandbox.Activities
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
