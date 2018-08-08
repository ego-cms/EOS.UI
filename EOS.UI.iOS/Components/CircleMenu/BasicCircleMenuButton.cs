using System;
using CoreGraphics;
using UIKit;

namespace EOS.UI.iOS.Components
{
    public class BasicCircleMenuButton : UIButton
    {

        bool _lock;
        internal bool Lock
        {
            get => _lock;
            set
            {
                _lock = value;
                UserInteractionEnabled = !_lock;
            }
        }
        
        public BasicCircleMenuButton(CGRect frame) : base(frame)
        {
            Initalize();
        }

        public BasicCircleMenuButton()
        {
            Initalize();
        }

        private void Initalize()
        {
            Layer.ShadowColor = UIColor.Black.CGColor;
            Layer.ShadowOffset = new CGSize(0, 6);
            Layer.ShadowRadius = 6;
            Layer.ShadowOpacity = 0.24f;
        }

        public override void MovedToSuperview()
        {
            base.MovedToSuperview();
            Layer.CornerRadius = Frame.Height / 2;
        }
    }
}
