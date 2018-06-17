using System;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using EOS.UI.Shared.Helpers;
using Android.Util;
using Android.Content.Res;
using Android.App;

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
        private float _densityOffsetX;
        private float _densityOffsetY;
        private float _offsetWithBlurX;
        private float _offsetWithBlurY;
        Paint _paint = new Paint();
        private IDictionary<int, Color> _colors = new Dictionary<int, Color>();

        public override int Opacity => 255;

        public CircleShadowDrawable(ShadowConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _paint.StrokeWidth = (float)(Math.Round(Application.Context.Resources.DisplayMetrics.Density) +1);

            _circleShadowState = new CircleShadowState(config);
            _config = config;
            _iterations = (int)Helpers.DpToPx(_config.Blur);
            _densityOffsetX = Helpers.DpToPx(_config.Offset.X);
            _densityOffsetY = Helpers.DpToPx(_config.Offset.Y);
            _offsetWithBlurX = GetOffsetWithBlur(_densityOffsetX, _iterations);
            _offsetWithBlurY = GetOffsetWithBlur(_densityOffsetY, _iterations);

            CalculateColors();
        }

        private void CalculateColors()
        {
            for (int i = 0; i <= _iterations; i++)
            {
                var a = GetAlpha(() => GetEquationX(i, _iterations));
                var c = _config.Color;
                c.A = a;
                _colors.Add(i, c);
                //formula doesn't work well with 'inside' blurring, just invert indexes and alpha values
                if (i == 0)
                    continue;
                
                c = _config.Color;
                c.A = (byte)(255 - a);
                _colors.Add(i * -1, c);
            }
        }

        public override void Draw(Canvas canvas)
        {
            _paint.SetStyle(Paint.Style.Stroke);
            DrawShadowsOutsideBackground(canvas, _paint, _iterations);
            DrawShadowsBehindBackground(canvas, _paint, _iterations);
        }

        private void DrawShadowsBehindBackground(Canvas canvas, Paint p, int iterations)
        {
            var pivotParameter = GetPivotParameter(canvas);
            for (int i = iterations + 1; i < pivotParameter / 2; i++)
            {
                //'Inside' shadow will be with negative index
                p.Color = GetColorValue((i - iterations) * -1);
                //If Alpha = 255 for 'inside' shadow, then we can draw solid circle and break the loop
                if (p.Color == _config.Color)
                {
                    DrawSolidCircle(canvas, i, iterations, p);
                    break;
                }

                var radius = GetInitialWidth(canvas) - (i - iterations);
                DrawCircle(canvas, p, radius);
            }
        }

        private void DrawCircle(Canvas canvas, Paint p, int radius)
        {
            var center = GetCanvasCenter(canvas);
            canvas.DrawCircle(center.X, center.Y, radius, p);
        }

        private void DrawSolidCircle(Canvas canvas, int i, int iterations, Paint p)
        {
            p.SetStyle(Paint.Style.FillAndStroke);
            var radius = GetInitialWidth(canvas) - (i - iterations);
            DrawCircle(canvas, p, radius);
        }

        private void DrawShadowsOutsideBackground(Canvas canvas, Paint p, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                p.Color = _colors[i];
                var radius = GetInitialWidth(canvas) +i;

                DrawCircle(canvas, p, radius);
            }
        }

        private int GetInitialWidth(Canvas canvas)
        {
            return (canvas.Width - (int)_offsetWithBlurX - _iterations * 2)/2;
        }

        private int GetInitialHeight(Canvas canvas)
        {
            return (canvas.Height - (int)_offsetWithBlurY - _iterations * 2) / 2;
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

        float GetEquationX(int i, int total)
        {
            return i * 50 / (float)total;
        }

        private PointF GetCanvasCenter(Canvas canvas)
        {
            float x = 0;
            float y = 0;
            if (_densityOffsetX > 0)
            {
                if (_offsetWithBlurX == 0)
                {
                    x = GetPivotParameter(canvas) / 2 + _offsetWithBlurX;
                }
                else
                {
                    x = GetInitialWidth(canvas) + _densityOffsetX;
                }
            }
            else
            {
                //shouldnt move anything because view itself already moved
                //just draw on the center 
                x = GetPivotParameter(canvas) / 2;
            }
            if (_densityOffsetY > 0)
            {
                y = GetPivotParameter(canvas) / 2 - _offsetWithBlurY;
            }
            else
            {
                if (_offsetWithBlurY == 0)
                {
                    y = GetPivotParameter(canvas) / 2 - _offsetWithBlurY * -1;
                }
                else
                {
                    y = GetInitialHeight(canvas) - _densityOffsetY;
                }
            }
            return new PointF(x, y);
        }

        //Radius depend on canvas width and height.
        //When there is some offset x or offset y, canvas size will have larger width or height
        //To find radius should select lesser parameter
        private int GetPivotParameter(Canvas canvas)
        {
            return Math.Min(canvas.Width, canvas.Height);
        }

        private float GetOffsetWithBlur(float offset, int blur)
        {
            return Math.Abs(offset) > blur ? Math.Abs(offset) - blur : 0;
        }

        #region overrides

        public override void SetAlpha(int alpha)
        {
        }

        public override void SetColorFilter(ColorFilter colorFilter)
        {
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
        #endregion
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
