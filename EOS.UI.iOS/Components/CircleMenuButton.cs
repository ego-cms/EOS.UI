using System;
using CoreGraphics;
using UIKit;

namespace EOS.UI.iOS.Components
{
    public class CircleMenuButton : UIButton
    {
        private const int _padding = 5;
        private const float _startScale = 0.85f;
        private const float _endScale = 1.0f;
        private const double _animationDuration = 0.1;

        public CircleMenuButton()
        {
            var image = UIImage.FromBundle("icHamburger");
            SetImage(image, UIControlState.Normal);
            ImageEdgeInsets = new UIEdgeInsets(_padding, _padding, _padding, _padding);
            BackgroundColor = UIColor.White;
            
            TouchDown += (sender, e) =>
            {
                UIView.Animate(_animationDuration, () =>
                {
                    Transform = CGAffineTransform.MakeScale(_startScale, _startScale);
                });
            };

            TouchUpInside += (sender, e) =>
            {
                UIView.Animate(_animationDuration, () =>
                {
                    Transform = CGAffineTransform.MakeScale(_endScale, _endScale);
                });
            };
        }

        public override void MovedToSuperview()
        {
            base.MovedToSuperview();
            Layer.CornerRadius = Frame.Height / 2;
        }
    }
}
