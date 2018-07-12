using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace EOS.UI.Android.Sandbox.RecyclerImplementation
{
    public class SimpleViewHolder : RecyclerView.ViewHolder
    {
        public TextView TitleView { get; private set; }
        
        public LinearLayout Layout { get; private set; }
        
        public View Devider { get; private set; }

        public SimpleViewHolder(View itemView) : base(itemView)
        {
            TitleView = itemView.FindViewById<TextView>(Resource.Id.textViewTitle);
            Layout = itemView.FindViewById<LinearLayout>(Resource.Id.linearLayout);
            Devider = itemView.FindViewById<View>(Resource.Id.devider);
        }
    }
}