using System;
using Android.App;
using Android.Util;

namespace EOS.UI.Android.Helpers
{
    public static class Helpers
    {
        public static float PxToDp(int px)
        {
            var displayMetrics = Application.Context.Resources.DisplayMetrics;
            return px / ((int)displayMetrics.DensityDpi / (float)DisplayMetricsDensity.Default);
        }

        public static float DpToPx(float dp)
        {
            var displayMetrics = Application.Context.Resources.DisplayMetrics;
            return dp * ((int)displayMetrics.DensityDpi / (float)DisplayMetricsDensity.Default);
        }
    }

    public static class ShadowHelpers
    {
        public static int GetNewWidth(int oldWidth, int offset, int blur)
        {
            return oldWidth + GetOffsetWithBlur(offset, blur) + blur * 2;;
        }

        public static int GetOldWidth(int newWidth, int offset, int blur)
        {
            return (newWidth - (int)offset - blur * 2) / 2;
        }

        //When offset larger than blur width of canvas larger than when it's lesser 
        public static int GetOffsetWithBlur(int offset, int blur)
        {
            return Math.Abs(offset) > blur ? Math.Abs(offset) - blur : 0;
        }
    }
}
