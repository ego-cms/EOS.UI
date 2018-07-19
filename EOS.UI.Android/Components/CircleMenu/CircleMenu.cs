using System;
using System.Collections.Generic;
using System.Linq;
using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Support.Animation;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using EOS.UI.Android.Interfaces;
using EOS.UI.Shared.Themes.DataModels;
using EOS.UI.Shared.Themes.Helpers;
using EOS.UI.Shared.Themes.Interfaces;
using UIFrameworks.Android.Themes;
using UIFrameworks.Shared.Themes.Interfaces;

namespace EOS.UI.Android.Components
{
    public class CircleMenu: FrameLayout, View.IOnTouchListener, IIsOpened, IEOSThemeControl, ICircleMenuClicable
    {
        #region constants

        private const int HintElevationValue = 2;
        private const int HintAnimationDuration = 300;
        private const int HintAnimationDeltaY = 10;
        private const int SubMenuMargin = 11;

        private const float Diameter = 52f;
        private const float Margin1 = 94f;
        private const float Margin2 = -36f;
        private const float Margin3 = 25f;
        private const float Margin4 = 84f;
        private const float Margin5 = 111f;

        private const int SwipeAnimateDuration = 300;
        private const int ShowHideAnimateDuration = 50;

        #endregion

        #region fields 

        private MainMenuButton _mainMenu;
        private RelativeLayout _container;
        private List<CircleMenuItem> _menuItems = new List<CircleMenuItem>();
        private PointF[] _mainMenuPositions = new PointF[7];

        private bool _forward;
        private bool _isSubMenuOpened;
        private int _showMenuItemsIteration;
        private int _startMenuItemsPosition = 1;
        private float _deltaNormalizePositions;

        private CircleMenuScrollListener _scrollListener = new CircleMenuScrollListener();
        private ScrollSpringAnimationEndListener _scrollSpringAnimationEndListener;
        private OpenSpringAnimationEndListener _openSpringAnimationEndListener;
        private NormalizationEndListener _normalizationEndListener;
        private UpdateMenuItemsVisibilityListener _updateMenuItemsVisibilityListener;

        #endregion

        #region properties

        internal bool IsScrolling { get; set; }
        private bool IsBusy => IsScrolling || _showMenuItemsIteration > 0 || _isSubMenuOpened;
        public bool ShowHintAnimation { get; set; } = true;

        #endregion

        #region events

        public event EventHandler<int> Clicked;

        #endregion

        #region IIsOpened implementation

        public bool IsOpened { get; private set; }

        #endregion

        #region .ctors

        public CircleMenu(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize();
        }

        public CircleMenu(Context context) : base(context)
        {
            Initialize();
        }

        public CircleMenu(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(attrs);
        }

        public CircleMenu(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(attrs);
        }

        public CircleMenu(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(attrs);
        }

        #endregion

        #region customization

        private Color _mainColor;
        public Color MainColor
        {
            get => _mainColor;
            set
            {
                _mainColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _focusedMainColor;
        public Color FocusedMainColor
        {
            get => _focusedMainColor;
            set
            {
                _focusedMainColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _focusedButtonMainColor;
        public Color FocusedButtonMainColor
        {
            get => _focusedButtonMainColor;
            set
            {
                _focusedButtonMainColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private Color _unfocusedButtonMainColor;
        public Color UnfocusedButtonMainColor
        {
            get => _unfocusedButtonMainColor;
            set
            {
                _unfocusedButtonMainColor = value;
                IsEOSCustomizationIgnored = true;
            }
        }

        private List<CircleMenuItemModel> _circleMenuItems;
        public List<CircleMenuItemModel> CircleMenuItems
        {
            get => _circleMenuItems;
            set
            {
                if(value?.Count > 9 || value?.Count < 4)
                    throw new ArgumentOutOfRangeException("Items should be more then 4 and less then 9");

                _circleMenuItems = value;

                _startMenuItemsPosition = 1;

                if(!IsOpened)
                    _menuItems[1].SetDataFromModel(_circleMenuItems[0].ImageSource, _circleMenuItems[0].Id);

                _menuItems[2].SetDataFromModel(_circleMenuItems[1].ImageSource, _circleMenuItems[1].Id);
                _menuItems[3].SetDataFromModel(_circleMenuItems[2].ImageSource, _circleMenuItems[2].Id);
                _menuItems[4].SetDataFromModel(_circleMenuItems[3].ImageSource, _circleMenuItems[3].Id);

                IsEOSCustomizationIgnored = true;
            }
        }

        #endregion

        #region IEOSThemeControl implementation

        public bool IsEOSCustomizationIgnored { get; private set; }

        public IEOSThemeProvider GetThemeProvider()
        {
            return EOSThemeProvider.Instance;
        }

        public void UpdateAppearance()
        {
            if(!IsEOSCustomizationIgnored)
            {
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

        #endregion

        #region public API

        public void Attach(ViewGroup viewGroup)
        {
            var parameters = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MatchParent, RelativeLayout.LayoutParams.MatchParent);
            viewGroup.AddView(this, parameters);
        }

        #endregion

        #region utility methods

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            FillMenuItemsPositions();
        }

        private void FillMenuItemsPositions()
        {
            var denisty = Context.Resources.DisplayMetrics.Density;
            var normalX = Width - Diameter * denisty;
            var normalY = Height - Diameter * denisty;
            _mainMenuPositions[0] = new PointF(Width, normalY - Margin1 * denisty);
            _mainMenuPositions[1] = new PointF(normalX - Margin2 * denisty, normalY - Margin1 * denisty);
            _mainMenuPositions[2] = new PointF(normalX - Margin3 * denisty, normalY - Margin5 * denisty);
            _mainMenuPositions[3] = new PointF(normalX - Margin4 * denisty, normalY - Margin4 * denisty);
            _mainMenuPositions[4] = new PointF(normalX - Margin5 * denisty, normalY - Margin3 * denisty);
            _mainMenuPositions[5] = new PointF(normalX - Margin1 * denisty, normalY - Margin2 * denisty);
            _mainMenuPositions[6] = new PointF(normalX - Margin1 * denisty, Height);
        }

        private void Initialize(IAttributeSet attrs = null)
        {
            var inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.CircleMenu, this);
            _container = view.FindViewById<RelativeLayout>(Resource.Id.container);

            _mainMenu = view.FindViewById<MainMenuButton>(Resource.Id.hamburgerMenu);
            _mainMenu.SetIIsOpenedItem(this);
            _mainMenu.Click += MainMenuClick;

            _container.SetOnTouchListener(this);

            _menuItems.AddRange(new List<CircleMenuItem>
            {
                FindViewById<CircleMenuItem>(Resource.Id.menu0),
                FindViewById<CircleMenuItem>(Resource.Id.menu1),
                FindViewById<CircleMenuItem>(Resource.Id.menu2),
                FindViewById<CircleMenuItem>(Resource.Id.menu3),
                FindViewById<CircleMenuItem>(Resource.Id.menu4),
                FindViewById<CircleMenuItem>(Resource.Id.menu5),
            });

            foreach(var menu in _menuItems)
                menu.SetICircleMenuClicable(this);

            _scrollSpringAnimationEndListener = new ScrollSpringAnimationEndListener(Context, this);
            _openSpringAnimationEndListener = new OpenSpringAnimationEndListener(Context, this);
            _normalizationEndListener = new NormalizationEndListener(Context, this);
            _updateMenuItemsVisibilityListener = new UpdateMenuItemsVisibilityListener(Context, this);

            _deltaNormalizePositions = (Diameter + Margin1) * Context.Resources.DisplayMetrics.Density;
        }

        private void MainMenuClick(object sender, EventArgs e)
        {
            if(_showMenuItemsIteration == 0 && !_isSubMenuOpened)
                UpdateMenuItemsVisiblility();
        }

        /// <summary>
        /// Method which spin round items by swipe action.
        /// Animation animated X and Y with spring interpolator (implemented with native SpringAnimation)
        /// Spin round can be forward (left to right) and back (right to left)
        /// </summary>
        private void MoveMenuItemsAnimation()
        {
            for(int i = 0; i < _menuItems.Count; i++)
            {
                var position = _mainMenuPositions[_forward ? _menuItems.Count - 1 - i : i + 1];

                var menu = default(CircleMenuItem);
                if(_forward || i == 0)
                    menu = _menuItems[i];
                else
                    menu = _menuItems[_menuItems.Count - i];

                var springX = new SpringAnimation(menu, DynamicAnimation.X, position.X);
                var springY = new SpringAnimation(menu, DynamicAnimation.Y, position.Y);

                if(i == _menuItems.Count - 1)
                    springY.AddEndListener(_scrollSpringAnimationEndListener);

                springX.Start();
                springY.Start();
            }
        }

        /// <summary>
        /// Before scroll right/left we should set invisible item to start position
        /// Start position depends on scroll direction
        /// After normalize starts scroll animation
        /// </summary>
        private void NormalizeHiddenMenuItem()
        {
            var model = FindNextWithbleModel(_forward);

            if(_forward)
            {
                _menuItems[1].SetDataFromModel(model.ImageSource, model.Id);
                _menuItems[0].Animate().X(Width - _deltaNormalizePositions).Y(Height).SetDuration(1).WithEndAction(_normalizationEndListener);
            }
            else
            {
                _menuItems[5].SetDataFromModel(model.ImageSource, model.Id);
                _menuItems[0].Animate().X(Width).Y(Height - _deltaNormalizePositions).SetDuration(1).WithEndAction(_normalizationEndListener);
            }
        }

        /// <summary>
        /// Method find next visible model after scrolling
        /// </summary>
        /// <param name="scrollForward">Sets direction of scrolling</param>
        /// <returns>Returns finded model on CircleMenuItems list</returns>
        private CircleMenuItemModel FindNextWithbleModel(bool scrollForward)
        {
            var index = 0;
            if(scrollForward)
            {
                if(_startMenuItemsPosition > 0)
                    --_startMenuItemsPosition;
                else
                    _startMenuItemsPosition = _circleMenuItems.Count - 1;

                index = _startMenuItemsPosition;
            }
            else
            {
                if(_startMenuItemsPosition == _circleMenuItems.Count - 1)
                {
                    _startMenuItemsPosition = 0;
                    index = _startMenuItemsPosition + 2;
                }
                else
                {
                    ++_startMenuItemsPosition;
                    if(_startMenuItemsPosition == _circleMenuItems.Count - 1)
                        index = 0;
                    else
                        index = _startMenuItemsPosition;
                }
            }
            return CircleMenuItems[index];
        }

        /// <summary>
        /// Method which spin round items by swipe action.
        /// Animation has 5 iterations. 
        /// For hiding all iterations animate X and Y without interpolator. 
        /// For showing first 4 iterations animate X and Y without interpolator, 
        /// and last has spring interpolator (implemented with native SpringAnimation)
        /// </summary>
        private void UpdateMenuItemsVisiblility()
        {
            ++_showMenuItemsIteration;
            if(IsOpened)
                HideMenuItems();
            else
                ShowMenuItems();
        }

        private void ShowMenuItems()
        {
            if(_showMenuItemsIteration == 1)
            {
                foreach(var menu in _menuItems)
                    menu.StartRotateAnimation();

                _menuItems[1].Animate().X(_mainMenuPositions[0].X).Y(_mainMenuPositions[0].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateMenuItemsVisibilityListener);
            }
            if(_showMenuItemsIteration == 2)
            {
                _menuItems[2].Animate().X(_mainMenuPositions[0].X).Y(_mainMenuPositions[0].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[1].Animate().X(_mainMenuPositions[1].X).Y(_mainMenuPositions[1].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateMenuItemsVisibilityListener);

            }
            if(_showMenuItemsIteration == 3)
            {
                _menuItems[3].Animate().X(_mainMenuPositions[0].X).Y(_mainMenuPositions[0].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[2].Animate().X(_mainMenuPositions[1].X).Y(_mainMenuPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[1].Animate().X(_mainMenuPositions[2].X).Y(_mainMenuPositions[2].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateMenuItemsVisibilityListener);

            }
            if(_showMenuItemsIteration == 4)
            {
                _menuItems[4].Animate().X(_mainMenuPositions[0].X).Y(_mainMenuPositions[0].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[3].Animate().X(_mainMenuPositions[1].X).Y(_mainMenuPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[2].Animate().X(_mainMenuPositions[2].X).Y(_mainMenuPositions[2].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[1].Animate().X(_mainMenuPositions[3].X).Y(_mainMenuPositions[3].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateMenuItemsVisibilityListener);
            }
            if(_showMenuItemsIteration == 5)
            {
                //last iteration of showing menus should be with spring animation
                for(int i = _menuItems.Count - 1; i > 0; i--)
                {
                    var menu = _menuItems[i];
                    var point = _mainMenuPositions[_menuItems.Count - i];

                    var springX = new SpringAnimation(menu, DynamicAnimation.X, point.X);
                    var springY = new SpringAnimation(menu, DynamicAnimation.Y, point.Y);

                    if(i == 1)
                    {
                        _container.SetBackgroundColor(!IsOpened ? Color.Argb(50, 0, 0, 0) : Color.Transparent);
                        springY.AddEndListener(_openSpringAnimationEndListener);
                    }

                    springX.Start();
                    springY.Start();
                }
            }
        }

        private void HideMenuItems()
        {
            if(_showMenuItemsIteration == 1)
            {
                //on hiding animation should be visible icon on last item
                var model = FindNextWithbleModel(true);
                _menuItems[1].SetDataFromModel(model.ImageSource, model.Id);

                _menuItems[1].Animate().X(_mainMenuPositions[4].X).Y(_mainMenuPositions[4].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[2].Animate().X(_mainMenuPositions[3].X).Y(_mainMenuPositions[3].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[3].Animate().X(_mainMenuPositions[2].X).Y(_mainMenuPositions[2].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[4].Animate().X(_mainMenuPositions[1].X).Y(_mainMenuPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[5].Animate().X(_mainMenuPositions[0].X).Y(_mainMenuPositions[0].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateMenuItemsVisibilityListener);
            }
            if(_showMenuItemsIteration == 2)
            {
                _menuItems[1].Animate().X(_mainMenuPositions[3].X).Y(_mainMenuPositions[3].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[2].Animate().X(_mainMenuPositions[2].X).Y(_mainMenuPositions[2].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[3].Animate().X(_mainMenuPositions[1].X).Y(_mainMenuPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[4].Animate().X(_mainMenuPositions[0].X).Y(_mainMenuPositions[0].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateMenuItemsVisibilityListener);
            }
            if(_showMenuItemsIteration == 3)
            {
                _menuItems[1].Animate().X(_mainMenuPositions[2].X).Y(_mainMenuPositions[2].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[2].Animate().X(_mainMenuPositions[1].X).Y(_mainMenuPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[3].Animate().X(_mainMenuPositions[0].X).Y(_mainMenuPositions[0].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateMenuItemsVisibilityListener);
            }
            if(_showMenuItemsIteration == 4)
            {
                _menuItems[1].Animate().X(_mainMenuPositions[1].X).Y(_mainMenuPositions[1].Y).SetDuration(ShowHideAnimateDuration);
                _menuItems[2].Animate().X(_mainMenuPositions[0].X).Y(_mainMenuPositions[0].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateMenuItemsVisibilityListener);
            }
            if(_showMenuItemsIteration == 5)
            {
                _menuItems[1].Animate().X(_mainMenuPositions[0].X).Y(_mainMenuPositions[0].Y).SetDuration(ShowHideAnimateDuration).WithEndAction(_updateMenuItemsVisibilityListener);
            }
        }

        /// <summary>
        /// Hint animation shows one time on start of application
        /// </summary>
        /// <param name="action">After animation complete action</param>
        private void StartHintAnimation(Action action)
        {
            ShowHintAnimation = false;

            var lastView = CreateHintView();
            var middleView = CreateHintView();

            var translateDown = CreateHintAnimation(false);

            translateDown.AnimationEnd += (s, e) =>
            {
                _container.RemoveViewAt(0);
                _container.RemoveViewAt(0);

                lastView?.Dispose();
                middleView?.Dispose();

                action?.Invoke();
            };

            var translateUp = CreateHintAnimation();

            lastView.Elevation = HintElevationValue;
            middleView.Elevation = HintElevationValue * 2;

            _container.AddView(middleView, 0);
            _container.AddView(lastView, 0);

            lastView.StartAnimation(translateDown);
            _menuItems[4].StartAnimation(translateUp);
        }

        private TranslateAnimation CreateHintAnimation(bool isUp = true)
        {
            var delta = (isUp ? -HintAnimationDeltaY : HintAnimationDeltaY) * Context.Resources.DisplayMetrics.Density;
            var translateAnimation = new TranslateAnimation(0, 0, 0, delta);
            translateAnimation.Interpolator = new DecelerateInterpolator();
            translateAnimation.FillAfter = false;
            translateAnimation.Duration = HintAnimationDuration;
            return translateAnimation;
        }

        private ObjectAnimator CreateAlphaAnimation(CircleMenuItem menu, int duration, int startDelay, bool isShow = true)
        {
            var alfaAnimation = isShow? ObjectAnimator.OfFloat(menu, "Alpha", 1f) : ObjectAnimator.OfFloat(menu, "Alpha", 0f);
            alfaAnimation.SetDuration(duration);
            alfaAnimation.StartDelay = startDelay;
            return alfaAnimation;
        }

        private View CreateHintView()
        {
            var hintView = new View(Context);
            var layoutParameters = new RelativeLayout.LayoutParams(
                (int)(Diameter * Context.Resources.DisplayMetrics.Density),
                (int)(Diameter * Context.Resources.DisplayMetrics.Density));

            layoutParameters.RightMargin = (int)(Margin3 * Context.Resources.DisplayMetrics.Density);
            layoutParameters.BottomMargin = (int)(Margin5 * Context.Resources.DisplayMetrics.Density);

            layoutParameters.AddRule(LayoutRules.AlignParentBottom);
            layoutParameters.AddRule(LayoutRules.AlignParentRight);

            hintView.LayoutParameters = layoutParameters;

            var roundedDrawable = new GradientDrawable();
            roundedDrawable.SetColor(Color.White);
            roundedDrawable.SetShape(ShapeType.Oval);

            hintView.SetBackgroundDrawable(roundedDrawable);

            return hintView;
        }

        private CircleMenuItem CreateSubMenu(int buttonMargin, int rightMargin)
        {
            var subMenu = new CircleMenuItem(Context);
            var layoutParameters = new RelativeLayout.LayoutParams(
                (int)(Diameter * Context.Resources.DisplayMetrics.Density),
                (int)(Diameter * Context.Resources.DisplayMetrics.Density));

            layoutParameters.RightMargin = (int)(rightMargin * Context.Resources.DisplayMetrics.Density);
            layoutParameters.BottomMargin = (int)(buttonMargin * Context.Resources.DisplayMetrics.Density);

            layoutParameters.AddRule(LayoutRules.AlignParentBottom);
            layoutParameters.AddRule(LayoutRules.AlignParentRight);

            subMenu.LayoutParameters = layoutParameters;

            var roundedDrawable = new GradientDrawable();
            roundedDrawable.SetColor(Color.White);
            roundedDrawable.SetShape(ShapeType.Oval);

            subMenu.SetBackgroundDrawable(roundedDrawable);

            subMenu.Alpha = 0f;

            return subMenu;
        }

        internal void HandleOnScrollSpringAnimationEnd()
        {
            //After end of swipe animation we should change indexes to default for items collection
            //and set invisible item to default position

            //HACK: if ImageView was out of the bounds it is invisible
            _menuItems[0].Visibility = ViewStates.Invisible;
            _menuItems[0].Visibility = ViewStates.Visible;

            if(_forward)
            {
                var menuItem = _menuItems.Last();
                _menuItems.RemoveAt(_menuItems.Count - 1);
                _menuItems.Insert(0, menuItem);
            }
            else
            {
                var menuItem = _menuItems.First();
                _menuItems.RemoveAt(0);
                _menuItems.Add(menuItem);
            }

            //Set to zero position
            _menuItems.First().Animate().X(Width).Y(Height).SetDuration(1);

            //reset data from model for part visible and not clickable menus
            _menuItems[1].ResetDataFromModel();
            _menuItems[5].ResetDataFromModel();
        }

        internal void HandleOnOpenSpringAnimationEnd()
        {
            //reset data from model for part visible and not clickable menus
            _menuItems[1].ResetDataFromModel();

            //after end of open/hide animation setup internal values to default
            var action = new Action(() =>
            {
                _showMenuItemsIteration = 0;
                IsOpened = !IsOpened;
            });

            if(ShowHintAnimation)
                StartHintAnimation(action);
            else
                action.Invoke();
        }

        internal void HandleNormalizationEnd()
        {
            MoveMenuItemsAnimation();
        }

        internal void HandleUpdateMenuItemsVisibility()
        {
            if(_showMenuItemsIteration > 0)
            {
                if(_showMenuItemsIteration != _menuItems.Count - 1)
                {
                    //Invoke animation until all items not on theirs positions
                    UpdateMenuItemsVisiblility();
                }
                else if(IsOpened)
                {
                    //after end of animation setup internal values to default and change background color
                    _showMenuItemsIteration = 0;
                    IsOpened = !IsOpened;
                    _container.SetBackgroundColor(IsOpened ? Color.Argb(50, 0, 0, 0) : Color.Transparent);
                }
            }
        }

        #endregion

        #region IOnTouchListener implementation

        public bool OnTouch(View v, MotionEvent e)
        {
            if(!IsBusy)
            {
                if(_scrollListener.IsScrolled(ref _forward, e))
                {
                    IsScrolling = true;
                    NormalizeHiddenMenuItem();
                }
            }
            return IsOpened;
        }

        #endregion

        #region ICircleMenuClicable implementation

        public void PerformClick(int id, bool isSubMenu, bool isOpened)
        {
            if(isSubMenu)
            {
                Clicked?.Invoke(this, id);
            }
            else
            {
                var menuItemModel = default(CircleMenuItemModel); //CircleMenuItems?.FirstOrDefault(item => item.Id == id);

                if(menuItemModel == null)
                    return;

                if(menuItemModel.Children.Count() > 0)
                {
                    var index = _menuItems.IndexOf(_menuItems.FirstOrDefault(item => item.CircleMenuModelId == menuItemModel.Id));

                    for(int i = 1; i < _menuItems.Count; i++)
                        if(i != index)
                            _menuItems[i].Enabled = !_menuItems[i].Enabled;

                    _mainMenu.Enabled = !_mainMenu.Enabled;

                    _isSubMenuOpened = !isOpened;

                    if(!isOpened)
                    {
                        var initBottomMargin = 0;
                        var initRightMargin = 0;

                        switch(index)
                        {
                            case 2:
                                break;
                            case 3:
                                initBottomMargin = 136;
                                initRightMargin = 90;
                                break;
                            case 4:
                                break;
                        }

                        for(int i = 0; i < menuItemModel.Children.Count; i++)
                        {
                            var subMenu = CreateSubMenu(initBottomMargin + SubMenuMargin + ((int)Diameter + SubMenuMargin) * i, initRightMargin);
                            subMenu.Tag = $"Child{i}";
                            subMenu.SetICircleMenuClicable(this);
                            menuItemModel.Children[i].ImageSource.SetColorFilter(Color.Black, PorterDuff.Mode.SrcIn);
                            subMenu.SetDataFromModel(menuItemModel.Children[i].ImageSource, menuItemModel.Children[i].Id, true);
                            _container.AddView(subMenu, 0);

                            var alfaAnimation = CreateAlphaAnimation(subMenu, 100, 100 * i);
                            alfaAnimation.Start();

                            if(i == menuItemModel.Children.Count() - 1)
                            {
                                alfaAnimation.AnimationEnd += (s, e) =>
                                {
                                    //TODO: After animation actions
                                };
                            }
                        }
                    }
                    else
                    {
                        for(int i = 0; i < menuItemModel.Children.Count; i++)
                        {
                            var subMenu = _container.FindViewWithTag($"Child{i}") as CircleMenuItem;
                            var alfaAnimation = CreateAlphaAnimation(subMenu, 100, 100 * i, false);
                            alfaAnimation.Start();

                            alfaAnimation.AnimationEnd += (s, e) =>
                            {
                                _container.RemoveViewAt(0);
                                //TODO: After animation actions
                            };
                        }
                    }
                }
                else
                {
                    Clicked?.Invoke(this, id);
                }
            }
        }

        #endregion
    }
}
