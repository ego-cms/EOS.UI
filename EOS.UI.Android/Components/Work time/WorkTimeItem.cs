using System;
using Android.Support.V7.Widget;
using Android.Views;

namespace EOS.UI.Android.Components
{
    internal class WorkTimeItem : RecyclerView.ViewHolder, View.IOnTouchListener
    {
        public EOSWorkTimeContainer Container { get; private set; }
        public EOSDayLabel DayLabel { get; private set; }
        public EOSDayDivider DayDivider { get; private set; }
        public EOSTimeLabel StartDayTimeLabel { get; private set; }
        public EOSTimeLabel EndDayTimeLabel { get; private set; }
        public EOSDayCircleDivider CircleDivider { get; private set; }
        public EOSTimeLabel StartBreakTimeLabel { get; private set; }
        public EOSTimeLabel EndBreakTimeLabel { get; private set; }

        private Action<int> _setSelectedDay;

        public WorkTimeItem(View itemView, Action<int> setSelectedDay) : base(itemView)
        {
            Container = itemView.FindViewById<EOSWorkTimeContainer>(Resource.Id.dayContainer);
            DayLabel = itemView.FindViewById<EOSDayLabel>(Resource.Id.dayOfWeekLabel);
            DayDivider = itemView.FindViewById<EOSDayDivider>(Resource.Id.dayTimeDivider);
            StartDayTimeLabel = itemView.FindViewById<EOSTimeLabel>(Resource.Id.startDayTimeLabel);
            EndDayTimeLabel = itemView.FindViewById<EOSTimeLabel>(Resource.Id.endDayTimeLabel);
            CircleDivider = itemView.FindViewById<EOSDayCircleDivider>(Resource.Id.circleDivider);
            StartBreakTimeLabel = itemView.FindViewById<EOSTimeLabel>(Resource.Id.startBreakTimeLabel);
            EndBreakTimeLabel = itemView.FindViewById<EOSTimeLabel>(Resource.Id.endBreakTimeLabel);

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
