using System;
using System.Collections.Generic;
using System.Linq;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Helpers;

namespace EOS.UI.Android.Helpers
{
    public static class Extentions
    {
        public static List<WorkTimeCalendarItem> SortByDayOfWeek(this List<WorkTimeCalendarItem> list, WeekStartEnum firstDayOfWeek)
        {
            list.Sort(delegate (WorkTimeCalendarItem firstDay, WorkTimeCalendarItem lastDay)
            {
                if(firstDay.WeekDay == lastDay.WeekDay)
                    return 0;
                else
                    return (int)firstDay.WeekDay > (int)lastDay.WeekDay ? 1 : -1;
            });
            list.SetFirstDayOfWeek(firstDayOfWeek);
            return list;
        }

        public static List<WorkTimeCalendarItem> SetFirstDayOfWeek(this List<WorkTimeCalendarItem> list, WeekStartEnum firstDayOfWeek)
        {
            var monday = list.FirstOrDefault(item => item.WeekDay == DayOfWeek.Monday);
            var sunday = list.FirstOrDefault(item => item.WeekDay == DayOfWeek.Sunday);

            list.Remove(monday);
            list.Remove(sunday);

            if(firstDayOfWeek == WeekStartEnum.Monday)
            {
                list.Insert(0, monday);
                list.Insert(list.Count - 1, sunday);
            }
            else
            {
                list.Insert(0, sunday);
                list.Insert(1, monday);
            }
            return list;
        }

        public static string GetString(this TimeSpan timeSpan)
        {
            return timeSpan.ToString(@"hh\:mm");
        }
    }
}
