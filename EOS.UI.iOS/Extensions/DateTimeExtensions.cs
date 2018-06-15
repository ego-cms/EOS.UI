using System;
using Foundation;

namespace EOS.UI.iOS.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDateTime(this NSDate nsDate)
        {
            if (nsDate == null) return new DateTime(); // ?
            var calendar = NSCalendar.CurrentCalendar;
            int year = (int)calendar.GetComponentFromDate(NSCalendarUnit.Year, nsDate);
            int month = (int)calendar.GetComponentFromDate(NSCalendarUnit.Month, nsDate);
            int day = (int)calendar.GetComponentFromDate(NSCalendarUnit.Day, nsDate);
            int hour = (int)calendar.GetComponentFromDate(NSCalendarUnit.Hour, nsDate);
            int minute = (int)calendar.GetComponentFromDate(NSCalendarUnit.Minute, nsDate);
            int second = (int)calendar.GetComponentFromDate(NSCalendarUnit.Second, nsDate);
            int nanosecond = (int)calendar.GetComponentFromDate(NSCalendarUnit.Nanosecond, nsDate);
            int millisecond = (nanosecond / 1000000);
            return new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Unspecified);
        }

        public static NSDate ToNSDate(this DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue) return null; // ?

            DateTime localDateTime = dateTime.ToLocalTime();
            NSDateComponents components = new NSDateComponents();
            components.Year = localDateTime.Year;
            components.Month = localDateTime.Month;
            components.Day = localDateTime.Day;
            components.Hour = localDateTime.Hour;
            components.Minute = localDateTime.Minute;
            components.Second = localDateTime.Second;
            components.Nanosecond = (localDateTime.Millisecond * 1000000);
            return NSCalendar.CurrentCalendar.DateFromComponents(components);
        }
    }
}
