using System.Collections.Generic;
using Android.App;
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
        private List<string> _controlNames = new List<string>
        {
            Controls.BadgeLabel,
            Controls.GhostButton,
            Controls.SimpleButton,
            Controls.SimpleLabel,
            Controls.Input,
            Controls.FabProgress,
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            _recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            var layoutManager = new LinearLayoutManager(BaseContext);
            _recyclerView.SetLayoutManager(layoutManager);

            var adapter = new ControlsAdapter(new List<string>(_controlNames));

            adapter.ItemClick += ItemClick;
            _recyclerView.SetAdapter(adapter);
        }

        private void ItemClick(object sender, int e)
        {
            switch(e)
            {
                case 0:
                    StartActivity(typeof(BadgeLabelActivity));
                    break;
                case 1:
                    StartActivity(typeof(GhostButtonActivity));
                    break;
                case 2:
                    StartActivity(typeof(SimpleButtonActivity));
                    break;
                case 3:
                    StartActivity(typeof(SimpleLabelActivity));
                    break;
                case 4:
                    StartActivity(typeof(InputActivity));
                    break;
                case 5:
                    StartActivity(typeof(FabProgressActivity));
                    break;
            }
        }
    }
}

