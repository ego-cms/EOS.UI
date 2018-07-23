using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using EOS.UI.Android.Components;
using EOS.UI.Shared.Themes.DataModels;
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

            circleMenu.CircleMenuItems = GenerateSource();
        }

        private List<CircleMenuItemModel> GenerateSource()
        {
            var menus = new List<CircleMenuItemModel>();
            var submenus = new List<CircleMenuItemModel>();

            submenus.Add(new CircleMenuItemModel(31, BaseContext.Resources.GetDrawable(Resource.Drawable.WidescreenIcon)));
            submenus.Add(new CircleMenuItemModel(32, BaseContext.Resources.GetDrawable(Resource.Drawable.OneToOneIcon)));
            submenus.Add(new CircleMenuItemModel(33, BaseContext.Resources.GetDrawable(Resource.Drawable.HDRIcon)));

            menus.Add(new CircleMenuItemModel(1, BaseContext.Resources.GetDrawable(Resource.Drawable.SwitchIcon)));
            menus.Add(new CircleMenuItemModel(2, BaseContext.Resources.GetDrawable(Resource.Drawable.CameraIcon)));
            menus.Add(new CircleMenuItemModel(3, BaseContext.Resources.GetDrawable(Resource.Drawable.ShutterIcon), submenus));
            menus.Add(new CircleMenuItemModel(4, BaseContext.Resources.GetDrawable(Resource.Drawable.TimerIcon)));
            menus.Add(new CircleMenuItemModel(5, BaseContext.Resources.GetDrawable(Resource.Drawable.BushIcon)));
            menus.Add(new CircleMenuItemModel(6, BaseContext.Resources.GetDrawable(Resource.Drawable.DurationIcon)));
            menus.Add(new CircleMenuItemModel(7, BaseContext.Resources.GetDrawable(Resource.Drawable.EffectsIcon)));
            menus.Add(new CircleMenuItemModel(8, BaseContext.Resources.GetDrawable(Resource.Drawable.HealIcon)));
            menus.Add(new CircleMenuItemModel(9, BaseContext.Resources.GetDrawable(Resource.Drawable.MasksIcon)));

            return menus;
        }
    }
}
