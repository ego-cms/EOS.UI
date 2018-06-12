
using Android.App;
using Android.OS;
using EOS.UI.Android.Components;
using UIFrameworks.Shared.Themes.Helpers;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = ControlNames.WorkTimeCalendar, Theme = "@style/Sandbox.Main")]
    public class WorkTimeActivity : BaseActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WorkTimeActivity);

            var workTime = FindViewById<WorkTime>(Resource.Id.workTime);

            workTime.UpdateAppearance();
        }
    }
}
