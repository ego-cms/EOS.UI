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

        public override int ItemCount => _controlNames != null ? _controlNames.Count : 0;

        public ControlsAdapter(List<string> names)
        {
            _controlNames = names;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as ControlsViewHolder;
            viewHolder.ControlTitle.Text = _controlNames[position];
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ControlsItemCell, parent, false);
            var viewHolder = new ControlsViewHolder(itemView, OnClick);
            return viewHolder;
        }

        private void OnClick(int position)
        {
            ItemClick?.Invoke(this, position);
        }
    }
}