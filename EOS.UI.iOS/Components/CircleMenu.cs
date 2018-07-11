using System;
using CoreGraphics;
using UIKit;

namespace EOS.UI.iOS.Components
{
    public class CircleMenu : UIView
    {
        private const int _menuSize = 200;
        private const int _buttonSize = 30;

        private readonly UIView _rootView;
        private readonly UIView _shadowView;
        private readonly UISwipeGestureRecognizer _leftSwipe;
        private readonly UISwipeGestureRecognizer _rightSwipe;

        private CircleMenuButton _mainButton;
        public CircleMenuButton MainButton => _mainButton;

        ///TODO: Events for test purpose only, need to remove in future
        public EventHandler LeftSwiped;
        public EventHandler RightSwiped;

        public CircleMenu(UIView rootView)
        {
            _rootView = rootView;

            var mainFrame = UIApplication.SharedApplication.KeyWindow.Frame;
            Frame = new CGRect(mainFrame.Width - _menuSize, mainFrame.Height - _menuSize, _menuSize, _menuSize);
            BackgroundColor = UIColor.Red;

            _shadowView = new UIView(mainFrame);
            _shadowView.BackgroundColor = UIColor.Black;
            _shadowView.Layer.Opacity = 0.05f;
            _shadowView.Hidden = true;
            _rootView.AddSubview(_shadowView);

            _mainButton = new CircleMenuButton();
            _mainButton.Frame = new CGRect((Frame.Width - _buttonSize) / 2, (Frame.Height - _buttonSize) / 2, _buttonSize, _buttonSize);
            _mainButton.TouchUpInside += ToggleShadowView;
            this.AddSubview(_mainButton);

            _leftSwipe = new UISwipeGestureRecognizer(() =>
            {
                LeftSwiped?.Invoke(this, EventArgs.Empty);
            });
            _leftSwipe.Direction = UISwipeGestureRecognizerDirection.Left;
            _rootView.AddGestureRecognizer(_leftSwipe);
            _rightSwipe = new UISwipeGestureRecognizer(() =>
            {
                RightSwiped?.Invoke(this, EventArgs.Empty);
            });
            _rightSwipe.Direction = UISwipeGestureRecognizerDirection.Right;
            _rootView.AddGestureRecognizer(_rightSwipe);
        }

        public void Attach()
        {
            _rootView.AddSubview(this);
        }

        void ToggleShadowView(object sender, EventArgs e)
        {
            _shadowView.Hidden = !_shadowView.Hidden;
        }
    }
}
