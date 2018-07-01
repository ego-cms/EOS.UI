using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using EOS.UI.Android.Controls;
using EOS.UI.Shared.Helpers;

namespace EOS.UI.Android.Sandbox.Activities
{
    [Activity(Label = "Fab progress sample", Theme = "@style/Sandbox.Main")]
    public class FabProgressSampleActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FabProgressSampleLayout);

            InitializeRecyclerView();

            InitializeFabProgress();
        }

        private void InitializeFabProgress()
        {
            var fab = FindViewById<FabProgress>(Resource.Id.fab_progress_fab);
            var shadowOffsetX = Intent.GetIntExtra(FabProgressActivity.ShadowOffsetXKey, 0);
            var shadowOffsetY = Intent.GetIntExtra(FabProgressActivity.ShadowOffsetYKey, 0);
            var shadowBlur = Intent.GetIntExtra(FabProgressActivity.ShadowBlurKey, 0);
            var a = (byte)Intent.GetShortExtra(FabProgressActivity.ShadowColorAKey, 0);
            var r = (byte)Intent.GetShortExtra(FabProgressActivity.ShadowColorRKey, 0);
            var g = (byte)Intent.GetShortExtra(FabProgressActivity.ShadowColorGKey, 0);
            var b = (byte)Intent.GetShortExtra(FabProgressActivity.ShadowColorBKey, 0);
            var shadowColor = new Color(r, g, b, a);

            fab.ShadowConfig = new ShadowConfig { 
                Offset = new Point(shadowOffsetX, shadowOffsetY), 
                Blur = shadowBlur, 
                Color = shadowColor 
            };

            fab.Click += async (sender, e) =>
            {
                if (fab.InProgress)
                    return;
                fab.StartProgressAnimation();
                await Task.Delay(5000);
                fab.StopProgressAnimation();
            };
        }

        private void InitializeRecyclerView()
        {
            List<string> items = new List<string>();
            for (int i = 1; i < 20; i++)
            {
                items.Add($"Item {i}");
            }
            var adapter = new FabProgressRecyclerAdapter(items.ToList<object>());

            var recyler = FindViewById<RecyclerView>(Resource.Id.fab_progress_recycler);
            recyler.SetLayoutManager(new LinearLayoutManager(this));
            recyler.SetAdapter(adapter);
        }
    }

    class FabProgressRecyclerAdapter : RecyclerView.Adapter
    {
        List<object> _items;
        public FabProgressRecyclerAdapter(List<object> items)
        {
            _items = items;
        }

        public override int GetItemViewType(int position) => 0;

        public override long GetItemId(int position) => position;

        public override int ItemCount => _items.Count();

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.FromContext(parent.Context).Inflate(Resource.Layout.FabProgressItemLayout, parent, false);
            return new FabProgressViewHolder(view);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = _items[position];
            var fabHolder = (FabProgressViewHolder)holder;
            fabHolder.TextView.Text = item.ToString();
        }
    }

    class FabProgressViewHolder : RecyclerView.ViewHolder
    {
        public TextView TextView;
        public FabProgressViewHolder(View itemView) : base(itemView)
        {
            TextView = itemView.FindViewById<TextView>(Resource.Id.fab_progress_item_text);
        }
    }
}
