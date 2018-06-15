using System;
using System.Collections.Generic;
using System.Linq;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Enums;

namespace EOS.UI.Shared.Themes.Extensions
{
    public static class WorkTimeCalendarExtension
    {
        public static IEnumerable<WorkTimeCalendarItem> SortWeekByFirstDay(this IEnumerable<WorkTimeCalendarItem> items, WeekStartEnum firstDayOfWeek)
        {
            if (firstDayOfWeek == WeekStartEnum.Monday)
            {
                var monday = items.Single(i => i.WeekDay == DayOfWeek.Monday);
                var sunday = items.Single(i => i.WeekDay == DayOfWeek.Sunday);
                items = items.Except(new[] { monday, sunday }).OrderBy(i => i.WeekDay);
                items = items.Prepend(monday);
                items = items.Append(sunday);
            }
            else
            {
                items = items.OrderBy(i => i.WeekDay);
            }
            return items;
        }
    }
}
