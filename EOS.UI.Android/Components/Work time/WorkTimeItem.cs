using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace EOS.UI.Android.Components
{
    internal class WorkTimeItem : RecyclerView.ViewHolder, View.IOnTouchListener
    {
        public LinearLayout Container { get; private set; }
        public TextView DayLabel { get; private set; }
        public View DayDivider { get; private set; }
        public TextView StartDayTimeLabel { get; private set; }
        public TextView EndDayTimeLabel { get; private set; }
        public View CircleDivider { get; private set; }
        public TextView StartBreakTimeLabel { get; private set; }
        public TextView EndBreakTimeLabel { get; private set; }

        private Action<int> _setSelectedDay;

        public WorkTimeItem(View itemView, Action<int> setSelectedDay) : base(itemView)
        {
            Container = itemView.FindViewById<LinearLayout>(Resource.Id.dayContainer);
            DayLabel = itemView.FindViewById<TextView>(Resource.Id.dayOfWeekLabel);
            DayDivider = itemView.FindViewById<View>(Resource.Id.dayTimeDivider);
            StartDayTimeLabel = itemView.FindViewById<TextView>(Resource.Id.startDayTimeLabel);
            EndDayTimeLabel = itemView.FindViewById<TextView>(Resource.Id.endDayTimeLabel);
            CircleDivider = itemView.FindViewById<View>(Resource.Id.circleDivider);
            StartBreakTimeLabel = itemView.FindViewById<TextView>(Resource.Id.startBreakTimeLabel);
            EndBreakTimeLabel = itemView.FindViewById<TextView>(Resource.Id.endBreakTimeLabel);

            _setSelectedDay = setSelectedDay;

            Container.SetOnTouchListener(this);
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            _setSelectedDay?.Invoke(Position);
            return false;
        }
    }
}
