using System;
using System.Collections.Generic;
using CoreGraphics;
using EOS.UI.iOS.Extensions;
using EOS.UI.Shared.Themes.DataModels;
using UIKit;

namespace EOS.UI.iOS.Components
{
    public class CircleMenuButton : BasicCircleMenuButton
    {
        private bool _isClicked;
        internal const int Size = 52;

        private const int _padding = 14;
        private List<CGPoint> _positions;

        private CircleMenuItemModel _model;
        internal CircleMenuItemModel Model
        {
            get => _model;
            set
            {
                _model = value;
                if (_model != null)
                {
                    SetImage(_model.ImageSource, UIControlState.Normal);
                    Hidden = false;
                }
                else
                {
                    Hidden = true;
                }
                if (Indicator != null)
                {
                    Indicator.Hidden = (!value?.HasChildren) ?? true;
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
                    Frame = new CGRect(value.X, value.Y, CircleMenuButton.Size, CircleMenuButton.Size);
                    var positionIndex = _positions.IndexOf(_position);
                    UserInteractionEnabled = PositionIndex != 4 && positionIndex != 0;
                }
            }
        }


        private UIColor _focusedBackgroundColor;
        internal UIColor FocusedBackgroundColor
        {
            get => _focusedBackgroundColor;
            set
            {
                _focusedBackgroundColor = value;
            }
        }

        private UIColor _unfocusedBackgroundColor;
        internal UIColor UnfocusedBackgroundColor
        {
            get => _unfocusedBackgroundColor;
            set
            {
                _unfocusedBackgroundColor = value;
                if (!_isClicked)
                    BackgroundColor = _unfocusedBackgroundColor;
            }
        }

        private UIColor _focusedIconColor;
        internal UIColor FocusedIconColor
        {
            get => _focusedIconColor;
            set
            {
                _focusedIconColor = value;
            }
        }

        private UIColor _unfocusedIconColor;
        internal UIColor UnfocusedIconColor
        {
            get => _unfocusedIconColor;
            set
            {
                _unfocusedIconColor = value;
                ImageView.TintColor = _unfocusedIconColor;
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
            SetTitleColor(UIColor.Black, UIControlState.Normal);

            ImageEdgeInsets = new UIEdgeInsets(_padding, _padding, _padding, _padding);
            BackgroundColor = UIColor.White;
            TouchUpInside += OnClicked;
        }

        internal void ResetPosition()
        {
            Position = _position;
        }

        void OnClicked(object sender, EventArgs e)
        {
            if (!Model.HasChildren)
                return;
            BackgroundColor = _isClicked ? _unfocusedBackgroundColor : _focusedBackgroundColor;
            ImageView.TintColor = _isClicked ? _unfocusedIconColor : _focusedIconColor;
            _isClicked = !_isClicked;
        }
    }
}
