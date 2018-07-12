using System;
using System.Collections.Generic;
using Airbnb.Lottie;
using CoreGraphics;
using EOS.UI.Shared.Themes.DataModels;
using UIKit;

namespace EOS.UI.iOS.Components
{
    public class CircleMenu : UIView
    {
        private const int _menuSize = 200;
        private const int _buttonSize = 52;
        private const int _animationPadding = 10;
        
        private const string _openAnimationKey = "Animations/hamburger-open";
        private const string _closeAnimationKey = "Animations/hamburger-close";

        private readonly UIView _rootView;
        private readonly UIView _shadowView;
        private readonly UISwipeGestureRecognizer _leftSwipe;
        private readonly UISwipeGestureRecognizer _rightSwipe;
        private readonly LOTAnimationView _openAnimation;
        private readonly LOTAnimationView _closeAnimation;
        private readonly UIView _animationView;

        private bool _isHamburgerMenuOpen = false;

        private List<CircleMenuButton> _menuButtons = new List<CircleMenuButton>();

        private List<CircleMenuItemModel> _source = new List<CircleMenuItemModel>();
        public List<CircleMenuItemModel> Source
        {
            get => _source;
            set
            {
                if (value.Count < 3 || value.Count > 8)
                    throw new ArgumentException("Source must contain 4-8 elements");
                _source = value;
            }
        }

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
            BackgroundColor = UIColor.LightGray;

            //shadowview init
            _shadowView = new UIView(mainFrame);
            _shadowView.BackgroundColor = UIColor.Black;
            _shadowView.Layer.Opacity = 0.05f;
            _shadowView.Hidden = true;
            _rootView.AddSubview(_shadowView);

            //mainbutton init
            _mainButton = new CircleMenuButton();
            _mainButton.Layer.ShadowColor = UIColor.Black.CGColor;
            _mainButton.Layer.ShadowOffset = new CGSize(0,4);
            _mainButton.Layer.ShadowRadius = 5;
            _mainButton.Layer.ShadowOpacity = 0.2f;
            _mainButton.Frame = new CGRect((Frame.Width - _buttonSize) / 2, (Frame.Height - _buttonSize) / 2, _buttonSize, _buttonSize);
            _mainButton.TouchUpInside += OnMainButtonClicked;
            this.AddSubview(_mainButton);

            //swipe init
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
            
            //animations init
            _animationView = new UIView()
            {
                Frame = new CGRect(_animationPadding,_animationPadding, _buttonSize - 2*_animationPadding , _buttonSize - 2*_animationPadding),
                BackgroundColor = UIColor.Clear
            };
            _openAnimation = LOTAnimationView.AnimationNamed(_openAnimationKey);
            _openAnimation.Hidden = false;
            _openAnimation.Frame = _animationView.Bounds;
            _closeAnimation = LOTAnimationView.AnimationNamed(_closeAnimationKey);
            _closeAnimation.Hidden = true;
            _closeAnimation.Frame = _animationView.Bounds;
            _animationView.AddSubview(_openAnimation);
            _animationView.AddSubview(_closeAnimation);
            _animationView.AddGestureRecognizer(new UITapGestureRecognizer(() => OnMainButtonClicked(this, EventArgs.Empty)));
            
            _mainButton.AddSubview(_animationView);
        }

        public void Attach()
        {
            _rootView.AddSubview(this);
        }

        async void OnMainButtonClicked(object sender, EventArgs e)
        {
            if (_isHamburgerMenuOpen)
            {
                _shadowView.Hidden = true;
                await _closeAnimation.PlayAsync();
                _closeAnimation.Hidden = true;
                _openAnimation.Hidden = false;
                _closeAnimation.Stop();
            }
            else
            {
                _shadowView.Hidden = false;
                await _openAnimation.PlayAsync();
                _openAnimation.Hidden = true;
                _closeAnimation.Hidden = false;
                _openAnimation.Stop();
            }
            _isHamburgerMenuOpen = !_isHamburgerMenuOpen;
        }
    }
}
