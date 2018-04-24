using System;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace EOS.UI.Android.Sandbox.RecyclerImplementation
{
    public class ControlsViewHolder : RecyclerView.ViewHolder, View.IOnTouchListener
    {
        private Color _normalBackground = Color.Transparent;
        private Color _selectedBackground = Color.Gray;
        private LinearLayout _container;
        private Action<int> _clickAction;

        public TextView ControlTitle { get; private set; }

        public ControlsViewHolder(View itemView, Action<int> clickAction) : base(itemView)
        {
            _clickAction = clickAction;
            _container = itemView.FindViewById<LinearLayout>(Resource.Id.holderContainer);
            ControlTitle = itemView.FindViewById<TextView>(Resource.Id.titleTextView);
            _container.SetOnTouchListener(this);
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            if(e.Action == MotionEventActions.Down)
            {
                _container.SetBackgroundColor(_selectedBackground);
            }
            else if(e.Action == MotionEventActions.Up || e.Action == MotionEventActions.Cancel)
            {
                _container.SetBackgroundColor(_normalBackground);
                _clickAction?.Invoke(Position);
            }
            return true;
        }
    }
}