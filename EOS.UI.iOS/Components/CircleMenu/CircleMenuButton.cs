using System;
using CoreGraphics;
using UIKit;

namespace EOS.UI.iOS.Components
{
    public class CircleMenuButton : UIButton
    {
        private const int _padding = 5;

        public int CircleMenuItemId { get; set; }
        
        public CircleMenuButton(CGRect frame) : base(frame)
        {
            Initialize();
        }

        public CircleMenuButton()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            ImageEdgeInsets = new UIEdgeInsets(_padding, _padding, _padding, _padding);
            BackgroundColor = UIColor.White;
            Layer.ShadowColor = UIColor.Black.CGColor;
            Layer.ShadowOffset = new CGSize(0, 6);
            Layer.ShadowRadius = 12;
            Layer.ShadowOpacity = 0.2f;
        }

        public override void MovedToSuperview()
        {
            base.MovedToSuperview();
            Layer.CornerRadius = Frame.Height / 2;
        }
    }
}
