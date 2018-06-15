using System;
namespace EOS.UI.Shared.Themes.DataModels
{
    public class WorkTimeCalendarItem
    {
        public DayOfWeek WeekDay { get; set; }

        public string ShortWeekDay => WeekDay.ToString().Substring(0, 2);
        
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public TimeSpan BreakStartTime { get; set; }
        
        public TimeSpan BreakEndTime { get; set; }
        
        public bool HasBreak { get; set; }
        
        public bool IsDayOff { get; set; }
    }
}
