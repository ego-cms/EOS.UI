using System;
using System.Collections.Generic;
using System.Linq;
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
        private bool _isTest = true;

        private const int _minimumCountOfElements = 3;
        private const int _maximumCountOfElements = 9;
        private const int _visibleCountOfElements = 5;
        private const int _menuSize = 300;
        private const double _menuOpenButtonAnimationDuration = 0.1;
        private const double _buttonMovementAnimationDuration = 0.2;
        private const int _radius = 96;
        private readonly nfloat _20degrees = 0.349066f;
        private readonly nfloat _10degrees = 0.174533f;

        private readonly UIView _rootView;
        private readonly UIView _shadowView;
        private readonly UISwipeGestureRecognizer _leftSwipe;
        private readonly UISwipeGestureRecognizer _rightSwipe;

        //view for circle of menu buttons
        private readonly UIView _menuButtonsView;
        private List<CircleMenuButton> _menuButtons = new List<CircleMenuButton>();
        private List<CGPoint> _buttonPositions = new List<CGPoint>();


        private List<CircleMenuItemModel> _circleMenuItems = new List<CircleMenuItemModel>();
        public List<CircleMenuItemModel> CircleMenuItems
        {
            get => _circleMenuItems;
            set
            {
                if (value.Count < _minimumCountOfElements || value.Count > _maximumCountOfElements)
                    throw new ArgumentException($"Source must contain {_minimumCountOfElements}-{_maximumCountOfElements} elements");
                _circleMenuItems = value;

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

            if (_isTest)
            {
                Frame = new CGRect(mainFrame.Width - _menuSize * 0.65, mainFrame.Height - _menuSize * 0.65, _menuSize, _menuSize);
            }
            else
            {
                Frame = new CGRect(mainFrame.Width - _menuSize, mainFrame.Height - _menuSize, _menuSize, _menuSize);
            }

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
                MoveLeft();
            });
            _leftSwipe.Direction = UISwipeGestureRecognizerDirection.Left;
            _rootView.AddGestureRecognizer(_leftSwipe);
            _rightSwipe = new UISwipeGestureRecognizer(() =>
            {
                RightSwiped?.Invoke(this, EventArgs.Empty);
                MoveRight();
            });
            _rightSwipe.Direction = UISwipeGestureRecognizerDirection.Right;
            _rootView.AddGestureRecognizer(_rightSwipe);

            _menuButtonsView = new UIView()
            {
                Frame = new CGRect(20, 20, this.Frame.Width - 40, this.Frame.Height - 40),
                BackgroundColor = _isTest ? UIColor.Clear : UIColor.DarkGray
            };
            AddSubview(_menuButtonsView);

            AddSubview(_mainButton);
        }

        public void Attach()
        {
            _rootView.AddSubview(this);
            FillbuttonsPositions();
            CreateMenuButtons();
        }

        void FillbuttonsPositions()
        {
            double startAngle = -0.977384;//56c
            double endAngle = 6.28 + startAngle;
            double diff = (endAngle - startAngle) / _maximumCountOfElements;

            double x;
            double y;
            for (int i = 0; i < 5; ++i)
            {
                x = Math.Cos(startAngle) * _radius + _menuButtonsView.Bounds.GetCenterX() - CircleMenuMainButton.Size / 2;
                y = Math.Sin(startAngle) * _radius + _menuButtonsView.Bounds.GetCenterY() - CircleMenuMainButton.Size / 2;
                _buttonPositions.Add(new CGPoint(x, y));
                startAngle -= diff;
            }

            startAngle = 0.785398;
            x = Math.Cos(startAngle) * 300 + _menuButtonsView.Bounds.GetCenterX() - CircleMenuMainButton.Size / 2;
            y = Math.Sin(startAngle) * 300 + _menuButtonsView.Bounds.GetCenterY() - CircleMenuMainButton.Size / 2;
            _buttonPositions.Add(new CGPoint(x, y));
        }

        void CreateMenuButtons()
        {
            var position = _buttonPositions.Last();
            for (int i = 0; i < _buttonPositions.Count; ++i)
            {
                var menuButton = new CircleMenuButton()
                {
                    TintColor = UIColor.Black,
                    BackgroundColor = UIColor.White,
                    Frame = new CGRect(position.X, position.Y, CircleMenuMainButton.Size, CircleMenuMainButton.Size),
                };
                menuButton.SetTitle(i.ToString(), UIControlState.Normal);
                menuButton.SetTitleColor(UIColor.Black, UIControlState.Normal);

                _menuButtonsView.InsertSubview(menuButton, 0);
                _menuButtons.Add(menuButton);
            }
        }

        async Task OpenMenu()
        {
            var tcs = new TaskCompletionSource<bool>();
            for (int i = 0; i < _menuButtons.Count - 1; ++i)
            {
                var delay = _menuOpenButtonAnimationDuration * i;
                for (int j = 0; j <= i; ++j)
                {
                    UIView.Animate(_menuOpenButtonAnimationDuration, delay, UIViewAnimationOptions.CurveEaseInOut, () =>
                    {
                        var positionIndex = i - j;
                        _menuButtons[j].Frame = _menuButtons[j].Frame.ResizeRect(x: _buttonPositions[positionIndex].X, y: _buttonPositions[positionIndex].Y);
                    }, () =>
                    {
                        if (i == _menuButtons.Count - 1 && j == i)
                        {
                            tcs.TrySetResult(true);
                        }
                    });
                }
            }
            await tcs.Task;
            StartSpringEffect(UISwipeGestureRecognizerDirection.Left, _20degrees);
        }

        async Task CloseMenu()
        {
            for (int i = 0; i < _menuButtons.Count; ++i)
            {
                for (int j = 0; j < _menuButtons.Count; ++j)
                {
                    var buttonPosition = new CGPoint(_menuButtons[j].Frame.X, _menuButtons[j].Frame.Y);
                    var positionIndex = _buttonPositions.IndexOf(buttonPosition);
                    if (positionIndex == _buttonPositions.Count - 1)
                        continue;
                    var previousPositionIndex = positionIndex > 0 ?
                        positionIndex - 1 : _buttonPositions.Count - 1;
                    UIView.Animate(_menuOpenButtonAnimationDuration, () =>
                    {
                        _menuButtons[j].Frame = _menuButtons[j].Frame.ResizeRect(x: _buttonPositions[previousPositionIndex].X, y: _buttonPositions[previousPositionIndex].Y);
                    });
                }
                await Task.Delay(TimeSpan.FromSeconds(_menuOpenButtonAnimationDuration));
            }
        }

        void MoveLeft()
        {
            for (int i = 0; i < _menuButtons.Count; ++i)
            {
                var buttonPosition = new CGPoint(_menuButtons[i].Frame.X, _menuButtons[i].Frame.Y);
                var positionIndex = _buttonPositions.IndexOf(buttonPosition);
                var nextPositionIndex = 0;
                if (positionIndex != _buttonPositions.Count - 1)
                {
                    nextPositionIndex = positionIndex + 1;
                }
                
                UIView.Animate(_buttonMovementAnimationDuration, () =>
                {
                    _menuButtons[i].Frame = _menuButtons[i].Frame.ResizeRect(x: _buttonPositions[nextPositionIndex].X, y: _buttonPositions[nextPositionIndex].Y);
                });
                
            }
            StartSpringEffect(UISwipeGestureRecognizerDirection.Left, _10degrees);
        }

        void MoveRight()
        {
            for (int i = 0; i < _menuButtons.Count; ++i)
            {
                var buttonPosition = new CGPoint(_menuButtons[i].Frame.X, _menuButtons[i].Frame.Y);
                var positionIndex = _buttonPositions.IndexOf(buttonPosition);
                var previousPositionIndex = _buttonPositions.Count - 1;
                if (positionIndex != 0)
                {
                    previousPositionIndex = positionIndex - 1;
                }
                UIView.Animate(_buttonMovementAnimationDuration, () =>
                {
                    _menuButtons[i].Frame = _menuButtons[i].Frame.ResizeRect(x: _buttonPositions[previousPositionIndex].X, y: _buttonPositions[previousPositionIndex].Y);
                });
            }
            StartSpringEffect(UISwipeGestureRecognizerDirection.Right, _10degrees);
        }

        void StartSpringEffect(UISwipeGestureRecognizerDirection direction, nfloat angle)
        {
            if(direction == UISwipeGestureRecognizerDirection.Left)
            {
                angle = -angle;
            }

            var leftAnimation = new CASpringAnimation();
            leftAnimation.KeyPath = "transform.rotation.z";
            leftAnimation.From = new NSNumber(0);
            leftAnimation.To = new NSNumber(angle);
            leftAnimation.RepeatCount = 1;
            leftAnimation.RemovedOnCompletion = false;
            leftAnimation.FillMode = CAFillMode.Forwards;

            var rightAnimation = new CASpringAnimation();
            rightAnimation.KeyPath = "transform.rotation.z";
            rightAnimation.From = new NSNumber(angle);
            rightAnimation.To = new NSNumber(0);
            rightAnimation.RepeatCount = 1;
            rightAnimation.RemovedOnCompletion = false;
            rightAnimation.FillMode = CAFillMode.Forwards;

            leftAnimation.AnimationStopped += (sender, e) =>
            {
                _menuButtonsView.Layer.AddAnimation(rightAnimation, null);
            };

            rightAnimation.AnimationStopped += (sender, e) =>
            {
                _menuButtonsView.Layer.RemoveAllAnimations();
            };

            _menuButtonsView.Layer.AddAnimation(leftAnimation, null);
        }

        async void OnMainButtonClicked(object sender, EventArgs e)
        {
            if (_mainButton.IsOpen)
            {
                _shadowView.Hidden = true;
                await CloseMenu();
            }
            else
            {
                _shadowView.Hidden = false;
                await OpenMenu();
            }
        }
    }
}
