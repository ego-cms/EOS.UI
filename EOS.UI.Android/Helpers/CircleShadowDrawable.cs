using System;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace EOS.UI.Android.Helpers
{

    public class CircleShadowDrawable : Drawable
    {
        private ShadowConfig _config;
        private CircleShadowState _circleShadowState;
        private bool _mutated;

        public override int Opacity => 255;

        public CircleShadowDrawable(ShadowConfig config)
        {
            _circleShadowState = new CircleShadowState(config);
            _config = config;
        }

        public override void Draw(Canvas canvas)
        {
            var p = new Paint();
            p.SetStyle(Paint.Style.Stroke);
            p.Dither = true;
            var iterations = _config.Blur;
            DrawShadowsOutsideBackground(canvas, p, iterations);
            DrawShadowsBehindBackground(canvas, p, iterations);
        }

        private void DrawShadowsBehindBackground(Canvas canvas, Paint p, int iterations)
        {
            for (int i = iterations + 1; i < canvas.Width / 2; i++)
            {
                var color = _config.Color;
                color.A = GetAlpha(() => GetEquationX(i - iterations, iterations) * -1);
                p.Color = color;
                var radius = canvas.Width / 2 - i;

                canvas.DrawCircle(canvas.Width / 2, canvas.Width / 2, radius, p);
            }
        }

        private void DrawShadowsOutsideBackground(Canvas canvas, Paint p, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                var color = _config.Color;
                color.A = GetAlpha(() => GetEquationX(i, iterations));
                p.Color = color;
                var radius = canvas.Width / 2 - (iterations - i);

                canvas.DrawCircle(canvas.Width / 2, canvas.Width / 2, radius, p);
            }
        }

        private byte GetAlpha(Func<float> getXFunc)
        {
            //color.A = (byte)(128 / (2 + 1.25 * i)); //y(x) = 128/(2+x*1.25) looks good
            var alpha = 127 + (-4.56 * getXFunc()) + 0.0411 * Math.Pow(getXFunc(), 2);// y(x) = 132 + (-4.64 * x ) + 0.0411*x^2
            return (byte)(alpha > 255 ? 255 : alpha);
        }

        public override void SetAlpha(int alpha)
        {
        }

        public override void SetColorFilter(ColorFilter colorFilter)
        {
        }

        float GetEquationX(int i, int total)
        {
            return i * 50 / total;
        }

        public override Drawable Mutate()
        {
            if (!_mutated && base.Mutate() == this)
            {
                _circleShadowState = new CircleShadowState(_circleShadowState);
                _mutated = true;
            }
            return this;
        }

        public override ConstantState GetConstantState()
        {
            _circleShadowState.cConfigurations = ChangingConfigurations;
            return _circleShadowState;
        }
    }


    class CircleShadowState : Drawable.ConstantState
    {
        public ConfigChanges cConfigurations { get; set; }

        public ShadowConfig Config { get; set; }

        public CircleShadowState(CircleShadowState state)
        {
            Config = state.Config;
            this.cConfigurations = state.cConfigurations;
        }

        public CircleShadowState(ShadowConfig config)
        {
            Config = config;
        }

        public override Drawable NewDrawable()
        {
            return new CircleShadowDrawable(Config);
        }

        public override ConfigChanges ChangingConfigurations => cConfigurations;
    }
}
