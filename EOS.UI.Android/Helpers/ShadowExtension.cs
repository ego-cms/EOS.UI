using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Java.Lang;
using Graphics = Android.Graphics;

namespace EOS.UI.Android.Helpers
{
    public static class ViewShadow
    {
        private const int SHADOW_RADIUS = 4;
        private const int OFFSET_X = 0;
        private const int TOP_DY = 3;
        private static int DEFAULT_SHADOW_COLOR = Color.ParseColor("#4a000000");

        public static void SetElevation(this View view, Color color, float elevation, int dx, int dy, int aplha)
        {
            if (view == null) return;
            ShadowData data = new ShadowData();
            Context context = view.Context.ApplicationContext;
            data.shadowRadius = elevation > 0 ? (int)elevation : SHADOW_RADIUS;
            //data.dx = System.Math.Abs(dip2px(context, OFFSET_X));
            //data.dy = dip2px(context, data.shadowRadius / 6f);
            data.dx = dip2px(context, dx);
            data.dy = dip2px(context, dy);
            data.inner = dip2px(context, data.shadowRadius / 10f);
            data.top = dip2px(context, TOP_DY);
            data.top = data.top < data.dy ? data.top : 0;
            data.color = DEFAULT_SHADOW_COLOR;
            ViewTreeObserver viewTreeObserver = view.ViewTreeObserver;
            GlobalLayoutListener layoutListener = new GlobalLayoutListener(view, data);
            PreDrawListener preDrawListener = new PreDrawListener(view, data);
            viewTreeObserver.AddOnGlobalLayoutListener(layoutListener);
            viewTreeObserver.AddOnPreDrawListener(preDrawListener);
        }

        private class PreDrawListener : Java.Lang.Object, ViewTreeObserver.IOnPreDrawListener
        {

            private View view;
            private ShadowData data;

            public PreDrawListener(View view, ShadowData data)
            {
                this.view = view;
                this.data = data;
            }

            public bool OnPreDraw()
            {
                trackTargetView(view, data);
                return true;
            }
        }

        private static void trackTargetView(View view, ShadowData data)
        {
            if (data.shadow == null || view == null) return;
            int top = view.Top - data.shadowRadius - data.dy;
            int left = view.Left - data.shadowRadius - data.dx;
            top = data.height >= ((ViewGroup)view.Parent).Height ? 0 : top;
            left = data.width >= ((ViewGroup)view.Parent).Width ? 0 : left;
            if (top != data.shadow.TranslationY) data.shadow.TranslationY = top;
            if (left != data.shadow.TranslationX) data.shadow.TranslationX = left;
        }

        private class GlobalLayoutListener : Java.Lang.Object, ViewTreeObserver.IOnGlobalLayoutListener
        {
            private View view;
            private ShadowData data;

            public GlobalLayoutListener(View view, ShadowData data)
            {
                this.view = view;
                this.data = data;
            }

            public void OnGlobalLayout()
            {
                createShadowIfNecessary(view, data);
                trackTargetView(view, data);
            }
        }

        private static void createShadowIfNecessary(View view, ShadowData data)
        {
            if (view.Width == 0 || data.shadow != null) return;
            int width = view.Width + data.shadowRadius * 2 + data.dx * 2;
            int height = view.Height + data.shadowRadius * 2 + data.dy * 2;

            ViewGroup.LayoutParams layoutParams = new ViewGroup.LayoutParams(width, height);
            View vShadow = new View(view.Context);
            vShadow.LayoutParameters = layoutParams;

            data.shadow = vShadow;
            //data.shadow.Alpha = data.alpha;
            data.width = width;
            data.height = height;

            Drawable drawable = view.Background;
            data.cornerRadius = obtainRadius(drawable);

            Bitmap bitmap = createShadowBitmap(
                    width, height,
                    data.cornerRadius,
                    data.shadowRadius,
                    data.dx,
                    data.dy,
                    data.top,
                    data.inner,
                    data.color,
                    Color.Transparent);
            BitmapDrawable bitmapDrawable = new BitmapDrawable(view.Resources, bitmap);

            if (Build.VERSION.SdkInt <= Build.VERSION_CODES.JellyBean)
            {
                vShadow.SetBackgroundDrawable(bitmapDrawable);
            }
            else
            {
                vShadow.Background = bitmapDrawable;
            }
            ViewGroup parent = (ViewGroup)view.Parent;
            parent.AddView(vShadow, parent.IndexOfChild(view));
        }

        private static float[] obtainRadius(Drawable background)
        {
            Drawable currentDrawable = background.Current;
            if (currentDrawable is GradientDrawable)
            {
                if (Build.VERSION.SdkInt >= Build.VERSION_CODES.N)
                {
                    try
                    {
                        float[] radii = ((GradientDrawable)currentDrawable).GetCornerRadii();
                        if (radii == null)
                        {
                            float radius = ((GradientDrawable)currentDrawable).CornerRadius;
                            return new float[] { radius, radius, radius, radius, radius, radius, radius, radius };
                        }
                        else
                        {
                            return radii;
                        }
                    }
                    catch (System.Exception e)
                    {
                        float radius = ((GradientDrawable)currentDrawable).CornerRadius;
                        return new float[] { radius, radius, radius, radius, radius, radius, radius, radius };
                    }
                }
                else
                {
                    try
                    {
                        Class c = Class.ForName("android.graphics.drawable.GradientDrawable$GradientState");
                        var mRadiusArray = c.GetDeclaredField("mRadiusArray");
                        mRadiusArray.Accessible = true;
                        float[] radii = (float[])mRadiusArray.Get(currentDrawable.GetConstantState());
                        if (radii == null)
                        {
                            var mRadius = c.GetDeclaredField("mRadius");
                            mRadius.Accessible = true;
                            float radius = (float)mRadius.Get(currentDrawable.GetConstantState());
                            return new float[] { radius, radius, radius, radius, radius, radius, radius, radius };
                        }
                        else
                        {
                            return radii;
                        }
                    }
                    catch (System.Exception e)
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        private static Bitmap createShadowBitmap(int shadowWidth, int shadowHeight, float[] cornerRadius, float shadowRadius,
                                             float dx, float dy, int top, int inner, int shadowColor, int fillColor)
        {
            Bitmap.Config config = shadowColor == DEFAULT_SHADOW_COLOR ? Bitmap.Config.Alpha8 : Bitmap.Config.Argb8888;
            Bitmap output = Bitmap.CreateBitmap(shadowWidth, shadowHeight, config);
            Canvas canvas = new Canvas(output);
            RectF shadowRect = new RectF(
                    shadowRadius + inner,
                    shadowRadius + System.Math.Abs(dy) - top,
                    shadowWidth - shadowRadius - inner,
                    shadowHeight - shadowRadius);

            shadowRect.Top += dy > 0 ? dy : -dy;
            shadowRect.Bottom -= dy > 0 ? dy : -dy;

            shadowRect.Left += dx > 0 ? dx : -dx;
            shadowRect.Right -= dx > 0 ? dx : -dx;

            Paint shadowPaint = new Paint(PaintFlags.AntiAlias);
            shadowPaint.Color = new Color(fillColor);
            shadowPaint.SetStyle(Paint.Style.Fill);
            shadowPaint.SetShadowLayer(shadowRadius, dx, dy, new Color(shadowColor));
            if (cornerRadius == null)
            {
                canvas.DrawRect(shadowRect, shadowPaint);
            }
            else
            {
                Path path = new Path();
                path.AddRoundRect(shadowRect, cornerRadius, Path.Direction.Cw);
                canvas.DrawPath(path, shadowPaint);
            }
            return output;
        }

        class ShadowData
        {
            public float[] cornerRadius;
            public int shadowRadius;
            public int dx;
            public int dy;
            public int inner;
            public int color;
            public int top;
            public int width;
            public int height;
            public int alpha;
            public View shadow;
        }

        private static int dip2px(Context context, float dpValue)
        {
            var scale = context.Resources.DisplayMetrics.Density;
            return (int)(dpValue * scale + 0.5f);
        }
    }
}
