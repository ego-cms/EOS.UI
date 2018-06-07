using System;
using Android.Support.V7.Widget;
using Android.Views;

namespace EOS.UI.Android.Components
{
    internal class WorkTimeAdapter : RecyclerView.Adapter
    {
        public override int ItemCount => 7;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var workTimeItem = holder as WorkTimeItem;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.WorkTimeItemLayout, parent, false);
            var viewHolder = new WorkTimeItem(itemView);
            return viewHolder;
        }
    }
}