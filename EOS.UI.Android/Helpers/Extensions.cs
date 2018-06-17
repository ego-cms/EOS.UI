using System;
using Android.Views;

namespace EOS.UI.Android.Helpers
{
    public static class Extensions
    {
        public static void SetLayoutParameters(this View v, int newWidth, int newHeight)
        {
            var lp = v.LayoutParameters;
            lp.Width = newWidth;
            lp.Height = newHeight;
            v.LayoutParameters = lp;
        }
    }
}
