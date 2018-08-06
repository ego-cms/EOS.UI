using System;
using System.Collections.Generic;
using CoreGraphics;
using EOS.UI.iOS.Extensions;
using EOS.UI.Shared.Themes.DataModels;
using UIKit;

namespace EOS.UI.iOS.Components
{
    public class CircleMenuButton : UIButton
    {
        internal const int Size = 52;

        private const int _padding = 5;
        private List<CGPoint> _positions;

        private CircleMenuItemModel _model;
        internal CircleMenuItemModel Model
        {
            get => _model;
            set
            {
                _model = value;
                if (Indicator != null)
                {
                    Indicator.Hidden = (!value?.HasChildren) ?? true;
                }

                //TODO need to remove after develop
                if (value != null)
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
                if (value != null)
                {
                    Frame = new CGRect(value.X, value.Y, CircleMenuMainButton.Size, CircleMenuMainButton.Size);
                    var positionIndex = _positions.IndexOf(_position);
                    UserInteractionEnabled = PositionIndex != 4 && positionIndex != 0;
                }
            }
        }

        internal int PositionIndex
        {
            get => _positions.IndexOf(Position);
        }

        public CircleMenuButton()
        {
            Initialize();
        }

        public CircleMenuButton(CGRect frame) : base(frame)
        {
            Initialize();
        }

        public CircleMenuButton(List<CGPoint> positions)
        {
            _positions = positions;
            Initialize();
        }

        private void Initialize()
        {
            //TODO need to remove
            TintColor = UIColor.Black;
            BackgroundColor = UIColor.White;
            SetTitleColor(UIColor.Black, UIControlState.Normal);

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

        internal void ResetPosition()
        {
            Position = _position;
        }
    }
}
