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
        private IDictionary<int, byte> _alphas = new Dictionary<int, byte>();

        public override int Opacity => 255;

        public CircleShadowDrawable(ShadowConfig config)
        {
            _circleShadowState = new CircleShadowState(config);
            _config = config;
            _iterations = (int)Helpers.DpToPx(_config.Blur);

            CalculateAlphas();
        }

        private void CalculateAlphas()
        {
            for (int i = 0; i <= _iterations; i++)
            {
                var a = GetAlpha(() => GetEquationX(i, _iterations));
                _alphas.Add(i, a);
                //formula doesn't work well with 'inside' blurring, just invert indexes and alpha values
                if (i == 0)
                    continue;
                
                _alphas.Add(i * -1, (byte)(255 - a));
            }
        }

        public override void Draw(Canvas canvas)
        {
            var p = new Paint();
            p.SetStyle(Paint.Style.Stroke);
            p.Dither = true;
            DrawShadowsOutsideBackground(canvas, p, _iterations);
            DrawShadowsBehindBackground(canvas, p, _iterations);
        }

        private void DrawShadowsBehindBackground(Canvas canvas, Paint p, int iterations)
        {
            for (int i = iterations + 1; i < canvas.Width / 2; i++)
            {
                var color = _config.Color;
                //'Inside' shadow will be with negative index
                color.A = GetAlphaValue(i * -1);
                p.Color = color;
                //If Alpha = 255 for 'inside' shadow, then we can draw solid circle and break the loop
                if (color.A >= 255)
                {
                    DrawSolidCircle(canvas, i, p);
                    break;
                }

                var radius = canvas.Width / 2 - i;
                canvas.DrawCircle(canvas.Width / 2, canvas.Width / 2, radius, p);
            }
        }

        private void DrawSolidCircle(Canvas canvas, int i, Paint p)
        {
            p.SetStyle(Paint.Style.FillAndStroke);
            var radius = canvas.Width / 2 - i;
            canvas.DrawCircle(canvas.Width / 2, canvas.Width / 2, radius, p);
        }

        private void DrawShadowsOutsideBackground(Canvas canvas, Paint p, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                var color = _config.Color;
                color.A = _alphas[i];
                p.Color = color;
                var radius = canvas.Width / 2 - (iterations - i);

                canvas.DrawCircle(canvas.Width / 2, canvas.Width / 2, radius, p);
            }
        }

        private byte GetAlphaValue(int i)
        {
            if (_alphas.ContainsKey(i))
            {
                return _alphas[i];
            }
            return (byte)255;
        }

        private byte GetAlpha(Func<float> getXFunc)
        {
            // Second degree polynom - y(x) = 132 + (-4.64 * x ) + 0.0411*x^2
            // Can be substituted with 10th degree polynom
            var alpha = 127 + (-4.56 * getXFunc()) + 0.0411 * Math.Pow(getXFunc(), 2);
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
