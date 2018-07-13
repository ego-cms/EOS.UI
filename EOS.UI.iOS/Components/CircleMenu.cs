using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Airbnb.Lottie;
using CoreAnimation;
using CoreGraphics;
using EOS.UI.iOS.Extensions;
using EOS.UI.Shared.Themes.DataModels;
using Foundation;
using UIKit;

namespace EOS.UI.iOS.Components
{
    public class CircleMenu : UIView
    {
        private const int _minimumCountOfElements = 3;
        private const int _maximumCountOfElements = 8;
        private const int _menuSize = 300;
        private const int _mainButtonSize = 52;
        private const int _animationPadding = 10;

        private const string _openAnimationKey = "Animations/hamburger-open";
        private const string _closeAnimationKey = "Animations/hamburger-close";

        private readonly UIView _rootView;
        private readonly UIView _shadowView;
        private readonly UISwipeGestureRecognizer _leftSwipe;
        private readonly UISwipeGestureRecognizer _rightSwipe;
        private readonly LOTAnimationView _openAnimation;
        private readonly LOTAnimationView _closeAnimation;
        //view for lotty animation on mainbutton
        private readonly UIView _animationView;
        //view for circle of menu buttons
        private readonly UIView _menuButtonsView;

        private bool _isHamburgerMenuOpen = false;

        private List<CircleMenuButton> _menuButtons = new List<CircleMenuButton>();

        private List<CircleMenuItemModel> _source = new List<CircleMenuItemModel>();
        public List<CircleMenuItemModel> Source
        {
            get => _source;
            set
            {
                if (value.Count < _minimumCountOfElements || value.Count > _maximumCountOfElements)
                    throw new ArgumentException($"Source must contain {_minimumCountOfElements}-{_maximumCountOfElements} elements");
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
            _mainButton.Layer.ShadowOffset = new CGSize(0, 4);
            _mainButton.Layer.ShadowRadius = 5;
            _mainButton.Layer.ShadowOpacity = 0.2f;
            _mainButton.Frame = new CGRect((Frame.Width - _mainButtonSize) / 2, (Frame.Height - _mainButtonSize) / 2, _mainButtonSize, _mainButtonSize);
            _mainButton.TouchUpInside += OnMainButtonClicked;

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
                Frame = new CGRect(_animationPadding, _animationPadding, _mainButtonSize - 2 * _animationPadding, _mainButtonSize - 2 * _animationPadding),
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

            _menuButtonsView = new UIView()
            {
                Frame = new CGRect(20,20, this.Frame.Width - 40, this.Frame.Height - 40),
                BackgroundColor = UIColor.DarkGray
            };
            AddSubview(_menuButtonsView);
            
            AddSubview(_mainButton);
        }

        public void Attach()
        {
            _rootView.AddSubview(this);
            CreateMenuButtons();
        }

        async void OnMainButtonClicked(object sender, EventArgs e)
        {
            if (_isHamburgerMenuOpen)
            {
                _shadowView.Hidden = true;
                HideMenuButtons();
                await _closeAnimation.PlayAsync();
                _closeAnimation.Hidden = true;
                _openAnimation.Hidden = false;
                _closeAnimation.Stop();
            }
            else
            {
                _shadowView.Hidden = false;
                ShowMenuButtons();
                await _openAnimation.PlayAsync();
                _openAnimation.Hidden = true;
                _closeAnimation.Hidden = false;
                _openAnimation.Stop();
            }
            _isHamburgerMenuOpen = !_isHamburgerMenuOpen;
        }

        void CreateMenuButtons()
        {
            double radius = 80;
            double startAngle = -0.61;//35c
            double endAngle = 6.28 + startAngle;
            double diff = (endAngle - startAngle) / 8;

            var index = 0;
            foreach (var model in Source)
            {
                var x = Math.Cos(startAngle) * radius + _menuButtonsView.Frame.Width / 2 - _mainButtonSize / 2;
                var y = Math.Sin(startAngle) * radius + _menuButtonsView.Frame.Height / 2 - _mainButtonSize / 2;

                var menuButton = new CircleMenuButton()
                {
                    TintColor = UIColor.Black,
                    BackgroundColor = UIColor.White,
                    Frame = new CGRect((nfloat)x, (nfloat)y, _mainButtonSize, _mainButtonSize),
                    Hidden = true,
                    //Alpha = 0.0f
                };
                //menuButton.SetImage(model.ImageSource, UIControlState.Normal);
                ///TODO need to remove
                menuButton.SetTitle(index.ToString(), UIControlState.Normal);
                menuButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
                ++index;
                
                _menuButtonsView.InsertSubview(menuButton, 0);
                _menuButtons.Add(menuButton);
                startAngle += diff;

            }
        }

        void ShowMenuButtons()
        {
            UIView.Animate(0.5, () =>
            {
                for (int i = 0; i < _minimumCountOfElements; ++i)
                {
                    _menuButtons[i].Hidden = false;
                }
                _menuButtonsView.Transform = CGAffineTransform.MakeRotation(3.49f);
            }, () => 
            {
                _menuButtons.ForEach(b => b.Hidden = false);
            });
        }
        
        void HideMenuButtons()
        {
            UIView.Animate(0.5, () =>
            {
                if (_source.Count > _minimumCountOfElements + 1)
                {
                    for (int i = _minimumCountOfElements + 1; i < _source.Count; ++i)
                    {
                        _menuButtons[i].Hidden = true;
                    }
                }
                
                _menuButtonsView.Transform = CGAffineTransform.MakeRotation(0);
            }, () => 
            {
                _menuButtons.ForEach(b => b.Hidden = true);
            });
        }


        void SetButtonVisibility()
        {
            foreach (var button in _menuButtons)
            {

            }
        }
    }
}
