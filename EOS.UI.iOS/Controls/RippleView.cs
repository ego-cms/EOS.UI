using System;
using CoreAnimation;
using CoreGraphics;
using EOS.UI.iOS.Extensions;
using UIKit;

namespace EOS.UI.iOS.Controls
{
    public class RippleView: UIView
    {
        private CGPoint _startLocation;

        public RippleView(CGRect frame, CGPoint startLocation, UIColor color): base(frame)
        {
            BackgroundColor = color;
            _startLocation = startLocation;
        }

        public override void Draw(CGRect rect)
        {
            const int side = 25;
            CGRect startRect;
            if (_startLocation == null)
                startRect = new CGRect(_startLocation.X - side / 2, _startLocation.Y - side / 2, side, side);
            else
                startRect = new CGRect(rect.Width / 2 - side / 2, rect.Height / 2 - side / 2, side, side);
            
            var path = UIBezierPath.FromRoundedRect(startRect, startRect.Width / 2);
            var layer = new CAShapeLayer();
            layer.Path = path.CGPath;
            this.Layer.Mask = layer;
        }
    }
}
