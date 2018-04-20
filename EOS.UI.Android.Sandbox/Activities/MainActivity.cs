using System;
using System.Linq;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using EOS.UI.Android.Sandbox.Activities;
using EOS.UI.Android.Sandbox.RecyclerImplementation;
using UIFrameworks.Shared.Themes.Helpers;

namespace EOS.UI.Android.Sandbox
{
    [Activity(Label = "Sandbox", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private RecyclerView _recyclerView;
        private Dictionary<string, Type> _controlDictionary = new Dictionary<string, Type>
        {
            { ControlNames.BadgeLabel, typeof(BadgeLabelActivity) },
            { ControlNames.GhostButton, typeof(GhostButtonActivity) },
            { ControlNames.SimpleButton,typeof(SimpleButtonActivity) },
            { ControlNames.SimpleLabel, typeof(SimpleLabelActivity) },
            { ControlNames.Input, typeof(InputActivity) },
            { ControlNames.FabProgress,typeof(FabProgressActivity) }
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

        private void ItemClick(object sender, int e)
        {
            StartActivity(new Intent(this, Java.Lang.Class.FromType(_controlDictionary.ElementAt(e).Value)));
        }
    }
}

