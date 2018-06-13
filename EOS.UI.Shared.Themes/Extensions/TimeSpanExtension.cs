using System;
using System.Collections.Generic;
using System.Text;

namespace EOS.UI.Shared.Themes.Extensions
{
    public static class TimeSpanExtension
    {
        public static string ToShortString(this TimeSpan time)
        {
            const string mask = @"hh\:mm";
            return time.ToString(mask);
        }
    }
}
