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
        private const int _maximumCountOfElements = 9;
        private const int _visibleCountOfElements = 5;
        private const int _menuSize = 300;

        private const int _radius = 96;
        private readonly nfloat _180degrees = 3.14159f;
        private readonly nfloat _36degrees = 0.628319f;

        private readonly UIView _rootView;
        private readonly UIView _shadowView;
        private readonly UISwipeGestureRecognizer _leftSwipe;
        private readonly UISwipeGestureRecognizer _rightSwipe;

        private readonly CASpringAnimation _menuOpenAnimation;
        private readonly CASpringAnimation _menuCloseAnimation;
        //view for circle of menu buttons
        private readonly UIView _menuButtonsView;

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

        private CircleMenuMainButton _mainButton;
        public CircleMenuMainButton MainButton => _mainButton;

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
            _mainButton = new CircleMenuMainButton();
            _mainButton.Frame = new CGRect(
                (Frame.Width - CircleMenuMainButton.Size) / 2,
                (Frame.Height - CircleMenuMainButton.Size) / 2,
                CircleMenuMainButton.Size, CircleMenuMainButton.Size);
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

            _menuButtonsView = new UIView()
            {
                Frame = new CGRect(20, 20, this.Frame.Width - 40, this.Frame.Height - 40),
                BackgroundColor = UIColor.DarkGray
            };
            AddSubview(_menuButtonsView);

            //animations init
            _menuOpenAnimation = new CASpringAnimation();
            _menuOpenAnimation.KeyPath = "transform.rotation.z";
            _menuOpenAnimation.Duration = 1;
            _menuOpenAnimation.From = new NSNumber(0);
            _menuOpenAnimation.To = new NSNumber(-_180degrees);
            _menuOpenAnimation.RepeatCount = 1;
            _menuOpenAnimation.RemovedOnCompletion = true;
            _menuOpenAnimation.AnimationStarted += (sender, e) => _mainButton.UserInteractionEnabled = false;
            _menuOpenAnimation.AnimationStopped += (s, e) =>
            {
                _menuButtonsView.Transform = CGAffineTransform.MakeRotation(-_180degrees);
                _menuButtons.ForEach(b =>
                {
                    b.Hidden = false;
                    b.Alpha = 1.0f;
                });
                _mainButton.UserInteractionEnabled = true;
            };

            _menuCloseAnimation = new CASpringAnimation();
            _menuCloseAnimation.KeyPath = "transform.rotation.z";
            _menuCloseAnimation.Duration = 1;
            _menuCloseAnimation.From = new NSNumber(-_180degrees);
            _menuCloseAnimation.To = new NSNumber(0f);
            _menuCloseAnimation.RepeatCount = 1;
            _menuCloseAnimation.RemovedOnCompletion = true;
            _menuCloseAnimation.AnimationStarted += (sender, e) => _mainButton.UserInteractionEnabled = false;
            _menuCloseAnimation.AnimationStopped += (s, e) =>
            {
                _menuButtonsView.Transform = CGAffineTransform.MakeRotation(0);
                _menuButtons.ForEach(b =>
                {
                    b.Hidden = true;
                    b.Alpha = 0.0f;
                });
                _mainButton.UserInteractionEnabled = true;
            };

            AddSubview(_mainButton);
        }

        public void Attach()
        {
            _rootView.AddSubview(this);
            CreateMenuButtons();
        }



        void CreateMenuButtons()
        {
            double startAngle = -_36degrees;
            double endAngle = 6.28 + startAngle;
            double diff = (endAngle - startAngle) / _maximumCountOfElements;

            foreach (var model in _source)
            {
                var x = Math.Cos(startAngle) * _radius + _menuButtonsView.Bounds.GetCenterX() - CircleMenuMainButton.Size / 2;
                var y = Math.Sin(startAngle) * _radius + _menuButtonsView.Bounds.GetCenterY() - CircleMenuMainButton.Size / 2;

                var menuButton = new CircleMenuButton()
                {
                    TintColor = UIColor.Black,
                    BackgroundColor = UIColor.White,
                    Frame = new CGRect((nfloat)x, (nfloat)y, CircleMenuMainButton.Size, CircleMenuMainButton.Size),
                    Hidden = true,
                    Alpha = 0.0f
                };
                //menuButton.SetImage(model.ImageSource, UIControlState.Normal);
                ///TODO need to remove
                menuButton.SetTitle(model.Id.ToString(), UIControlState.Normal);
                menuButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
                //menuButton.TouchUpInside += (sender, e) => model.Clicked?.Invoke(menuButton, EventArgs.Empty);

                _menuButtonsView.InsertSubview(menuButton, 0);
                _menuButtons.Add(menuButton);
                startAngle += diff;
            }
        }

        void ShowMenuButtons()
        {
            _menuButtonsView.Layer.AddAnimation(_menuOpenAnimation, null);

            var count = _source.Count >= _visibleCountOfElements ? _visibleCountOfElements : _source.Count;
            UIView.Animate(0.8, () =>
            {
                for (int i = 0; i < count; ++i)
                {
                    _menuButtons[i].Hidden = false;
                    _menuButtons[i].Alpha = 1;
                }
            });
        }

        void HideMenuButtons()
        {
            if (_source.Count >= _visibleCountOfElements)
            {
                for (int i = _visibleCountOfElements - 1; i < _source.Count; ++i)
                {
                    _menuButtons[i].Hidden = true;
                    _menuButtons[i].Alpha = 0f;
                }
            }

            var count = _source.Count >= _visibleCountOfElements ? _visibleCountOfElements : _source.Count;
            UIView.Animate(0.2, () =>
            {
                for (int i = 0; i < count; ++i)
                {
                    _menuButtons[i].Alpha = 0f;
                }
            });
            _menuButtonsView.Layer.AddAnimation(_menuCloseAnimation, null);
        }

        void OnMainButtonClicked(object sender, EventArgs e)
        {
            if (_mainButton.IsOpen)
            {
                _shadowView.Hidden = true;
                HideMenuButtons();
            }
            else
            {
                _shadowView.Hidden = false;
                ShowMenuButtons();
            }
        }
    }
}
