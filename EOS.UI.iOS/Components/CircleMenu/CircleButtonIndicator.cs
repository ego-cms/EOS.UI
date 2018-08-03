using System;
using CoreGraphics;
using EOS.UI.iOS.Extensions;
using EOS.UI.Shared.Themes.Extensions;
using UIKit;

namespace EOS.UI.iOS.Components
{
    public class CircleButtonIndicator : UIView
    {
        internal const int Size = 6;

        private CGPoint _position;
        internal CGPoint Position
        {
            get => _position;
            set
            {
                _position = value;
                Frame = new CGRect(value.X, value.Y, CircleButtonIndicator.Size, CircleButtonIndicator.Size);
            }
        }

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
        
        internal void ResetPosition()
        {
            Position = _position;
        }
    }
}
