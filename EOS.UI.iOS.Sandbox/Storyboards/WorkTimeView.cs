using CoreGraphics;
using EOS.UI.iOS.CollectionViewSources;
using EOS.UI.iOS.Models;
using EOS.UI.iOS.Sandbox.Storyboards;
using EOS.UI.Shared.Themes.DataModels;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace EOS.UI.iOS.Sandbox
{
    public partial class WorkTimeView : BaseViewController
    {
        public const string Identifier = "WorkTimeView";

        public WorkTimeView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var dataSource = CreateSchedule();
            var source = new WorkTimeCalendarCollectionSource(workTimeCollection);
            //source.CalendarModel.DayTextSize = 25;
            //source.CalendarModel.TitleTextSize = 35;
            //source.CalendarModel.DayTextColor = UIColor.Blue;
            //source.CalendarModel.TitleColor = UIColor.Green;
            //source.CalendarModel.CurrentDayBackgroundColor = UIColor.Yellow;
            source.CalendarModel.Items = dataSource;
            source.CalendarModel.WeekStart = DayOfWeek.Sunday;
            workTimeCollection.Source = source;
        }

        private List<WorkTimeCalendarItem> CreateSchedule()
        {
            var schedule = new List<WorkTimeCalendarItem>();
            for (int i = 0; i < 7; ++i)
            {
                var day = new WorkTimeCalendarItem();
                day.WeekDay = (DayOfWeek) i;
                if (i == 0 || i == 6)
                {
                    day.IsDayOff = true;
                    schedule.Add(day);
                    continue;
                }

                day.StartTime = TimeSpan.FromHours(8);
                day.EndTime = TimeSpan.FromHours(18);
                day.BreakStartTime = TimeSpan.FromHours(13);
                day.BreakEndTime = TimeSpan.FromHours(14);
                schedule.Add(day);
            }

            return schedule;
        }
    }
}