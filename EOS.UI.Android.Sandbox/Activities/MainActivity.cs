using System;
using System.Linq;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using EOS.UI.Android.Sandbox.Activities;
using EOS.UI.Android.Sandbox.RecyclerImplementation;
using C = UIFrameworks.Shared.Themes.Helpers.Controls;

namespace EOS.UI.Android.Sandbox
{
    [Activity(Label = "Sandbox", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private RecyclerView _recyclerView;
        private Dictionary<string, Type> _controlDictionary = new Dictionary<string, Type>
        {
            { C.BadgeLabel, typeof(BadgeLabelActivity) },
            { C.GhostButton, typeof(GhostButtonActivity) },
            { C.SimpleButton,typeof(SimpleButtonActivity) },
            { C.SimpleLabel, typeof(SimpleLabelActivity) },
            { C.Input, typeof(InputActivity) },
            { C.FabProgress,typeof(FabProgressActivity) }
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

