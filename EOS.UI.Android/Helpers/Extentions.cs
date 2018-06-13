using System;
using System.Collections.Generic;
using System.Linq;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Enums;
using EOS.UI.Shared.Themes.Helpers;

namespace EOS.UI.Android.Helpers
{
    public static class Extentions
    {
        public static string GetString(this TimeSpan timeSpan)
        {
            return timeSpan.ToString(@"hh\:mm");
        }
    }
}
