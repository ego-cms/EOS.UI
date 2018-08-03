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
        //TODO need to remove after develop
#if DEBUG
        private bool _isTest = false;
#else
        private bool _isTest = true;
#endif

        private const int _minimumCountOfElements = 3;
        private const int _maximumCountOfElements = 9;
        private const int _visibleCountOfElements = 5;
        private const int _mainButtonPadding = 15;
        private const int _menuSize = 300;
        private const double _menuOpenButtonAnimationDuration = 0.1;
        private const double _buttonMovementAnimationDuration = 0.2;
        private const int _radius = 96;
        private readonly nfloat _20degrees = 0.349066f;
        private readonly nfloat _10degrees = 0.174533f;
        private readonly nfloat _55degrees = 0.959931f;
        private readonly UIView _rootView;
        private readonly UIView _shadowView;
        private readonly UISwipeGestureRecognizer _leftSwipe;
        private readonly UISwipeGestureRecognizer _rightSwipe;
        private readonly UISwipeGestureRecognizer _upSwipe;
        private readonly UISwipeGestureRecognizer _downSwipe;
        private readonly UIView _menuButtonsView;

        //view for circle of menu buttons
        private List<CircleMenuButton> _menuButtons = new List<CircleMenuButton>();
        private List<CircleButtonIndicator> _buttonIndicators = new List<CircleButtonIndicator>();
        private List<CGPoint> _buttonPositions = new List<CGPoint>();
        private List<CGPoint> _indicatorPositions = new List<CGPoint>();
        private List<CircleMenuButton> _submenuButtons = new List<CircleMenuButton>();
        private bool _isSubmenuOpen;


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

        public CircleMenuMainButton MainButton { get; private set; }

        private bool IsSwipeBlocked
        {
            get
            {
                return !MainButton.IsOpen || _circleMenuItems.Count == _minimumCountOfElements || _isSubmenuOpen;
            }
        }

        ///TODO: Events for test purpose only, need to remove in future
        public EventHandler LeftSwiped;
        public EventHandler RightSwiped;

        public EventHandler<int> Clicked;

        public CircleMenu(UIView rootView)
        {
            _rootView = rootView;

            var mainFrame = UIApplication.SharedApplication.KeyWindow.Frame;

            if (_isTest)
            {
                Frame = new CGRect(mainFrame.Width - (_menuSize / 2 + CircleMenuMainButton.Size / 2 + _mainButtonPadding),
                                   mainFrame.Height - (_menuSize / 2 + CircleMenuMainButton.Size / 2 + _mainButtonPadding),
                                   _menuSize, _menuSize);
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
            MainButton = new CircleMenuMainButton();
            MainButton.Frame = new CGRect(
                (Frame.Width - CircleMenuMainButton.Size) / 2,
                (Frame.Height - CircleMenuMainButton.Size) / 2,
                CircleMenuMainButton.Size, CircleMenuMainButton.Size);
            MainButton.TouchUpInside += OnMainButtonClicked;

            //swipe init
            _leftSwipe = new UISwipeGestureRecognizer(OnLeftSwipe);
            _leftSwipe.Direction = UISwipeGestureRecognizerDirection.Left;
            _rootView.AddGestureRecognizer(_leftSwipe);
            _rightSwipe = new UISwipeGestureRecognizer(OnRightSwipe);
            _rightSwipe.Direction = UISwipeGestureRecognizerDirection.Right;
            _rootView.AddGestureRecognizer(_rightSwipe);
            _upSwipe = new UISwipeGestureRecognizer(OnRightSwipe);
            _upSwipe.Direction = UISwipeGestureRecognizerDirection.Up;
            _rootView.AddGestureRecognizer(_upSwipe);
            _downSwipe = new UISwipeGestureRecognizer(OnLeftSwipe);
            _downSwipe.Direction = UISwipeGestureRecognizerDirection.Down;
            _rootView.AddGestureRecognizer(_downSwipe);

            _menuButtonsView = new UIView()
            {
                Frame = new CGRect(20, 20, this.Frame.Width - 40, this.Frame.Height - 40),
                BackgroundColor = _isTest ? UIColor.Clear : UIColor.DarkGray
            };
            AddSubview(_menuButtonsView);
            AddSubview(MainButton);
            Clicked += PrepareSubmenuIfNeeded;
        }

        public void Attach()
        {
            _rootView.AddSubview(this);
            FillbuttonsPositions();
            CreateMenuButtons();
            FillIndicatorPositions();
            CreateMenuIndicators();
        }

        void FillbuttonsPositions()
        {
            double startAngle = -_55degrees;//56c
            double endAngle = 6.28 + startAngle;
            double diff = (endAngle - startAngle) / _maximumCountOfElements;

            double x;
            double y;
            for (int i = 0; i < _visibleCountOfElements; ++i)
            {
                x = Math.Cos(startAngle) * _radius + _menuButtonsView.Bounds.GetCenterX() - CircleMenuMainButton.Size / 2;
                y = Math.Sin(startAngle) * _radius + _menuButtonsView.Bounds.GetCenterY() - CircleMenuMainButton.Size / 2;
                _buttonPositions.Add(new CGPoint(x, y));
                startAngle -= diff;
            }

            startAngle = 0.785398;
            x = Math.Cos(startAngle) * (_isTest ? _menuSize : _radius) + _menuButtonsView.Bounds.GetCenterX() - CircleMenuMainButton.Size / 2;
            y = Math.Sin(startAngle) * (_isTest ? _menuSize : _radius) + _menuButtonsView.Bounds.GetCenterY() - CircleMenuMainButton.Size / 2;
            _buttonPositions.Add(new CGPoint(x, y));
        }

        void FillIndicatorPositions()
        {
            double startAngle = -_55degrees;//56c
            double endAngle = 6.28 + startAngle;
            double diff = (endAngle - startAngle) / _maximumCountOfElements;

            double x;
            double y;
            for (int i = 0; i < _visibleCountOfElements; ++i)
            {
                x = Math.Cos(startAngle) * 130 + _menuButtonsView.Bounds.GetCenterX() - CircleButtonIndicator.Size / 2;
                y = Math.Sin(startAngle) * 130 + _menuButtonsView.Bounds.GetCenterY() - CircleButtonIndicator.Size / 2;
                _indicatorPositions.Add(new CGPoint(x, y));
                startAngle -= diff;
            }

            startAngle = 0.785398;
            x = Math.Cos(startAngle) * 150 + _menuButtonsView.Bounds.GetCenterX() - CircleButtonIndicator.Size / 2;
            y = Math.Sin(startAngle) * 150 + _menuButtonsView.Bounds.GetCenterY() - CircleButtonIndicator.Size / 2;
            _indicatorPositions.Add(new CGPoint(x, y));
        }

        void CreateMenuButtons()
        {
            var position = _buttonPositions.Last();
            for (int i = 0; i < _buttonPositions.Count; ++i)
            {
                var menuButton = new CircleMenuButton(_buttonPositions);
                menuButton.TouchUpInside += (sender, e) =>
                {
                    var model = menuButton.Model;
                    if (model != null)
                    {
                        Clicked?.Invoke(menuButton, model.Id);
                    }
                };
                menuButton.Position = position;
                _menuButtonsView.InsertSubview(menuButton, 0);
                _menuButtons.Add(menuButton);
            }
        }

        void CreateMenuIndicators()
        {
            var position = _indicatorPositions.Last();
            for (int i = 0; i < _indicatorPositions.Count; ++i)
            {
                var indicator = new CircleButtonIndicator();
                indicator.Position = position;
                _menuButtons[i].Indicator = indicator;
                _menuButtonsView.InsertSubview(indicator, 0);
                _buttonIndicators.Add(indicator);
            }
        }

        void PrepareMenuButtons()
        {
            if (_circleMenuItems.Count == _minimumCountOfElements)
            {
                for (int modelIndex = 0, buttonIndex = 1; modelIndex < _minimumCountOfElements; ++modelIndex, ++buttonIndex)
                {
                    _menuButtons[buttonIndex].Model = _circleMenuItems[modelIndex];
                }
                foreach (var button in _menuButtons)
                {
                    if (button.Model == null)
                        button.Hidden = true;
                }
            }

            if (_circleMenuItems.Count == 4)
            {
                for (int i = 0; i < _circleMenuItems.Count; ++i)
                {
                    _menuButtons[i].Model = _circleMenuItems[i];
                }
                _menuButtons[4].Model = _circleMenuItems[0];
            }

            if (_circleMenuItems.Count > 4)
            {
                for (int i = 0; i < _circleMenuItems.Count && i < _menuButtons.Count; ++i)
                {
                    _menuButtons[i].Model = _circleMenuItems[i];
                }
            }
        }

        async Task OpenMenu()
        {
            PrepareMenuButtons();
            var tcs = new TaskCompletionSource<bool>();
            for (int i = 0; i < _visibleCountOfElements; ++i)
            {
                var delay = _menuOpenButtonAnimationDuration * i;
                for (int j = 0; j <= i; ++j)
                {
                    UIView.Animate(_menuOpenButtonAnimationDuration, delay, UIViewAnimationOptions.CurveEaseInOut, () =>
                    {
                        var positionIndex = i - j;
                        _menuButtons[j].Position = _buttonPositions[positionIndex];
                        _menuButtons[j].Indicator.Position = _indicatorPositions[positionIndex];
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
                    var positionIndex = _menuButtons[j].PositionIndex;
                    if (positionIndex == _buttonPositions.Count - 1)
                        continue;
                    var previousPositionIndex = positionIndex > 0 ?
                        positionIndex - 1 : _buttonPositions.Count - 1;
                    UIView.Animate(_menuOpenButtonAnimationDuration, () =>
                    {
                        _menuButtons[j].Position = _buttonPositions[previousPositionIndex];
                        _menuButtons[j].Indicator.Position = _indicatorPositions[previousPositionIndex];
                    });
                }
                await Task.Delay(TimeSpan.FromSeconds(_menuOpenButtonAnimationDuration));
            }
        }

        void MoveLeft()
        {
            var zeroButtonPositionModel = _menuButtons.SingleOrDefault(b => b.Position == _buttonPositions[0]).Model;
            var indexOfZerobuttonPositionModel = _circleMenuItems.IndexOf(zeroButtonPositionModel);

            for (int i = 0; i < _menuButtons.Count; ++i)
            {
                var positionIndex = _buttonPositions.IndexOf(_menuButtons[i].Position);
                var nextPositionIndex = positionIndex != _buttonPositions.Count - 1 ? positionIndex += 1 : 0;

                if (nextPositionIndex == 0)
                {
                    var indexOfNextModel = indexOfZerobuttonPositionModel < _circleMenuItems.Count - 1 ?
                                                                                            indexOfZerobuttonPositionModel + 1 : 0;
                    _menuButtons[i].Model = _circleMenuItems[indexOfNextModel];
                }

                UIView.Animate(_buttonMovementAnimationDuration, () =>
                {
                    _menuButtons[i].Position = _buttonPositions[nextPositionIndex];
                    _menuButtons[i].Indicator.Position = _indicatorPositions[nextPositionIndex];
                });
            }
            StartSpringEffect(UISwipeGestureRecognizerDirection.Left, _10degrees);
        }

        void MoveRight()
        {
            var lastButtonPositionModel = _menuButtons.SingleOrDefault(b => b.Position == _buttonPositions[4]).Model;
            var indexOfLastButtonPositionModel = _circleMenuItems.IndexOf(lastButtonPositionModel);

            for (int i = 0; i < _menuButtons.Count; ++i)
            {
                var positionIndex = _menuButtons[i].PositionIndex;
                var previousPositionIndex = positionIndex != 0 ? positionIndex - 1 : _buttonPositions.Count - 1;

                if (previousPositionIndex == 4)
                {
                    var indexOfPreviousModel = indexOfLastButtonPositionModel > 0 ?
                        indexOfLastButtonPositionModel - 1 : _circleMenuItems.Count - 1;
                    _menuButtons[i].Model = _circleMenuItems[indexOfPreviousModel];
                }

                UIView.Animate(_buttonMovementAnimationDuration, () =>
                {
                    _menuButtons[i].Position = _buttonPositions[previousPositionIndex];
                    _menuButtons[i].Indicator.Position = _indicatorPositions[previousPositionIndex];
                });
            }
            StartSpringEffect(UISwipeGestureRecognizerDirection.Right, _10degrees);
        }

        void StartSpringEffect(UISwipeGestureRecognizerDirection direction, nfloat angle)
        {
            if (direction == UISwipeGestureRecognizerDirection.Left)
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

            leftAnimation.AnimationStarted += (sender, e) => MainButton.UserInteractionEnabled = false; ;
            leftAnimation.AnimationStopped += (sender, e) =>
            {
                _menuButtonsView.Layer.AddAnimation(rightAnimation, null);
            };

            rightAnimation.AnimationStopped += (sender, e) =>
            {
                _menuButtonsView.Layer.RemoveAllAnimations();
                MainButton.UserInteractionEnabled = true;
            };

            _menuButtonsView.Layer.AddAnimation(leftAnimation, null);
        }

        async void PrepareSubmenuIfNeeded(object sender, int id)
        {
            var model = _circleMenuItems.Single(s => s.Id == id);
            if (!model.HasChildren)
                return;
            var invokedButton = (CircleMenuButton)sender;
            if (!_isSubmenuOpen)
            {
                PrepareSubmenu(invokedButton, model.Children);
                await ShowSubmenu(invokedButton);
                _isSubmenuOpen = true;
            }
            else
            {
                await CloseSubmenu(invokedButton);
                _submenuButtons.ForEach(b => b.RemoveFromSuperview());
                _submenuButtons.Clear();
                _isSubmenuOpen = false;
            }
        }

        void PrepareSubmenu(CircleMenuButton invokedButton, List<CircleMenuItemModel> chilren)
        {
            nfloat yPosition = invokedButton.Frame.Y;
            nfloat xPosition = invokedButton.Frame.X;

            var buttonPosition = _buttonPositions.IndexOf(invokedButton.Position);
            if (buttonPosition == 3)
            {
                xPosition = xPosition - CircleMenuMainButton.Size - 10;
                yPosition = invokedButton.Frame.Y + CircleMenuMainButton.Size + 10;
            }
            foreach (var children in chilren)
            {
                yPosition = yPosition - CircleMenuMainButton.Size - 10;

                var button = new CircleMenuButton()
                {
                    Model = children,
                    Frame = new CGRect(xPosition, yPosition, CircleMenuMainButton.Size, CircleMenuMainButton.Size),
                    Alpha = 0
                };
                _submenuButtons.Add(button);
            }

            var indicator = invokedButton.Indicator;
            indicator.Hidden = true;
            var buttonFrame = _submenuButtons.Last().Frame;
            indicator.Frame = indicator.Frame.ResizeRect(x: buttonFrame.X + buttonFrame.Width / 2 - indicator.Frame.Width / 2, y: buttonFrame.Y - 15);
        }

        async Task ShowSubmenu(CircleMenuButton invokedButton)
        {
            var tcs = new TaskCompletionSource<bool>();
            for (int i = 0; i < _submenuButtons.Count; ++i)
            {
                _menuButtonsView.AddSubview(_submenuButtons[i]);
                UIView.Animate(_menuOpenButtonAnimationDuration, () =>
                 {
                     _submenuButtons[i].Alpha = 1;
                 }, () => tcs.SetResult(true));
                await tcs.Task;
                tcs = new TaskCompletionSource<bool>();
            }
            invokedButton.Indicator.Hidden = false;
        }

        async Task CloseSubmenu(CircleMenuButton invokedButton)
        {
            var tcs = new TaskCompletionSource<bool>();
            invokedButton.Indicator.Hidden = true;
            for (int i = _submenuButtons.Count - 1; i >= 0; --i)
            {
                UIView.Animate(_menuOpenButtonAnimationDuration, () =>
                {
                    _submenuButtons[i].Alpha = 0;
                }, () => tcs.SetResult(true));
                await tcs.Task;
                tcs = new TaskCompletionSource<bool>();
            }
            invokedButton.Indicator.ResetPosition();
            invokedButton.Indicator.Hidden = false;
        }

        async void OnMainButtonClicked(object sender, EventArgs e)
        {
            if (MainButton.IsOpen)
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

        void OnRightSwipe()
        {
            if (IsSwipeBlocked)
                return;
            RightSwiped?.Invoke(this, EventArgs.Empty);
            MoveRight();
        }

        void OnLeftSwipe()
        {
            if (IsSwipeBlocked)
                return;
            LeftSwiped?.Invoke(this, EventArgs.Empty);
            MoveLeft();
        }
    }
}
