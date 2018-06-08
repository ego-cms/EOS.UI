using CoreAnimation;
using CoreGraphics;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using System;
using UIFrameworks.Shared.Themes.Helpers;
using UIFrameworks.Shared.Themes.Interfaces;
using UIKit;

namespace EOS.UI.iOS
{
    public partial class CircleProgress : UIView, IEOSThemeControl
    {
        private bool _isRunnung;
        private const int _lineWidth = 3;
        private const int _radius = 14;
        private readonly nfloat _startAngle = 0f;
        private readonly nfloat _rotatienAngle = -1.57f;
        private readonly nfloat _360angle = 6.28319f;
        private const string _zeroPercents = "0 %";
        private CAShapeLayer _circleLayer;
        private CAShapeLayer _fillCircleLayer;

        public event EventHandler Started;
        public event EventHandler Stopped;
        public event EventHandler Finished;

        public bool IsEOSCustomizationIgnored { get; private set; }

        private int _progress;
        public int Progress
        {
            get => _progress;
            set
            {
                if (value > 100)
                    return;

                _progress = value;
                InvokeOnMainThread(() =>
                {
                    if (imageView.Hidden == false)
                        imageView.Hidden = true;
                    percentLabel.Text = $"{_progress.ToString()} %";
                    RedrawCircle();
                    if (_progress == 100)
                    {
                        ShowCheckmark();
                    }
                });
            }
        }

        private UIColor _color;
        public UIColor Color
        {
            get => _color;
            set
            {
                _color = value;
                IsEOSCustomizationIgnored = true;
                stopView.BackgroundColor = _color;
                percentLabel.TextColor = _color;
                if (_circleLayer != null)
                    _circleLayer.StrokeColor = _color.CGColor;
            }
        }

        private UIColor _alternativeColor;
        public UIColor AlternativeColor
        {
            get => _alternativeColor;
            set
            {
                _alternativeColor = value;
                IsEOSCustomizationIgnored = true;
                imageView.BackgroundColor = _alternativeColor;
            }
        }

        private UIColor _fillColor;
        public UIColor FillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                IsEOSCustomizationIgnored = true;
                if (_fillCircleLayer != null)
                    _fillCircleLayer.StrokeColor = _fillColor.CGColor;
            }
        }

        private bool _showProgress;
        public bool ShowProgress
        {
            get => _showProgress;
            set
            {
                _showProgress = value;
                IsEOSCustomizationIgnored = true;
                percentLabel.Hidden = !_showProgress;
            }
        }

        private UIFont _font;
        public UIFont Font
        {
            get => _font;
            set
            {
                _font = value.WithSize(TextSize);
                IsEOSCustomizationIgnored = true;
                percentLabel.Font = _font;
            }
        }

        private float _textSize;
        public float TextSize
        {
            get => _textSize;
            set
            {
                _textSize = value;
                IsEOSCustomizationIgnored = true;
                percentLabel.Font = Font.WithSize(_textSize);
            }
        }

        public CircleProgress(IntPtr handle) : base(handle)
        {
        }

        public static CircleProgress Create()
        {
            var array = NSBundle.MainBundle.LoadNib(nameof(CircleProgress), null, null);
            var view = array.GetItem<CircleProgress>(0);
            view.Initalize();
            return view;
        }

        public IEOSThemeProvider GetThemeProvider()
        {
            return EOSThemeProvider.Instance;
        }

        public void ResetCustomization()
        {
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public void UpdateAppearance()
        {
            if (!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                Color = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                AlternativeColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                ShowProgress = provider.GetEOSProperty<bool>(this, EOSConstants.CircleProgressShown);
                Font = provider.GetEOSProperty<UIFont>(this, EOSConstants.Font);
                TextSize = provider.GetEOSProperty<int>(this, EOSConstants.TextSize);
                FillColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor4);
                IsEOSCustomizationIgnored = false;
            }
        }

        public IEOSStyle GetCurrentEOSStyle()
        {
            return null;
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {

        }

        private void Initalize()
        {
            circleView.AddGestureRecognizer(new UITapGestureRecognizer((obj) =>
            {
                if (!_isRunnung)
                {
                    Started?.Invoke(this, EventArgs.Empty);
                    _isRunnung = true;
                }
                else
                {
                    Stopped?.Invoke(this, EventArgs.Empty);
                    _isRunnung = false;
                }
            }));
            UpdateAppearance();
            InitCircles();
            circleView.Layer.CornerRadius = circleView.Frame.Height / 2;
            imageView.Layer.CornerRadius = circleView.Frame.Height / 2;
            imageView.Hidden = true;
            percentLabel.Text = _zeroPercents;
        }

        private void InitCircles()
        {
            _circleLayer = new CAShapeLayer();
            _circleLayer.FillColor = UIColor.Clear.CGColor;
            _circleLayer.StrokeColor = Color.CGColor;
            _circleLayer.LineWidth = _lineWidth;
            var center = new CGPoint(circleView.Frame.Width / 2, circleView.Frame.Height / 2);
            var circlePath = new UIBezierPath();
            circlePath.AddArc(center, _radius, _startAngle, _startAngle, true);
            _circleLayer.Path = circlePath.CGPath;
            circleView.Transform = CGAffineTransform.MakeRotation(_rotatienAngle);
            
            _fillCircleLayer = new CAShapeLayer();
            _fillCircleLayer.FillColor = UIColor.Clear.CGColor;
            _fillCircleLayer.StrokeColor = FillColor.CGColor;
            _fillCircleLayer.LineWidth = _lineWidth;
            circlePath = new UIBezierPath();
            circlePath.AddArc(center, _radius, _startAngle, _360angle, true);
            _fillCircleLayer.Path = circlePath.CGPath;
            circleView.Transform = CGAffineTransform.MakeRotation(_rotatienAngle);
            
            circleView.Layer.AddSublayer(_fillCircleLayer);
            circleView.Layer.AddSublayer(_circleLayer);
        }

        private void RedrawCircle()
        {
            var endAngle = _360angle * (Progress / 100.0);
            var circlePath = new UIBezierPath();
            var center = new CGPoint(circleView.Frame.Width / 2, circleView.Frame.Height / 2);
            circlePath.AddArc(center, _radius, _startAngle, (nfloat)endAngle, true);
            _circleLayer.Path = circlePath.CGPath;
        }

        private void ShowCheckmark()
        {
            Finished?.Invoke(this, EventArgs.Empty);
            imageView.Hidden = false;
            _circleLayer.Path = null;
            _isRunnung = false;
        }
    }
}