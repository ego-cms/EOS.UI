

using System;
using System.Threading.Tasks;
using CoreGraphics;
using UIKit;

namespace EOS.UI.iOS.Components
{
    internal class CircleMenuPanGestureAnalyzer
    {
        private const int MinLenght = 30;
        private CGPoint _pointStart;
        private CGPoint _pointEnd;
        private UIView _view;

        public CircleMenuPanGestureAnalyzer(UIView view)
        {
            _view = view;
        }

        public UISwipeGestureRecognizerDirection? GetDirection(UIPanGestureRecognizer recognizer)
        {
            double heightDiff = 0;
            double widthDiff = 0;
            double heightDiffAbs = 0;
            double widthDiffAbs = 0;

            UISwipeGestureRecognizerDirection? verticalDirection = null;
            UISwipeGestureRecognizerDirection? horizontalDirection = null;
            UISwipeGestureRecognizerDirection? generalDirection = null;

            switch (recognizer.State)
            {
                case UIGestureRecognizerState.Began:
                    _pointStart = recognizer.LocationInView(_view);
                    break;
                case UIGestureRecognizerState.Ended:
                    _pointEnd = recognizer.LocationInView(_view);


                    widthDiff = _pointStart.X - _pointEnd.X;
                    widthDiffAbs = Math.Abs(widthDiff);
                    if (widthDiffAbs < MinLenght)
                    {
                        widthDiff = widthDiffAbs = 0;
                    }

                    heightDiff = _pointStart.Y - _pointEnd.Y;
                    heightDiffAbs = Math.Abs(heightDiff);
                    if (heightDiffAbs < MinLenght)
                    {
                        heightDiff = heightDiffAbs = 0;
                    }

                    if (widthDiff != 0)
                    {
                        horizontalDirection = widthDiff < 0 ? UISwipeGestureRecognizerDirection.Right : UISwipeGestureRecognizerDirection.Left;
                    }

                    if (heightDiff != 0)
                    {
                        verticalDirection = heightDiff < 0 ? UISwipeGestureRecognizerDirection.Down : UISwipeGestureRecognizerDirection.Up;
                    }

                    if (widthDiffAbs != heightDiffAbs)
                    {
                        generalDirection = widthDiffAbs > heightDiffAbs ? horizontalDirection : verticalDirection;
                    }
                    break;
            }

            return generalDirection;
        }
    }
}
