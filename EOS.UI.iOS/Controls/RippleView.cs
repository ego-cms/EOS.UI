using System;
using CoreAnimation;
using CoreGraphics;
using EOS.UI.iOS.Extensions;
using UIKit;

namespace EOS.UI.iOS.Controls
{
    public class RippleView: UIView
    {

        public RippleView(CGRect frame, UIColor color): base(frame)
        {
            BackgroundColor = color;
        }

        public override void Draw(CGRect rect)
        {
            const int side = 25;
            var newRect = new CGRect(rect.Width/2 - side/2, rect.Height/2 - side/2, side, side);
            var path = UIBezierPath.FromRoundedRect(newRect, newRect.Width / 2);
            var layer = new CAShapeLayer();
            layer.Path = path.CGPath;
            this.Layer.Mask = layer;
        }
    }
}
