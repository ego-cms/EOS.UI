using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.Widget;
using EOS.UI.Android.Sandbox.Activities;
using EOS.UI.Android.Sandbox.RecyclerImplementation;
using UIFrameworks.Shared.Themes.Helpers;

namespace EOS.UI.Android.Sandbox
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/Sandbox.Main", Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : BaseActivity
    {
        private RecyclerView _recyclerView;
        private Dictionary<string, Type> _controlDictionary = new Dictionary<string, Type>
        {
            { ControlNames.BadgeLabel, typeof(BadgeLabelActivity) },
            { ControlNames.SimpleLabel, typeof(SimpleLabelActivity) },
            { ControlNames.GhostButton, typeof(GhostButtonActivity) },
            { ControlNames.SimpleButton,typeof(SimpleButtonActivity) },
            { ControlNames.FabProgress,typeof(FabProgressActivity) },
            { ControlNames.Input, typeof(InputActivity) },
            { ControlNames.CircleProgress, typeof(CircleProgressActivity) },
            { ControlNames.Section, typeof(SectionActivity) },
            { ControlNames.CTAButton, typeof(CTAActivity) },
            { ControlNames.WorkTimeCalendar, typeof(WorkTimeActivity) },
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            _recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            var layoutManager = new LinearLayoutManager(BaseContext);
            _recyclerView.SetLayoutManager(layoutManager);

            var adapter = new ControlsAdapter(_controlDictionary.Select(item => item.Key).ToList());

            adapter.ItemClick += ItemClick;
            _recyclerView.SetAdapter(adapter);
        }

        protected override void OnStart()
        {
            base.OnStart();
            UpdateApperaence();
        }

        private void ItemClick(object sender, int e)
        {
            StartActivity(new Intent(this, Java.Lang.Class.FromType(_controlDictionary.ElementAt(e).Value)));
        }
    }
}

