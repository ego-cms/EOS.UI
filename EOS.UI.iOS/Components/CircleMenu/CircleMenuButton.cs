using System;
using CoreGraphics;
using EOS.UI.iOS.Extensions;
using EOS.UI.Shared.Themes.DataModels;
using UIKit;

namespace EOS.UI.iOS.Components
{
    public class CircleMenuButton : UIButton
    {
        private const int _padding = 5;

        private CircleMenuItemModel _model;
        internal CircleMenuItemModel Model 
        {
            get => _model;
            set
            {
                _model = value;
                Indicator.Hidden = value?.HasChildren ?? true;
                //TODO need to remove after develop
                if(value != null)
                {
                    SetTitle(value.Id.ToString(), UIControlState.Normal);
                }
            }
        }
        
        internal CircleButtonIndicator Indicator { get; set; }

        private CGPoint _position;
        internal CGPoint Position
        {
            get => _position;
            set
            {
                _position = value;
                if(value != null)
                {
                    Frame = Frame.ResizeRect(x: value.X, y: value.Y);
                }
            }
        }
        
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
