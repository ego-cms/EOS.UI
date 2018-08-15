using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airbnb.Lottie;
using CoreAnimation;
using CoreGraphics;
using EOS.UI.iOS.Extensions;
using EOS.UI.iOS.Themes;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Extensions;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using Foundation;
using UIKit;
using System.Threading;

namespace EOS.UI.iOS.Components
{
    public class CircleMenu : PassthroughToWindowView, IEOSThemeControl
    {
        //TODO need to remove after develop
#if DEBUG
        private bool _isTest = true;
#else
        private bool _isTest = true;
#endif

        private const int _minimumCountOfElements = 3;
        private const int _maximumCountOfElements = 9;
        private const int _visibleCountOfElements = 5;
        private const int _maximumCountOfChildren = 5;
        private const int _mainButtonPadding = 15;
        private const int _menuSize = 300;
        private const int _submenuButtonsStartTag = 500;
        private const double _menuOpenButtonAnimationDuration = 0.05;
        private const double _buttonMovementAnimationDuration = 0.1;
        private const double _buttonHintAnimationDuration = 0.4;
        private const double _showSubmenuDuration = 0.1;
        private const int _radius = 96;
        private readonly nfloat _6degrees = 0.10472f;
        private readonly nfloat _8degrees = 0.0698132f;
        private readonly nfloat _55degrees = 0.959931f;
        private UIView _rootView;
        private UIView _shadowView;
        private UISwipeGestureRecognizer _leftSwipe;
        private UISwipeGestureRecognizer _rightSwipe;
        private UISwipeGestureRecognizer _upSwipe;
        private UISwipeGestureRecognizer _downSwipe;
        private UIView _menuButtonsView;
        private CircleMenuMainButton _mainButton;

        //view for circle of menu buttons
        private List<CircleMenuButton> _menuButtons = new List<CircleMenuButton>();
        private List<CircleButtonIndicator> _buttonIndicators = new List<CircleButtonIndicator>();
        private List<CGPoint> _buttonPositions = new List<CGPoint>();
        private List<CGPoint> _indicatorPositions = new List<CGPoint>();
        private bool _isSubmenuOpen;
        private bool _isHintShown;

        public bool IsEOSCustomizationIgnored { get; private set; }

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

        private bool IsSwipeBlocked
        {
            get
            {
                return !_mainButton.IsOpen || _circleMenuItems.Count == _minimumCountOfElements || _isSubmenuOpen;
            }
        }

        private UIColor _focusedBackgroundColor;
        public UIColor FocusedBackgroundColor
        {
            get => _focusedBackgroundColor;
            set
            {
                _focusedBackgroundColor = value;
                IsEOSCustomizationIgnored = true;
                _menuButtons.ForEach(b => b.FocusedBackgroundColor = _focusedBackgroundColor);
                _buttonIndicators.ForEach(b => b.BackgroundColor = _focusedBackgroundColor);
            }
        }

        private UIColor _unfocusedBackgroundColor;
        public UIColor UnfocusedBackgroundColor
        {
            get => _unfocusedBackgroundColor;
            set
            {
                _unfocusedBackgroundColor = value;
                IsEOSCustomizationIgnored = true;
                _menuButtons.ForEach(b => b.UnfocusedBackgroundColor = _unfocusedBackgroundColor);
                _mainButton.BackgroundColor = _unfocusedBackgroundColor;
            }
        }

        private UIColor _focusedIconColor;
        public UIColor FocusedIconColor
        {
            get => _focusedIconColor;
            set
            {
                _focusedIconColor = value;
                IsEOSCustomizationIgnored = true;
                _menuButtons.ForEach(b => b.FocusedIconColor = _focusedIconColor);
            }
        }

        private UIColor _unfocusedIconColor;
        public UIColor UnfocusedIconColor
        {
            get => _unfocusedIconColor;
            set
            {
                _unfocusedIconColor = value;
                IsEOSCustomizationIgnored = true;
                _menuButtons.ForEach(b => b.UnfocusedIconColor = UnfocusedIconColor);
                _mainButton.UnfocusedIconColor = _unfocusedIconColor;
            }
        }

        private UIColor _blackoutColor;
        private UIColor BlackoutColor
        {
            get => _blackoutColor;
            set
            {
                _blackoutColor = value;
                _shadowView.BackgroundColor = _blackoutColor;
            }
        }

        public EventHandler<int> Clicked;

        public CircleMenu()
        {
            var mainFrame = UIApplication.SharedApplication.KeyWindow.Frame;
            if (_isTest)
            {
                Frame = new CGRect(mainFrame.Width - (_menuSize / 2 + CircleMenuButton.Size / 2 + _mainButtonPadding),
                                   mainFrame.Height - (_menuSize / 2 + CircleMenuButton.Size / 2 + _mainButtonPadding),
                                   _menuSize, _menuSize);
            }
            else
            {
                Frame = new CGRect(mainFrame.Width - _menuSize, mainFrame.Height - _menuSize, _menuSize, _menuSize);
            }
            BackgroundColor = _isTest ? UIColor.Clear : UIColor.LightGray;
        }

        public void Attach(UIViewController viewController)
        {
            _rootView = viewController.View;

            var mainFrame = UIApplication.SharedApplication.KeyWindow.Frame;
            //shadowview init
            _shadowView = new UIView(mainFrame);
            _shadowView.Hidden = true;
            _rootView.AddSubview(_shadowView);

            //mainbutton init
            _mainButton = new CircleMenuMainButton();
            _mainButton.Frame = new CGRect(
                (Frame.Width - CircleMenuButton.Size) / 2,
                (Frame.Height - CircleMenuButton.Size) / 2,
                CircleMenuButton.Size, CircleMenuButton.Size);
            _mainButton.TouchUpInside += OnMainButtonClicked;

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

            _menuButtonsView = new PassthroughToWindowView()
            {
                Frame = new CGRect(0, 0, this.Frame.Width, this.Frame.Height),
                BackgroundColor = _isTest ? UIColor.Clear : UIColor.DarkGray,
                ClipsToBounds = false,
            };
            AddSubview(_menuButtonsView);
            AddSubview(_mainButton);

            FillbuttonsPositions();
            CreateMenuButtons();
            FillIndicatorPositions();
            CreateMenuIndicators();
            _rootView.AddSubview(this);

            UpdateAppearance();
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
                x = Math.Cos(startAngle) * _radius + _menuButtonsView.Bounds.GetCenterX() - CircleMenuButton.Size / 2;
                y = Math.Sin(startAngle) * _radius + _menuButtonsView.Bounds.GetCenterY() - CircleMenuButton.Size / 2;
                _buttonPositions.Add(new CGPoint(x, y));
                startAngle -= diff;
            }

            startAngle = 0.785398;
            x = Math.Cos(startAngle) * (_isTest ? _menuSize : _radius) + _menuButtonsView.Bounds.GetCenterX() - CircleMenuButton.Size / 2;
            y = Math.Sin(startAngle) * (_isTest ? _menuSize : _radius) + _menuButtonsView.Bounds.GetCenterY() - CircleMenuButton.Size / 2;
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
                        PrepareSubmenuIfNeeded(menuButton, model);
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

        void SetModelsForButtons()
        {
            _menuButtons.ForEach(b => b.Model = null);
            if (_circleMenuItems.Count == _minimumCountOfElements)
            {
                for (int modelIndex = 0, buttonIndex = 1; modelIndex < _minimumCountOfElements; ++modelIndex, ++buttonIndex)
                {
                    _menuButtons[buttonIndex].Model = _circleMenuItems[modelIndex];
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
            SetModelsForButtons();
            _menuButtonsView.Transform = CGAffineTransform.MakeRotation(-_6degrees);
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
            _menuButtons.ForEach(b => b.UserInteractionEnabled = false);
            await tcs.Task;
            await StartOpenSpringEffect(-_6degrees);
            if (!_isHintShown)
            {
                await StartHintAnimation();
                _isHintShown = true;
            }
            _menuButtons.ForEach(b => b.UserInteractionEnabled = b.PositionIndex != 4 && b.PositionIndex != 0);
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
            StartMoveSpringEffect(-_8degrees);
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
            StartMoveSpringEffect(_8degrees);
        }

        Task<bool> StartOpenSpringEffect(nfloat angle)
        {
            var tcs = new TaskCompletionSource<bool>();
            var leftAnimation = new CASpringAnimation();
            leftAnimation.KeyPath = "transform.rotation.z";
            leftAnimation.From = new NSNumber(angle);
            leftAnimation.To = new NSNumber(0);
            leftAnimation.RepeatCount = 1;
            leftAnimation.RemovedOnCompletion = false;
            leftAnimation.FillMode = CAFillMode.Forwards;
            leftAnimation.InitialVelocity = 40;
            leftAnimation.Duration = 0.8;

            leftAnimation.AnimationStopped += (sender, e) =>
            {
                _menuButtonsView.Transform = CGAffineTransform.MakeRotation(0);
                _menuButtonsView.Layer.RemoveAllAnimations();
                tcs.SetResult(true);
            };
            _menuButtonsView.Layer.AddAnimation(leftAnimation, null);
            return tcs.Task;
        }

        Task<bool> StartMoveSpringEffect(nfloat angle)
        {
            var tcs = new TaskCompletionSource<bool>();
            var firstAnimation = new CASpringAnimation();
            firstAnimation.KeyPath = "transform.rotation.z";
            firstAnimation.From = new NSNumber(0);
            firstAnimation.To = new NSNumber(angle);
            firstAnimation.RepeatCount = 1;
            firstAnimation.RemovedOnCompletion = false;
            firstAnimation.FillMode = CAFillMode.Forwards;
            firstAnimation.InitialVelocity = 30;
            firstAnimation.Duration = 0.3;

            var secondAnimation = new CASpringAnimation();
            secondAnimation.KeyPath = "transform.rotation.z";
            secondAnimation.From = new NSNumber(angle);
            secondAnimation.To = new NSNumber(0);
            secondAnimation.RemovedOnCompletion = false;
            secondAnimation.FillMode = CAFillMode.Forwards;
            secondAnimation.InitialVelocity = 30;
            secondAnimation.Duration = 0.5;

            firstAnimation.AnimationStopped += (sender, e) =>
            {
                _menuButtonsView.Layer.AddAnimation(secondAnimation, null);
            };

            secondAnimation.AnimationStopped += (sender, e) =>
            {
                _menuButtonsView.Layer.RemoveAllAnimations();
                tcs.SetResult(true);
            };
            _menuButtonsView.Layer.AddAnimation(firstAnimation, null);
            return tcs.Task;
        }

        async Task PrepareSubmenuIfNeeded(CircleMenuButton invokedButton, CircleMenuItemModel model)
        {
            if (!model.HasChildren)
                return;
            if (model.Children.Count > _maximumCountOfChildren)
                throw new ArgumentException($"Submenu must contain no more then {_maximumCountOfChildren} elements");
            invokedButton.UserInteractionEnabled = false;
            if (!_isSubmenuOpen)
            {
                SendViewToBack();
                var submenuButtons = PrepareSubmenu(invokedButton, model.Children);
                await ShowSubmenu(invokedButton, submenuButtons);
                _isSubmenuOpen = true;
            }
            else
            {
                SendViewToFront();
                await CloseSubmenu(invokedButton);
                _isSubmenuOpen = false;
            }
            invokedButton.UserInteractionEnabled = true;
        }

        List<CircleMenuButton> PrepareSubmenu(CircleMenuButton invokedButton, List<CircleMenuItemModel> children)
        {
            var convertedPosition = _menuButtonsView.ConvertPointToView(invokedButton.Position, _rootView);
            nfloat xPosition = convertedPosition.X;
            nfloat yPosition = convertedPosition.Y;
            var submenuButtons = new List<CircleMenuButton>();
            var buttonPosition = _buttonPositions.IndexOf(invokedButton.Position);
            if (buttonPosition == 3)
            {
                xPosition = xPosition - CircleMenuButton.Size - 10;
                yPosition = yPosition + CircleMenuButton.Size + 10;
            }
            for (int i = 0, j = _submenuButtonsStartTag; i < children.Count; ++i, ++j)
            {
                yPosition = yPosition - CircleMenuButton.Size - 10;

                var button = new CircleMenuButton()
                {
                    Model = children[i],
                    Frame = new CGRect(xPosition, yPosition, CircleMenuButton.Size, CircleMenuButton.Size),
                    Alpha = 0,
                    Tag = j,
                    UnfocusedBackgroundColor = this.UnfocusedBackgroundColor ?? UIColor.White,
                    UnfocusedIconColor = this.UnfocusedIconColor ?? UIColor.Black
                };
                button.TouchUpInside += OnSubmenuClicked;
                submenuButtons.Add(button);
            }

            var indicator = invokedButton.Indicator;
            indicator.Hidden = true;
            var buttonFrame = submenuButtons.Last().Frame;
            var leftUpCorner = _rootView.ConvertPointToView(new CGPoint(buttonFrame.X, buttonFrame.Y), _menuButtonsView);
            indicator.Frame = indicator.Frame.ResizeRect(x: leftUpCorner.X + buttonFrame.Width / 2 - indicator.Frame.Width / 2, y: leftUpCorner.Y - 15);
            return submenuButtons;
        }

        private Task ShowSubmenu(CircleMenuButton invokedButton, List<CircleMenuButton> submenuButtons)
        {
            var task = Task.CompletedTask;
            
            var convertedPosition = _menuButtonsView.ConvertPointToView(invokedButton.Position, _rootView);
            invokedButton.Frame = invokedButton.Frame.ResizeRect(x: convertedPosition.X, y: convertedPosition.Y);
            _rootView.InsertSubview(invokedButton, _rootView.Subviews.Length);
            
            for (int i = 0; i < submenuButtons.Count; ++i)
            {
                _rootView.AddSubview(submenuButtons[i]);
                //Have to close on local variable
                var localViewIndex = i;
                task = task.ContinueWith(t => ChangeAlphaAnimation(submenuButtons[localViewIndex], 1, _showSubmenuDuration),
                                         TaskScheduler.FromCurrentSynchronizationContext())
                           .Unwrap();
            }
            task = task.ContinueWith((t) =>
            {
                invokedButton.Indicator.Hidden = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
            return task;
        }

        private Task CloseSubmenu(CircleMenuButton invokedButton)
        {
            invokedButton.ResetPosition();
            _menuButtonsView.InsertSubview(invokedButton, _menuButtonsView.Subviews.Length);
            
            var submenuButtons = _rootView.Subviews.Where(v =>
                                                          v.Tag >= 0
                                                          && v.Tag <= _submenuButtonsStartTag + _maximumCountOfChildren
                                                          && v is CircleMenuButton).OrderByDescending(v => v.Tag).ToList();
            if (submenuButtons.Count == 0)
                return Task.CompletedTask;

            invokedButton.Indicator.Hidden = true;
            var task = Task.CompletedTask;
            for (int i = 0; i < submenuButtons.Count; ++i)
            {
                //Have to close on local variable
                var localViewIndex = i;
                task = task.ContinueWith(t => ChangeAlphaAnimation((CircleMenuButton)submenuButtons[localViewIndex], 0, _showSubmenuDuration),
                                         TaskScheduler.FromCurrentSynchronizationContext())
                           .Unwrap();
            }

            task = task.ContinueWith((t) =>
            {
                invokedButton.Indicator.ResetPosition();
                invokedButton.Indicator.Hidden = false;
                foreach (CircleMenuButton button in submenuButtons)
                {
                    button.RemoveFromSuperview();
                    button.TouchUpInside -= OnSubmenuClicked;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
            return task;
        }

        Task ChangeAlphaAnimation(CircleMenuButton button, nfloat alpha, double duration)
        {
            var tcs = new TaskCompletionSource<bool>();
            UIView.Animate(_showSubmenuDuration, () =>
            {
                button.Alpha = alpha;
            }, () => tcs.SetResult(true));
            return tcs.Task;
        }

        async void OnMainButtonClicked(object sender, EventArgs e)
        {
            Clicked?.Invoke(_mainButton, _mainButton.Id);
            SwitchInteractions(false);
            if (_mainButton.IsOpen)
            {
                _shadowView.Hidden = true;
                await CloseMenu();
            }
            else
            {
                _shadowView.Hidden = false;
                SetModelsForButtons();
                await OpenMenu();
            }
            SwitchInteractions(true);
        }

        void OnRightSwipe()
        {
            if (IsSwipeBlocked)
                return;
            MoveRight();
        }

        void OnLeftSwipe()
        {
            if (IsSwipeBlocked)
                return;
            MoveLeft();
        }

        Task<bool> StartHintAnimation()
        {
            var padding = 7;
            var tcs = new TaskCompletionSource<bool>();
            var hintViews = new List<UIView>();
            var invokedButton = _menuButtons.Single(b => b.PositionIndex == 1);
            for (int i = 0; i < 2; ++i)
            {
                var hintView = new UIView(invokedButton.Frame);
                hintView.BackgroundColor = UnfocusedBackgroundColor;
                hintView.Layer.CornerRadius = invokedButton.Frame.Height / 2;
                hintView.Layer.ShadowColor = UIColor.Black.CGColor;
                hintView.Layer.ShadowOffset = new CGSize(0, 1);
                hintView.Layer.ShadowRadius = 1;
                hintView.Layer.ShadowOpacity = 0.2f;

                hintViews.Add(hintView);
                _menuButtonsView.InsertSubview(hintView, 0);
            }

            UIView.Animate(_buttonHintAnimationDuration, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
            {
                invokedButton.Frame = invokedButton.Frame.ResizeRect(y: invokedButton.Frame.Y - padding);
                invokedButton.Indicator.Frame = invokedButton.Indicator.Frame.ResizeRect(y: invokedButton.Indicator.Frame.Y - padding);
                hintViews[1].Frame = hintViews[1].Frame.ResizeRect(y: hintViews[1].Frame.Y + padding);
            }, null);
            UIView.Animate(_buttonHintAnimationDuration, _buttonHintAnimationDuration, UIViewAnimationOptions.CurveEaseInOut, () =>
            {
                invokedButton.ResetPosition();
                invokedButton.Indicator.ResetPosition();
                hintViews[0].Frame = invokedButton.Frame;
                hintViews[1].Frame = invokedButton.Frame;
            }, () =>
            {
                hintViews.ForEach(b => b.RemoveFromSuperview());
                tcs.SetResult(true);
            });
            return tcs.Task;
        }

        void OnSubmenuClicked(object sender, EventArgs e)
        {
            var button = (CircleMenuButton)sender;
            Clicked?.Invoke(button, button.Model.Id);
        }
        
        void SendViewToBack()
        {
            var index = _rootView.Subviews.ToList().IndexOf(_shadowView);
            _rootView.InsertSubview(this, index - 1);
        }
        
        void SendViewToFront()
        {
            var index = _rootView.Subviews.ToList().IndexOf(_shadowView);
            _rootView.InsertSubview(this, _rootView.Subviews.Length);
        }

        void SwitchInteractions(bool enabled)
        {
            _mainButton.UserInteractionEnabled = enabled;
            _leftSwipe.Enabled = enabled;
            _rightSwipe.Enabled = enabled;
        }

        public IEOSThemeProvider GetThemeProvider()
        {
            return EOSThemeProvider.Instance;
        }

        public void UpdateAppearance()
        {
            if (!IsEOSCustomizationIgnored)
            {
                var provider = GetThemeProvider();
                UnfocusedBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6s);
                FocusedBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                FocusedIconColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6s);
                UnfocusedIconColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor1s);
                BlackoutColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.Blackout);
                IsEOSCustomizationIgnored = false;
            }
        }

        public void ResetCustomization()
        {
            IsEOSCustomizationIgnored = false;
            UpdateAppearance();
        }

        public IEOSStyle GetCurrentEOSStyle()
        {
            return null;
        }

        public void SetEOSStyle(EOSStyleEnumeration style)
        {
        }
    }
}
