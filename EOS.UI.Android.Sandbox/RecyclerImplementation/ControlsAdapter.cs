using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;

namespace EOS.UI.Android.Sandbox.RecyclerImplementation
{
    public class ControlsAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        private List<string> _controlNames;
        private RecyclerView _recycler;

        public override int ItemCount => _controlNames != null ? _controlNames.Count : 0;

        public ControlsAdapter(List<string> names, RecyclerView recycler)
        {
            _recycler = recycler;
            _controlNames = names;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as EOSSandboxControlsViewHolder;
            viewHolder.ControlTitle.Text = _controlNames[position];
            viewHolder.UpdateAppearance();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ControlsItemCell, parent, false);
            var viewHolder = new EOSSandboxControlsViewHolder(itemView, OnClick, _recycler);
            return viewHolder;
        }

        private void OnClick(int position)
        {
            ItemClick?.Invoke(this, position);
        }
    }
}
