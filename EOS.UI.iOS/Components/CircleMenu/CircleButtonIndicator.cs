using System;
using CoreGraphics;
using EOS.UI.Shared.Themes.Extensions;
using UIKit;

namespace EOS.UI.iOS.Components
{
    public class CircleButtonIndicator: UIView
    {
        public const int Size = 6;
        
        public CircleButtonIndicator(CGRect frame) : base(frame)
        {
            Initialize();
        }

        public CircleButtonIndicator()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            BackgroundColor = ColorExtension.FromHex("#3C6DF0");
            Layer.ShadowColor = UIColor.Black.CGColor;
            Hidden = true;
        }
        
        public override void MovedToSuperview()
        {
            base.MovedToSuperview();
            Layer.CornerRadius = Frame.Height / 2;
        }
    }
}
