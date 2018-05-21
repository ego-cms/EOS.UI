using System;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace EOS.UI.Android.Helpers
{
    public class ShadowViewDrawable: Drawable
    {
        private Paint paint;

        private RectF bounds = new RectF();

        private int width;
        private int height;

        private ShadowProperty shadowProperty;
        private int shadowOffset;

        private RectF drawRect;

        private float rx;
        private float ry;

        public ShadowViewDrawable(ShadowProperty shadowProperty, int color, float rx, float ry)
        {
            this.shadowProperty = shadowProperty;
            shadowOffset = this.shadowProperty.getShadowOffset();

            this.rx = rx;
            this.ry = ry;

            paint = new Paint();
            paint.AntiAlias = true;
            paint.FilterBitmap = true;
            paint.Dither = true;
            paint.SetStyle(Paint.Style.Fill);
            paint.Color = new Color(color);
            paint.SetShadowLayer(shadowProperty.getShadowRadius(), shadowProperty.getShadowDx(), shadowProperty.getShadowDy(), shadowProperty.getShadowColor());

            drawRect = new RectF();
        }

        protected override void OnBoundsChange(Rect bounds)
        {
            base.OnBoundsChange(bounds);
            if (bounds.Right - bounds.Left > 0 && bounds.Bottom - bounds.Top > 0)
            {
                this.bounds.Left = bounds.Left;
                this.bounds.Right = bounds.Right;
                this.bounds.Top = bounds.Top;
                this.bounds.Bottom = bounds.Bottom;
                width = (int)(this.bounds.Right - this.bounds.Left);
                height = (int)(this.bounds.Bottom - this.bounds.Top);

                int shadowSide = shadowProperty.getShadowSide();
                int left = (shadowSide & ShadowProperty.LEFT) == ShadowProperty.LEFT ? shadowOffset : 0;
                int top = (shadowSide & ShadowProperty.TOP) == ShadowProperty.TOP ? shadowOffset : 0;
                int right = width - ((shadowSide & ShadowProperty.RIGHT) == ShadowProperty.RIGHT ? shadowOffset : 0);
                int bottom = height - ((shadowSide & ShadowProperty.BOTTOM) == ShadowProperty.BOTTOM ? shadowOffset : 0);

                drawRect = new RectF(left, top, right, bottom);

                InvalidateSelf();
            }
        }

        private PorterDuffXfermode srcOut = new PorterDuffXfermode(PorterDuff.Mode.SrcOut);

        public override int Opacity => 20;

        public override void Draw(Canvas canvas)
        {
            paint.SetXfermode(null);

            canvas.DrawRoundRect(
                    drawRect,
                    rx, ry,
                    paint
            );

            paint.SetXfermode(srcOut);
            canvas.DrawRoundRect(drawRect, rx, ry, paint);
        }

        public ShadowViewDrawable setColor(int color)
        {
            paint.Color = new Color(color);
            return this;
        }

        public override void SetAlpha(int alpha)
        {
        }

        public override void SetColorFilter(ColorFilter colorFilter)
        {
        }
    }
}
