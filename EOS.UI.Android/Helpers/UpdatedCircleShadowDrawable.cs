using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using EOS.UI.Shared.Helpers;

namespace EOS.UI.Android.Helpers
{
    //When blurring is added to the shadow there a gaussian bluring added to the image
    //In this implementation used 2nd degree polynom formula to calculate alpha for blurring lim->0
    //To calculate blurring lim->255 we just do bLim255 = 255 - bLim0. 
    public class UpdatedCircleShadowDrawable : Drawable
    {
        private ShadowConfig _config;
        private UpdatedCircleShadowState _circleShadowState;
        private bool _mutated;
        //Number of 'circles' for blurring. Depends on blur value in points. And device pixel density.
        private int _iterations;
        private float _densityOffsetX;
        private float _densityOffsetY;
        //When offset larger than blur width of canvas larger than when it's lesser 
        private float _offsetWithBlurX;
        private float _offsetWithBlurY;
        private Paint _paint = new Paint();
        private IDictionary<int, Color> _colors = new Dictionary<int, Color>();

        public override int Opacity => 255;

        public UpdatedCircleShadowDrawable(ShadowConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            //Stroke width sgould be device density, to avoid broken pixels
            //_paint.StrokeWidth = 1f;//(float)(Math.Round(Application.Context.Resources.DisplayMetrics.Density));

            _circleShadowState = new UpdatedCircleShadowState(config);
            _config = config;
            _iterations = (int)Helpers.DpToPx(_config.Blur);
            _densityOffsetX = Helpers.DpToPx(_config.Offset.X);
            //Wrong implementation for y offset should be inverted
            //TODO need to fix
            _densityOffsetY = Helpers.DpToPx(_config.Offset.Y) * -1;
            _offsetWithBlurX = ShadowHelpers.GetOffsetWithBlur((int)_densityOffsetX, _iterations);
            _offsetWithBlurY = ShadowHelpers.GetOffsetWithBlur((int)_densityOffsetY, _iterations);
        }

        public override void Draw(Canvas canvas)
        {
            var center = GetCanvasCenter(canvas);
            var oldWidth = ShadowHelpers.GetOldWidth(canvas.Width, (int)_densityOffsetX, _iterations) / 2;
            var radius = oldWidth + _iterations -1;
            CreateAndSetShader(canvas, center, oldWidth, radius);
            canvas.DrawCircle(center.X, center.Y, radius, _paint);
        }

        private void CreateAndSetShader(Canvas canvas, PointF center, int oldWidth, int radius)
        {
            float startGradientPoint = oldWidth - _iterations;
            var colorWith0Alpha = _config.Color;
            colorWith0Alpha.A = 0;
            var startGradientColor = startGradientPoint / radius;
            var shader = new RadialGradient(center.X, center.Y, radius,
                                            new[] { _config.Color.ToArgb(), _config.Color.ToArgb(), colorWith0Alpha.ToArgb() },
                                            new[] { 0f, startGradientColor, 1f },
                                            Shader.TileMode.Clamp);
            _paint.SetShader(shader);
        }

        //Get center point for drawing shadow circle, 
        //Take into account offset, blur, view translations, etc.
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
                    x = ShadowHelpers.GetOldWidth(canvas.Width, (int)_densityOffsetX, _iterations) / 2 + _densityOffsetX;
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
                    y = GetPivotParameter(canvas) / 2;// + _densityOffsetY * -1;
                }
                else
                {
                    y = ShadowHelpers.GetOldWidth(canvas.Height, (int)_densityOffsetY, _iterations) / 2 - _densityOffsetY;
                }
            }
            return new PointF(x, y);
        }

        //Radius depend on canvas width and height.
        //When there is some offset x or offset y, canvas size will have larger width(when x) or height(when y)
        //To find radius should select lesser parameter
        private int GetPivotParameter(Canvas canvas)
        {
            return Math.Min(canvas.Width, canvas.Height);
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
                _circleShadowState = new UpdatedCircleShadowState(_circleShadowState);
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


    class UpdatedCircleShadowState : Drawable.ConstantState
    {
        public ConfigChanges cConfigurations { get; set; }

        public ShadowConfig Config { get; set; }

        public UpdatedCircleShadowState(UpdatedCircleShadowState state)
        {
            Config = state.Config;
            this.cConfigurations = state.cConfigurations;
        }

        public UpdatedCircleShadowState(ShadowConfig config)
        {
            Config = config;
        }

        public override Drawable NewDrawable()
        {
            return new UpdatedCircleShadowDrawable(Config);
        }

        public override ConfigChanges ChangingConfigurations => cConfigurations;
    }
}
