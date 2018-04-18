using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace EOS.UI.Android.Sandbox.RecyclerImplementation
{
    public class ControlsViewHolder : RecyclerView.ViewHolder
    {
        public TextView ControlTitle { get; private set; }
        public ControlsViewHolder(View itemView, Action<int> clickAction) : base(itemView)
        {
            ControlTitle = itemView.FindViewById<TextView>(Resource.Id.titleTextView);
            itemView.Click += (s, e) => clickAction?.Invoke(Position);
        }
    }
}