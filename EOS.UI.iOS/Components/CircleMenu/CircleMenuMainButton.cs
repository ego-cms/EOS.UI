using System;
using Airbnb.Lottie;
using CoreGraphics;
using UIKit;

namespace EOS.UI.iOS.Components
{
    public class CircleMenuMainButton: BasicCircleMenuButton
    {
        private const int Size = 52;
        private const int AnimationViewSize = 24;
        private const float _startScale = 0.85f;
        private const float _endScale = 1.0f;
        private const double _scaleDuration = 0.1;
        private const string _openAnimationKey = "Animations/hamburger-open";
        private const string _closeAnimationKey = "Animations/hamburger-close";
        private LOTAnimationView _mainButtonOpenAnimation;
        private LOTAnimationView _mainButtonCloseAnimation;

        internal bool IsOpen { get; private set; }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
            }
        }

        public CircleMenuMainButton(CGRect frame): base(frame)
        {
            Initalize();
        }
        
        public CircleMenuMainButton()
        {
            Initalize();
        }
        
        void Initalize()
        {
            BackgroundColor = UIColor.White;
            var mainButtonAnimationView = new UIView()
            {
                Frame = new CGRect((Size - AnimationViewSize) / 2,
                                  (Size - AnimationViewSize) / 2,
                                  AnimationViewSize,
                                  AnimationViewSize),
                BackgroundColor = UIColor.Clear
            };
            mainButtonAnimationView.Layer.BackgroundColor = UIColor.LightGray.CGColor;
            
            _mainButtonOpenAnimation = LOTAnimationView.AnimationNamed(_openAnimationKey);
            _mainButtonOpenAnimation.Hidden = false;
            _mainButtonOpenAnimation.Frame = mainButtonAnimationView.Bounds;
            _mainButtonCloseAnimation = LOTAnimationView.AnimationNamed(_closeAnimationKey);
            _mainButtonCloseAnimation.Hidden = true;
            _mainButtonCloseAnimation.Frame = mainButtonAnimationView.Bounds;
            mainButtonAnimationView.AddSubview(_mainButtonOpenAnimation);
            mainButtonAnimationView.AddSubview(_mainButtonCloseAnimation);
            mainButtonAnimationView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                this.SendActionForControlEvents(UIControlEvent.TouchUpInside);
            }));
            this.AddSubview(mainButtonAnimationView);
            
            this.TouchUpInside += OnMainButtonClicked;
        }
        
        void AnimateScale()
        {
            UIView.Animate(_scaleDuration, () =>
            {
                Transform = CGAffineTransform.MakeScale(_startScale, _startScale);
            }, () =>
            {
                UIView.Animate(_scaleDuration, () =>
                {
                    Transform = CGAffineTransform.MakeScale(_endScale, _endScale);
                });
            });
        }
        
        async void OnMainButtonClicked(object sender, EventArgs e)
        {
            AnimateScale();
            if (IsOpen)
            {
                await _mainButtonCloseAnimation.PlayAsync();
                _mainButtonCloseAnimation.Hidden = true;
                _mainButtonOpenAnimation.Hidden = false;
                _mainButtonCloseAnimation.Stop();
            }
            else
            {
                await _mainButtonOpenAnimation.PlayAsync();
                _mainButtonOpenAnimation.Hidden = true;
                _mainButtonCloseAnimation.Hidden = false;
                _mainButtonOpenAnimation.Stop();
            }
            IsOpen = !IsOpen;
        }
    }
}
