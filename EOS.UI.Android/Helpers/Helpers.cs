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

        public static float GetScaleFactor()
        {
            var displayMetrics = Application.Context.Resources.DisplayMetrics;
            return (int)displayMetrics.DensityDpi / (float)DisplayMetricsDensity.Default;
        }
    }
}
