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
    public class CircleMenu : UIView, IEOSThemeControl
    {
        private const int MinimumCountOfElements = 3;
        private const int MaximumCountOfElements = 9;
        private const int VisibleCountOfElements = 5;
        private const int MaximumCountOfChildren = 5;
        private const int MainButtonPadding = 15;
        private const int HintPadding = 7;
        private const int SubmenuElementMargin = 10;
        private const int SubmenuIndicatorMargin = 15;
        private const int MenuSize = 300;
        private const int SubmenuButtonsStartTag = 500;
        private const int ButtonPositionDistanceKoeff = 130;
        private const int IndicatorPositionDistanceKoeff = 150;
        private const double MenuOpenButtonAnimationDuration = 0.05;
        private const double ButtonMovementAnimationDuration = 0.1;
        private const double ButtonHintAnimationDuration = 0.4;
        private const double ShowSubmenuDuration = 0.1;
        private const int Radius = 96;
        private readonly nfloat _6Degrees = 0.10472f;
        private readonly nfloat _2Degrees = 0.0174533f;
        private readonly nfloat _55Degrees = 0.959931f;
        private readonly double _45Degrees = 0.785398;
        private readonly double _360Degrees = 6.28;
        private UIView _rootView;
        private UIView _shadowView;
        private UIPanGestureRecognizer _panSwipe;
        private CircleMenuPanGestureAnalyzer _gestureAnalyzer;
        private UIView _menuButtonsView;
        private CircleMenuMainButton _mainButton;

        //view for circle of menu buttons
        private readonly List<CircleMenuButton> _menuButtons = new List<CircleMenuButton>();
        private readonly List<CircleButtonIndicator> _buttonIndicators = new List<CircleButtonIndicator>();
        private readonly List<CGPoint> _buttonPositions = new List<CGPoint>();
        private readonly List<CGPoint> _indicatorPositions = new List<CGPoint>();
        private bool _isSubmenuOpen;
        private bool _isHintShown;

        public bool IsEOSCustomizationIgnored { get; private set; }

        private List<CircleMenuItemModel> _circleMenuItems = new List<CircleMenuItemModel>();
        public List<CircleMenuItemModel> CircleMenuItems
        {
            get => _circleMenuItems;
            set
            {
                if (value.Count < MinimumCountOfElements || value.Count > MaximumCountOfElements)
                    throw new ArgumentException($"Source must contain {MinimumCountOfElements}-{MaximumCountOfElements} elements");
                _circleMenuItems = value;
            }
        }

        private bool IsSwipeBlocked
        {
            get
            {
                return !_mainButton.IsOpen || _circleMenuItems.Count == MinimumCountOfElements || _isSubmenuOpen;
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
        }

        public void Attach(UIViewController viewController)
        {
            _rootView = viewController.View;
            Frame = new CGRect(_rootView.Frame.Width - (MenuSize / 2 + CircleMenuButton.Size / 2 + MainButtonPadding),
                               _rootView.Frame.Height - (MenuSize / 2 + CircleMenuButton.Size / 2 + MainButtonPadding),
                              MenuSize, MenuSize);
            BackgroundColor = UIColor.Clear;

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

            _gestureAnalyzer = new CircleMenuPanGestureAnalyzer(_rootView);
            _panSwipe = new UIPanGestureRecognizer(PanAction);
            _rootView.AddGestureRecognizer(_panSwipe);

            _menuButtonsView = new PassthroughToWindowView()
            {
                Frame = new CGRect(0, 0, this.Frame.Width, this.Frame.Height),
                BackgroundColor = UIColor.Clear,
                ClipsToBounds = false,
            };
            AddSubview(_menuButtonsView);
            AddSubview(_mainButton);

            FillButtonsPositions();
            CreateMenuButtons();
            FillIndicatorPositions();
            CreateMenuIndicators();
            _rootView.AddSubview(this);

            UpdateAppearance();
        }
        /// <summary>
        /// Fills the buttons position's array with X,Y frame's coordinates
        /// </summary>
        void FillButtonsPositions()
        {
            double startAngle = -_55Degrees;
            double endAngle = 6.28 + startAngle;
            double diff = (endAngle - startAngle) / MaximumCountOfElements;
            for (int i = 0; i < VisibleCountOfElements; ++i)
            {
                var x = Math.Cos(startAngle) * Radius + _menuButtonsView.Bounds.GetCenterX() - CircleMenuButton.Size / 2;
                var y = Math.Sin(startAngle) * Radius + _menuButtonsView.Bounds.GetCenterY() - CircleMenuButton.Size / 2;
                _buttonPositions.Add(new CGPoint(x, y));
                startAngle -= diff;
            }
            FillLastButtonPosition();
        }

        /// <summary>
        /// Added X,Y position for last button
        /// </summary>
        void FillLastButtonPosition()
        {
            var startAngle = _45Degrees;
            var x = Math.Cos(startAngle) * MenuSize + _menuButtonsView.Bounds.GetCenterX() - CircleMenuButton.Size / 2;
            var y = Math.Sin(startAngle) * MenuSize + _menuButtonsView.Bounds.GetCenterY() - CircleMenuButton.Size / 2;
            _buttonPositions.Add(new CGPoint(x, y));
        }

        /// <summary>
        /// Fills the buttons indicator's array with X,Y frame's coordinates
        /// </summary>
        void FillIndicatorPositions()
        {
            double startAngle = -_55Degrees;//56c
            double endAngle = _360Degrees + startAngle;
            double diff = (endAngle - startAngle) / MaximumCountOfElements;
            for (int i = 0; i < VisibleCountOfElements; ++i)
            {
                var x = Math.Cos(startAngle) * ButtonPositionDistanceKoeff + _menuButtonsView.Bounds.GetCenterX() - CircleButtonIndicator.Size / 2;
                var y = Math.Sin(startAngle) * ButtonPositionDistanceKoeff + _menuButtonsView.Bounds.GetCenterY() - CircleButtonIndicator.Size / 2;
                _indicatorPositions.Add(new CGPoint(x, y));
                startAngle -= diff;
            }
            FillLastIndicatorPosition();
        }

        /// <summary>
        /// Added X,Y position for last indicator
        /// </summary>
        void FillLastIndicatorPosition()
        {
            var startAngle = _45Degrees;
            var x = Math.Cos(startAngle) * IndicatorPositionDistanceKoeff + _menuButtonsView.Bounds.GetCenterX() - CircleButtonIndicator.Size / 2;
            var y = Math.Sin(startAngle) * IndicatorPositionDistanceKoeff + _menuButtonsView.Bounds.GetCenterY() - CircleButtonIndicator.Size / 2;
            _indicatorPositions.Add(new CGPoint(x, y));
        }

        /// <summary>
        /// Fills the array of menu buttons and adds to the superview
        /// </summary>
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

        /// <summary>
        /// Fills the array of button's indicators and adds to the superview
        /// </summary>
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

        /// <summary>
        /// Adds CircleMenuItemModel for the regarding buttons
        /// </summary>
        void SetModelsForButtons()
        {
            _menuButtons.ForEach(b => b.Model = null);
            if (_circleMenuItems.Count == MinimumCountOfElements)
            {
                SetModelsForThreeButtons();
            }
            if (_circleMenuItems.Count == 4)
            {
                SetModelsForFourButtons();
            }
            if (_circleMenuItems.Count > 4)
            {
                SetModelsForMoreThenFourButtons();
            }
        }

        void SetModelsForThreeButtons()
        {
            for (int modelIndex = 0, buttonIndex = 1; modelIndex < MinimumCountOfElements; ++modelIndex, ++buttonIndex)
            {
                _menuButtons[buttonIndex].Model = _circleMenuItems[modelIndex];
            }
        }

        void SetModelsForFourButtons()
        {
            for (int i = 0; i < _circleMenuItems.Count; ++i)
            {
                _menuButtons[i].Model = _circleMenuItems[i];
            }
            _menuButtons[4].Model = _circleMenuItems[0];
        }

        void SetModelsForMoreThenFourButtons()
        {
            for (int i = 0; i < _circleMenuItems.Count && i < _menuButtons.Count; ++i)
            {
                _menuButtons[i].Model = _circleMenuItems[i];
            }
        }

        /// <summary>
        /// Launchs "open" menu animation
        /// </summary>
        void OpenMenu()
        {
            SwitchSwipeInteractions(false);
            _menuButtonsView.Transform = CGAffineTransform.MakeRotation(-_6Degrees);
            var task = Task.CompletedTask;
            var tcs = new TaskCompletionSource<bool>();
            var callback = new Action(() => tcs.SetResult(true));
            task = task.ContinueWith((arg) =>
            {
                for (int i = 0; i < VisibleCountOfElements; ++i)
                {
                    var delay = MenuOpenButtonAnimationDuration * i;
                    for (int j = 0; j <= i; ++j)
                    {
                        var positionIndex = i - j;
                        if (i == VisibleCountOfElements - 1 && j == i)
                        {
                            StartMoveButtonAnmation(_menuButtons[j], _buttonPositions[positionIndex], _indicatorPositions[positionIndex], MenuOpenButtonAnimationDuration, delay, callback);
                        }
                        else
                        {
                            StartMoveButtonAnmation(_menuButtons[j], _buttonPositions[positionIndex], _indicatorPositions[positionIndex], MenuOpenButtonAnimationDuration, delay);
                        }
                    }
                }
                return tcs.Task;
            }, TaskScheduler.FromCurrentSynchronizationContext()).Unwrap();
            task = task.ContinueWith((t) => SwitchButtonsInteractions(false), TaskScheduler.FromCurrentSynchronizationContext());
            task = task.ContinueWith((arg) => StartOpenSpringEffect(-_6Degrees), TaskScheduler.FromCurrentSynchronizationContext()).Unwrap();
            if (!_isHintShown)
            {
                task = task.ContinueWith((arg) => StartHintAnimation(), TaskScheduler.FromCurrentSynchronizationContext()).Unwrap();
                _isHintShown = true;
            }
            task = task.ContinueWith((t) =>
            {
                SwitchButtonsInteractions(true);
                SwitchSwipeInteractions(true);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }


        /// <summary>
        /// Launchs "close" menu animation
        /// </summary>
        void CloseMenu()
        {
            var task = Task.CompletedTask;
            task = task.ContinueWith(async (arg) =>
            {
                SwitchSwipeInteractions(false);
                for (int i = 0; i < _menuButtons.Count; ++i)
                {
                    var tcs = new TaskCompletionSource<bool>();
                    task = task.ContinueWith((t) =>
                    {
                        for (int j = 0; j < _menuButtons.Count; ++j)
                        {
                            var positionIndex = _menuButtons[j].PositionIndex;
                            if (positionIndex == _buttonPositions.Count - 1)
                                continue;
                            var previousPositionIndex = positionIndex > 0 ?
                                positionIndex - 1 : _buttonPositions.Count - 1;
                            StartMoveButtonAnmation(_menuButtons[j], _buttonPositions[previousPositionIndex], _indicatorPositions[previousPositionIndex], MenuOpenButtonAnimationDuration);
                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext());

                    await Task.Delay(TimeSpan.FromSeconds(MenuOpenButtonAnimationDuration));
                    tcs.SetResult(true);
                }
                SwitchSwipeInteractions(true);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Launchs left animation for the circle menu
        /// </summary>
        void MoveLeft()
        {
            var zeroButtonPositionModel = _menuButtons.SingleOrDefault(b => b.Position == _buttonPositions[0]).Model;
            var indexOfZerobuttonPositionModel = _circleMenuItems.IndexOf(zeroButtonPositionModel);
            var task = Task.CompletedTask;
            task = task.ContinueWith((t) =>
            {
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
                    StartMoveButtonAnmation(_menuButtons[i], _buttonPositions[nextPositionIndex], _indicatorPositions[nextPositionIndex], ButtonMovementAnimationDuration);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
            task = task.ContinueWith(t => SwitchButtonsInteractions(false), TaskScheduler.FromCurrentSynchronizationContext());
            task = task.ContinueWith(t => StartMoveSpringEffect(-_2Degrees), TaskScheduler.FromCurrentSynchronizationContext()).Unwrap();
            task = task.ContinueWith(t => SwitchButtonsInteractions(true), TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Launchs right animation fro the circle menu
        /// </summary>
        void MoveRight()
        {
            var lastButtonPositionModel = _menuButtons.SingleOrDefault(b => b.Position == _buttonPositions[4]).Model;
            var indexOfLastButtonPositionModel = _circleMenuItems.IndexOf(lastButtonPositionModel);
            var task = Task.CompletedTask;
            task = task.ContinueWith((t) =>
            {
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
                    StartMoveButtonAnmation(_menuButtons[i], _buttonPositions[previousPositionIndex], _indicatorPositions[previousPositionIndex], ButtonMovementAnimationDuration);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
            task = task.ContinueWith(t => SwitchButtonsInteractions(false), TaskScheduler.FromCurrentSynchronizationContext());
            task = task.ContinueWith(t => StartMoveSpringEffect(_2Degrees), TaskScheduler.FromCurrentSynchronizationContext()).Unwrap();
            task = task.ContinueWith(t => SwitchButtonsInteractions(true), TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Creates and launchs "open" menu spring animation
        /// </summary>
        /// <param name="angle">Angle for the spring animation</param>
        /// <returns>TaskCompletitionSource</returns>
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
            leftAnimation.Duration = 0.7;

            leftAnimation.AnimationStopped += (sender, e) =>
            {
                _menuButtonsView.Transform = CGAffineTransform.MakeRotation(0);
                _menuButtonsView.Layer.RemoveAllAnimations();
                tcs.SetResult(true);
            };
            _menuButtonsView.Layer.AddAnimation(leftAnimation, null);
            return tcs.Task;
        }

        /// <summary>
        /// Creates and launchs "move" spring animation
        /// </summary>
        /// <param name="angle">Angle for the spring animation</param>
        /// <returns>TaskCompletitionSource</returns>
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
            firstAnimation.InitialVelocity = 80;
            firstAnimation.Duration = 0.2;

            var secondAnimation = new CASpringAnimation();
            secondAnimation.KeyPath = "transform.rotation.z";
            secondAnimation.From = new NSNumber(angle);
            secondAnimation.To = new NSNumber(0);
            secondAnimation.RemovedOnCompletion = false;
            secondAnimation.FillMode = CAFillMode.Forwards;
            secondAnimation.InitialVelocity = 70;
            secondAnimation.Duration = 0.6;

            firstAnimation.AnimationStopped += (sender, e) =>
            {
                _menuButtonsView.Layer.AddAnimation(secondAnimation, null);
            };

            secondAnimation.AnimationStopped += (sender, e) =>
            {
                _menuButtonsView.Layer.RemoveAnimation(nameof(firstAnimation));
                _menuButtonsView.Layer.RemoveAnimation(nameof(secondAnimation));
                tcs.SetResult(true);
            };
            _menuButtonsView.Layer.AddAnimation(firstAnimation, null);
            return tcs.Task;
        }

        /// <summary>
        /// Creates submenus if needed
        /// </summary>
        /// <param name="invokedButton">Pressed button</param>
        /// <param name="model">ItemModel for pressed button</param>
        /// <returns></returns>
        void PrepareSubmenuIfNeeded(CircleMenuButton invokedButton, CircleMenuItemModel model)
        {
            if (!model.HasChildren)
                return;
            if (model.Children.Count > MaximumCountOfChildren)
                throw new ArgumentException($"Submenu should contain no more then {MaximumCountOfChildren} elements");
            invokedButton.UserInteractionEnabled = false;
            SwitchSwipeInteractions(false);
            SwitchButtonsInteractions(false);
            var task = Task.CompletedTask;
            task.ContinueWith(t =>
            {
                if (!_isSubmenuOpen)
                {
                    SendViewToBack();
                    var submenuButtons = PrepareSubmenu(invokedButton, model.Children);
                    task = OpenSubmenu(invokedButton, submenuButtons);
                    _isSubmenuOpen = true;
                }
                else
                {
                    SendViewToFront();
                    task = CloseSubmenu(invokedButton);
                    _isSubmenuOpen = false;
                }
                task = task.ContinueWith(arg =>
                {
                    invokedButton.UserInteractionEnabled = true;
                    SwitchButtonsInteractions(true);
                    SwitchSwipeInteractions(true);
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Creates submenus
        /// </summary>
        /// <param name="invokedButton">Pressed button</param>
        /// <param name="children">List of the children nodes for submenu</param>
        /// <returns>List of submenu buttons</returns>
        List<CircleMenuButton> PrepareSubmenu(CircleMenuButton invokedButton, List<CircleMenuItemModel> children)
        {
            var convertedPosition = _menuButtonsView.ConvertPointToView(invokedButton.Position, _rootView);
            nfloat xPosition = convertedPosition.X;
            nfloat yPosition = convertedPosition.Y;
            var submenuButtons = new List<CircleMenuButton>();
            var buttonPosition = _buttonPositions.IndexOf(invokedButton.Position);
            if (buttonPosition == 3)
            {
                xPosition = xPosition - CircleMenuButton.Size - SubmenuElementMargin;
                yPosition = yPosition + CircleMenuButton.Size + SubmenuElementMargin;
            }
            for (int i = 0, j = SubmenuButtonsStartTag; i < children.Count; ++i, ++j)
            {
                yPosition = yPosition - CircleMenuButton.Size - SubmenuElementMargin;

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
            return submenuButtons;
        }

        /// <summary>
        /// Launchs "open" submenu animation
        /// </summary>
        /// <param name="invokedButton">Pressed button</param>
        /// <param name="submenuButtons">List of the submenu buttons</param>
        /// <returns></returns>
        private Task OpenSubmenu(CircleMenuButton invokedButton, List<CircleMenuButton> submenuButtons)
        {
            var task = Task.CompletedTask;
            var convertedPosition = _menuButtonsView.ConvertPointToView(invokedButton.Position, _rootView);
            invokedButton.Frame = invokedButton.Frame.ResizeRect(x: convertedPosition.X, y: convertedPosition.Y);
            _rootView.InsertSubview(invokedButton, _rootView.Subviews.Length);

            var indicator = invokedButton.Indicator;
            indicator.Hidden = true;
            var lastButtonFrame = submenuButtons.Last().Frame;
            indicator.Frame = indicator.Frame.ResizeRect(x: lastButtonFrame.X + lastButtonFrame.Width / 2 - indicator.Frame.Width / 2, y: lastButtonFrame.Y - SubmenuIndicatorMargin);
            _rootView.InsertSubview(indicator, _rootView.Subviews.Length);

            for (int i = 0; i < submenuButtons.Count; ++i)
            {
                _rootView.AddSubview(submenuButtons[i]);
                //Have to close on local variable
                var localViewIndex = i;
                task = task.ContinueWith(t => StartChangeAlphaAnimation(submenuButtons[localViewIndex], 1, ShowSubmenuDuration),
                                         TaskScheduler.FromCurrentSynchronizationContext())
                           .Unwrap();
            }
            task = task.ContinueWith((t) =>
            {
                invokedButton.Indicator.Hidden = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
            return task;
        }

        /// <summary>
        /// Launch "close" submenu animation
        /// </summary>
        /// <param name="invokedButton">Pressed button</param>
        /// <returns></returns>
        private Task CloseSubmenu(CircleMenuButton invokedButton)
        {
            invokedButton.ResetPosition();
            invokedButton.Indicator.ResetPosition();
            _menuButtonsView.InsertSubview(invokedButton, _menuButtonsView.Subviews.Length);
            _menuButtonsView.InsertSubview(invokedButton.Indicator, _menuButtonsView.Subviews.Length);
            invokedButton.UserInteractionEnabled = false;

            var submenuButtons = _rootView.Subviews.Where(v =>
                                                          v.Tag >= 0
                                                          && v.Tag <= SubmenuButtonsStartTag + MaximumCountOfChildren
                                                          && v is CircleMenuButton).OrderByDescending(v => v.Tag).ToList();
            if (submenuButtons.Count == 0)
                return Task.CompletedTask;

            invokedButton.Indicator.Hidden = true;
            var task = Task.CompletedTask;
            for (int i = 0; i < submenuButtons.Count; ++i)
            {
                //Have to close on local variable
                var localViewIndex = i;
                task = task.ContinueWith(t => StartChangeAlphaAnimation((CircleMenuButton)submenuButtons[localViewIndex], 0, ShowSubmenuDuration),
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
                invokedButton.UserInteractionEnabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
            return task;
        }

        /// <summary>
        /// Launchs alpha animations
        /// </summary>
        /// <param name="button">Current button</param>
        /// <param name="alpha">Needed alpha</param>
        /// <param name="duration">Needed duration</param>
        /// <returns></returns>
        Task StartChangeAlphaAnimation(CircleMenuButton button, nfloat alpha, double duration)
        {
            var tcs = new TaskCompletionSource<bool>();
            UIView.Animate(ShowSubmenuDuration, () =>
            {
                button.Alpha = alpha;
            }, () => tcs.SetResult(true));
            return tcs.Task;
        }

        /// <summary>
        /// Launchs move animationf for menu buttons
        /// </summary>
        /// <param name="button">Current button</param>
        /// <param name="buttonPosition">Needed button position</param>
        /// <param name="indicatorPosition">Needed indicator position</param>
        /// <param name="duration">Duration</param>
        /// <param name="delay">Delay</param>
        /// <param name="callback">Ended callback</param>
        void StartMoveButtonAnmation(CircleMenuButton button, CGPoint buttonPosition, CGPoint indicatorPosition, double duration, double delay = 0, Action callback = null)
        {
            UIView.Animate(duration, delay, UIViewAnimationOptions.CurveEaseInOut, () =>
            {
                button.Position = buttonPosition;
                button.Indicator.Position = indicatorPosition;
            }, callback);
        }

        void OnMainButtonClicked(object sender, EventArgs e)
        {
            Clicked?.Invoke(_mainButton, _mainButton.Id);
            if (_mainButton.IsOpen)
            {
                _shadowView.Hidden = true;
                CloseMenu();
            }
            else
            {
                _shadowView.Hidden = false;
                SetModelsForButtons();
                OpenMenu();
            }
        }

        /// <summary>
        /// Action for PanGestureRecognizer
        /// </summary>
        /// <param name="recognizer">Current recognizer</param>
        void PanAction(UIPanGestureRecognizer recognizer)
        {
            var direction = _gestureAnalyzer.GetDirection(recognizer);
            if (direction.HasValue)
            {
                if (IsSwipeBlocked)
                    return;

                if (direction.Value == UISwipeGestureRecognizerDirection.Left || direction.Value == UISwipeGestureRecognizerDirection.Down)
                {
                    MoveLeft();
                }
                else
                {
                    MoveRight();
                }
            }
        }

        /// <summary>
        /// Launchs hint animation
        /// </summary>
        /// <returns></returns>
        Task<bool> StartHintAnimation()
        {
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

            UIView.Animate(ButtonHintAnimationDuration, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
            {
                invokedButton.Frame = invokedButton.Frame.ResizeRect(y: invokedButton.Frame.Y - HintPadding);
                invokedButton.Indicator.Frame = invokedButton.Indicator.Frame.ResizeRect(y: invokedButton.Indicator.Frame.Y - HintPadding);
                hintViews[1].Frame = hintViews[1].Frame.ResizeRect(y: hintViews[1].Frame.Y + HintPadding);
            }, null);
            UIView.Animate(ButtonHintAnimationDuration, ButtonHintAnimationDuration, UIViewAnimationOptions.CurveEaseInOut, () =>
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

        /// <summary>
        /// Click eventhandler for submenu button
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">args</param>
        void OnSubmenuClicked(object sender, EventArgs e)
        {
            var button = (CircleMenuButton)sender;
            Clicked?.Invoke(button, button.Model.Id);
        }

        /// <summary>
        /// Send current view to the back of the subview's list
        /// </summary>
        void SendViewToBack()
        {
            var index = _rootView.Subviews.ToList().IndexOf(_shadowView);
            _rootView.InsertSubview(this, index - 1);
        }

        /// <summary>
        /// Send current view to the front of the subview's list
        /// </summary>
        void SendViewToFront()
        {
            var index = _rootView.Subviews.ToList().IndexOf(_shadowView);
            _rootView.InsertSubview(this, _rootView.Subviews.Length);
        }

        void SwitchSwipeInteractions(bool enabled)
        {
            _mainButton.UserInteractionEnabled = enabled;
            _panSwipe.Enabled = enabled;
        }

        void SwitchButtonsInteractions(bool enabled)
        {
            if (!enabled)
            {
                _menuButtons.ForEach(b => b.UserInteractionEnabled = false);
            }
            else
            {
                _menuButtons.ForEach(b => b.UserInteractionEnabled = b.PositionIndex != 4 && b.PositionIndex != 0);
            }
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
                UnfocusedBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6S);
                FocusedBackgroundColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.BrandPrimaryColor);
                FocusedIconColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor6S);
                UnfocusedIconColor = provider.GetEOSProperty<UIColor>(this, EOSConstants.NeutralColor1S);
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

        public override UIView HitTest(CGPoint point, UIEvent uievent)
        {
            var view = base.HitTest(point, uievent);
            return view == this ? null : view;
        }
    }
}
