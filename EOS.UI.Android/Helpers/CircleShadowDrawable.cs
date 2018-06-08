using System;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace EOS.UI.Android.Helpers
{
    //When blurring is added to the shadow there a gaussian bluring added to the image
    //In this implementation used 2nd degree polynom formula to calculate alpha for blurring lim->0
    //To calculate blurring lim->255 we just do bLim255 = 255 - bLim0. 
    public class CircleShadowDrawable : Drawable
    {
        private ShadowConfig _config;
        private CircleShadowState _circleShadowState;
        private bool _mutated;
        //Number of 'circles' for blurring. Depends on blur value in points. And device pixel density.
        private int _iterations;
        Paint _paint = new Paint();
        private IDictionary<int, Color> _colors = new Dictionary<int, Color>();

        public override int Opacity => 255;

        public CircleShadowDrawable(ShadowConfig config)
        {
            _circleShadowState = new CircleShadowState(config);
            _config = config;
            _iterations = (int)Helpers.DpToPx(_config.Blur);

            CalculateColors();
        }

        private void CalculateColors()
        {
            for (int i = 0; i <= _iterations; i++)
            {
                var a = GetAlpha(() => GetEquationX(i, _iterations));
                _colors.Add(i, GetColorWithoutAlpha(a, _config.Color));
                //formula doesn't work well with 'inside' blurring, just invert indexes and alpha values
                if (i == 0)
                    continue;
                
                _colors.Add(i * -1, GetColorWithoutAlpha(255 - a, _config.Color));
            }
        }

        public override void Draw(Canvas canvas)
        {
            _paint.SetStyle(Paint.Style.Fill);
            DrawShadowsOutsideBackground(canvas, _paint, _iterations);
            DrawShadowsBehindBackground(canvas, _paint, _iterations);
        }

        private Color GetColorWithoutAlpha(int alpha, Color color)
        {
            if (alpha == 255)
                return color;

            var r = color.R * (alpha / (float)255) + 255 * (1 - (alpha / (float)255));
            var g = color.G * (alpha / (float)255) + 255 * (1 - (alpha / (float)255));
            var b = color.B * (alpha / (float)255) + 255 * (1 - (alpha / (float)255));
            return new Color((byte)r, (byte)g, (byte)b);
        }

        private void DrawShadowsBehindBackground(Canvas canvas, Paint p, int iterations)
        {
            for (int i = iterations + 1; i < canvas.Width / 2; i++)
            {
                //'Inside' shadow will be with negative index
                p.Color= GetColorValue((i - iterations) * -1);
                if (p.Color == _config.Color)
                {
                    DrawCircle(canvas, p, i);
                    break;
                }
                DrawCircle(canvas, p, i);
            }
        }

        private void DrawCircle(Canvas canvas, Paint p, int i)
        {
            var radius = canvas.Width / 2 - i;
            canvas.DrawCircle(canvas.Width / 2, canvas.Width / 2, radius, p);
        }

        private void DrawShadowsOutsideBackground(Canvas canvas, Paint p, int iterations)
        {
            for (int i = iterations; i > 0; i--)
            {
                p.Color = _colors[i];
                var radius = canvas.Width / 2 - (iterations - i);

                canvas.DrawCircle(canvas.Width / 2, canvas.Width / 2, radius, p);
            }
        }

        private Color GetColorValue(int i)
        {
            if (_colors.ContainsKey(i))
            {
                return _colors[i];
            }
            return _config.Color;
        }

        private byte GetAlpha(Func<float> getXFunc)
        {
            // Second degree polynom - y(x) = 127 + (-4.56 * x ) + 0.0411*x^2
            // Can be substituted with 10th degree polynom
            var x = getXFunc();
            var alpha = 127 + (-4.56 * x) + 0.0411 * Math.Pow(x, 2);
            return (byte)(alpha > 255 ? 255 : Math.Round(alpha));
        }

        public override void SetAlpha(int alpha)
        {
        }

        public override void SetColorFilter(ColorFilter colorFilter)
        {
        }

        float GetEquationX(int i, int total)
        {
            return i * 50 / (float)total;
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
